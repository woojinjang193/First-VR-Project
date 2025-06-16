using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TMP_Text gameOverText;

    public void GameOverResult()
    {
        int cleared = WaveManager.instance.clearedWave;   //Ŭ������ ���̺�
        gameOverText.text = $"Cleared {cleared} Waves out of 5!";
    }
}