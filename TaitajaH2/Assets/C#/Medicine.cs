using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Medicine : MonoBehaviour
{
    [SerializeField] private int _healthPoints = 10;
    public Slider proximitySlider;

    // Update is called once per frame
    public void MedicineDoing()
    {
        if (proximitySlider != null)
        {
            float currentValue = proximitySlider.value;

            float maxValue = proximitySlider.maxValue;

            float newValue = currentValue - (_healthPoints / maxValue);

            newValue = Mathf.Clamp(newValue, proximitySlider.minValue, maxValue);

            proximitySlider.value = newValue;
        }
    }
}
