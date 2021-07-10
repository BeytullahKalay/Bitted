using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");


        if (player != null)
        {
            Vector3 playerPos = player.transform.position;
            transform.position = new Vector3(playerPos.x, playerPos.y, -10);
        }

    }
}
