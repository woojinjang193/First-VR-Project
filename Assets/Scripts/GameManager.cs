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
    }
}
