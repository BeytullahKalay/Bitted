using System.Collections;
using UnityEngine;

public class Checker : MonoBehaviour
{


    public Material normalMat;
    public Material detectMat;
    public Material attackMat;

    [SerializeField] private float _attackSpeed = 6f;
    [SerializeField] private float _normalMoveSpeed = 6f;
    [SerializeField] private float _checkTime = 2f;
    [SerializeField] private float _returnPosAfterSeconds = 2f;

    public bool playerDetectedByBullet;
    public bool playerDetected;

    bool canMoveTheStartPosition;
    bool stillAttackToPlayer;

    Rigidbody2D rb;
    SpriteRenderer sr;

    GameObject _player;
    Vector3 _startPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        _startPos = transform.position;
    }

    void Update()
    {

        PlayerDetection();
        BackToStartPos();

    }
    private void PlayerDetection()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        if (_player != null)
        {
            playerDetected = GetComponentInChildren<CheckerTriggerArea>().playerDetector;

            print(playerDetectedByBullet);

            if (playerDetected || playerDetectedByBullet)
            {
                //if (GetComponent<Patrol>() != null)
                //{
                //    GetComponent<Patrol>().enabled = false;
                //}

                GetComponentInChildren<CheckerTriggerArea>().playerDetector = false;
                playerDetectedByBullet = false;
                GetComponent<Patrol>().playerDetectedByBullet = false;
                StartCoroutine(WaitAndDecideToAction(_checkTime));
            }
        }
    }

    private void BackToStartPos()
    {
        if (canMoveTheStartPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, _startPos, _normalMoveSpeed * Time.deltaTime);


            if (transform.position == _startPos)
            {
                canMoveTheStartPosition = false;

                stillAttackToPlayer = false;

                //if (GetComponent<Patrol>() != null)
                //{
                //    if (!GetComponent<Patrol>().enabled)
                //    {
                //        print("enable");
                //        GetComponent<Patrol>().enabled = true;
                //    }
                //}

            }

        }
    }


    public IEnumerator WaitAndDecideToAction(float waitTime)
    {

        sr.color = detectMat.color; // Yellow

        yield return new WaitForSeconds(waitTime);


        if (_player != null && _player.GetComponentInChildren<GuiltyValue>().isGuilty)
        {
            if (!stillAttackToPlayer)
            {
                AttackToPlayer();
            }
        }
        else if (_player != null && !_player.GetComponentInChildren<GuiltyValue>().isGuilty)
        {
            sr.color = normalMat.color;

            //if (GetComponent<Patrol>() != null)
            //{
            //    if (!GetComponent<Patrol>().enabled)
            //    {
            //        print("enable");
            //        GetComponent<Patrol>().enabled = true;
            //    }
            //}

        }
        else
        {
            StartCoroutine(ReturnToPosition(_returnPosAfterSeconds));
        }

    }

    private void AttackToPlayer()
    {


        sr.color = attackMat.color; // Red
        Vector3 attackDir = GameObject.FindObjectOfType<PolygonCollider2D>().transform.position - transform.position;
        rb.AddForce(attackDir * _attackSpeed, ForceMode2D.Impulse);
        stillAttackToPlayer = true;
        GetComponentInChildren<CheckerTriggerArea>().playerDetector = false;
        StartCoroutine(ReturnToPosition(_returnPosAfterSeconds));
    }

    private IEnumerator ReturnToPosition(float WaitToReturnTime)
    {
        yield return new WaitForSeconds(WaitToReturnTime);
        rb.velocity = Vector3.zero;
        canMoveTheStartPosition = true;
        sr.color = normalMat.color;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 11) // 11 = Player Layer
        {
            _player.GetComponent<GuiltyValue>().isGuilty = false;
        }

        if (collision.gameObject.layer == 9) // 9 = Enemy Layer
        {
            CheckerDied();
        }
    }

    public void CheckerDied()
    {
        //play particle effect
        PlayDeathParticleEffect();

        //do camere shake
        GameObject.Find("Camera").GetComponentInChildren<CameraShake>().Shake(.2f, .1f);

        //destroy
        Destroy(gameObject);
    }

    private void PlayDeathParticleEffect()
    {
        GameManager gameManager = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        gameManager.PlayParticle(gameManager.enemyParticle, transform.position, transform.rotation);
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, GetComponentInChildren<CircleCollider2D>().radius);
    }
}
