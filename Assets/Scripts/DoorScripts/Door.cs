using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Material defaultWhiteMat;

    [SerializeField] private float _doorForce = 3f;

    SpriteRenderer sr;
    Rigidbody2D playerRb;

    public enum keyColor { Purple, Orange, Yellow };
    public keyColor myColor;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 11) // 11 = Player Layer
        {


            SpriteRenderer playerSpriteRenderer = collision.gameObject.GetComponentInChildren<SpriteRenderer>();

            if (playerSpriteRenderer.color == sr.color)
            {

                //play partical effects
                PlayParticlEffectAnim();

                //set color
                playerSpriteRenderer.color = defaultWhiteMat.color;

                //destroy
                Destroy(gameObject);
            }
            else
            {
                playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

                Vector3 direction = collision.gameObject.transform.position - transform.position;

                direction = direction.normalized;

                playerRb.AddForce(direction * _doorForce, ForceMode2D.Impulse);

                StartCoroutine(setPlayerRigidbodyVelocityToZero());
            }
        }
    }

    private void PlayParticlEffectAnim()
    {
        GameManager gameManager = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();

        switch (myColor)
        {
            case keyColor.Purple:
                gameManager.PlayParticle(gameManager.doorParticle_P, transform.position,transform.rotation);
                break;
            case keyColor.Orange:
                gameManager.PlayParticle(gameManager.doorParticle_O, transform.position, transform.rotation);
                break;
            case keyColor.Yellow:
                gameManager.PlayParticle(gameManager.doorParticle_Y, transform.position, transform.rotation);
                break;
            default:
                break;
        }

    }

    IEnumerator setPlayerRigidbodyVelocityToZero()
    {
        yield return new WaitForSeconds(.5f);

        if (playerRb != null)
        {
            playerRb.velocity = Vector3.zero;
        }
    }
}
