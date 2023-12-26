using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Jogador : MonoBehaviour
{
    [Header("Vari�veis de jogador")]
    [SerializeField] private Text coinTxt;
    [SerializeField] private Text levelTxt;
    [SerializeField] private Image levelImg;

    private int money, level = 1, valueTrade;
    private float xp;

    public int Money { get => money; }
    public int Level { get => level; }

    void Start()
    {
        money = Int32.Parse(coinTxt.text);
        levelTxt.text = "Level " + level.ToString();
    }

    void Update()
    {

    }

    #region M�todos de Dinheiro

    public void Purchase(int value)
    {
        valueTrade = value;
        coinTxt.text = (money - valueTrade).ToString() + " (-" + valueTrade.ToString() + ")";
    }


    public void Convert(bool confirm)
    {
        if (confirm == true)
        {
            money -= valueTrade;
            coinTxt.text = money.ToString();
        }
        else
            coinTxt.text = money.ToString();
    }

    public void Payment(int value)
    {
        money += value;
        coinTxt.text = money.ToString();
    }

    #endregion

    #region M�todos de N�vel

    public void RecebeXP(float bonus)
    {
        xp += (bonus / level);
        if (xp >= 1)
        {
            level++;
            levelTxt.text = "Level " + level.ToString();
            xp = 0f;
        }
        levelImg.fillAmount = xp;
    }

    #endregion
}
