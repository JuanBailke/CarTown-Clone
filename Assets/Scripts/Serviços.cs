using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Serviços : MonoBehaviour
{

    public static Serviços serviços;

    [Header("Seleções")]
    [SerializeField] private Carro[] carros;
    [SerializeField] private Sprite[] imagens;
    [SerializeField] private List<Destino> destiny;
    private float tempoInicio = 0f, timer = 0f;
    private int randomCar, randomImage;
    private static int destinyCount = 0;
    [SerializeField] private Transform parent;
    private bool globalValidation;

    //Lista de Elevadores ativos
    private static List<Elevador> elevadores = new List<Elevador>();

    //public static float[] TempoFinal { get => tempoFinal; set => tempoFinal = value; }

    void Start()
    {
        /*incompleteBackground.SetActive(true);
        imageService.GetComponent<Image>().sprite = imagens[0];*/
        //Debug.Log(destiny[destiny.Length - 1].Liberated);
        
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (destiny[0].Liberated == false && destiny[1].Liberated == false && destiny[2].Liberated == false)
        {
            globalValidation = false;
            destinyCount = 0;
        }
        else globalValidation = true;

        if (timer > 5 && globalValidation)
        {
            StartCoroutine(GeraServiço());
            timer = 0f;
        }


        tempoInicio += Time.deltaTime;

    }

    #region Métodos de Serviços


    IEnumerator GeraServiço()
    {
        if (destiny[destinyCount].Liberated == true)
        {
            randomCar = Random.Range(0, carros.Length);
            randomImage = Random.Range(0, imagens.Length);
            carros[randomCar].indice = randomImage;
            carros[randomCar].ImageService.GetComponent<Image>().sprite = imagens[randomImage];
            carros[randomCar].PosicaoUtilizada = destinyCount;
            carros[randomCar].CarDestiny = destiny[destinyCount];
            carros[randomCar].CarDestiny.liberated = false;
            Instantiate(carros[randomCar].GetComponent<Transform>(), destiny[destinyCount].transform.position, destiny[destinyCount].transform.rotation, parent);
            yield return new WaitForSeconds(1);
            destiny[destinyCount].Liberated = false;
            destinyCount++;

        }
    }

    #endregion

    #region Métodos para a lista de Elevadores

    public static void AdicionaNaLista(Transform elevador)
    {
        elevadores.Add(new Elevador() { Obj = elevador, Active = false });
        Debug.Log("Elevador adicionado na posição" + (elevadores.Count - 1).ToString());
    }

    public static void LiberaElevador(Vector3 pos)
    {
        elevadores[elevadores.Count - 1].Active = true;
        elevadores[elevadores.Count - 1].Posicao = pos;
        Debug.Log("Elevador ativado");
    }

    public static void ModificaPosicaoElevador(Vector3 pos)
    {
        //A IMPLEMENTAR
        Debug.Log("Elevador modificado");
    }

    public static void RemoveUltimoDaLista()
    {
        elevadores.RemoveAt(elevadores.Count - 1);
        Debug.Log("Elevador removido");
    }

    public static void AtivaElevador(Elevador elevador)
    {
        elevador.Active = true;
    }

    public static Elevador ProcuraLocal(Carro car)
    {
        foreach (Elevador elevador in elevadores)
        {
            if (elevador.Active == true)
            {
                car.GetComponent<Transform>().transform.position = elevador.Posicao;
                car.CarDestiny.liberated = true;
                elevador.Active = false;
                car.Activate = true;
                destinyCount = 0;
                return elevador;
            }
        }
        return null;
    }

    #endregion
}
