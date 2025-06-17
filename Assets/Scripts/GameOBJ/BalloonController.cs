using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour
{
    [SerializeField] private GameObject RedLeft;
    [SerializeField] private GameObject RedRight;
    [SerializeField] private GameObject BlueLeft;
    [SerializeField] private GameObject BlueRight;
    [SerializeField] private Transform LeftSpawnPosition;
    [SerializeField] private Transform RightSpawnPosition;

    [SerializeField] private int spawnTryTime; //풍선 소환 시도간격
    [SerializeField] private int spawnPercent;  // 소환 성공 확률
    [SerializeField] private float despawnPoint; //디스폰될 Y 지점
    [SerializeField] private float moveSpeed;

    private List<GameObject> activeBalloons = new List<GameObject>(); //활성화된 풍선 리스트
    private float timer = 0f;

    private void Update()
    {
        if (WaveManager.instance.isGameStarted && !GameManager.instance.isGameOver && !GameManager.instance.isGameClear)
        {
            timer += Time.deltaTime; //타이머 시작

            if (timer >= spawnTryTime)
            {
                timer = 0f;

                int random = Random.Range(0, 100);
                if (random < spawnPercent) //확률보다 낮으면 실행
                {
                    SpawnBalloon();
                }
            }

            for (int i = activeBalloons.Count - 1; i >= 0; i--)  //for문에서는 앞에서부터 지우면 특정인덱스를 건너뛰는 상황이 발생할 수 있음
            {
                GameObject balloon = activeBalloons[i];
                balloon.transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

                if (balloon.transform.position.y >= despawnPoint)  //풍선이 despawnPoint에 도달하면
                {
                    balloon.SetActive(false);  //비활성화하고 
                    activeBalloons.RemoveAt(i); //리스에서 제거
                }
            }
        }
    }

    private void SpawnBalloon()
    {
        GameObject balloon = null;
        int balloonNum = Random.Range(0, 4); //0~3까지중 랜덤숫자

        switch (balloonNum)
        {
            case 0: balloon = RedRight; break;
            case 1: balloon = RedLeft; break;
            case 2: balloon = BlueRight; break;
            case 3: balloon = BlueLeft; break;
        }

        if (balloon != null && !balloon.activeSelf)
        {
            balloon.SetActive(true);

            if (balloon == RedRight || balloon == BlueRight) //오른쪽에서 스폰될 풍선들
                balloon.transform.position = RightSpawnPosition.position; //오른쪽으로 위치 지정
            else
                balloon.transform.position = LeftSpawnPosition.position;  //왼쪽

            activeBalloons.Add(balloon); //리스트에 넣어줌
        }
    }
}
