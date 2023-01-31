using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.EventSystems;

public class Test : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //Output the name of the GameObject that is being clicked
        if (Mouse.current.leftButton.isPressed)
        {
            Debug.Log(name + "Game Object Click in Progress");
        }
    }
}
