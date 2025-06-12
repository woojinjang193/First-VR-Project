using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGrab : MonoBehaviour
{
    [SerializeField] public Rigidbody rigid;

    private void Start()
    {
        //rigid.isKinematic = false;
    }
    public void ArrowUseable()
    {
        Debug.Log("화살잡음");
        Debug.Log(rigid);
        rigid.isKinematic = false;
    }

    
}
