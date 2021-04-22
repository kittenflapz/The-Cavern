using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    [SerializeField]
    Vector3 playerStartPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // maybe some logic for bigness in here
            other.gameObject.transform.position = playerStartPosition;
        }
    }
}
