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

    public bool isLoaded;
    public bool isGameOver;

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
        player.transform.position = startPoint.position;
        player.transform.rotation = startPoint.rotation;
    }

    public void ArrowLoad()
    {
        ArrowNoCollider.SetActive(true);
        //Debug.Log("장전완료 오브젝트 활성화");
        isLoaded = true;
    }

     public void ArrowFired()
     {
    
         //Debug.Log("발사완료 오브젝트 비활성화");
         ArrowNoCollider.SetActive(false);
         isLoaded = false;
     }

    public void GameOver()
    {
        Debug.Log("게임오버");
        isGameOver = true;
        gameOverUI.SetActive(true);
        gameOverResult.GameOverResult();

    }

    public void GameClear()
    {
        gameClearUI.SetActive(true);
        particleBox.SetActive(true);
        //foreach (Transform child in particleBox.transform)  //축하 파티클 재생
        //{
        //    ParticleSystem particle = child.GetComponent<ParticleSystem>(); //자식들의 파티클 전부 실행
        //    particle.Play();
        //}
    }
}
