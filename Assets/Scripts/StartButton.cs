using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    [SerializeField] private GameObject startUiOBJ;

    private void OnTriggerEnter(Collider other)
    {
        WaveManager.instance.WaveStartRequest();
        startUiOBJ.SetActive(false);
    }
}
