using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Settings")]
    public float kecepatanJalan = 3f;
    public float waktuPutarArah = 2f; // Setiap berapa detik musuh balik badan

    private Rigidbody2D rb;
    private bool menghadapKanan = true;
    private float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = waktuPutarArah;
    }

    void Update()
    {
        // Hitung mundur waktu untuk berbalik arah
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            BalikBadan();
            timer = waktuPutarArah;
        }
    }

    void FixedUpdate()
    {
        // Mengatur arah jalan musuh
        float kecepatanLokal = menghadapKanan ? kecepatanJalan : -kecepatanJalan;
        rb.linearVelocity = new Vector2(kecepatanLokal, rb.linearVelocity.y);
    }

    void BalikBadan()
    {
        menghadapKanan = !menghadapKanan;

        // Membalik arah grafik sprite musuh
        Vector3 skalaLokal = transform.localScale;
        skalaLokal.x *= -1;
        transform.localScale = skalaLokal;
    }
}