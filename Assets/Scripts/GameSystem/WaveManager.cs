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
    public bool isGameStarted = false;
    private int remainingMonsters = 0;

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

    public void StartWave() //���̺� ����
    {
        if (!isGameStarted)
        {
            isGameStarted = true;
        }

        if (currentWave >= waves.Length)
        {
            return;
        }

        AudioManager.instance.PlayBgm(); //bgm ����

        waves[currentWave].SetActive(true);  //���� ���̺� ������Ʈ Ȱ��ȭ 0�� ���̺� 1��
        isWaveStarted = true;
        Debug.Log("���̺�" + (currentWave + 1) + "����");

    }

    public void RegisterMonster() //���� ������ ȣ�� 
    {
        remainingMonsters++;
        Debug.Log("����ͼ�: " + remainingMonsters);
    }

    public void OnMonsterDied() //���� ������ ȣ��
    {
        remainingMonsters--;
        Debug.Log("���� ������: " + remainingMonsters);

        if (remainingMonsters <= 0 && isWaveStarted)  // ���� �� ������
        {
            Debug.Log("���̺�" + (currentWave + 1) + "Ŭ����");
            isWaveStarted = false;
            Debug.Log(waveStartDelay + "�� �� ���� ���̺� ����...");
            Invoke("CurrentWaveFalse", waveStartDelay); //���Ͱ� ������ ������� ������ ��Ȱ��ȭ ���ϱ�
        }
    }

    private void CurrentWaveFalse()
    {
        waves[currentWave].SetActive(false); // ������̺� ��Ȱ��ȭ
        currentWave++;

        if (currentWave < waves.Length) //���� ���� Ŭ�������̸�
        {
            StartWave();
        }
        else // ���̺���� ���ų� Ŀ����
        {
            GameManager.instance.GameClear();
            Debug.Log("���� Ŭ����");
        }
    }

    public void WaveStartRequest()
    {
        audioSorce.Play();
        Invoke("StartWave", 3f);
    }

}
