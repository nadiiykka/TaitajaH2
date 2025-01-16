using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private bool isGameOver = false; // Перевірка, щоб уникнути повторного запуску програшу

    void Start()
    {
        if (proximitySlider != null)
        {
            proximitySlider.minValue = 0;
            proximitySlider.maxValue = 1;
            proximitySlider.value = 0;
        }
        else
        {
            Debug.LogError("ProximitySlider is not assigned in the Inspector.");
        }
    }

    void Update()
    {
        if (proximitySlider != null && targetObject != null && movingObjects != null && movingObjects.Length > 0 && !isGameOver)
        {
            float totalInfluence = 0f;

            foreach (Transform movingObject in movingObjects)
            {
                float distance = Vector3.Distance(targetObject.position, movingObject.position);
                distance = Mathf.Clamp(distance, minDistance, maxDistance);

                float normalizedValue = 1 - ((distance - minDistance) / (maxDistance - minDistance));

                totalInfluence += normalizedValue;
            }

            proximitySlider.value = Mathf.Clamp(baseValue + totalInfluence, 0, 1);

            if (proximitySlider.value >= proximitySlider.maxValue)
            {
                if (playerAnim != null)
                {
                    playerAnim.SetBool("Death", true);
                }

                // Уникнення повторного запуску програшу
                isGameOver = true;

                // Запускаємо затримку перед завантаженням сцени
                StartCoroutine(WaitAndLoadScene(1f, 4)); // Затримка 1 секунда, завантаження сцени з індексом 4
            }
            else
            {
                if (playerAnim != null)
                {
                    playerAnim.SetBool("Death", false);
                }
            }
        }
    }

    public void AdjustBaseValue(float amount)
    {
        baseValue = Mathf.Clamp(baseValue + amount, 0, 1);
    }

    // Корутин для затримки перед завантаженням сцени
    private IEnumerator WaitAndLoadScene(float delay, int sceneIndex)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneIndex);
    }
}
