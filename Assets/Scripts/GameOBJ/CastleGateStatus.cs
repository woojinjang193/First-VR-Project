using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class CastleGateStatus : MonoBehaviour
{
    [SerializeField] private int maxHP;
    private int curHP;
    [SerializeField] private GameObject particle; // ����Ʈ �ı� ��ƼŬ
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
        hpBarTransform.rotation = Quaternion.LookRotation(hpBarTransform.position - _camera.transform.position); //�׻� �÷��̾ �ٶ󺸰���
    }
    public void TakeDamage(int damage)
    {
        curHP -= damage;
        hpBar.value = curHP;

        Debug.Log("���� ü��: " + curHP);

        if (curHP <= 0)
        {
            GateBrokeDown();
        }
        else
        {
            audioSource.clip = gateGetDamaged; //����Ʈ ���ݹ޴¼Ҹ�
            audioSource.Play();
        }

    }

    private void GateBrokeDown()
    {
        audioSource.clip = gateBrokeDown; //����Ʈ �μ����¼Ҹ�
        audioSource.Play();

        Instantiate(particle, particleOnPosition.position, Quaternion.identity);
        meshRenderer.enabled = false;

        Debug.Log("����Ʈ �μ���");
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
        Debug.Log("��!!!!!! ���� ü��: " + curHP);
    }
}
