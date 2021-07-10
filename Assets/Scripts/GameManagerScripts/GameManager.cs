using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if ( instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    [Header("Player and Bullet Particles")]
    public ParticleSystem bulletExplosionPrefab;
    public ParticleSystem playerDeadPrefab;

    [Header("Key Particles")]
    public ParticleSystem keyParticle_P;
    public ParticleSystem keyParticle_O;
    public ParticleSystem keyParticle_Y;

    [Header("Door Particles")]
    public ParticleSystem doorParticle_P;
    public ParticleSystem doorParticle_O;
    public ParticleSystem doorParticle_Y;

    [Header("Enemy Particle")]
    public ParticleSystem enemyParticle;



    private void Update()
    {
        RestratScene();
    }

    private void RestratScene()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void PlayParticle(ParticleSystem particleName,Vector3 playPos,Quaternion playRotation)
    {
        particleName.transform.position = playPos;
        particleName.transform.rotation = playRotation * Quaternion.Euler(0,0,90);
        particleName.Play();
    }

}
