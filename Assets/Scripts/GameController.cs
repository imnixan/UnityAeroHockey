using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI endText,
        tigerScore,
        bullScore;

    [SerializeField]
    private Image winnerPlate;

    [SerializeField]
    private GameObject endCanvas;

    [SerializeField]
    private Image character;

    [SerializeField]
    private Sprite bull,
        tiger;

    [SerializeField]
    private ParticleSystem[] confetti;

    [SerializeField]
    private ParticleSystem[] shine;

    public static event UnityAction StartRound;

    [SerializeField]
    private RectTransform goalPos;

    private GameGoalsCounter gameGoalsCounter;

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
        gameGoalsCounter = FindAnyObjectByType<GameGoalsCounter>();
    }

    void OnDisable()
    {
        GameGoalsCounter.GameEnd -= GameEnd;
        Puck.PlayerScoredGoal -= OnGoalsHappens;
    }

    private void OnGoalsHappens(int playerId)
    {
        confetti[playerId].Play();
        Sequence goalShow = DOTween.Sequence();
        goalShow
            .PrependCallback(() =>
            {
                character.sprite = playerId == 0 ? tiger : bull;
                goalPos.anchoredPosition = new Vector2(1000, 0);
            })
            .Append(goalPos.DOAnchorPosX(0, 0.7f))
            .AppendInterval(0.8f)
            .Append(goalPos.DOAnchorPosX(-1000, 0.5f))
            .AppendCallback(() =>
            {
                goalPos.anchoredPosition = new Vector2(1000, 0);
                StartRound?.Invoke();
            });
        goalShow.Restart();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Exit();
        }
    }

    private void GameEnd(int winner)
    {
        endCanvas.SetActive(true);
        bullScore.text = gameGoalsCounter.playersGoals[1].ToString();
        tigerScore.text = gameGoalsCounter.playersGoals[0].ToString();
        shine[winner].Play();
        if (winner == 1)
        {
            endText.text = "Bulls Wins";
            winnerPlate.color = Color.blue;
        }
        else
        {
            endText.text = "Tigers Wins";
            winnerPlate.color = Color.red;
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
