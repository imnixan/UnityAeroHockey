using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class Tutor : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textTutor;

    public void Back()
    {
        SceneManager.LoadScene("ChooseMenu");
    }

    public void ScoreGoalsText()
    {
        textTutor.text = "Score goals!";
    }

    public void DefendGatesText()
    {
        textTutor.text = "Defend your gates!";
    }
}
