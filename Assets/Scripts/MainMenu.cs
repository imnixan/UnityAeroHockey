using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Image soundIcon,
        vibroIcon;

    [SerializeField]
    private Sprite[] sound,
        vibro;

    [SerializeField]
    private TextMeshProUGUI appname;

    void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Application.targetFrameRate = 300;
    }

    void Start()
    {
        soundIcon.sprite = sound[PlayerPrefs.GetInt("SoundEffects", 1)];
        vibroIcon.sprite = vibro[PlayerPrefs.GetInt("VibroEffects", 1)];
        appname.text = Application.productName;
    }

    public void ChangeSound()
    {
        PlayerPrefs.SetInt("SoundEffects", PlayerPrefs.GetInt("SoundEffects", 1) == 1 ? 0 : 1);
        PlayerPrefs.Save();
        soundIcon.sprite = sound[PlayerPrefs.GetInt("SoundEffects", 1)];
    }

    public void ChangeVibro()
    {
        PlayerPrefs.SetInt("VibroEffects", PlayerPrefs.GetInt("VibroEffects", 1) == 1 ? 0 : 1);
        PlayerPrefs.Save();
        vibroIcon.sprite = vibro[PlayerPrefs.GetInt("VibroEffects", 1)];
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("ChooseMenu");
    }
}
