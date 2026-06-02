using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Panels Pop-Up")]
    public GameObject panelTutorial; // Taruh objek PanelTutorial lu di sini
    public GameObject panelLevel;    // Taruh objek PanelLevel lu di sini

    [Header("Speaker Button Image Setup")]
    public Image imageTombolSpeaker; // Komponen Image dari tombol speaker lu
    public Sprite iconSpeakerOn;     // Asset gambar speaker suara nyala
    public Sprite iconSpeakerOff;    // Asset gambar speaker tanda silang (mute)

    private void Start()
    {
        // Pas masuk Main Menu, cek status mute terakhir di GameManager biar gambar icon-nya sinkron
        if (GameManager.instance != null && imageTombolSpeaker != null)
        {
            if (GameManager.instance.GetMuteStatus())
            {
                imageTombolSpeaker.sprite = iconSpeakerOff;
            }
            else
            {
                imageTombolSpeaker.sprite = iconSpeakerOn;
            }
        }
    }

    // --- 1. FUNGSI TOMBOL PLAY (HIJAU BESAR) ---
    public void TombolPlay()
    {
        // Langsung loading masuk ke scene Level 1
        SceneManager.LoadScene("Level_1"); // <-- PASTIIN NAMA SCENE LEVEL 1 LU SAMA PERSIS YA!
    }

    // --- 2. FUNGSI PANEL TUTORIAL ('i') ---
    public void BukaTutorial()
    {
        panelTutorial.SetActive(true);
    }

    public void TutupTutorial()
    {
        panelTutorial.SetActive(false);
    }

    // --- 3. FUNGSI PANEL PILIH LEVEL ---
    public void BukaPilihLevel()
    {
        panelLevel.SetActive(true);
    }

    public void TutupPilihLevel()
    {
        panelLevel.SetActive(false);
    }

    // Fungsi sakti buat 6 tombol angka di dalam grid level selector
    public void PindahKeLevel(int nomorLevel)
    {
        // Reset statistik nyawa & koin dulu biar gak bawa sisa data lama
        if (GameManager.instance != null)
        {
            GameManager.instance.nyawa = 3;
            GameManager.instance.skorKoin = 0;
        }

        // Mengarahkan player ke scene sesuai angka tombol (Contoh: Level_1, Level_2, dst)
        SceneManager.LoadScene("Level_" + nomorLevel);
    }

    // --- 4. FUNGSI TOMBOL SPEAKER (MUTE/UNMUTE) ---
    public void KlikTombolSpeaker()
    {
        if (GameManager.instance != null && imageTombolSpeaker != null)
        {
            // Panggil saklar mute di GameManager, ambil hasilnya (true/false)
            bool sedangMute = GameManager.instance.ToggleMute();

            // Ganti gambar icon tombolnya sat-set biar player tau suaranya aktif atau mati!
            if (sedangMute)
            {
                imageTombolSpeaker.sprite = iconSpeakerOff;
            }
            else
            {
                imageTombolSpeaker.sprite = iconSpeakerOn;
            }
        }
    }
}