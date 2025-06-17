using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.CoreUtils;
using Unity.VisualScripting;

public class HeightAdjuster : MonoBehaviour
{
    [SerializeField] private float change = 0.1f;  //�ѹ��� �̵��� ��
    [SerializeField] private float minY = 9f;       // �ּҳ���
    [SerializeField] private float maxY = 11f;      // �ִ����

    private XROrigin player;
    private Transform _camera;

    void Start()
    {
        player = GetComponent<XROrigin>();
        _camera = player.CameraFloorOffsetObject.transform;
    }

    public void UPButtonClick()  //�ö�
    {
        Vector3 offset = _camera.localPosition;

        float newY = offset.y + change;
        if (player.transform.position.y + newY >= minY && player.transform.position.y + newY <= maxY) //��ȭ�� �÷��̾�(ī�޶�)�� ���̰� �̴ϸ�Y���� ũ�� �ƽø�Y���� ������
        {
            offset.y = newY; //��ȭ�� ������ ����
            _camera.localPosition = offset; //��ȭ�� ���� ����
        }
    }

    public void DownButtonClick() //������
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

