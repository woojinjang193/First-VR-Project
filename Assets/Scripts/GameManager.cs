using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] GameObject ArrowNoCollider;
    public bool isLoaded;

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
    }

    public void ArrowLoad()
    {
        ArrowNoCollider.SetActive(true);
        Debug.Log("�����Ϸ� ������Ʈ Ȱ��ȭ");
        isLoaded = true;
    }

     public void ArrowFired()
     {
    
         Debug.Log("�߻�Ϸ� ������Ʈ ��Ȱ��ȭ");
         ArrowNoCollider.SetActive(false);
         isLoaded = false;
     }
}
