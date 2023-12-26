using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Destino : MonoBehaviour
{

    //public NavMeshAgent agent;
    public Transform localization;
    public bool liberated = true;
    //private int id;

    public bool Liberated { get => liberated; set => liberated = value; }
    //public int Id { get => id; set => id = value; }

    void Update()
    {
        /*comando que executa a movimentação do carro teste
        agent.SetDestination(localization.position);*/
    }
}
