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

    [SerializeField] private int spawnTryTime;
    [SerializeField] private int spawnPercent;
    [SerializeField] private float despawnPoint;
    [SerializeField] private float moveSpeed;

    private List<GameObject> activeBalloons = new List<GameObject>();
    private float timer = 0f;

    private void Update()
    {
        if (WaveManager.instance.isGameStarted && !GameManager.instance.isGameOver && !GameManager.instance.isGameClear)
        {
            timer += Time.deltaTime;

            if (timer >= spawnTryTime)
            {
                timer = 0f;

                int random = Random.Range(0, 100);
                if (random < spawnPercent)
                {
                    SpawnBalloon();
                }
            }

            // 모든 풍선 위로 이동
            for (int i = activeBalloons.Count - 1; i >= 0; i--)
            {
                GameObject balloon = activeBalloons[i];
                balloon.transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

                if (balloon.transform.position.y >= despawnPoint)
                {
                    balloon.SetActive(false);
                    activeBalloons.RemoveAt(i);
                }
            }
        }
    }

    private void SpawnBalloon()
    {
        GameObject balloon = null;
        int balloonNum = Random.Range(0, 4);

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

            if (balloon == RedRight || balloon == BlueRight)
                balloon.transform.position = RightSpawnPosition.position;
            else
                balloon.transform.position = LeftSpawnPosition.position;

            activeBalloons.Add(balloon);
        }
    }
}
