using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiLookAtPlayer : MonoBehaviour
{
    [SerializeField] private Transform uiTransFrom;

    [SerializeField] private Camera _camera;


    private void Update()
    {
        uiTransFrom.rotation = Quaternion.LookRotation(uiTransFrom.position - _camera.transform.position); //�׻� �÷��̾ �ٶ󺸰���

        
    }
}
