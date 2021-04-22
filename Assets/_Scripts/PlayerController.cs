using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Movement setup

    [SerializeField]
    float speed;
    [SerializeField]
    Transform playerTransform;

    Vector3 moveDirection;
    float rotationDirection;
    private Vector2 inputVector;

    // Animation setup (forgive me)
    [SerializeField]
    Animator animator;

    // Misc gameplay setup
    bool isPaused;

    [SerializeField]
    GameManager gameManager;


    // UI setup

    [SerializeField]
    GameObject pauseMenu;

    private void Awake()
    {
        moveDirection = Vector3.zero;
        inputVector = Vector2.zero;
        rotationDirection = 0;
        gameManager = FindObjectOfType<GameManager>();

        isPaused = false;

        // fixes a bug where if you pause, go to main menu, then load into the game again, it is still paused
        Time.timeScale = 1;
    }


    private void Update()
    {
        // Movement handling

        moveDirection = playerTransform.forward * inputVector.y;
        rotationDirection = inputVector.x * 1.75f;

        if (moveDirection.magnitude > float.Epsilon || rotationDirection > float.Epsilon)
        {
            animator.SetInteger("Walk", 1);
        }
        else
        {
            animator.SetInteger("Walk", 0);
        }

        Vector3 movementDirection = moveDirection * (speed * Time.deltaTime);
        playerTransform.position += movementDirection;
        playerTransform.Rotate(new Vector3(0, rotationDirection, 0));

    }


    // Input handling
    public void OnPause()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
        }

        pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
    }

    public void OnMovement(InputValue value)
    {
        if (!isPaused)
        {
            inputVector = value.Get<Vector2>();
        }
    }
}
