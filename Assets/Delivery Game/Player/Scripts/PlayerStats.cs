using System.Globalization;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public float tips;
    
    [SerializeField] private float mileage;
    [SerializeField] private float energyLoss;
    [SerializeField] private float dehydration;
    [SerializeField] private float bikeDegrading;
    
    public float fuelRatePerLitre;
    public float foodRatePerMeal;
    public float drinkRatePerDrink;
    public float longevityRatePer;
    
    [SerializeField] private float maxFuel;
    [SerializeField] private float maxEnergy;
    [SerializeField] private float maxDrink;
    [SerializeField] private float maxLongevity;
    
    public float fuel;
    public float energy;
    public float hydration;
    public float longevity;
    
    [SerializeField] private TMP_Text fuelText;
    [SerializeField] private TMP_Text energyText;
    [SerializeField] private TMP_Text hydroText;
    [SerializeField] private TMP_Text longevityText;
    [SerializeField] private TMP_Text tipsText;
    
    [SerializeField] private Transform fuelValue;
    [SerializeField] private Transform energyValue;
    [SerializeField] private Transform hydroValue;
    [SerializeField] private Transform longValue;

    [SerializeField] private GameObject endPanel;
    [SerializeField] private TMP_Text endText;
    [SerializeField] private TMP_Text endTime;
    
    private Rigidbody _rb;

    private bool _played1;
    private bool _played2;
    private bool _played3;
    private bool _played4;

    private float _time;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        fuelText.text = fuel.ToString("0.00");
        energyText.text = energy.ToString("0.00");
        hydroText.text = hydration.ToString("0.00");
        longevityText.text = longevity.ToString("0.00");

        InvokeRepeating(nameof(UpdateStats), 1.0f, 1.0f);
    }

    private void EndGame()
    {
        endPanel.SetActive(true);
        if (fuel < 0f)
            endText.text = "No fuel!";
        else if (energy < 0f)
            endText.text = "Died of hunger!";
        else if (hydration < 0f)
            endText.text = "Dehydration!";
        else if (longevity < 0f)
            endText.text = "Your bike broke!";
        endTime.text = "Time survived: " + _time.ToString(CultureInfo.InvariantCulture) + "s";
        GetComponent<PlayerController>().enabled = false;
        CancelInvoke(nameof(UpdateStats));
        
        JSLink.Instance.ExitGame((int) _time);
        
        enabled = false;
    }

    private void Update()
    {
        if (fuel < 0f || energy < 0 || hydration < 0 || longevity < 0)
        {
            EndGame();
        }
        
        tipsText.text = "Tips: $" + tips.ToString("0.00");
        UpdateUI(fuelValue, fuel);
        UpdateUI(energyValue, energy);
        UpdateUI(hydroValue, hydration);
        UpdateUI(longValue, longevity);

        if (fuel < 1.0f && !_played1)
        {
            _played1 = true;
            fuelValue.parent.GetComponent<Animator>().Play("FuelLow");
        }
        else if (fuel >= 1.0f && _played1)
        {
            _played1 = false;
            fuelValue.parent.GetComponent<Animator>().Play("FuelFine");
        }
        if (energy < 1.0f && !_played2)
        {
            _played2 = true;
            energyValue.parent.GetComponent<Animator>().Play("FoodLow");
        }
        else if (energy >= 1.0f && _played2)
        {
            _played2 = false;
            energyValue.parent.GetComponent<Animator>().Play("FoodFine");
        }
        if (hydration < 1.0f && !_played3)
        {
            _played3 = true;
            hydroValue.parent.GetComponent<Animator>().Play("WaterLow");
        }
        else if (hydration >= 1.0f && _played3)
        {
            _played3 = false;
            hydroValue.parent.GetComponent<Animator>().Play("WaterFine");
        }
        if (longevity < 1.0f && !_played4)
        {
            _played4 = true;
            longValue.parent.GetComponent<Animator>().Play("LongLow");
        }
        else if (longevity >= 1.0f && _played4)
        {
            _played4 = false;
            longValue.parent.GetComponent<Animator>().Play("LongFine");
        }
            
    }

    private void UpdateUI(Transform scale, float x)
    {
        var value = scale.localScale;
        value.x = Mathf.Lerp(value.x, x * 20f, 5f * Time.deltaTime);
        scale.localScale = value;
    }

    private void UpdateStats()
    {
        _time++;
        
        if (_rb.velocity != Vector3.zero) fuel -= mileage;
        energy -= energyLoss;
        hydration -= dehydration;
        longevity -= bikeDegrading;
            
        fuelText.text = fuel.ToString("0.00");
        energyText.text = energy.ToString("0.00");
        hydroText.text = hydration.ToString("0.00");
        longevityText.text = longevity.ToString("0.00");

    }

    private void UpdateMoney(float money)
    {
        tips -= money;
    }

    public void BuyLongevity(float money)
    {
        if (tips < money) return;
        if (longevity > maxLongevity) return;
        longevity += money / longevityRatePer;

        longevity = Mathf.Clamp(longevity, 0f, maxLongevity);

        UpdateMoney(money);

        longevityText.text = longevity.ToString("0.00");
    }
    
    public void BuyFuel(float money)
    {
        if (tips < money) return;
        if (fuel > maxFuel) return;
        fuel += money / fuelRatePerLitre;

        fuel = Mathf.Clamp(fuel, 0f, maxFuel);

        UpdateMoney(money);

        fuelText.text = fuel.ToString("0.00");
    }

    public void BuyFood(float money)
    {
        if (tips < money) return;
        if (energy > maxEnergy) return;
        energy += money / foodRatePerMeal;

        energy = Mathf.Clamp(energy, 0f, maxEnergy);

        UpdateMoney(money);

        energyText.text = energy.ToString("0.00");
    }

    public void BuyDrink(float money)
    {
        if (tips < money) return;
        if (hydration > maxDrink) return;
        hydration += money / drinkRatePerDrink;

        hydration = Mathf.Clamp(hydration, 0f, maxDrink);

        UpdateMoney(money);

        hydroText.text = hydration.ToString("0.00");
    }
}
