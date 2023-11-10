using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiBehaivor : MonoBehaviour
{   
    protected AiController ac;
    protected RectTransform pointer;
    protected RectTransform puck;
    protected RectTransform striker;
    protected void Start()
    {
        striker = GetComponent<RectTransform>();
        ac = GetComponent<AiController>();
        puck = ac.GetPuck();
        pointer = ac.GetPointer();
    }
}
