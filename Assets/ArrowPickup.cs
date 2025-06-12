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
        if (other.TryGetComponent(out XRController interactor))  //�ݶ��̴��� �� ��Ʈ�ѷ��� ���� ��Ʈ�ѷ��� ���� , TryGetComponent �� bool �� ��ȯ
        {
            isHandinZone = true;
            curController = interactor;
            Debug.Log(curController.name + "����");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out XRController interactor) && interactor == curController)
        {
            isHandinZone = false;
            curController = null;
            Debug.Log(curController.name + "��������");
        }
    }

    void Update()
    {
        if (isHandinZone && curController != null)
        {
            if (curController.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed))
            {
                Debug.Log("ȭ�����");
            }
        }
    }
}
