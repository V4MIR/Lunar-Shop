using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderDone : MonoBehaviour
{
    public ResourcesChange scriptResourcesChange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PressButton()
    {
        if (scriptResourcesChange.GetTea() >= 1)
        {
            scriptResourcesChange.ChangeTea(-1);
            scriptResourcesChange.ChangeMoney(15);
        }
    }
}
