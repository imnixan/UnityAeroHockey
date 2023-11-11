using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Striker : MonoBehaviour
{
    [SerializeField]
    protected Vector2 spawnPos;

    [SerializeField]
    private bool botField;

    protected RectTransform rt;
    protected Rigidbody2D rb;
    protected float radius;
    protected Vector2 worldPosition,
        canvasPosition;
    protected float maxY;
    protected bool fingerOnAWrongSide;
    protected bool move;

    protected void Start()
    {
        move = true;
        rb = GetComponent<Rigidbody2D>();
        rt = GetComponent<RectTransform>();
        radius = rt.sizeDelta.x * rt.localScale.x / 2;
    }

    void OnEnable()
    {
        Puck.Goal += OnGoalsHappens;
        GameController.StartRound += StartRound;
        GameGoalsCounter.GameEnd += GameEnd;
    }

    protected void OnDisable()
    {
        Puck.Goal -= OnGoalsHappens;
        GameController.StartRound -= StartRound;
        GameGoalsCounter.GameEnd -= GameEnd;
        Debug.Log(gameObject.name + " unsubscribed");
    }

    protected void InitalLine()
    {
        rt.anchoredPosition = new Vector2(0, -radius);
        maxY = rt.position.y;
        rt.anchoredPosition = spawnPos;
    }

    public virtual void MoveTo(RectTransform objectToMove)
    {
        if (move)
        {
            worldPosition = objectToMove.position;
            canvasPosition = objectToMove.anchoredPosition;

            if (botField)
            {
                if (worldPosition.y > 0)
                {
                    worldPosition.y = 0;
                }
            }
            else
            {
                if (worldPosition.y < 0)
                {
                    worldPosition.y = 0;
                }
            }
            rb.MovePosition(worldPosition);
        }
    }

    protected void OnGoalsHappens()
    {
        rb.velocity = Vector2.zero;
        rt.anchoredPosition = spawnPos;
        move = false;
    }

    protected void StartRound()
    {
        move = true;
    }

    private void GameEnd(int winner)
    {
        Destroy(gameObject);
    }
}
