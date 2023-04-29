using System;
using System.Collections;
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
    
    [SerializeField] private float fuelRatePerLitre;
    [SerializeField] private float foodRatePerMeal;
    [SerializeField] private float drinkRatePerDrink;
    [SerializeField] private float longevityRatePer;
    
    [SerializeField] private float maxFuel;
    [SerializeField] private float maxEnergy;
    [SerializeField] private float maxDrink;
    [SerializeField] private float maxLongevity;
    
    [SerializeField] private float fuel;
    [SerializeField] private float energy;
    [SerializeField] private float hydration;
    [SerializeField] private float longevity;
    
    [SerializeField] private TMP_Text fuelText;
    [SerializeField] private TMP_Text energyText;
    [SerializeField] private TMP_Text hydroText;
    [SerializeField] private TMP_Text longevityText;
    [SerializeField] private TMP_Text tipsText;
    
    [SerializeField] private Transform fuelValue;
    [SerializeField] private Transform energyValue;
    [SerializeField] private Transform hydroValue;
    [SerializeField] private Transform longValue;

    private Rigidbody _rb;

    private bool _played1;
    private bool _played2;
    private bool _played3;
    private bool _played4;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        tipsText.text = tips.ToString("0.00");
        fuelText.text = fuel.ToString("0.00");
        energyText.text = energy.ToString("0.00");
        hydroText.text = hydration.ToString("0.00");
        longevityText.text = longevity.ToString("0.00");

        InvokeRepeating(nameof(UpdateStats), 1.0f, 1.0f);
    }

    private void Update()
    {
        tipsText.text = tips.ToString(CultureInfo.InvariantCulture);
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
        if (_rb.velocity == Vector3.zero) return;
        fuel -= mileage;
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
        if (!(tips > longevityRatePer) || !(tips > money)) return;
        if (longevity < maxFuel)
            longevity += money / longevityRatePer;

        longevity = Mathf.Clamp(longevity, 0f, maxLongevity);

        UpdateMoney(money);

        longevityText.text = longevity.ToString("0.00");
    }
    
    public void BuyFuel(float money)
    {
        if (!(tips > fuelRatePerLitre) || !(tips > money)) return;
        if (fuel < maxFuel)
            fuel += money / fuelRatePerLitre;

        fuel = Mathf.Clamp(fuel, 0f, maxFuel);

        UpdateMoney(money);

        fuelText.text = fuel.ToString("0.00");
    }

    public void BuyFood(float money)
    {
        if (!(tips > foodRatePerMeal) || !(tips > money)) return;
        if (energy < maxEnergy)
            energy += money / foodRatePerMeal;

        energy = Mathf.Clamp(energy, 0f, maxEnergy);

        UpdateMoney(money);

        energyText.text = energy.ToString("0.00");
    }

    public void BuyDrink(float money)
    {
        if (!(tips > drinkRatePerDrink) || !(tips > money)) return;
        if (hydration < maxDrink)
            hydration += money / drinkRatePerDrink;

        hydration = Mathf.Clamp(hydration, 0f, maxDrink);

        UpdateMoney(money);

        hydroText.text = hydration.ToString("0.00");
    }
}
