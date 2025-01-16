using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform target;               // Гравець або ціль
    [SerializeField] Vector3 zoneCenter;             // Центр зони
    [SerializeField] float zoneRadius = 10f;         // Радіус зони, в якій ворог почне переслідувати
    [SerializeField] float safeDistance = 1.3f;        // Відстань безпеки, на яку ворог може наблизитись
    NavMeshAgent agent;

    private bool isPlayerInZone = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
            }
            else
            {
                // Встановлюємо нову ціль в такому випадку, щоб ворог зупинявся на safeDistance
                Vector3 direction = (target.position - agent.transform.position).normalized;
                Vector3 targetPosition = target.position - direction * safeDistance;
                agent.SetDestination(targetPosition);
            }
        }
        else
        {
            agent.ResetPath();
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
