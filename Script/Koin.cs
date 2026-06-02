using UnityEngine;

public class Koin : MonoBehaviour
{
    // Fungsi bawaan Unity yang otomatis aktif saat ada objek lain menembus koin ini
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Mengecek apakah objek yang menabrak koin memiliki Tag "Player"
        if (collision.CompareTag("Player"))
        {
            // Memanggil GameManager untuk menambah skor sebanyak 1
            if (GameManager.instance != null)
            {
                GameManager.instance.TambahKoin(1);
            }

            // Menghancurkan/menghilangkan objek koin ini dari game
            Destroy(gameObject);
        }
    }
}