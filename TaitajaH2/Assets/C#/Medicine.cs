using UnityEngine;
using UnityEngine.UI;

public class Medicine : MonoBehaviour
{
    [SerializeField] private int _healthPoints = 10; // Очки здоров'я
    [SerializeField] private GameObject particles;  // Частинки
    [SerializeField] private Slider stressSlider;   // Слайдер стресу
    private Transform player;

    private void Awake()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("Player not found in the scene. Ensure the Player has the correct tag.");
        }
    }

    public void MedicineDoing()
    {
        if (stressSlider != null)
        {
            if (particles != null && player != null)
            {
                Instantiate(particles, player.position, Quaternion.identity);
            }

            // Зменшуємо значення слайдера
            float adjustment = _healthPoints / 100f; // Перетворення очок здоров'я у значення від 0 до 1
            stressSlider.value = Mathf.Clamp(stressSlider.value - adjustment, stressSlider.minValue, stressSlider.maxValue);
        }
        else
        {
            Debug.LogWarning("StressSlider is not assigned in Medicine script.");
        }
    }
}
