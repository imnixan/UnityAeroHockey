using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerStriker : Striker
{
   
    private void Start()
    {
        base.Start();
        spawnPos = new Vector2(0, -400);
        InitalLine();
    }

   

}
