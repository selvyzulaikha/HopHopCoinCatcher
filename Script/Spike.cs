using UnityEngine;

public class Spike : MonoBehaviour
{
    // Fungsi otomatis Unity saat ada objek masuk ke area Is Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Memeriksa apakah objek yang menginjak duri adalah Player
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player terkena duri. Mengurangi kesehatan Player.");

            // Memanggil fungsi dari GameManager agar angka nyawa berkurang DAN gambar hati ikut hilang
            if (GameManager.instance != null)
            {
                GameManager.instance.KurangNyawa();
            }
        }
    }
}