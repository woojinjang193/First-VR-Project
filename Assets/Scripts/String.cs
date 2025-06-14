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

    //발사관련 변수
    private float currentPullAmount; //현재 당긴 양 
    [SerializeField] private float minPullToFire = 0.1f;  // 이 이상 당겨야 발사 처리
    //[SerializeField] private Arrow arrow;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float firePower = 30f;






    private Vector3 defaultPullingPosition; //디폴트 줄의 위치
    private Vector3 defaultPullingFollowerPosition;


    [SerializeField] private float stringStretchLimit = 0.5f;

    void Start()
    {
        defaultPullingPosition = pullingPoint.localPosition;  //디폴트 위치 저장
        defaultPullingFollowerPosition = pullingPointFollower.localPosition;
        grab.selectExited.AddListener(OnReleased); // 이벤트 등록

    }

    void Update()

    {
        if (grab != null && grab.isSelected)  //그랩중일때
        {
            float z = transform.InverseTransformPoint(pullingPoint.position).z;  //로컬 좌표로 변환
            z = Mathf.Clamp(z, -stringStretchLimit, 0f);  //당겨지는 거리 제한
            pullingPointFollower.localPosition = new Vector3(0, 0, z); //팔로워의 z 만 움직이게 함

            currentPullAmount = Mathf.Abs(z);  // 당긴 거리 저장 (절댓값)
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
    private void OnReleased(SelectExitEventArgs args) //발사 함수
    {
        if (currentPullAmount >= minPullToFire && GameManager.instance.isLoaded)  //minPullToFire 만큼 당겨야 발동
        {
            GameManager.instance.ArrowFired();
            Fire();
            Debug.Log("화살 발사");
           
        }

        pullingPoint.localPosition = defaultPullingPosition;
        pullingPointFollower.localPosition = defaultPullingFollowerPosition;

        currentPullAmount = 0f;
    }

    private void Fire()
    {

        GameObject arrowObj = ObjectPoolManager.instance.GetFromPool();  //풀에서 화살 가져옴
        Arrow arrow = arrowObj.GetComponent<Arrow>();
        Rigidbody rigid = arrow.GetComponent<Rigidbody>();
        rigid.isKinematic = false;  //키네마틱 비활성화

        arrow.transform.position = firePoint.position;  //firePoint 위치에서 발사
        arrow.transform.rotation = firePoint.rotation;
      
       
       
        rigid.velocity = firePoint.forward * (currentPullAmount * firePower) ;  // 당겨진양 * 파워
 

        Debug.Log("화살 발사");
    }



}