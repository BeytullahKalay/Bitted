using UnityEngine;

public class CollisionDetection : MonoBehaviour
{

    public event System.Action OnPlayerDeath;

    private void Start()
    {
        OnPlayerDeath += PlayDeathEffect;
        OnPlayerDeath += SetGuiltyBoolean;
        OnPlayerDeath += CallDestroyMethod;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 10 || collision.gameObject.layer == 9) // 10 = Wall Layer ----- 9 = Enemy Layer
        {
            FindObjectOfType<GameManager>().SetPlayerColorToWhite();
            OnPlayerDeath();
        }
    }

    private void PlayDeathEffect()
    {
        GameManager gameManager = GameObject.FindWithTag("GM").GetComponent<GameManager>();

        gameManager.PlayParticle(gameManager.playerDeadPrefab, transform.position, transform.rotation);

        GameObject.Find("Camera").GetComponentInChildren<CameraShake>().Shake(.4f, .2f); // screen shake
    }

    private void CallDestroyMethod()
    {
        Destroy(gameObject);
    }

    private void SetGuiltyBoolean()
    {
        gameObject.GetComponent<GuiltyValue>().isGuilty = false;
    }

}
