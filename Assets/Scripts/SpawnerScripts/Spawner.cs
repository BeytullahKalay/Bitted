using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float spawnTime = 2f;
    public GameObject playerPrefab;

    private void Start()
    {
        FindObjectOfType<CollisionDetection>().OnPlayerDeath += OnGameOver;
    }

    public void OnGameOver()
    {
        StartCoroutine(SpawnPlayer(spawnTime));
    }

    private IEnumerator SpawnPlayer(float spawnTime)
    {
        yield return new WaitForSeconds(spawnTime); // wait for spawnTime seconds

        if (!GameObject.Find("Player"))
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        GameObject spawnPoint = GameObject.Find("PlayerStartPos");
        GameObject player = Instantiate(playerPrefab, transform.position, transform.rotation);
        player.GetComponent<CollisionDetection>().OnPlayerDeath += OnGameOver;
    }
}
