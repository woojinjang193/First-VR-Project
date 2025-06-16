using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Balloon : MonoBehaviour
{
    [SerializeField] private GameObject particle;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Collider _collider;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        _collider.enabled = false;
        particle.SetActive(true);
        meshRenderer.enabled = false;
        audioSource.Play();
        Invoke("BalloonFalse", 1f);

        if (gameObject.name == "RedRight" || gameObject.name == "RedLeft")
        {
            GameManager.instance.PlayerHeal();
        }
        else
        {
            GameManager.instance.CastleHeal();
        }
    }

    private void BalloonFalse()
    {
        _collider.enabled = true;
        gameObject.SetActive(false);
        particle.SetActive(false);
        meshRenderer.enabled = true;
    }

    

    

}
