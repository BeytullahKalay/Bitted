using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataOpenShoot : MonoBehaviour
{
    public GameObject dialog3;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject playerPrefab = GameObject.Find("Spawner").GetComponent<Spawner>().playerPrefab;
            playerPrefab.GetComponent<Shoot>().enabled = true;
            collision.gameObject.transform.parent.GetComponent<Shoot>().enabled = true;
            dialog3.SetActive(true);

            GameManager gmScript = FindObjectOfType<GameManager>();
            gmScript.PlayParticle(gmScript.keyParticle_Y, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }
}
