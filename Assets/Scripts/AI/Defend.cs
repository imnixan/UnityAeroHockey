using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defend : AiBehaivor
{
    
    private float defendLineY;
    void Start()
    {
        base.Start();
        defendLineY = Camera.main.ViewportToWorldPoint(new Vector2(1,1)).y - 1.5f;
        
    }

    void OnEnabled()
    {
        pointer.position = new Vector2(0, defendLineY);
    }
    
    void Update()
    {
        pointer.position = Vector2.MoveTowards(pointer.position, new Vector2(puck.position.x, defendLineY), Time.fixedDeltaTime);
    }
}
