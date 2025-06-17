using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.CoreUtils;
using Unity.VisualScripting;

public class HeightAdjuster : MonoBehaviour
{
    [SerializeField] private float change = 0.1f;  //한번에 이동할 양
    private XROrigin player;

    void Start()
    {
        player = GetComponent<XROrigin>();
    }

    public void UPButtonClick()  //올라감
    {
        Vector3 height = player.transform.position; //플레이어 위치
        height.y += change;
        player.transform.position = height;
    }

    public void DownButtonClick() //내려감
    {
        Vector3 height = player.transform.position;
        height.y -= change;
        player.transform.position = height;
    }
 
}

