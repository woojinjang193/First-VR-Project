using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ArrowPickup : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;

    public void ReplaceWithArrow()
    {

        GameObject arrow = Instantiate(arrowPrefab, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}

