using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager inst;

    [Header("Canvas HUD")]
    [SerializeField] private GameObject panelShop;
    [SerializeField] private GameObject panelCars;
    [SerializeField] private GameObject panelItems;
    [SerializeField] private GameObject main;
    [SerializeField] private Transform garage;
    [SerializeField] private GameObject btnShop;

    [Header("Canvas Loja")]
    [SerializeField] private Text nameCar;
    [SerializeField] private Text valueCar;
    [SerializeField] private Text nameItem;
    [SerializeField] private Text valueItem;
    [SerializeField] private Button buttonCarBuy;
    [SerializeField] private Button buttonItemBuy;
    [SerializeField] private GameObject panel;
    [SerializeField] private Jogador jogador;
    private Transform objInst;

    //Variáveis de Gameplay
    private int coin, valueTrade;
    private Transform copyObject;
    public bool create;
    public static bool itemSelecionado = false;

    // Start is called before the first frame update
    void Start()
    {
        panelShop.SetActive(false);
        panelCars.SetActive(false);
        panelItems.SetActive(false);

        //coin = int.Parse(coinTxt.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setCoins(int qtd)
    {
        coin += qtd;
    }

    public void CallShop(Itens item)
    {
        nameCar.text = item.Name;
        valueCar.text = "Valor: " + item.Value;
        copyObject = item.Item;
        if (jogador.Money >= item.Value)
        {
            buttonCarBuy.enabled = true;
            valueTrade = item.Value;
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
        jogador.Purchase(valueTrade);
        nameCar.text = "Selecione um item";
        valueCar.text = "";
    }

    public void CallShopItem(Itens item)
    {
        nameItem.text = item.Name;
        valueItem.text = "Valor: " + item.Value;
        copyObject = item.Item;
        if (jogador.Money >= item.Value)
        {
            buttonItemBuy.enabled = true;
            valueTrade = item.Value;
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
        Serviços.AdicionaNaLista(copyObject);
        jogador.Purchase(valueTrade);
        nameItem.text = "Selecione um item";
        valueItem.text = "";
    }

    public void OpenShop()
    {
        panelShop.SetActive(true);
        main.SetActive(false);
        itemSelecionado = true;
    }

    public void CloseShop()
    {
        panelShop.SetActive(false);
        main.SetActive(true);
        itemSelecionado = false;
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
