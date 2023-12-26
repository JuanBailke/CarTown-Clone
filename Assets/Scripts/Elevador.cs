using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevador : MonoBehaviour
{
    [Header("Variáveis para Gerenciamento de Serviços")]
    [SerializeField] private Transform obj;
    private bool active;
    private Vector3 posicao;

    public Transform Obj { get => obj; set => obj = value; }
    public bool Active { get => active; set => active = value; }
    public Vector3 Posicao { get => posicao; set => posicao = value; }
}
