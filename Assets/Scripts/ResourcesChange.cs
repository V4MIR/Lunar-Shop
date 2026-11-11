using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourcesChange : MonoBehaviour
{
    private int money = 0;
    private int Grass = 0;
    private int tea = 0;

    public TMP_Text textMoney;
    public TMP_Text textGrass;
    public TMP_Text textTea;

    // Start is called before the first frame update
    void Start()
    {
        textMoney.text = "Монеты\n0";
        textGrass.text = "Мятолист\n0";
        textTea.text = "Чай\n0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetMoney()
    {
        return money;
    }

    public void ChangeMoney(int a)
    {
        money += a;
        textMoney.text = $"Монеты\n{money}";
    }

    public int GetGrass()
    {
        return Grass;
    }

    public void ChangeGrass(int a)
    {
        Grass += a;
        textGrass.text = $"Мятолист\n{Grass}";
    }

    public int GetTea()
    {
        return tea;
    }

    public void ChangeTea(int a)
    {
        tea += a;
        textTea.text = $"Чай\n{tea}";
    }
}
