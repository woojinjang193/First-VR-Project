using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.CoreUtils;
using Unity.VisualScripting;

public class HeightAdjuster : MonoBehaviour
{
    [SerializeField] private float change = 0.1f;  //한번에 이동할 양
    [SerializeField] private float minY = 9f;       // 최소높이
    [SerializeField] private float maxY = 11f;      // 최대높이

    private XROrigin player;
    private Transform _camera;

    void Start()
    {
        player = GetComponent<XROrigin>();
        _camera = player.CameraFloorOffsetObject.transform;
    }

    public void UPButtonClick()  //올라감
    {
        Vector3 offset = _camera.localPosition;

        float newY = offset.y + change;
        if (player.transform.position.y + newY >= minY && player.transform.position.y + newY <= maxY) //변화한 플레이어(카메라)의 높이가 미니멈Y보다 크고 맥시멈Y보다 작을때
        {
            offset.y = newY; //변화된 값으로 적용
            _camera.localPosition = offset; //변화한 값을 저장
        }
    }

    public void DownButtonClick() //내려감
    {
        Vector3 offset = _camera.localPosition;

        float newY = offset.y - change;
        if (player.transform.position.y + newY >= minY && player.transform.position.y + newY <= maxY)
        {
            offset.y = newY;
            _camera.localPosition = offset;
        }
    }

}

