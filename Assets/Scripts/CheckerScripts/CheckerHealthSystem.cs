using UnityEngine;

public class CheckerHealthSystem : MonoBehaviour
{

    [SerializeField] private int totalHealth = 100;

    private int currentHealth;

    Checker checkerScript;

    void Start()
    {
        currentHealth = totalHealth;
        checkerScript = GetComponent<Checker>();
    }

    public void TakeDamage(int damage)
    {
        AttackToPlayer();

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            checkerScript.CheckerDied();
        }
    }

    private void AttackToPlayer()
    {

        checkerScript.playerDetectedByBullet = true;

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<GuiltyValue>().isGuilty = true;
        }
    }
}
