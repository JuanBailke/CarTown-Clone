using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AjusteRotacao : MonoBehaviour
{

    Quaternion quaternion;
    
    void Awake()
    {
        quaternion = transform.rotation;
    }

    
    private void LateUpdate()
    {
        transform.rotation = quaternion;
    }
}
