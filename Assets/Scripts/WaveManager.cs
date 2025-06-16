using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameObject[] waves; // ���̺���
    private int currentWave = 0;
    private bool isWaveStarted = false;
    [SerializeField] private float waveStartDelay = 10f;

    void Update()
    {
        if (isWaveStarted && IsCurrentWaveCleared())  //���̺갡 ��������
        {
            Debug.Log("���̺�" + (currentWave + 1) + "Ŭ����");
            waves[currentWave].SetActive(false);  //����� ���̺������Ʈ ��Ȱ��ȭ
            isWaveStarted = false;  //�ѹ��� ����ǰ�
            currentWave++;

            if (currentWave < waves.Length)
            {
                Invoke("StartNextWaveDelay", waveStartDelay);  //�������̺� ������ ������
            }
            else
            {
                Debug.Log("��� ���̺� Ŭ����!");
            }
        }
    }

    public void StartWave() //���̺� ����
    {
        if (currentWave >= waves.Length)
        {
            Debug.Log("���� Ŭ����");
            return;
        }

        waves[currentWave].SetActive(true);  //���� ���̺� ������Ʈ Ȱ��ȭ 0�� ���̺� 1��
        isWaveStarted = true;
        Debug.Log("���̺�" + (currentWave + 1) + "����");

    }

    private void StartNextWaveDelay()
    {
        Debug.Log(waveStartDelay + "�� �� ���� ���̺� ����...");
        StartWave();
    }

    private bool IsCurrentWaveCleared() //���̺� Ŭ���� �˻��Լ�
    {
        GameObject currentWaveObj = waves[currentWave]; //���� ���̺� ������Ʈ

        foreach (Transform child in currentWaveObj.transform) // ���� ���̺� ������Ʈ�� �˻��ؼ� 
        {
            if (child.gameObject.activeSelf) //Ȱ��ȭ�� ���Ͱ� ������
                return false; //false ��ȯ
        }

        return true;  //���Ͱ� ���� ��Ȱ��ȭ�� true
    }
}
