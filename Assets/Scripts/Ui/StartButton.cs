using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    [SerializeField] private GameObject startUiOBJ;
    [SerializeField] private GameObject HeightAdjustUI;
    [SerializeField] private GameObject instruction;
    [SerializeField] private GameObject rayInteractor;

    //private void OnTriggerEnter(Collider other)
    //{
    //    WaveManager.instance.WaveStartRequest();
    //    startUiOBJ.SetActive(false);
    //    Debug.Log("���ӽ���");
    //}

    private void OnCollisionEnter(Collision collision)
    {
        WaveManager.instance.WaveStartRequest();
        startUiOBJ.SetActive(false);
        Debug.Log("���ӽ���");
        HeightAdjustUI.SetActive(false);
        instruction.SetActive(false);
        rayInteractor.SetActive(false);

    }


}
