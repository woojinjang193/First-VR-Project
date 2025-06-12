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



    private Vector3 defaultPullingPosition; //����Ʈ ���� ��ġ
    private Vector3 defaultPullingFollowerPosition;


    [SerializeField] private float stringStretchLimit = 0.5f;

    void Start()
    {
        defaultPullingPosition = pullingPoint.localPosition;  //����Ʈ ��ġ ����
        defaultPullingFollowerPosition = pullingPointFollower.localPosition;
    }

    void Update()

    {
        if (grab != null && grab.isSelected)  //�׷����϶�
        {
            float z = transform.InverseTransformPoint(pullingPoint.position).z;  //���� ��ǥ�� ��ȯ
            z = Mathf.Clamp(z, -stringStretchLimit, 0f);  //������� �Ÿ� ����
            pullingPointFollower.localPosition = new Vector3(0, 0, z); //�ȷο��� z �� �����̰� ��


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


}