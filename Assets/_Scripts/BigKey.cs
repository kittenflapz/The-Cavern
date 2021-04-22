using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigKey : MonoBehaviour
{
    PlayerController playerController;


    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // no tomfoolery
            if (playerController.playerSize == PlayerSize.BIG)
            {
                playerController.SetCanGetKey(Key.BIG);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController.SetCannotGetKey();
        }
    }
}
