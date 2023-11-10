using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : AiBehaivor
{
    private bool onAttackPosition;

    void OnEnabled()
    {
        onAttackPosition = striker.position.y >= puck.position.y + 1;
    }
    void Update()
    {
        if(onAttackPosition)
        {
            pointer.position = puck.position;
        }
        else
        {
            pointer.position = new Vector2(puck.position.x + Random.Range(-0.5f, 0.5f), puck.position.y + 1.5f);
            onAttackPosition = striker.position.y >= puck.position.y + 1;
        }
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Puck"))
        {
            onAttackPosition = false;
            other.gameObject.GetComponent<Puck>().Push();
        }
    }



                    
            
        


}
