using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUiRotation : MonoBehaviour
{
    [SerializeField] private Transform startUiTransForm;

    [SerializeField] private Camera _camera;


    private void Update()
    {
        startUiTransForm.rotation = Quaternion.LookRotation(startUiTransForm.position - _camera.transform.position); //항상 플레이어를 바라보게함

        
    }
}
