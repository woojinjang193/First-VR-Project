using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SwitchToArrow : MonoBehaviour
{
    //[SerializeField] private GameObject loadArrowPrefab;
    // [SerializeField] private Transform spawnPosition;
    [SerializeField] private XRGrabInteractable cubeGrab;
    [SerializeField] private Transform playerBackTransform;

    Vector3 playerBack;

    private void OnEnable()
    {
        cubeGrab.selectEntered.AddListener(OnCubeGrabbed);
    }

    private void OnDisable()
    {
        cubeGrab.selectEntered.RemoveListener(OnCubeGrabbed);
    }

    private void Update()
    {
        if (cubeGrab != null && cubeGrab.gameObject.activeSelf)
        {
            cubeGrab.transform.position = playerBackTransform.position;
            cubeGrab.transform.rotation = playerBackTransform.rotation;
        }

    }

    private void OnCubeGrabbed(SelectEnterEventArgs args)
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ArrowTaking); //ȭ�� �������� �Ҹ� ���
        //Debug.Log("Ȱ�� ����");

        cubeGrab.gameObject.SetActive(false);

        Transform handTransform = args.interactorObject.transform;

        // GameObject arrow = Instantiate(ArrowPrefab, handTransform.position, handTransform.rotation); //ȭ�� ����
        GameObject arrow = ObjectPoolManager.instance.GetReloadArrow(); //Ǯ���� ������ȭ�� ����
        arrow.transform.position = handTransform.position;
        arrow.transform.rotation = handTransform.rotation;

        XRGrabInteractable arrowGrab = arrow.GetComponent<XRGrabInteractable>();  //ȭ������
        IXRSelectInteractor interactorInterface = args.interactorObject;
        XRBaseInteractor interactor = interactorInterface.transform.GetComponent<XRBaseInteractor>(); //�� ���� 
        //Debug.Log("���ͷ����̸�: " + interactor.name);

        interactor.interactionManager.SelectEnter(interactor as IXRSelectInteractor, arrowGrab as IXRSelectInteractable);
        // interactionManager.SelectEnter(����, ������) �����

        Invoke("CubeActivate", 0.5f);

    }

    private void CubeActivate()
    {
        cubeGrab.gameObject.SetActive(true);
    }
}
