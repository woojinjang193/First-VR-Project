using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResetPosition : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform startPoint;
    public void PlayerPositionRsetButton()
    {
        player.transform.position = startPoint.position;
        player.transform.rotation = startPoint.rotation;
    }    
}
