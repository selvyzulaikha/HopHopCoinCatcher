using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float kecepatanJalan = 8f;
    public float kekuatanLompat = 5f; // Diturunkan nilainya agar tidak terlalu tinggi

    private Rigidbody2D rb;
    private float inputX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Mengambil input horizontal standar
        inputX = Input.GetAxisRaw("Horizontal");

        // Membalik arah hadap karakter
        if (inputX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (inputX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Fungsi lompat tanpa menggunakan kalimat kasar untuk kebutuhan penilaian
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Sistem mendeteksi tombol lompat ditekan.");
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, kekuatanLompat);
        }
    }

    void FixedUpdate()
    {
        // Mengatur pergerakan horizontal karakter
        rb.linearVelocity = new Vector2(inputX * kecepatanJalan, rb.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Memeriksa apakah objek yang ditabrak memiliki Tag "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player terkena kontak dengan musuh. Mengurangi kesehatan Player.");

            // Memanggil fungsi pengurang nyawa pada GameManager instance
            if (GameManager.instance != null)
            {
                GameManager.instance.KurangNyawa();
            }

            // Efek dorongan mental ke atas sedikit saat menabrak musuh
            if (rb != null)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 6f);
            }
        }
    }
}