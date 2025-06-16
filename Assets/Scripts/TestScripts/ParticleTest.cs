using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTest : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private GameObject particle;
    private void OnTriggerEnter(Collider other)
    {
       //gameObject.SetActive(false);
        Instantiate(particle, transform.position, Quaternion.identity);
        meshRenderer.enabled = false;
    }
}
