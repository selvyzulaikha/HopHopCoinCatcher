using UnityEngine;

public class KameraFollow : MonoBehaviour
{
    public Transform target; // Diisi objek Player
    public float smoothing = 5f; // Kecepatan smooth kamera
    public Vector3 offset = new Vector3(0f, 1.8f, -10f); // Y: 1.8f bikin kamera agak naik ke atas kepala player

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 targetCamPos = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.fixedDeltaTime);
        }
    }
}