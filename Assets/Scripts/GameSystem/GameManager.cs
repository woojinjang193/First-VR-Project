using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] GameObject ArrowNoCollider;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameOverUI gameOverResult;
    [SerializeField] GameObject gameClearUI;
    [SerializeField] private Transform player;
    [SerializeField] private Transform startPoint;

    [SerializeField] private GameObject particleBox;

    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private CastleGateStatus castleGateStatus;

    [SerializeField] private int playerHealAmount;
    [SerializeField] private int castleHealAmount;

    public bool isLoaded;
    public bool isGameOver;
    public bool isGameClear;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

    }

    private void Start()
    {
        isLoaded = false;
        isGameOver = false;
        isGameClear = false;
        player.transform.position = startPoint.position;
        player.transform.rotation = startPoint.rotation;
    }

    public void ArrowLoad()
    {
        ArrowNoCollider.SetActive(true);
        //Debug.Log("�����Ϸ� ������Ʈ Ȱ��ȭ");
        isLoaded = true;
    }

     public void ArrowFired()
     {
    
         //Debug.Log("�߻�Ϸ� ������Ʈ ��Ȱ��ȭ");
         ArrowNoCollider.SetActive(false);
         isLoaded = false;
     }

    public void GameOver()
    {
        Debug.Log("���ӿ���");
        isGameOver = true;
        gameOverUI.SetActive(true);
        gameOverResult.GameOverResult();

    }

    public void GameClear()
    {
        gameClearUI.SetActive(true);
        particleBox.SetActive(true);
        isGameClear = true;

        //foreach (Transform child in particleBox.transform)  //���� ��ƼŬ ���
        //{
        //    ParticleSystem particle = child.GetComponent<ParticleSystem>(); //�ڽĵ��� ��ƼŬ ���� ����
        //    particle.Play();
        //}
    }

    public void PlayerHeal()
    {
        playerStatus.PlayerHeal(playerHealAmount);
    }

    public void CastleHeal()
    {
        castleGateStatus.CastleHeal(castleHealAmount);
    }
}
