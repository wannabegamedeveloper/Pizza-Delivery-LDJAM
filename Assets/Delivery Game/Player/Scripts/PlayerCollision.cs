using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerCollision : MonoBehaviour
{
    public float money;
    public List<int> collectedPizzas;
    public PlayerStats playerStats;
    
    [SerializeField] private int pizzaCapacity;
    [SerializeField] private PizzaOrders orders;    
    [SerializeField] private TMP_Text tipsText2;
    
    private bool _inPetrol;
    private bool _inWater;
    private bool _inFood;
    private bool _inRepair;
    private bool _inLocation;
    private bool _inPizza;

    private string _locationID;

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.E)) return;
        if (_inPetrol)
        {
            playerStats.BuyFuel(money);
        }
        else if (_inWater)
        {
            playerStats.BuyDrink(money);
        }
        else if (_inFood)
        {
            playerStats.BuyFood(money);
        }
        else if (_inRepair)
        {
            playerStats.BuyLongevity(money);
        }
        else if (_inPizza)
        {
            foreach (int currentOrder in orders.currentOrders)
            {
                if (collectedPizzas.Count < pizzaCapacity)
                {
                    if (!collectedPizzas.Contains(currentOrder))
                    {
                        collectedPizzas.Add(currentOrder);
                        orders.deliveryLocations[currentOrder].transform.GetChild(0).GetChild(0).GetComponent<Animator>().Play("LocationShowYellow", -1, 0f);
                    }
                }
            }
        }
        else if (_inLocation)
        {
            int.TryParse(_locationID, out int locationID);
            if (collectedPizzas.Contains(locationID))
            {
                orders.deliveryLocations[locationID].transform.GetChild(0).GetChild(0).GetComponent<Animator>().Play("HideYellow", -1, 0f);
                collectedPizzas.Remove(locationID);
                orders.currentOrders.Remove(locationID);
                orders.deliveryLocations[locationID].enabled = false;
                orders.deliveryLocations[locationID].transform.GetChild(1).GetChild(0).GetComponent<Animator>().Play("EHide", -1, 0f);
                if (orders.times[locationID] > 0)
                    playerStats.tips += Random.Range(0.6f, 1.0f);
                else
                    playerStats.tips += Random.Range(0.3f, 0.6f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Petrol Pump"))
        {
            other.transform.GetChild(0).GetChild(0).GetComponent<Animator>().Play("ShopOpen", -1, 0f);
            other.transform.GetChild(1).GetChild(0).GetComponent<Animator>().Play("EShow", -1, 0f);
            tipsText2.gameObject.SetActive(true);
        }
        else if (other.CompareTag("Food"))
        {
            other.transform.GetChild(0).GetChild(0).GetComponent<Animator>().Play("ShopOpen", -1, 0f);
            other.transform.GetChild(1).GetChild(0).GetComponent<Animator>().Play("EShow", -1, 0f);
            tipsText2.gameObject.SetActive(true);
        }
        else if (other.CompareTag("Pizza"))
        {
            _inPizza = true;
            other.transform.GetChild(1).GetChild(0).GetComponent<Animator>().Play("EShow", -1, 0f);
        }
        else if (other.CompareTag("Water"))
        {
            other.transform.GetChild(0).GetChild(0).GetComponent<Animator>().Play("ShopOpen", -1, 0f);
            other.transform.GetChild(1).GetChild(0).GetComponent<Animator>().Play("EShow", -1, 0f);
            tipsText2.gameObject.SetActive(true);
        }
        else if (other.CompareTag("RepairShop"))
        {
            other.transform.GetChild(0).GetChild(0).GetComponent<Animator>().Play("ShopOpen", -1, 0f);
            other.transform.GetChild(1).GetChild(0).GetComponent<Animator>().Play("EShow", -1, 0f);
            tipsText2.gameObject.SetActive(true);
        }
        else if (other.CompareTag("Location"))
        {
            _inLocation = true;
            _locationID = other.name;
            int.TryParse(_locationID, out int locationID);
            if (collectedPizzas.Contains(locationID))
                other.transform.GetChild(1).GetChild(0).GetComponent<Animator>().Play("EShow", -1, 0f);
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
        if (other.CompareTag("Petrol Pump"))
        {
            other.transform.GetChild(0).GetChild(0).GetComponent<Animator>().Play("CloseShop", -1, 0f);
            other.transform.GetChild(1).GetChild(0).GetComponent<Animator>().Play("EHide", -1, 0f);
            tipsText2.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Food"))
        {
            other.transform.GetChild(0).GetChild(0).GetComponent<Animator>().Play("CloseShop", -1, 0f);
            other.transform.GetChild(1).GetChild(0).GetComponent<Animator>().Play("EHide", -1, 0f);
            tipsText2.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Pizza"))
        {
            other.transform.GetChild(1).GetChild(0).GetComponent<Animator>().Play("EHide", -1, 0f);
        }
        else if (other.CompareTag("Water"))
        {
            other.transform.GetChild(0).GetChild(0).GetComponent<Animator>().Play("CloseShop", -1, 0f);
            other.transform.GetChild(1).GetChild(0).GetComponent<Animator>().Play("EHide", -1, 0f);
            tipsText2.gameObject.SetActive(false);
        }
        else if (other.CompareTag("RepairShop"))
        {
            other.transform.GetChild(0).GetChild(0).GetComponent<Animator>().Play("CloseShop", -1, 0f);
            other.transform.GetChild(1).GetChild(0).GetComponent<Animator>().Play("EHide", -1, 0f);
            tipsText2.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Location"))
        {
            int.TryParse(_locationID, out int locationID);
            if (collectedPizzas.Contains(locationID))
                other.transform.GetChild(1).GetChild(0).GetComponent<Animator>().Play("EHide", -1, 0f);
        }
        _inPetrol = false;
        _inWater = false;
        _inFood = false;
        _inRepair = false;
        _inLocation = false;
        _inPizza = false;
        _locationID = "";
    }
}
