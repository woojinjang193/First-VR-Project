using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonTest : MonoBehaviour
{
    public void OnButtonClick()
    {
        Debug.Log("��ưŬ��");
        WaveManager.instance.StartWave();
    }
}
