using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class PizzaOrders : MonoBehaviour
{
    public List<int> currentOrders;
    public BoxCollider[] deliveryLocations;
    
    [SerializeField] private PlayerCollision playerCollision;
    [SerializeField] private TMP_Text orders;
    [SerializeField] private int numDeliveries = 3;
    
    private int _time;
    
    private void Start()
    {
        InvokeRepeating(nameof(NewOrders), 5.0f, 5.0f);
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
                deliveryLocations[x].transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    private void Update()
    {
        orders.text = (currentOrders.Count - playerCollision.collectedPizzas.Count).ToString();
    }
}
