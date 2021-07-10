using UnityEngine;

public class CameraShake : MonoBehaviour
{

    private float shakeAmount;

    public void Shake(float amt, float lenght)
    {
        shakeAmount = amt;
        InvokeRepeating("BeginShake", 0, 0.1f);
        Invoke("StopShake", lenght);
    }

    private void BeginShake()
    {
        if (shakeAmount > 0)
        {
            Vector3 camPos = transform.position;

            float offsetX = Random.value * shakeAmount * 2 - shakeAmount;
            float offsetY = Random.value * shakeAmount * 2 - shakeAmount;

            camPos.x += offsetX;
            camPos.y += offsetY;

            transform.position = camPos;
        }
    }

    private void StopShake()
    {
        CancelInvoke("BeginShake");
        transform.localPosition = Vector3.zero;
    }

}
