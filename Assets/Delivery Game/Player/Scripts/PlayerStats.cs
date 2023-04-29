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

    [SerializeField] private float fuelRatePerLitre;
    [SerializeField] private float foodRatePerMeal;
    [SerializeField] private float drinkRatePerDrink;
    
    [SerializeField] private float maxFuel;
    [SerializeField] private float maxEnergy;
    [SerializeField] private float maxDrink;
    
    [SerializeField] private float fuel;
    [SerializeField] private float energy;
    [SerializeField] private float hydration;

    [SerializeField] private TMP_Text fuelText;
    [SerializeField] private TMP_Text energyText;
    [SerializeField] private TMP_Text hydroText;
    [SerializeField] private TMP_Text tipsText;

    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        tipsText.text = tips.ToString(CultureInfo.InvariantCulture);
        fuelText.text = fuel.ToString(CultureInfo.InvariantCulture);
        energyText.text = energy.ToString(CultureInfo.InvariantCulture);
        hydroText.text = hydration.ToString(CultureInfo.InvariantCulture);

        InvokeRepeating(nameof(UpdateStats), 1.0f, 1.0f);
    }

    private void UpdateStats()
    {
        if (_rb.velocity != Vector3.zero)
        {
            fuel -= mileage;
            energy -= energyLoss;
            hydration -= dehydration;
            fuelText.text = fuel.ToString(CultureInfo.InvariantCulture);
            energyText.text = energy.ToString(CultureInfo.InvariantCulture);
            hydroText.text = hydration.ToString(CultureInfo.InvariantCulture);
        }
        
    }

    private void UpdateMoney(float money)
    {
        tips -= money;
        tipsText.text = tips.ToString(CultureInfo.InvariantCulture);
    }
    
    public void BuyFuel(float money)
    {
        if (!(tips > fuelRatePerLitre) || !(tips > money)) return;
        if (fuel < maxFuel)
            fuel += money / fuelRatePerLitre;

        fuel = Mathf.Clamp(fuel, 0f, maxFuel);

        UpdateMoney(money);

        fuelText.text = fuel.ToString(CultureInfo.InvariantCulture);
    }

    public void BuyFood(float money)
    {
        if (!(tips > foodRatePerMeal) || !(tips > money)) return;
        if (energy < maxEnergy)
            energy = money / foodRatePerMeal;

        energy = Mathf.Clamp(energy, 0f, maxEnergy);

        UpdateMoney(money);

        energyText.text = energy.ToString(CultureInfo.InvariantCulture);
    }

    public void BuyDrink(float money)
    {
        if (!(tips > drinkRatePerDrink) || !(tips > money)) return;
        if (hydration < maxDrink)
            hydration = money / drinkRatePerDrink;

        hydration = Mathf.Clamp(hydration, 0f, maxDrink);

        UpdateMoney(money);

        hydroText.text = hydration.ToString(CultureInfo.InvariantCulture);
    }
}
