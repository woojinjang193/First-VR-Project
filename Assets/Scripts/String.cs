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



    private Vector3 defaultPullingPosition; //디폴트 줄의 위치
    private Vector3 defaultPullingFollowerPosition;


    [SerializeField] private float stringStretchLimit = 0.5f;

    void Start()
    {
        defaultPullingPosition = pullingPoint.localPosition;  //디폴트 위치 저장
        defaultPullingFollowerPosition = pullingPointFollower.localPosition;
    }

    void Update()

    {
        if (grab != null && grab.isSelected)  //그랩중일때
        {
            float z = transform.InverseTransformPoint(pullingPoint.position).z;  //로컬 좌표로 변환
            z = Mathf.Clamp(z, -stringStretchLimit, 0f);  //당겨지는 거리 제한
            pullingPointFollower.localPosition = new Vector3(0, 0, z); //팔로워의 z 만 움직이게 함


        }

        else
        {
            pullingPoint.localPosition = defaultPullingPosition;  //손을 놓으면 디폴트 위치로 이동
            pullingPointFollower.localPosition = defaultPullingFollowerPosition;
        }

        lineRenderer.SetPosition(0, bowTop.position);
        lineRenderer.SetPosition(1, pullingPointFollower.position); //줄의 가운데는 팔로워를 따라다님
        lineRenderer.SetPosition(2, bowBottom.position);
    }


}