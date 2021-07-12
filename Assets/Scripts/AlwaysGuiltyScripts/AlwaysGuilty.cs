using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysGuilty : MonoBehaviour
{
    private void Update()
    {
        if (FindObjectOfType<GuiltyValue>() != null)
        {
            if (!FindObjectOfType<GuiltyValue>().isGuilty)
            {
                FindObjectOfType<GuiltyValue>().isGuilty = true;
            }
        }
    }
}
