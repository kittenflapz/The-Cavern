using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetLittlePotion : MonoBehaviour
{
    AudioSource audioSource;
    bool pickedUp;
    PlayerController player;

    [SerializeField]
    Vector3 playerStartPosition;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = FindObjectOfType<PlayerController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // stops it playing a buncha times
            if (!pickedUp)
            {
                pickedUp = true;
                audioSource.Play();
                player.ChangePlayerSize(PlayerSize.LITTLE);
            }
        }
    }
}
