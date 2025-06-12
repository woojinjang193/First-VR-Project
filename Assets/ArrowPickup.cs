using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ArrowPickup : MonoBehaviour
{
   // [SerializeField] private GameObject arrowPrefab;
   // [SerializeField] private Transform arrowSpawnPoint;

    private XRController curController;
    private bool isHandinZone = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out XRController interactor))  //콜라이더에 들어간 컨트롤러를 현재 컨트롤러로 지정 , TryGetComponent 는 bool 을 반환
        {
            isHandinZone = true;
            curController = interactor;
            Debug.Log(curController.name + "감지");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out XRController interactor) && interactor == curController)
        {
            isHandinZone = false;
            curController = null;
            Debug.Log(curController.name + "감지종료");
        }
    }

    void Update()
    {
        if (isHandinZone && curController != null)
        {
            if (curController.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed))
            {
                Debug.Log("화살생성");
            }
        }
    }
}
