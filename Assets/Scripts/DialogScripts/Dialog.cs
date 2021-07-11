using System.Collections;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    public GameObject image;
    public GameObject pressAnyKey;
    public TextMeshProUGUI textDisplay;

    public string[] sentences;
    private int index;
    public float typingSpeed = 0.02f;
    public GameObject openObject;

    public int visibleDeathCount;
    public int waitTime = 2;

    AudioClip audioClip;
    AudioSource audioSource;

    public enum SoundFX { systemSound, virusSound };
    public SoundFX mySound;

    private bool uiStarted;

    private void Start()
    {
        image.SetActive(false);

        if (visibleDeathCount < 1)
        {
            uiStarted = true;
            StartRoutine(); 
        }
    }



    private void Update()
    {

        if (FindObjectOfType<DeathCounter>().GetDeathCount() >= visibleDeathCount)
        {
            if (!uiStarted)
            {
                uiStarted = true;
                StartCoroutine(StartRoutineWithDelay());
            }
        }



        ContinueDialog();
    }


    private IEnumerator StartRoutineWithDelay()
    {
        yield return new WaitForSeconds(waitTime);
        StartRoutine();
    }

    private void StartRoutine()
    {
        FindObjectOfType<GuiltyValue>().inDialog = true;
        image.SetActive(true);
        pressAnyKey.SetActive(false);
        DecideToSoundEffect();
        FindObjectOfType<PlayerMovement>().enabled = false;
        StartCoroutine(Type());
    }

    private void DecideToSoundEffect()
    {
        switch (mySound)
        {
            case SoundFX.systemSound:
                audioClip = FindObjectOfType<GameManager>().systemSound.clip;
                break;
            case SoundFX.virusSound:
                audioClip = FindObjectOfType<GameManager>().virusSound.clip;
                break;
            default:
                break;
        }
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
    }

    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            audioSource.Play();
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private void ContinueDialog()
    {
        if (textDisplay.text == sentences[index])
        {
            pressAnyKey.SetActive(true);
            if (Input.anyKeyDown)
            {
                if (index < sentences.Length - 1)
                {
                    index++;
                    textDisplay.text = "";
                    StartCoroutine(Type());
                }
                else
                {
                    textDisplay.text = "";

                    if (openObject != null)
                    {
                        openObject.SetActive(true);
                    }
                    image.SetActive(false);
                    FindObjectOfType<PlayerMovement>().enabled = true;
                    FindObjectOfType<GuiltyValue>().inDialog = false;
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            pressAnyKey.SetActive(false);
        }
    }
}
