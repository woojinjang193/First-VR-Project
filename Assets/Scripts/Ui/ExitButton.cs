using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        //Application.Quit();
        Debug.Log("게임종료");
    }
}
