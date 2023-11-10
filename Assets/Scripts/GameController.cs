using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goalText;
    [SerializeField] private TextMeshProUGUI endText;
    [SerializeField] private GameObject endCanvas;
    public static event UnityAction StartRound;
    private RectTransform goalPos;

    void Awake()
    {
        Application.targetFrameRate = 120;
        Screen.orientation = ScreenOrientation.Portrait;
    }

    void Start()
    {
        Puck.PlayerScoredGoal += OnGoalsHappens;
        GameGoalsCounter.GameEnd += GameEnd;

        endCanvas.SetActive(false);
        goalPos = goalText.GetComponent<RectTransform>();
    }

    void OnDisable()
    {
        GameGoalsCounter.GameEnd -= GameEnd;
        Puck.PlayerScoredGoal -= OnGoalsHappens;
    }

    private void OnGoalsHappens(int playerId)
    {
        StartCoroutine(GoalMessage(playerId));
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Exit();
        }
    }

    IEnumerator GoalMessage(int playerId)
    {
        goalText.color = playerId == 0? Color.red : Color.blue;
        goalPos.anchoredPosition = new Vector2(1000, 0);
        while(goalPos.anchoredPosition.x > -1000)
        {
            goalPos.anchoredPosition = Vector2.MoveTowards(goalPos.anchoredPosition, new Vector2(-1000, 0), 20f);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        StartRound?.Invoke();
    }

    private void GameEnd(int winner)
    {
        endCanvas.SetActive(true);
        if(winner == 1)
        {
            endText.text = "Blue Wins";
            endText.color = Color.blue;
        }
        else
        {
            endText.text = "Red Wins";
            endText.color = Color.red;
        }

    }


    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        SceneManager.LoadScene("AirHockey");
    }
}
