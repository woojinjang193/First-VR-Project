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
    public bool isGameStarted = false;
    private int remainingMonsters = 0;

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

    public void StartWave() //웨이브 시작
    {
        if (!isGameStarted)
        {
            isGameStarted = true;
        }

        if (currentWave >= waves.Length)
        {
            return;
        }

        AudioManager.instance.PlayBgm(); //bgm 시작

        waves[currentWave].SetActive(true);  //현재 웨이브 오브젝트 활성화 0이 웨이브 1임
        isWaveStarted = true;
        Debug.Log("웨이브" + (currentWave + 1) + "시작");

    }

    public void RegisterMonster() //몬스터 생성시 호출 
    {
        remainingMonsters++;
        Debug.Log("몬수터수: " + remainingMonsters);
    }

    public void OnMonsterDied() //몬스터 죽을때 호출
    {
        remainingMonsters--;
        Debug.Log("몬스터 남은수: " + remainingMonsters);

        if (remainingMonsters <= 0 && isWaveStarted)  // 몬스터 다 잡으면
        {
            Debug.Log("웨이브" + (currentWave + 1) + "클리어");
            isWaveStarted = false;
            Debug.Log(waveStartDelay + "초 후 다음 웨이브 시작...");
            Invoke("CurrentWaveFalse", waveStartDelay); //몬스터가 완전히 사라지기 전까지 비활성화 안하기
        }
    }

    private void CurrentWaveFalse()
    {
        waves[currentWave].SetActive(false); // 현재웨이브 비활성화
        currentWave++;

        if (currentWave < waves.Length) //게임 아직 클리어전이면
        {
            StartWave();
        }
        else // 웨이브수랑 같거나 커지면
        {
            GameManager.instance.GameClear();
            Debug.Log("게임 클리어");
        }
    }

    public void WaveStartRequest()
    {
        audioSorce.Play();
        Invoke("StartWave", 3f);
    }

}
