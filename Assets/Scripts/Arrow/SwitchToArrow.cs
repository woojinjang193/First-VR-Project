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
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ArrowTaking); //화살 가져오는 소리 재생
        //Debug.Log("활로 변경");

        cubeGrab.gameObject.SetActive(false);

        Transform handTransform = args.interactorObject.transform;

        // GameObject arrow = Instantiate(ArrowPrefab, handTransform.position, handTransform.rotation); //화살 생성
        GameObject arrow = ObjectPoolManager.instance.GetReloadArrow(); //풀에서 장전용화살 꺼냄
        arrow.transform.position = handTransform.position;
        arrow.transform.rotation = handTransform.rotation;

        XRGrabInteractable arrowGrab = arrow.GetComponent<XRGrabInteractable>();  //화살정보
        IXRSelectInteractor interactorInterface = args.interactorObject;
        XRBaseInteractor interactor = interactorInterface.transform.GetComponent<XRBaseInteractor>(); //손 정보 
        //Debug.Log("인터렉터이름: " + interactor.name);

        interactor.interactionManager.SelectEnter(interactor as IXRSelectInteractor, arrowGrab as IXRSelectInteractable);
        // interactionManager.SelectEnter(누가, 무엇을) 잡게함

        Invoke("CubeActivate", 0.5f);

    }

    private void CubeActivate()
    {
        cubeGrab.gameObject.SetActive(true);
    }
}
