using UnityEngine;

public class HiddenWall : MonoBehaviour
{
    public GameObject keyWall;

    void Start()
    {
        keyWall.gameObject.tag = "Untagged";
        keyWall.gameObject.layer = 0; // 0 = Default Layer
    }
}
