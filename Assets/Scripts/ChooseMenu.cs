using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseMenu : MonoBehaviour
{
    public void PlaySingle()
    {
        SceneManager.LoadScene("AirHockey");
    }

    public void BackMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Multiplayer()
    {
        SceneManager.LoadScene("Multiplayer");
    }
}
