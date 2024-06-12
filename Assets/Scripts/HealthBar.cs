using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public float hp
    {
        get => slider.value;
        set => slider.value = value;
    }

    public float maxHealth
    {
        get => slider.maxValue;
        set => slider.maxValue = value;
    }
}
