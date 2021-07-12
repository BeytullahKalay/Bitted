using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFinishCollider : MonoBehaviour
{
    public GameObject gameEndCanvas;

    public GameObject text;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            gameEndCanvas.SetActive(true);
            StartCoroutine(ShowText2SecondsLater());
        }
    }

    private IEnumerator ShowText2SecondsLater()
    {
        yield return new WaitForSeconds(2f);
        text.SetActive(true);

        StartCoroutine(Quit3SecondsLater());
    }

    private IEnumerator Quit3SecondsLater()
    {
        yield return new WaitForSeconds(3f);
        Application.Quit();
    }
}
