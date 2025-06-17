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

    [SerializeField] private int spawnTryTime; //ǳ�� ��ȯ �õ�����
    [SerializeField] private int spawnPercent;  // ��ȯ ���� Ȯ��
    [SerializeField] private float despawnPoint; //������ Y ����
    [SerializeField] private float moveSpeed;

    private List<GameObject> activeBalloons = new List<GameObject>(); //Ȱ��ȭ�� ǳ�� ����Ʈ
    private float timer = 0f;

    private void Update()
    {
        if (WaveManager.instance.isGameStarted && !GameManager.instance.isGameOver && !GameManager.instance.isGameClear)
        {
            timer += Time.deltaTime; //Ÿ�̸� ����

            if (timer >= spawnTryTime)
            {
                timer = 0f;

                int random = Random.Range(0, 100);
                if (random < spawnPercent) //Ȯ������ ������ ����
                {
                    SpawnBalloon();
                }
            }

            for (int i = activeBalloons.Count - 1; i >= 0; i--)  //for�������� �տ������� ����� Ư���ε����� �ǳʶٴ� ��Ȳ�� �߻��� �� ����
            {
                GameObject balloon = activeBalloons[i];
                balloon.transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

                if (balloon.transform.position.y >= despawnPoint)  //ǳ���� despawnPoint�� �����ϸ�
                {
                    balloon.SetActive(false);  //��Ȱ��ȭ�ϰ� 
                    activeBalloons.RemoveAt(i); //�������� ����
                }
            }
        }
    }

    private void SpawnBalloon()
    {
        GameObject balloon = null;
        int balloonNum = Random.Range(0, 4); //0~3������ ��������

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

            if (balloon == RedRight || balloon == BlueRight) //�����ʿ��� ������ ǳ����
                balloon.transform.position = RightSpawnPosition.position; //���������� ��ġ ����
            else
                balloon.transform.position = LeftSpawnPosition.position;  //����

            activeBalloons.Add(balloon); //����Ʈ�� �־���
        }
    }
}
