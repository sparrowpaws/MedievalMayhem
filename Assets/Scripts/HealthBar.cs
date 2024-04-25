using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider healthSlider;

    public void SetSlider(int amount)
    {
        healthSlider.value = amount;
    }

    public void SetSliderMax(int amount)
    {
        healthSlider.maxValue = amount; //sets the max value for the slider
        SetSlider(amount);
    }
}
