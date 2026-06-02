using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI Elements (Otomatis Dicari Saat Pindah/Reload)")]
    public TextMeshProUGUI textSkor;
    public Image[] ikonHati;

    [Header("Game Stats")]
    public int skorKoin = 0;
    public int nyawa = 3;

    [Header("Audio System Settings")]
    public AudioSource bgmSource; // Komponen Audio Source buat muter BGM
    public AudioClip sfxKoin;     // Slot file suara pas dapet koin
    public AudioClip sfxLuka;     // Slot file suara pas kena musuh/duri

    private bool isMuted = false; // Status suara game saat ini (Mute/Unmute)

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Daftarkan fungsi ke sistem perpindahan scene Unity
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        // Bersihkan pendaftaran fungsi saat objek dihancurkan agar tidak terjadi leak memory
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Fungsi sakti Unity 6: Otomatis berjalan SETIAP KALI pindah level atau reload level!
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Biar efisien, sistem hanya nyari UI hati & koin kalau bukan di scene MainMenu
        if (scene.name != "MainMenu")
        {
            CariKomponenUIOtomatis();
            UpdateUI();
        }
    }

    private void CariKomponenUIOtomatis()
    {
        // Mencari teks skor secara otomatis di scene yang baru dimuat
        textSkor = Object.FindFirstObjectByType<TextMeshProUGUI>();

        // Mencari semua gambar hati di dalam Canvas baru secara otomatis
        Canvas canvasBaru = Object.FindFirstObjectByType<Canvas>();
        if (canvasBaru != null)
        {
            Image[] semuaImage = canvasBaru.GetComponentsInChildren<Image>();
            System.Collections.Generic.List<Image> listHati = new System.Collections.Generic.List<Image>();

            foreach (Image img in semuaImage)
            {
                if (img.gameObject.name.ToLower().Contains("hati") || img.gameObject.name.ToLower().Contains("heart") || img.gameObject.name.ToLower().Contains("ikon"))
                {
                    listHati.Add(img);
                }
            }

            if (listHati.Count > 0)
            {
                ikonHati = listHati.ToArray();
            }
        }
    }

    // ================= DITAMBAHKAN: SISTEM LOGIKA AUDIO GACOR =================

    public void TambahKoin(int jumlah)
    {
        skorKoin += jumlah;
        UpdateUI();
        PutarSFX(sfxKoin); // Setiap dapet koin, otomatis bunyi "Tring!"
    }

    public void KurangNyawa()
    {
        nyawa--;
        UpdateUI();
        PutarSFX(sfxLuka); // Setiap ketabrak musuh/duri, otomatis bunyi "Dugh!"

        if (nyawa <= 0)
        {
            GameOver();
        }
    }

    // Fungsi pembantu untuk muter efek suara pendek sekali lewat tanpa motong musik utama
    public void PutarSFX(AudioClip clip)
    {
        if (clip != null && bgmSource != null)
        {
            bgmSource.PlayOneShot(clip);
        }
    }

    // Saklar On/Off suara yang bakal di-klik dari tombol speaker Main Menu
    public bool ToggleMute()
    {
        isMuted = !isMuted; // Membalikkan status (Kalau true jadi false, kalau false jadi true)

        if (bgmSource != null)
        {
            bgmSource.mute = isMuted; // Set sistem mute bawaan Unity Audio Source
        }

        return isMuted; // Mengirim status terbaru ke MainMenuManager buat ganti gambar icon
    }

    // Fungsi biar script Main Menu bisa tahu keadaan suara terakhir pas baru masuk game
    public bool GetMuteStatus()
    {
        return isMuted;
    }

    // =========================================================================

    public void UpdateUI()
    {
        if (textSkor == null || ikonHati == null || ikonHati.Length == 0)
        {
            CariKomponenUIOtomatis();
        }

        if (textSkor != null) textSkor.text = skorKoin.ToString();

        if (ikonHati != null)
        {
            for (int i = 0; i < ikonHati.Length; i++)
            {
                if (ikonHati[i] != null)
                {
                    ikonHati[i].enabled = (i < nyawa);
                }
            }
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over! Nyawa Habis total.");
        nyawa = 3;
        skorKoin = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}