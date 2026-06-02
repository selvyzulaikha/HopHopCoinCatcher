using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [Header("Level Settings")]
    // Kita ganti pake public biasa biar Unity 6 dijamin langsung memunculkan kolomnya
    public string namaLevelBerikutnya;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Memeriksa apakah objek yang menyentuh bendera memiliki Tag Player
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player telah mencapai garis finish. Memuat level berikutnya...");
            MemuatLevelBerikutnya();
        }
    }

    private void MemuatLevelBerikutnya()
    {
        // Memeriksa apakah nama level berikutnya sudah diisi di Inspector
        if (!string.IsNullOrEmpty(namaLevelBerikutnya))
        {
            SceneManager.LoadScene(namaLevelBerikutnya);
        }
        else
        {
            Debug.LogWarning("Nama level berikutnya belum ditentukan di Inspector!");
        }
    }
}