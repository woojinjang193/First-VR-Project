using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGrab : MonoBehaviour
{
    [SerializeField] private Rigidbody rigid;

    public void ArrowUseable()
    {
        rigid.isKinematic = false;
    }

 
}
