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
        healthSliderMax.value = amount;
        SetSlider(amount);
    }
}
