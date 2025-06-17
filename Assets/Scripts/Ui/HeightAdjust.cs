using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.CoreUtils;
using Unity.VisualScripting;

public class HeightAdjuster : MonoBehaviour
{
    [SerializeField] private float change = 0.1f;  //�ѹ��� �̵��� ��
    private XROrigin player;

    void Start()
    {
        player = GetComponent<XROrigin>();
    }

    public void UPButtonClick()  //�ö�
    {
        Vector3 height = player.transform.position; //�÷��̾� ��ġ
        height.y += change;
        player.transform.position = height;
    }

    public void DownButtonClick() //������
    {
        Vector3 height = player.transform.position;
        height.y -= change;
        player.transform.position = height;
    }
 
}

