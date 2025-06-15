using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class AutoGrab : MonoBehaviour
{
    [SerializeField] private XRGrabInteractable pullingPointGrab;
    [SerializeField] Arrow arrow;

    private bool isHandIn = false;
    private bool isArrowIn = false;
    private bool canReloadAgain = true;
    private XRBaseInteractor currentInteractor;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHand"))
        {
            isHandIn = true;

            currentInteractor = other.GetComponentInParent<XRBaseInteractor>(); // 충돌한 트리거의 부모의 손을 받아옴
            //Debug.Log("핸드 충돌");
     
        }
        
        if (other.CompareTag("ArrowButtom"))
        {
            isArrowIn = true;
            arrow = other.GetComponentInParent<Arrow>();//////////////////
            Debug.Log("화살 아래 충돌");
        }

  

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerHand"))
        {
            isHandIn = false;
            //Debug.Log("플레이어 핸드 충돌해제");
        }
        
        if (other.CompareTag("ArrowButtom"))
        {
            isArrowIn = false;
            Debug.Log("화살 아래 충돌해체");
        }
    }

    

      private void Update()
      {
          if ( isHandIn && isArrowIn && !GameManager.instance.isLoaded && canReloadAgain)
          {
              isArrowIn = false;
              //Debug.Log("장전!");
              GameManager.instance.ArrowLoad();

              //arrow.gameObject.SetActive(false);   // 화살 비활성화 
              arrow.ArrowReturnPool(); //리턴

            if (currentInteractor != null && pullingPointGrab != null)
              {
                  currentInteractor.interactionManager.SelectEnter(currentInteractor as IXRSelectInteractor, pullingPointGrab as IXRSelectInteractable);
                  // interactionManager.SelectEnter(누가, 무엇을) 잡게함
              }
    
              canReloadAgain = false;
              
              Invoke("ReloadTimming", 0.9f); // isloaded가 바로 트루가 되어서 바로 또 손에 잡혀서 딜레이 시간추가
        }
      }

    private void ReloadTimming() 
    {
        canReloadAgain = true;
        
    }

}
