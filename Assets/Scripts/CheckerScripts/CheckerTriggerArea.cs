using UnityEngine;

public class CheckerTriggerArea : MonoBehaviour
{
    [HideInInspector] public bool playerDetector;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetector = true;
        }
    }


}
