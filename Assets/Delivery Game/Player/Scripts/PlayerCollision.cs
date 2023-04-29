using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private int pizzaCapacity;
    [SerializeField] private int pizza;

    [SerializeField] private Slider moneySlider;
    [SerializeField] private TMP_Text sliderText;
    
    private bool _inPetrol;
    private bool _inWater;
    private bool _inFood;
    private bool _inPizza;

    private void Update()
    {
        moneySlider.maxValue = playerStats.tips;
        sliderText.text = moneySlider.value.ToString(CultureInfo.InvariantCulture);
        
        if (!Input.GetKeyDown(KeyCode.E)) return;
        if (_inPetrol)
        {
            playerStats.BuyFuel(moneySlider.value);
        }
        else if (_inWater)
        {
            playerStats.BuyDrink(moneySlider.value);
        }
        else if (_inFood)
        {
            playerStats.BuyFood(moneySlider.value);
        }
        else if (_inPizza)
        {
            // GET PIZZA
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Petrol Pump"))
        {
            _inPetrol = true;
        }
        else if (other.CompareTag("Food"))
        {
            _inFood = true;
        }
        else if (other.CompareTag("Pizza"))
        {
            _inPizza = true;
        }
        else if (other.CompareTag("Water"))
        {
            _inWater = true;
        }
        else
        {
            _inPetrol = false;
            _inWater = false;
            _inFood = false;
            _inPizza = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _inPetrol = false;
        _inWater = false;
        _inFood = false;
        _inPizza = false;
    }
}
