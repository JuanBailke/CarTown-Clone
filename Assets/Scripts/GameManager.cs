using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    //public static Gamemanager inst;

    [Header("CanvasHUD")]
    public GameObject panelShop;
    public Text coinTxt;
    public Text levelTxt;

    public GameObject btnShop;

    //Variáveis de Gameplay
    private double coin, totalCoin;


    // Start is called before the first frame update
    void Start()
    {
        panelShop.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getCoins(double qtd)
    {
        coin += qtd;
    }
}
