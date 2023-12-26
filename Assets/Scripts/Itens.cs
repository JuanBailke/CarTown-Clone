using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itens : MonoBehaviour
{
    //Variáveis compartilhadas
    [SerializeField] private int id;
    [SerializeField] private string name;
    [SerializeField] private int value;
    [SerializeField] private int saleValue;
    [SerializeField] private Transform item;
    //Variáveis de elevador
    private bool liberated;

    public int Id { get => id; }
    public string Name { get => name; }
    public int Value { get => value; }
    public int SaleValue { get => saleValue; }
    public Transform Item { get => item; }
    public bool Liberated { get => liberated; }
}
