using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Carro : MonoBehaviour
{
    //[SerializeField] private NavMeshAgent carObject;
    [SerializeField] private GameObject carObject, incompleteBackground, fullBackground, imageService;
    private int posicaoUtilizada;
    public Destino carDestiny;
    private bool activate = false, unlockXP = false, waiting = true;
    private float tempoInicio;
    private float[] timeService = { 10f, 20f, 30f };
    private float[] xpServico = { 0.1f, 0.2f, 0.3f };
    private int[] value = { 50, 100, 200 };
    public int indice;
    private Jogador jogador;
    private Elevador elevador;

    //public NavMeshAgent CarObject { get => carObject;}
    public GameObject CarObject { get => carObject; }
    public GameObject IncompleteBackground { get => incompleteBackground;}
    public GameObject FullBackground { get => fullBackground;}
    public GameObject ImageService { get => imageService; }
    public int PosicaoUtilizada { get => posicaoUtilizada; set => posicaoUtilizada = value; }
    public float[] TimeService { get => timeService; set => timeService = value; }
    public Destino CarDestiny { get => carDestiny; set => carDestiny = value; }
    public bool Activate { get => activate; set => activate = value; }
    public bool UnlockXP { get => unlockXP; set => unlockXP = value; }
    public bool Waiting { get => waiting; set => waiting = value; }

    void Start()
    {
        GameObject Player = GameObject.Find("Player");
        jogador = Player.GetComponent<Jogador>();
        //Debug.Log("Índice: " + indice);
        //Debug.Log("carro.TimeService = " + TimeService[indice]);
    }

    void Update()
    {
        if(activate == true)
        {
            tempoInicio += Time.deltaTime;
            if(tempoInicio < TimeService[indice])
            {
                IncompleteBackground.GetComponent<Image>().fillAmount = (tempoInicio / TimeService[indice]);
            }
            else
            {
                IncompleteBackground.SetActive(false);
                FullBackground.SetActive(true);
                UnlockXP = true;
            }
        }
    }

    public void ProcuraLocal(Carro car)
    {
        if (unlockXP == false)
        {
            elevador = Serviços.ProcuraLocal(car);
        }

    }

    public void AplicaXP(GameObject car)
    {
        if(unlockXP == true)
        {
            Destroy(car);
            jogador.RecebeXP(xpServico[indice]);
            jogador.Payment(value[indice]);
            Serviços.AtivaElevador(elevador);
        }
    }
}
