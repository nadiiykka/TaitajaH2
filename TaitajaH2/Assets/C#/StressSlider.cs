using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StressSlider : MonoBehaviour
{
    public Slider proximitySlider; // Слайдер
    public Transform targetObject; // Об'єкт, до якого наближаються
    public Transform[] movingObjects; // Масив об'єктів, які наближаються
    public float maxDistance = 10f; // Максимальна дистанція
    public float minDistance = 0f; // Мінімальна дистанція
    public Animator playerAnim;

    private float baseValue = 0; // Базове значення, яке змінюється зовнішніми скриптами

    void Start()
    {
        if (proximitySlider != null)
        {
            proximitySlider.minValue = 0;
            proximitySlider.maxValue = 1;
            proximitySlider.value = 0;
        }
    }

    void Update()
    {
        if (proximitySlider != null && targetObject != null && movingObjects != null && movingObjects.Length > 0)
        {
            float totalInfluence = 0f;

            // Перебираємо всі об'єкти і додаємо їхні відстані
            foreach (Transform movingObject in movingObjects)
            {
                float distance = Vector3.Distance(targetObject.position, movingObject.position);
                distance = Mathf.Clamp(distance, minDistance, maxDistance);

                // Нормалізуємо значення відстані для кожного об'єкта
                float normalizedValue = 1 - ((distance - minDistance) / (maxDistance - minDistance));

                // Додаємо вплив кожного об'єкта до загального впливу
                totalInfluence += normalizedValue;
            }

            // Встановлюємо нове значення слайдера, враховуючи сумарний вплив
            proximitySlider.value = Mathf.Clamp(baseValue + totalInfluence, 0, 1);

            // Перевірка, чи слайдер досяг максимального значення
            if (proximitySlider.value >= proximitySlider.maxValue)
            {
                if (playerAnim != null)
                {
                    playerAnim.SetBool("Death", true); // "Death" - це параметр в аніматорі, який вмикається, коли слайдер на максимумі
                }
            }
            else
            {
                if (playerAnim != null)
                {
                    playerAnim.SetBool("Death", false); // Якщо слайдер не на максимумі, змінюємо параметр на false
                }
            }
        }
    }

    // Публічний метод для зміни базового значення
    public void AdjustBaseValue(float amount)
    {
        baseValue = Mathf.Clamp(baseValue + amount, 0, 1);
    }
}
