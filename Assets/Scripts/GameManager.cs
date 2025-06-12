using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] GameObject ArrowNoCollider;
    [SerializeField] Arrow arrow;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

    }

    public void ArrowLoad()
    {
        ArrowNoCollider.SetActive(true);
    }

    public void ArrowFired()
    {

        ArrowNoCollider.SetActive(false);
        arrow.isLoaded = false;
    }
}
