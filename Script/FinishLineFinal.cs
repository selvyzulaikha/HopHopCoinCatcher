using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLineFinal : MonoBehaviour
{
    [Header("UI Settings")]
    // Tempat memasukkan objek PanelWin yang tadi dimatikan
    public GameObject panelKemenangan;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Memeriksa apakah yang menyentuh bendera adalah Player
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player memenangkan game! Menampilkan UI Kemenangan.");

            if (panelKemenangan != null)
            {
                // Mengaktifkan kembali panel You Win agar muncul di layar
                panelKemenangan.SetActive(true);

                // Opsional: Menghentikan pergerakan game (Freeze waktu) agar player gak bisa gerak lagi
                Time.timeScale = 0f;
            }
        }
    }

    // Fungsi sat-set yang bakal dipanggil saat tombol Main Menu diklik
    public void KembaliKeMainMenu()
    {
        // Kembalikan waktu game menjadi normal sebelum pindah scene
        Time.timeScale = 1f;

        // Reset statistik di GameManager agar koin dan nyawa kembali normal dari awal
        if (GameManager.instance != null)
        {
            GameManager.instance.nyawa = 3;
            GameManager.instance.skorKoin = 0;
        }

        // Memuat scene Main Menu (Pastikan nama scene di tanda petik ini sesuai dengan nama file Scene Main Menu kamu!)
        SceneManager.LoadScene("MainMenu");
    }
}