using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Destino : MonoBehaviour
{

    public NavMeshAgent agent;
    public Transform destiny;

    void Start()
    {
        
    }

    void Update()
    {
        agent.SetDestination(destiny.position);
    }
}
