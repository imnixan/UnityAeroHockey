using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private Image soundStriker;
    [SerializeField] private Image vibroStriker;
    private Dictionary <string, Image> buttons = new Dictionary<string, Image>();

    void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait; 
    }

   void Start()
   {
        buttons.Add("SoundEffects", soundStriker);
        buttons.Add("VibroEffects", vibroStriker);

        foreach(var button in buttons)
        {
            button.Value.color = PlayerPrefs.GetInt(button.Key,1) == 1? Color.blue : Color.red;
        }
   }


    public void UpdateStriker(string striker)
    {
        PlayerPrefs.SetInt(striker, PlayerPrefs.GetInt(striker, 1) == 1? 0 : 1);
        PlayerPrefs.Save();
        buttons[striker].color = PlayerPrefs.GetInt(striker) == 1? Color.blue : Color.red;
    }





   public void ExitGame()
   {
        Application.Quit();
   }

   public void PlayGame()
   {
        SceneManager.LoadScene("AirHockey");
   }
}
