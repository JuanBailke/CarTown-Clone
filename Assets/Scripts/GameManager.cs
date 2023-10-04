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
    public Text nameCar;
    public Text valueCar;
    public Text nameItem;
    public Text valueItem;
    public Button buttonCarBuy;
    public Button buttonItemBuy;
    public GameObject panel;
    private Transform objInst;

    //Variáveis de Gameplay
    private int coin, valueTrade;
    private Transform copyObject;
    public bool create;

    // Start is called before the first frame update
    void Start()
    {
        panelShop.SetActive(false);
        panelCars.SetActive(false);
        panelItems.SetActive(false);

        coin = int.Parse(coinTxt.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setCoins(int qtd)
    {
        coin += qtd;
    }

    public void CallShop(Carro car)
    {
        nameCar.text = car.carNome;
        valueCar.text = "Valor: " + car.carValue;
        copyObject = car.carObject;
        if (coin >= car.carValue)
        {
            buttonCarBuy.enabled = true;
            valueTrade = car.carValue;
        }
        else
        {
            buttonCarBuy.enabled = false;
        }
    }

    public void CreateObjectInScene()
    {
        objInst = Instantiate(copyObject, new Vector3(-20, 0.1f, -20), Quaternion.Euler(0, 0, 0), garage);
        create = true;
        panelShop.SetActive(false);
        panelCars.SetActive(false);
        main.SetActive(true);
        coin -= valueTrade;
        coinTxt.text = coin.ToString();
        nameCar.text = "Selecione um item";
        valueCar.text = "";
    }

    public void CallShop(Itens item)
    {
        nameItem.text = item.itemNome;
        valueItem.text = "Valor: " + item.itemValue;
        copyObject = item.itemObject;
        if (coin >= item.itemValue)
        {
            buttonItemBuy.enabled = true;
            valueTrade = item.itemValue;
        }
        else
        {
            buttonItemBuy.enabled = false;
        }
    }

    public void CreateItemInScene()
    {
        objInst = Instantiate(copyObject, new Vector3(-20, 0.1f, -20), Quaternion.Euler(0, 0, 0), garage);
        create = true;
        panelShop.SetActive(false);
        panelItems.SetActive(false);
        main.SetActive(true);
        coin -= valueTrade;
        coinTxt.text = coin.ToString();
        nameItem.text = "Selecione um item";
        valueItem.text = "";
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
        buttonCarBuy.enabled = false;
    }

    public void CloseShopCars()
    {
        panelCars.SetActive(false);
        nameCar.text = "Selecione um item";
        valueCar.text = "";
    }

    public void OpenShopItems()
    {
        panelItems.SetActive(true);
        buttonItemBuy.enabled = false;
    }

    public void CloseShopItems()
    {
        panelItems.SetActive(false);
        nameItem.text = "Selecione um item";
        valueItem.text = "";
    }
}
