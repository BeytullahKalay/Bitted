using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DeathChecker : MonoBehaviour
{
    public List<GameObject> keys;

    void Update()
    {
        int deathCount = GetComponent<DeathCounter>().GetDeathCount();

        CheckDeathCounts(deathCount);

        //RestartScene(deathCount);
    }

    //private static void RestartScene(int deathCount)
    //{
    //    if (deathCount > 2)
    //    {
    //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //    }
    //}

    private void CheckDeathCounts(int deathCount)
    {
        foreach (GameObject key in keys)
        {
            if (key != null)
            {
                if (deathCount >= key.GetComponent<Key>().visibleDeathCount)
                {
                    key.SetActive(true);
                }
            }
        }

    }
}
