using UnityEngine;

public class DeathCounter : MonoBehaviour
{
    private int deathCount;

    private void Start()
    {
        deathCount = 0;
    }


    public void IncreaseDeathCount()
    {
        deathCount++;
    }

    public int GetDeathCount()
    {
        return deathCount;
    }
}
