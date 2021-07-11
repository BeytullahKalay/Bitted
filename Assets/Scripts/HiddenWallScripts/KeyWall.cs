using UnityEngine;

public class KeyWall : MonoBehaviour
{
    public GameObject hiddenWall;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && gameObject.tag != "Wall")
        {
            GameManager gmScript =  FindObjectOfType<GameManager>();
            gmScript.PlayParticle(gmScript.doorParticle_Y, transform.position, transform.rotation);
            hiddenWall.SetActive(true);
            Destroy(gameObject);
        }
    }
}
