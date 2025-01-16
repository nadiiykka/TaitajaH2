using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StressSlider : MonoBehaviour
{
    public Slider proximitySlider; // Слайдер
    public Transform targetObject; // Об'єкт, до якого наближається
    public Transform movingObject; // Об'єкт, який наближається
    public float maxDistance = 10f; // Максимальна дистанція
    public float minDistance = 0f; // Мінімальна дистанція

    void Start()
    {
        if (proximitySlider != null)
        {
            proximitySlider.minValue = 0;
            proximitySlider.maxValue = 1; // Значення слайдера в межах 0-1
            proximitySlider.value = 0; // Початкове значення
        }
    }

    void Update()
    {
        if (proximitySlider != null && targetObject != null && movingObject != null)
        {
            // Обчислюємо відстань між об'єктами
            float distance = Vector3.Distance(targetObject.position, movingObject.position);

            // Обмежуємо відстань в межах [minDistance, maxDistance]
            distance = Mathf.Clamp(distance, minDistance, maxDistance);

            // Нормалізуємо значення слайдера (чим ближче, тим більше значення)
            float normalizedValue = 1 - ((distance - minDistance) / (maxDistance - minDistance));

            // Оновлюємо слайдер
            proximitySlider.value = normalizedValue;
        }
    }
}
