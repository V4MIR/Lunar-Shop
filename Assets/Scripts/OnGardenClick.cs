using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnGardenClick : MonoBehaviour
{
    public UnityEvent onClickEvent;

    private void OnMouseDown()
    {
        onClickEvent.Invoke();
    }
}
