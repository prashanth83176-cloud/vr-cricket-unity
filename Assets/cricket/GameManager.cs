using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0;
    public bool isOut = false;

    public GameObject outText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI runPopupText;
    public GameObject restartButton;

    // 🎧 AUDIO
    public AudioSource bgAudio;   // background
    public AudioSource sfxAudio;  // 4, 6, OUT

    public AudioClip fourSound;
    public AudioClip sixSound;
    public AudioClip outSound;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateScoreUI();

        if (outText != null) outText.SetActive(false);
        if (runPopupText != null) runPopupText.gameObject.SetActive(false);
        if (restartButton != null) restartButton.SetActive(false);

        // 🎵 BACKGROUND SOUND
        if (bgAudio != null)
        {
            bgAudio.loop = true;
            bgAudio.volume = 0.25f;
            bgAudio.Play();
        }
    }

    // 🏏 ADD RUNS
    public void AddRuns(int runs)
    {
        if (isOut) return;

        score += runs;
        UpdateScoreUI();
        ShowRunPopup(runs);

        // 🔊 PLAY 4 / 6 SOUND
        if (sfxAudio != null)
        {
            if (runs == 4 && fourSound != null)
                sfxAudio.PlayOneShot(fourSound);

            else if (runs == 6 && sixSound != null)
                sfxAudio.PlayOneShot(sixSound);
        }
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    void ShowRunPopup(int runs)
    {
        if (runPopupText == null) return;

        runPopupText.gameObject.SetActive(true);
        runPopupText.text = runs.ToString();

        runPopupText.color = (runs == 4) ? Color.green : Color.red;

        Transform cam = Camera.main.transform;

        runPopupText.transform.position =
            cam.position + cam.forward * 2f;

        runPopupText.transform.LookAt(cam);
        runPopupText.transform.Rotate(0, 180, 0);

        Invoke(nameof(HideRunPopup), 2f);
    }

    void HideRunPopup()
    {
        if (runPopupText != null)
            runPopupText.gameObject.SetActive(false);
    }

    // 🟥 OUT
    public void Out()
    {
        if (isOut) return;

        isOut = true;

        if (outText != null)
        {
            outText.SetActive(true);

            outText.transform.position =
                Camera.main.transform.position +
                Camera.main.transform.forward * 2f;

            outText.transform.LookAt(Camera.main.transform);
            outText.transform.Rotate(0, 180, 0);
        }

        // 🔊 OUT SOUND
        if (sfxAudio != null && outSound != null)
        {
            sfxAudio.PlayOneShot(outSound);
        }

        if (restartButton != null)
        {
            restartButton.SetActive(true);

            Transform cam = Camera.main.transform;

            restartButton.transform.LookAt(cam);
            restartButton.transform.eulerAngles =
                new Vector3(0, cam.eulerAngles.y, 0);
        }

        Time.timeScale = 0.2f;
    }

    // 🔁 RESTART
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}