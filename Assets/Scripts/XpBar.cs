using UnityEngine;
using UnityEngine.UI;

public class XpBar : MonoBehaviour
{
    public Slider slider;

    public float xp
    {
        get => slider.value;
        set => slider.value = value;
    }
    public float xpToNextLevel
    {
        get => slider.maxValue;
        set => slider.maxValue = value;
    }
}
