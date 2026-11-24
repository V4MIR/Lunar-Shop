using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourcesChange : MonoBehaviour
{
    public int money = 0;

    public TMP_Text textMoney;

    // Start is called before the first frame update
    void Start()
    {
        textMoney.text = money.ToString();
    }

    public int GetMoney()
    {
        return money;
    }

    public void ChangeMoney(int a)
    {
        money += a;
        textMoney.text = $"{money}";
    }
}
