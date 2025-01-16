using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform target;               // Гравець або ціль
    [SerializeField] Vector3 zoneCenter;             // Центр зони
    [SerializeField] float zoneRadius = 10f;         // Радіус зони, в якій ворог почне переслідувати
    [SerializeField] float safeDistance = 1.3f;      // Відстань безпеки, на яку ворог може наблизитись
    private float mediumDistance = 5f;               // Середня відстань, на якій ворог почне говорити
    [SerializeField] Animator santaTalk;             // Аніматор для анімації "Talk"
    [SerializeField] TextMeshProUGUI santaText;      // Компонент тексту
    [SerializeField]
    string[] santaPhrases = new string[5];           // Масив фраз для випадкового вибору
    NavMeshAgent agent;

    private bool isPlayerInZone = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        santaTalk.SetBool("Talk", false); // Початково "Talk" виключено
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInZone && target)
        {
            // Обчислюємо відстань між ворогом і гравцем
            float distanceToPlayer = Vector3.Distance(agent.transform.position, target.position);

            // Якщо відстань більше ніж safeDistance, встановлюємо ціль
            if (distanceToPlayer > safeDistance)
            {
                agent.SetDestination(target.position);

                // Якщо відстань до гравця менша або рівна середній відстані, ворог починає говорити
                if (distanceToPlayer <= mediumDistance)
                {
                    if (santaTalk != null)
                    {
                        santaTalk.SetBool("Talk", true); // Включаємо анімацію "Talk"
                    }

                    // Вибір випадкової фрази з масиву та оновлення тексту
                    if (santaText != null && santaPhrases.Length > 0)
                    {
                        int randomIndex = Random.Range(0, santaPhrases.Length); // Генерація випадкового індексу
                        santaText.text = santaPhrases[randomIndex]; // Встановлюємо текст
                    }
                }
                else
                {
                    if (santaTalk != null)
                    {
                        santaTalk.SetBool("Talk", false); // Вимикаємо анімацію "Talk"
                    }
                }
            }
            else
            {
                agent.ResetPath(); // Встановлюємо шлях до гравця тільки якщо відстань більше безпечної
            }
        }
    }

    void FixedUpdate()
    {
        // Перевірка, чи гравець у зоні
        if (target != null)
        {
            float distanceToZone = Vector3.Distance(target.position, zoneCenter);
            isPlayerInZone = distanceToZone <= zoneRadius;
        }
        else
        {
            isPlayerInZone = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        // Візуалізація зони в редакторі
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(zoneCenter, zoneRadius);
    }
}
