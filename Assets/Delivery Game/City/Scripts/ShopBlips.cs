using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopBlips : MonoBehaviour
{
    [SerializeField] private GameObject otherFuel;
    [SerializeField] private PlayerCollision playerCollision;
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text tipsText;
    [SerializeField] private TMP_Text sliderText;
    [SerializeField] private TMP_Text subtractedText;
    [SerializeField] private Transform ind;
    [SerializeField] private TMP_Text tipsText2;
    
    private float _subtracted;
    
    private void Update()
    {
        if (!slider.interactable)
        {
            if (ind.name == "Fuel")
            {
                if (!otherFuel.GetComponent<Slider>().interactable)
                {
                    ind.gameObject.SetActive(false);
                    ind.parent.GetChild(3).gameObject.SetActive(false);
                }
            }
            else
            {
                ind.gameObject.SetActive(false);
                ind.parent.GetChild(3).gameObject.SetActive(false);
            }

            return;
        }

        tipsText.text = "$" + playerCollision.playerStats.tips.ToString("0.00");
        slider.maxValue = playerCollision.playerStats.tips;
        sliderText.text = "-$" + slider.value.ToString("0.00");
        _subtracted = playerCollision.playerStats.tips - slider.value;
        tipsText2.text = subtractedText.text = "$" + _subtracted.ToString("0.00");
        playerCollision.money = slider.value;
        
        ind.gameObject.SetActive(true);
        ind.parent.GetChild(3).gameObject.SetActive(true);
        
        var scale = ind.localScale;
        if (ind.name == "Fuel")
        {
            scale.x = ((slider.value / playerCollision.playerStats.fuelRatePerLitre) / 5) +
                      ind.parent.GetChild(0).localScale.x / 100f;
            scale.x = Mathf.Clamp(scale.x, 0f, 1f);
            ind.localScale = scale;
            
            ind.parent.GetChild(3).GetComponent<TMP_Text>().text =
                Mathf.Clamp(playerCollision.playerStats.fuel + slider.value / playerCollision.playerStats.fuelRatePerLitre, 0f, 5f)
                .ToString("0.00");
        }
        else if (ind.name == "Food")
        {
            scale.x = ((slider.value / playerCollision.playerStats.foodRatePerMeal) / 5) +
                      ind.parent.GetChild(0).localScale.x / 100f;
            scale.x = Mathf.Clamp(scale.x, 0f, 1f);
            ind.localScale = scale;
            
            ind.parent.GetChild(3).GetComponent<TMP_Text>().text =
                Mathf.Clamp(playerCollision.playerStats.energy + slider.value / playerCollision.playerStats.foodRatePerMeal, 0f, 5f)
                .ToString("0.00");
        }
        else if (ind.name == "Hydro")
        {
            scale.x = ((slider.value / playerCollision.playerStats.drinkRatePerDrink) / 5) +
                      ind.parent.GetChild(0).localScale.x / 100f;
            scale.x = Mathf.Clamp(scale.x, 0f, 1f);
            ind.localScale = scale;
            
            ind.parent.GetChild(3).GetComponent<TMP_Text>().text =
                Mathf.Clamp(playerCollision.playerStats.hydration + slider.value / playerCollision.playerStats.drinkRatePerDrink, 0f, 5f)
                .ToString("0.00");
        }
        else if (ind.name == "Long")
        {
            scale.x = ((slider.value / playerCollision.playerStats.longevityRatePer) / 5) +
                      ind.parent.GetChild(0).localScale.x / 100f;
            scale.x = Mathf.Clamp(scale.x, 0f, 1f);
            ind.localScale = scale;
            
            ind.parent.GetChild(3).GetComponent<TMP_Text>().text =
                Mathf.Clamp(playerCollision.playerStats.longevity + slider.value / playerCollision.playerStats.longevityRatePer, 0f, 5f)
                .ToString("0.00");
        }
    }
}
