using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class GameGoalsCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] playersGoalsCounter;
    [SerializeField] int[] playersGoals = new int[2];
    public static event UnityAction <int> GameEnd;

    private void Start()
    {
        Puck.PlayerScoredGoal += OnGoalsHappen;
    }

    void OnDisable()
    {
       Puck.PlayerScoredGoal -= OnGoalsHappen;
    }

    private void OnGoalsHappen(int playerId)
    {
        playersGoals[playerId] ++;
        if(playersGoals[playerId] == 5)
        {
            GameEnd?.Invoke(playerId);
        }
        playersGoalsCounter[playerId].text = playersGoals[playerId].ToString(); 
    }

}
