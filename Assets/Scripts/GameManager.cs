using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] GameObject ArrowNoCollider;
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
    }
}
