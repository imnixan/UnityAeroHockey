using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(ComputerStriker))]
[RequireComponent(typeof(Defend))]
[RequireComponent(typeof(Attack))]
public class AiController : MonoBehaviour
{
    public static event UnityAction <bool> PuckChangeSide;

    [SerializeField] private RectTransform pointer;
    [SerializeField] private RectTransform puck;

    private ComputerStriker striker;
    private Defend defend;
    private Attack attack;
    private bool _puckOnComputerSide = false;
    
    private bool PuckOnComputerSide
    {
        get
        {
            return _puckOnComputerSide;
        }
        
        set
        {
            if(_puckOnComputerSide != value)
            {
                _puckOnComputerSide = value;
                PuckChangeSide?.Invoke(_puckOnComputerSide);
                ChangeState();
            }
        }
    }

    private void ChangeState()
    {
        defend.enabled = !PuckOnComputerSide;
        attack.enabled = PuckOnComputerSide;
    }
    
    
    void Start()
    {
        striker = GetComponent<ComputerStriker>();
        defend = GetComponent<Defend>();
        attack = GetComponent<Attack>();
        attack.enabled = false;
    }

    void LateUpdate()
    {
            PuckOnComputerSide = puck.position.y > 0.1f;
            striker.MoveTo(pointer);
    }

    public RectTransform GetPointer()
    {
        return pointer;
    }

    public RectTransform GetPuck()
    {
        return puck;
    }


}
