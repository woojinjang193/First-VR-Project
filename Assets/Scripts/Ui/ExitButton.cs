using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{

    //private void OnTriggerEnter(Collider other)
    //{
    //    Application.Quit();
    //    Debug.Log("��������");
    //}

    private void OnCollisionEnter(Collision collision)
    {
        Application.Quit();
        Debug.Log("��������");
    }


}
