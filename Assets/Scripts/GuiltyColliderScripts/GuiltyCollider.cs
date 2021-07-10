using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiltyCollider : MonoBehaviour
{
    public GameObject playerPrefab;
    public Material red;




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11) // 11 = Player Layer
        {
            collision.gameObject.transform.parent.GetComponent<GuiltyValue>().isGuilty = true;
            collision.gameObject.transform.parent.GetComponentInChildren<SpriteRenderer>().color = red.color;

        }
    }
}
