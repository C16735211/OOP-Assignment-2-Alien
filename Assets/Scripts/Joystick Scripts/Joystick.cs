using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {

    public void OnPointerDown(PointerEventData data) {
        if (gameObject.name == "Left") {
            
        } else {
           
        }
    }

    public void OnPointerUp(PointerEventData data) {
        if (gameObject.name == "Left")
        {
            Debug.Log("Released The Left Button");
        }
        else
        {
            Debug.Log("Released The Right Button");
        }
    }
} // Joystick
