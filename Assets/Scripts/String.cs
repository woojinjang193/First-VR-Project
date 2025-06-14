using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.XR.Interaction.Toolkit;

public class String : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    
    [SerializeField] private Transform bowTop;
    [SerializeField] private Transform bowBottom;
    [SerializeField] private XRGrabInteractable grab;

    [SerializeField] private Transform pullingPoint;
    [SerializeField] private Transform pullingPointFollower;

    //�߻���� ����
    private float currentPullAmount; //���� ��� �� 
    [SerializeField] private float minPullToFire = 0.1f;  // �� �̻� ��ܾ� �߻� ó��
    //[SerializeField] private Arrow arrow;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float firePower = 30f;






    private Vector3 defaultPullingPosition; //����Ʈ ���� ��ġ
    private Vector3 defaultPullingFollowerPosition;


    [SerializeField] private float stringStretchLimit = 0.5f;

    void Start()
    {
        defaultPullingPosition = pullingPoint.localPosition;  //����Ʈ ��ġ ����
        defaultPullingFollowerPosition = pullingPointFollower.localPosition;
        grab.selectExited.AddListener(OnReleased); // �̺�Ʈ ���

    }

    void Update()

    {
        if (grab != null && grab.isSelected)  //�׷����϶�
        {
            float z = transform.InverseTransformPoint(pullingPoint.position).z;  //���� ��ǥ�� ��ȯ
            z = Mathf.Clamp(z, -stringStretchLimit, 0f);  //������� �Ÿ� ����
            pullingPointFollower.localPosition = new Vector3(0, 0, z); //�ȷο��� z �� �����̰� ��

            currentPullAmount = Mathf.Abs(z);  // ��� �Ÿ� ���� (����)
        }

        else
        {
            pullingPoint.localPosition = defaultPullingPosition;  //���� ������ ����Ʈ ��ġ�� �̵�
            pullingPointFollower.localPosition = defaultPullingFollowerPosition;
        }

        lineRenderer.SetPosition(0, bowTop.position);
        lineRenderer.SetPosition(1, pullingPointFollower.position); //���� ����� �ȷο��� ����ٴ�
        lineRenderer.SetPosition(2, bowBottom.position);

    }
    private void OnReleased(SelectExitEventArgs args) //�߻� �Լ�
    {
        if (currentPullAmount >= minPullToFire && GameManager.instance.isLoaded)  //minPullToFire ��ŭ ��ܾ� �ߵ�
        {
            GameManager.instance.ArrowFired();
            Fire();
            Debug.Log("ȭ�� �߻�");
           
        }

        pullingPoint.localPosition = defaultPullingPosition;
        pullingPointFollower.localPosition = defaultPullingFollowerPosition;

        currentPullAmount = 0f;
    }

    private void Fire()
    {

        GameObject arrowObj = ObjectPoolManager.instance.GetFromPool();  //Ǯ���� ȭ�� ������
        Arrow arrow = arrowObj.GetComponent<Arrow>();
        Rigidbody rigid = arrow.GetComponent<Rigidbody>();
        rigid.isKinematic = false;  //Ű�׸�ƽ ��Ȱ��ȭ

        arrow.transform.position = firePoint.position;  //firePoint ��ġ���� �߻�
        arrow.transform.rotation = firePoint.rotation;
      
       
       
        rigid.velocity = firePoint.forward * (currentPullAmount * firePower) ;  // ������� * �Ŀ�
 

        Debug.Log("ȭ�� �߻�");
    }



}