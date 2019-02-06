using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovementScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        if(this.gameObject.tag == "MoveLeftButton")
        {
            PlayerScript.instance.MoveThePlayerLeft();
        }
        else if(this.gameObject.tag == "MoveRightButton")
        {
            PlayerScript.instance.MoveThePlayerRight();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PlayerScript.instance.StopMoving();
    }


}
