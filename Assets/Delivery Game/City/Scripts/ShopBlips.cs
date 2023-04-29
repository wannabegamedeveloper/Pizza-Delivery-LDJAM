using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopBlips : MonoBehaviour
{
    [SerializeField] private PlayerCollision playerCollision;
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text tipsText;
    [SerializeField] private TMP_Text sliderText;
    [SerializeField] private TMP_Text subtractedText;

    private float _subtracted;
    
    private void Update()
    {
        if (!slider.interactable) return;
        tipsText.text = "$" + playerCollision.playerStats.tips.ToString("0.00");
        slider.maxValue = playerCollision.playerStats.tips;
        sliderText.text = "-$" + slider.value.ToString("0.00");
        _subtracted = playerCollision.playerStats.tips - slider.value;
        subtractedText.text = "$" + _subtracted.ToString("0.00");
        playerCollision.money = slider.value;
    }
}
