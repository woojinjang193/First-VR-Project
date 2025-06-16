using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    [SerializeField] private GameObject[] waves; // 웨이브목록
    [SerializeField] private float waveStartDelay = 6f;

    AudioSource audioSorce;

    private int currentWave = 0; 
    private bool isWaveStarted = false;

    public int clearedWave => currentWave;  // 외부읽기전용

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
        if (isWaveStarted && IsCurrentWaveCleared())  //웨이브가 끝났을때
        {
            Debug.Log("웨이브" + (currentWave + 1) + "클리어");
            waves[currentWave].SetActive(false);  //종료된 웨이브오브젝트 비활성화
            isWaveStarted = false;  //한번만 실행되게
            currentWave++;

            if (currentWave < waves.Length)
            {
                Invoke("StartNextWaveDelay", waveStartDelay);  //다음웨이브 시작전 딜레이
            }
            else
            {
                Debug.Log("모든 웨이브 클리어!");
            }
        }
    }

    public void StartWave() //웨이브 시작
    {
        if (currentWave >= waves.Length)
        {
            Debug.Log("게임 클리어");
            return;
        }

        AudioManager.instance.PlayBgm();
        waves[currentWave].SetActive(true);  //현재 웨이브 오브젝트 활성화 0이 웨이브 1임
        isWaveStarted = true;
        Debug.Log("웨이브" + (currentWave + 1) + "시작");

    }

    private void StartNextWaveDelay()
    {
        Debug.Log(waveStartDelay + "초 후 다음 웨이브 시작...");
        StartWave();
    }

    private bool IsCurrentWaveCleared() //웨이브 클리어 검사함수
    {
        GameObject currentWaveObj = waves[currentWave]; //현재 웨이브 오브젝트

        foreach (Transform child in currentWaveObj.transform) // 현재 웨이브 오브젝트를 검사해서 
        {
            if (child.gameObject.activeSelf) //활성화된 몬스터가 있으면
                return false; //false 반환
        }

        return true;  //몬스터가 전부 비활성화면 true
    }

    public void WaveStartRequest()
    {
        audioSorce.Play();
        Invoke("StartWave", 3f);
    }

}
