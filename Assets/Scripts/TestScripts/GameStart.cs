using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        WaveManager.instance.StartWave();
    }
}
