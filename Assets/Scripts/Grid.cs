using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Grid : MonoBehaviour
{
    //Variáveis de cálculos e movimentação
    public float grid = 1;
    private float x = 0;
    private float y = 0;
    private Plane movePlane;
    [SerializeField] private float fixdist = 0F;
    private float hitdist;
    private float calc;
    private Ray camRay;
    private Vector3 point, cordPoint;
    [SerializeField] private float rot;
    [SerializeField] private GameObject canvasMovimento;
    [SerializeField] private GameObject canvasCriacao;
    static private Transform trSelect = null;
    public bool selected = false;
    public Color[] colors = new Color[2];
    public Vector3 posInicial;
    private bool liberaObj;
    public LayerMask layerMask;
    public BoxCollider colisorCaixa;
    public bool colisao;



    void Start()
    {
        transform.Rotate(new Vector3(0, rot, 0));
        canvasMovimento.SetActive(false);

        if (GameManager.inst.create)
        {
            canvasCriacao.SetActive(true);
            selected = true;
            trSelect = transform;
            canvasMovimento.SetActive(true);
            colisorCaixa.enabled = false;
            liberaObj = false;
            GameManager.inst.create = false;
        }
    }

    void Update()
    {
        //Comando que faz a movimentação em grid, utilizando apenas números inteiros
        if (grid > 0)
        {
            float recalc = 1f / grid;

            x = Mathf.Round(transform.position.x * recalc) / recalc;
            y = Mathf.Round(transform.position.z * recalc) / recalc;

            transform.position = new Vector3(x, transform.position.y, y);
        }

        //Retira seleção
        if (selected && transform != trSelect)
        {
            selected = false;
            canvasMovimento.SetActive(false);
        }
        
        /*if (selected)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                transform.Rotate(new Vector3(0, rot - 90, 0));
            }
        }*/

        AjusteAlturaSelecao();
        RetiraSelecao();

    }

    private void FixedUpdate()
    {
        Colisoes();
    }

    private void Colisoes()
    {
        if (selected)
        {
            colisao = Physics.CheckBox(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), new Vector3(2.5f, 0.3f, 2.5f), Quaternion.identity, layerMask, QueryTriggerInteraction.UseGlobal);
            if (colisao)
            {
                Debug.Log("Colidiu");
                GetComponent<Renderer>().material.color = colors[2];
            }
            else
            {
                GetComponent<Renderer>().material.color = colors[1];
                Debug.Log("Sem colisão");
            }
        }
        else
        {
            GetComponent<Renderer>().material.color = colors[0];
        }
    }
    
    private void AjusteAlturaSelecao()
    {
        if (selected)
            transform.position = new Vector3(transform.position.x, 0.02f, transform.position.z);
        else
            transform.position = new Vector3(transform.position.x, 0.01f, transform.position.z);
    }

    private void OnMouseDrag() //Corrige a movimentação para cima e para baixo
    {
        camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (movePlane.Raycast(camRay, out hitdist))
        {
            point = camRay.GetPoint(hitdist);

            calc = (fixdist - camRay.origin.y) / (camRay.origin.y - point.y);

            cordPoint.x = camRay.origin.x + (point.x - camRay.origin.x) * -calc;
            cordPoint.y = camRay.origin.y + (point.y - camRay.origin.y) * -calc;
            cordPoint.z = camRay.origin.z + (point.z - camRay.origin.z) * -calc;
            

            if(Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                transform.position = cordPoint;
            }
        }
    }

    #region Configurações dos botões de UI

    private void OnMouseDown() //Ativa o canvasMovimento com os botões
    {
        movePlane = new Plane(Camera.main.transform.forward.normalized, transform.position);
        selected = true;
        colisorCaixa.enabled = false;
        trSelect = transform;
        canvasMovimento.SetActive(true);
    }

    public void MovimentoZ(string dir)
    {
        if(dir == "baixo")
        {
            cordPoint.z = transform.position.z - 1;
        }

        else if (dir == "cima")
        {
            cordPoint.z = transform.position.z + 1;
        }

        cordPoint.x = transform.position.x;
        cordPoint.y = transform.position.y;

        transform.position = cordPoint;
    }

    public void MovimentoX(string dir)
    {
        if (dir == "baixo")
        {
            cordPoint.x = transform.position.x - 1;
        }

        else if (dir == "cima")
        {
            cordPoint.x = transform.position.x + 1;
        }

        cordPoint.z = transform.position.z;
        cordPoint.y = transform.position.y;

        transform.position = cordPoint;
    }

    public void MovimentoRot(Transform obj)
    {
        obj.transform.Rotate(new Vector3(0, rot + 90, 0));
    }

    #endregion

    private void RetiraSelecao()
    {
        if (liberaObj)
        {
            /*if (EventSystem.current.currentSelectedGameObject == null)
            {*/


                if (Input.GetMouseButtonDown(0))
                {
                    RaycastHit hit;
                    Ray rayVar = Camera.main.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(rayVar, out hit))
                    {
                        if (!hit.transform.CompareTag("cena"))
                        {
                            /*trSelect = null;
                            canvasMovimento.SetActive(false);
                            selected = false;*/
                            DesligaSelecao();
                        }
                    }
                }
            //}
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && !SobreUI())
            {
                RaycastHit hit;
                Ray rayVar = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(rayVar, out hit))
                {
                    if (!hit.transform.CompareTag("cena"))
                    {
                        DeletaObj(gameObject);
                    }
                }
            }
        }
        
    }

    private bool SobreUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    public void DesligaSelecao()
    {
        if (!liberaObj)
        {
            if (colisao)
                return;
            else
                liberaObj = true;
        }
        if (liberaObj)
        {
            if (colisao)
            {
                transform.position = posInicial;
                canvasCriacao.SetActive(false);
                trSelect = null;
                canvasMovimento.SetActive(false);
                selected = false;
                GetComponent<Renderer>().material.color = colors[0];
                colisorCaixa.enabled = true;
            }
            else
            {
                posInicial = transform.position;
                canvasCriacao.SetActive(false);
                trSelect = null;
                canvasMovimento.SetActive(false);
                selected = false;
                GetComponent<Renderer>().material.color = colors[0];
                colisorCaixa.enabled = true;
            }
        }
    }

    public void DeletaObj(GameObject obj)
    {
        Destroy(obj);
    }
}
