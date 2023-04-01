using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] Canvas pauseUI;
    [SerializeField] Canvas gameOverUI;

    [SerializeField] AudioSource musicAudioSource;
    [SerializeField] AudioSource effectsAudioSource;

    [SerializeField] AudioClip clickEffect;

    [SerializeField] Slider musicSlider;
    [SerializeField] Slider effectsSlider;

    void Awake()
    {
        Time.timeScale = 1;

        if (pauseUI != null)
        {
            pauseUI.gameObject.SetActive(false);
        }

        if (gameOverUI != null)
        {
            gameOverUI.gameObject.SetActive(false);
        }

        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        effectsSlider.value = PlayerPrefs.GetFloat("EffectsVolume", 0.5f);

        musicAudioSource.volume = musicSlider.value;
        effectsAudioSource.volume = effectsSlider.value;
    }

    public void OnClickPauseButton()
    {
        Time.timeScale = 0;
        pauseUI.gameObject.SetActive(true);
    }

    public void OnGameOver()
    {
        Time.timeScale = 0;
        gameOverUI.gameObject.SetActive(true);
    }

    public void OnClickPlayButton()
    {
        Time.timeScale = 1;
        pauseUI.gameObject.SetActive(false);
    }

    public void OnClickTryAgainButton()
    {
        SceneManager.LoadScene("InGameScene");
    }

    public void OnClickGiveUpButton()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void OnMusicValueChange()
    {
        musicAudioSource.volume = musicSlider.value;
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }

    public void OnEffectsValueChange()
    {
        effectsAudioSource.volume = effectsSlider.value;
        PlayerPrefs.SetFloat("EffectsVolume", effectsSlider.value);
    }

    public void OnClickExitGameButton()
    {
        Application.Quit();
    }

    public void OnClickSound()
    {
        effectsAudioSource.PlayOneShot(clickEffect);
    }
}
