using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Grid : MonoBehaviour
{
    public float grid = 1;
    private float x = 0;
    private float y = 0;
    private Plane movePlane;
    [SerializeField] private float fixdist = 0F;
    private float hitdist;
    private float calc;
    private Ray camRay;
    private Vector3 point, cordPoint;
    private float rot;
    private Renderer render;

    [SerializeField] 
    private GameObject canvas;
    static private Transform trSelect = null;
    public bool selected = false;


    void Start()
    {
        rot = transform.rotation.y;
        render = GetComponent<Renderer>();

        canvas.SetActive(false);
    }

    void Update()
    {

        if (grid > 0) //Comando que faz a movimentação em grid, utilizando apenas números inteiros
        {
            float recalc = 1f / grid;

            x = Mathf.Round(transform.position.x * recalc) / recalc;
            y = Mathf.Round(transform.position.z * recalc) / recalc;

            transform.position = new Vector3(x, transform.position.y, y);
        }

        if (selected && transform != trSelect)
        {
            selected = false;
            canvas.SetActive(false);
        }

        if (selected)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                transform.Rotate(new Vector3(0, rot - 90, 0));
            }
        }

        RetiraSelecao();

    }


    private void OnMouseDown() //Ativa o canvas com os botões
    {
        movePlane = new Plane(Camera.main.transform.forward.normalized, transform.position);

        selected = true;
        trSelect = transform;
        canvas.SetActive(true);
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
    /*
    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.Rotate(new Vector3(0, rot - 90, 0));
        }
    }
    */
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("cena"))
        {
            render.material.color = Color.red;
            print("colidiu");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("cena"))
        {
            render.material.color = Color.white;
        }
    }

    private void RetiraSelecao()
    {
        if(EventSystem.current.currentSelectedGameObject == null)
        {


            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray rayVar = Camera.main.ScreenPointToRay(Input.mousePosition);

                if(Physics.Raycast(rayVar, out hit))
                {
                    if (hit.transform.CompareTag("fora"))
                    {
                        trSelect = null;
                        canvas.SetActive(false);
                        selected = false;
                    }
                }
            }
        }
    }
}
