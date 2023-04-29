using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class PizzaOrders : MonoBehaviour
{
    public List<int> currentOrders;
    public BoxCollider[] deliveryLocations;
    public List<int> times;

    [SerializeField] private PlayerCollision playerCollision;
    [SerializeField] private TMP_Text orders;
    [SerializeField] private int numDeliveries = 3;
    
    private int _time;

    private bool _newOrder;
    
    private void Start()
    {
        InvokeRepeating(nameof(NewOrders), 5.0f, 5.0f);
        InvokeRepeating(nameof(DeliveryTime), 1.0f, 1.0f);

        for (int i = 0; i < deliveryLocations.Length; i++)
            times.Add(0);
    }

    private void DeliveryTime()
    {
        if (times.Count == 0) return;
        for (int i = 0; i < times.Count; i++)
        {
            if (times[i] > 0)
                times[i]--;
        }

        for (int i = 0; i < deliveryLocations.Length; i++)
        {
            if (times[i] != 0)
            {
                deliveryLocations[i].transform.GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetComponent<TMP_Text>()
                    .text = times[i].ToString();
                deliveryLocations[i].transform.GetChild(0).GetChild(0).GetChild(1).GetChild(1).GetComponent<TMP_Text>()
                    .text = times[i].ToString();
            }
            else
            {
                deliveryLocations[i].transform.GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetComponent<TMP_Text>()
                    .text = "No extra tip!";
                deliveryLocations[i].transform.GetChild(0).GetChild(0).GetChild(1).GetChild(1).GetComponent<TMP_Text>()
                    .text = "No extra tip!";
            }
        }
        
    }

    private void NewOrders()
    {
        _time++;

        for (int i = 0; i < numDeliveries; i++)
        {
            int x = Random.Range(0, deliveryLocations.Length);
            if (!currentOrders.Contains(x) && !playerCollision.collectedPizzas.Contains(x))
            {
                currentOrders.Add(x);
                deliveryLocations[x].enabled = true;
                deliveryLocations[x].transform.GetChild(0).GetChild(0).GetComponent<Animator>().Play("LocationBlueShow", -1, 0f);
                times[x] = 20;
                _newOrder = true;
            }
        }

        if (_newOrder)
        {
            _newOrder = false;
            orders.transform.parent.GetComponent<Animator>().Play("NewOrder", -1, 0f);
        }
    }

    private void Update()
    {
        orders.text = (currentOrders.Count - playerCollision.collectedPizzas.Count).ToString();
    }
}
