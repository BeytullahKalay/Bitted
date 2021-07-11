using UnityEngine;

public class Key : MonoBehaviour
{

    SpriteRenderer sr;

    public enum keyColor { Purple,Orange,Yellow};
    public keyColor myColor;

    public int visibleDeathCount;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        FindObjectOfType<DeathChecker>().keys.Add(gameObject);
        gameObject.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11) // 11 = Player Layer
        {
            GameObject playerSpriteObject = collision.gameObject.transform.parent.GetChild(0).gameObject;

            //set player color to key color
            playerSpriteObject.GetComponent<SpriteRenderer>().color = sr.color;

            //play particle Effects
            PlayParticleEffectAnim();

            //destroy
            Destroy(gameObject);
        }
    }

    private void PlayParticleEffectAnim()
    {
        GameManager gameManager = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        

        switch (myColor)
        {
            case keyColor.Purple:
                gameManager.PlayParticle(gameManager.keyParticle_P, transform.position, transform.rotation);
                break;
            case keyColor.Orange:
                gameManager.PlayParticle(gameManager.keyParticle_O, transform.position, transform.rotation);
                break;
            case keyColor.Yellow:
                gameManager.PlayParticle(gameManager.keyParticle_Y, transform.position, transform.rotation);
                break;
            default:
                Debug.LogError("No collor ");
                break;
        }
    }

}
