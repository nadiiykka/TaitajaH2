using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject item;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    public void SpawnObject()
    {
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y + 1);
        Instantiate(item, playerPos, Quaternion.identity);
    }
}
