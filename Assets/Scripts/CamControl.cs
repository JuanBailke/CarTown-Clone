using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CamControl : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && GameManager.itemSelecionado == false)
        {
            transform.Translate(-Input.GetAxis("Mouse X") * Time.deltaTime * 30, 0, -Input.GetAxis("Mouse Y") * Time.deltaTime * 30);
        }

        LimitaPosicao();
    }

    void LimitaPosicao()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(transform.position.x, -44f, -4f);
        pos.z = Mathf.Clamp(transform.position.z, -50f, -10f);
        pos.y = 19f;
        transform.position = pos;
    }

}
