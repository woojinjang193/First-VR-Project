using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLoadTrigger : MonoBehaviour
{
    private Arrow arrow;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("�浹");
        arrow.ArrowReturnPool();
    }
}
