using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    [SerializeField] private GameObject[] waves; // ���̺���
    [SerializeField] private float waveStartDelay = 6f;

    AudioSource audioSorce;

    private int currentWave = 0; 
    private bool isWaveStarted = false;

    public int clearedWave => currentWave;  // �ܺ��б�����

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        audioSorce = GetComponent<AudioSource>();
    }

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

        AudioManager.instance.PlayBgm();
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

    public void WaveStartRequest()
    {
        audioSorce.Play();
        Invoke("StartWave", 3f);
    }

}
