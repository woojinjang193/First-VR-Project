using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class CastleGateStatus : MonoBehaviour
{
    [SerializeField] private int maxHP;
    private int curHP;
    [SerializeField] private GameObject particle; // 게이트 파괴 파티클
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Transform particleOnPosition;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip gateGetDamaged;
    [SerializeField] private AudioClip gateBrokeDown;

    [SerializeField] private Slider hpBar;
    [SerializeField] private Transform hpBarTransform;
    [SerializeField] private Camera _camera;

    private void Start()
    {
        curHP = maxHP;
        hpBar.maxValue = maxHP;
        hpBar.value = curHP;
    }

    private void Update()
    {
        hpBarTransform.rotation = Quaternion.LookRotation(hpBarTransform.position - _camera.transform.position); //항상 플레이어를 바라보게함
    }
    public void TakeDamage(int damage)
    {
        curHP -= damage;
        hpBar.value = curHP;

        Debug.Log("성문 체력: " + curHP);

        if (curHP <= 0)
        {
            GateBrokeDown();
        }
        else
        {
            audioSource.clip = gateGetDamaged; //게이트 공격받는소리
            audioSource.Play();
        }

    }

    private void GateBrokeDown()
    {
        audioSource.clip = gateBrokeDown; //게이트 부셔지는소리
        audioSource.Play();

        Instantiate(particle, particleOnPosition.position, Quaternion.identity);
        meshRenderer.enabled = false;

        Debug.Log("게이트 부셔짐");
        GameManager.instance.GameOver();
    }

    public void CastleHeal(int amount)
    {
        if (curHP >= maxHP)
        {
            return;
        }
        else
        {
            curHP += amount;
            hpBar.value = curHP;
        }
        Debug.Log("힐!!!!!! 성문 체력: " + curHP);
    }
}
