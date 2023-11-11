using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerStriker : Striker
{
    private float speed = 3;

    void Start()
    {
        base.Start();
        InitalLine();
        AiController.PuckChangeSide += OnPuckStateChanged;
        Physics2D.IgnoreLayerCollision(8, 9);
    }

    void OnDisable()
    {
        base.OnDisable();
        AiController.PuckChangeSide -= OnPuckStateChanged;
    }

    public override void MoveTo(RectTransform objectToMove)
    {
        if (move)
        {
            rb.velocity = (objectToMove.position - rt.position) * speed;
        }
    }

    private void OnPuckStateChanged(bool aiSide)
    {
        if (aiSide)
        {
            speed = 8;
        }
        else
        {
            speed = 2;
        }
    }
}
