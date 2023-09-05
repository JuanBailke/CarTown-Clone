using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager inst;

    [Header("Canvas HUD")]
    public GameObject panelShop;
    public GameObject panelCars;
    public GameObject panelItems;
    public GameObject main;
    public Text coinTxt;
    public Text levelTxt;
    public Transform garage;
    public GameObject btnShop;

    [Header("Canvas Loja")]
    public Text name;
    public Text value;
    public Button buttonBuy;
    public GameObject panel;
    private GameObject copy;

    //Variáveis de Gameplay
    private int coin, totalCoin;
    private GameObject copyCar;
    public bool create;

    // Start is called before the first frame update
    void Start()
    {
        panelShop.SetActive(false);
        panelCars.SetActive(false);
        panelItems.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getCoins(int qtd)
    {
        coin += qtd;
    }

    public void CallShop(Carro car)
    {
        name.text = car.carNome;
        value.text = "Valor: " + car.carValue;
        copyCar = car.carObject;
    }

    public void EnableBuy()
    {
        buttonBuy.enabled = true;
    }

    public void CreateObjectInScene()
    {
        GameObject objInst = Instantiate(copyCar, new Vector3(-20, 0.1f, -20), Quaternion.Euler(0, 270, 0), garage);
        create = true;
        panelShop.SetActive(false);
        main.SetActive(true);

        //Camera.main.ScreenToViewportPoint(Input.mousePosition)
    }

    public void OpenShop()
    {
        panelShop.SetActive(true);
        main.SetActive(false);
        
    }

    public void CloseShop()
    {
        panelShop.SetActive(false);
        main.SetActive(true);
    }

    public void OpenShopCars()
    {
        panelCars.SetActive(true);
        buttonBuy.enabled = false;
    }

    public void CloseShopCars()
    {
        panelCars.SetActive(false);
    }
}
