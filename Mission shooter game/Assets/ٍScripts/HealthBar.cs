using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBarSilder;

    public void GivefullHealth(float health)
    {
        healthBarSilder.maxValue = health;
        healthBarSilder.value = health;
    }

    public void SetHealth(float health)
    {
        healthBarSilder.value = health;
    }
}
