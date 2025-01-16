using System.Collections;
using UnityEngine;

public class Medicine : MonoBehaviour
{
    [SerializeField] private int _healthPoints = 10;
    [SerializeField] private GameObject particles;
    private StressSlider stressSlider;
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

    private void Start()
    {
        stressSlider = FindObjectOfType<StressSlider>();

        if (stressSlider == null)
        {
            Debug.LogError("StressSlider not found in the scene. Attempting to retry after one frame...");
            StartCoroutine(TryFindStressSlider());
        }
        else
        {
            Debug.Log("StressSlider found immediately.");
        }
    }

    private IEnumerator TryFindStressSlider()
    {
        yield return null;

        stressSlider = FindObjectOfType<StressSlider>();

        if (stressSlider == null)
        {
            Debug.LogError("StressSlider still not found. Please check the scene.");
        }
        else
        {
            Debug.Log("StressSlider found after delay.");
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

            // Розрахунок зменшення стресу
            float adjustment = _healthPoints / 100f; // Перетворюємо очки здоров'я у значення від 0 до 1
            stressSlider.AdjustBaseValue(-adjustment); // Викликаємо метод StressSlider
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("StressSlider is still not available. Double check if it's in the scene and active.");
        }
    }
}
