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

            currentInteractor = other.GetComponentInParent<XRBaseInteractor>(); // �浹�� Ʈ������ �θ��� ���� �޾ƿ�
            //Debug.Log("�ڵ� �浹");
     
        }
        
        if (other.CompareTag("ArrowButtom"))
        {
            isArrowIn = true;
            arrow = other.GetComponentInParent<Arrow>();//////////////////
            Debug.Log("ȭ�� �Ʒ� �浹");
        }

  

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerHand"))
        {
            isHandIn = false;
            //Debug.Log("�÷��̾� �ڵ� �浹����");
        }
        
        if (other.CompareTag("ArrowButtom"))
        {
            isArrowIn = false;
            Debug.Log("ȭ�� �Ʒ� �浹��ü");
        }
    }

    

      private void Update()
      {
          if ( isHandIn && isArrowIn && !GameManager.instance.isLoaded && canReloadAgain)
          {
              isArrowIn = false;
              //Debug.Log("����!");
              GameManager.instance.ArrowLoad();

              //arrow.gameObject.SetActive(false);   // ȭ�� ��Ȱ��ȭ 
              arrow.ArrowReturnPool(); //����

            if (currentInteractor != null && pullingPointGrab != null)
              {
                  currentInteractor.interactionManager.SelectEnter(currentInteractor as IXRSelectInteractor, pullingPointGrab as IXRSelectInteractable);
                  // interactionManager.SelectEnter(����, ������) �����
              }
    
              canReloadAgain = false;
              
              Invoke("ReloadTimming", 0.9f); // isloaded�� �ٷ� Ʈ�簡 �Ǿ �ٷ� �� �տ� ������ ������ �ð��߰�
        }
      }

    private void ReloadTimming() 
    {
        canReloadAgain = true;
        
    }

}
