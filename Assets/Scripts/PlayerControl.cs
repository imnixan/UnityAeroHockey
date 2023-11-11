using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    RectTransform mousePointer;
    private Striker striker;
    public bool strikerGrabed;
    private Camera camera;

    private void Start()
    {
        Puck.Goal += OnGoalsHappens;

        camera = Camera.main;
        striker = GetComponent<Striker>();
    }

    void OnDisable()
    {
        Puck.Goal -= OnGoalsHappens;
    }

    //private void Update()
    //{
    //    if (strikerGrabed)
    //    {
    //        mousePointer.position = camera.ScreenToWorldPoint(Input.mousePosition);
    //        striker.MoveTo(mousePointer);
    //    }
    //}

    public void MoveTo(Vector3 position)
    {
        mousePointer.position = position;
        striker.MoveTo(mousePointer);
    }

    //private void OnMouseDown()
    //{
    //    strikerGrabed = true;
    //}

    //private void OnMouseUp()
    //{
    //    strikerGrabed = false;
    //}

    private void OnGoalsHappens()
    {
        strikerGrabed = false;
    }

    ///76.8 on canvas -0.576 on world;
}
