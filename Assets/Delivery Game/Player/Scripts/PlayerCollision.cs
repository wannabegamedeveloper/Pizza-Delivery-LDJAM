using System;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerCollision : MonoBehaviour
{
    public List<int> collectedPizzas;
    
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private int pizzaCapacity;
    [SerializeField] private PizzaOrders orders;
    
    [SerializeField] private Slider moneySlider;
    [SerializeField] private TMP_Text sliderText;

    [SerializeField] private TMP_Text carrying;

    private bool _inPetrol;
    private bool _inWater;
    private bool _inFood;
    private bool _inRepair;
    private bool _inLocation;
    private bool _inPizza;

    private string _locationID;

    private void Update()
    {
        moneySlider.maxValue = playerStats.tips;
        sliderText.text = "$" + moneySlider.value.ToString("0.00");
        carrying.text = collectedPizzas.Count.ToString();
        
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
        else if (_inRepair)
        {
            playerStats.BuyLongevity(moneySlider.value);
        }
        else if (_inPizza)
        {
                foreach (int currentOrder in orders.currentOrders)
                {
                    if (collectedPizzas.Count < pizzaCapacity)
                    {
                        if (!collectedPizzas.Contains(currentOrder))
                            collectedPizzas.Add(currentOrder);
                    }
                }
        }
        else if (_inLocation)
        {
            int.TryParse(_locationID, out int locationID);
            if (collectedPizzas.Contains(locationID))
            {
                collectedPizzas.Remove(locationID);
                orders.currentOrders.Remove(locationID);
                orders.deliveryLocations[locationID].enabled = false;
                orders.deliveryLocations[locationID].transform.GetChild(0).gameObject.SetActive(false);
                playerStats.tips += Random.Range(0.3f, 0.6f);
            }
        }
    }

    private void OnTriggerStay(Collider other)
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
        else if (other.CompareTag("RepairShop"))
        {
            _inRepair = true;
        }
        else if (other.CompareTag("Location"))
        {
            _inLocation = true;
            _locationID = other.name;
        }
        else
        {
            _inPetrol = false;
            _inWater = false;
            _inFood = false;
            _inRepair = false;
            _inLocation = false;
            _inPizza = false;
            _locationID = "";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _inPetrol = false;
        _inWater = false;
        _inFood = false;
        _inRepair = false;
        _inLocation = false;
        _inPizza = false;
        _locationID = "";
    }
}
