using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldBorder : MonoBehaviour
{
   private void Start()
    {
        GetComponent<BoxCollider2D>().size = GetComponent<RectTransform>().rect.size;
    }
}

