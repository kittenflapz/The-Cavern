using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public enum PlayerSize
{
    BIG,
    MEDIUM,
    LITTLE
}

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


    // UI 
    [SerializeField]
    TextMeshProUGUI tabToPauseLabel;

    // Animation setup
    //[SerializeField]
    // Animator animator;

    // Misc gameplay setup
    [SerializeField]
    GameManager gameManager;

    bool isPaused;

    // Stuff to do with size changing
    [SerializeField]
    PlayerSize initialPlayerSize;
    
    PlayerSize playerSize;
    Vector3 big;
    Vector3 medium;
    Vector3 little;



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
        Time.timeScale = 1;
        tabToPauseLabel.SetText("tab to pause");

        big = new Vector3(0.5f, 12, 0.5f);
        medium = new Vector3(0.5f, 4, 0.5f);
        little = new Vector3(0.5f, 1f, 0.5f);

        ChangePlayerSize(initialPlayerSize);
    }


    private void Update()
    {
        // Movement handling

        moveDirection = playerTransform.forward * inputVector.y;
        rotationDirection = inputVector.x * 1.75f;

        //if (moveDirection.magnitude > float.Epsilon || rotationDirection > float.Epsilon)
        //{
        //    animator.SetInteger("Walk", 1);
        //}
        //else
        //{
        //    animator.SetInteger("Walk", 0);
        //}

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
            tabToPauseLabel.SetText("tab to pause");
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
            tabToPauseLabel.SetText("tab to unpause");
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

    public void ChangePlayerSize(PlayerSize size)
    {
        playerSize = size;
        switch (size)
        {
            case PlayerSize.BIG:
                transform.localScale = big;
                break;
            case PlayerSize.MEDIUM:
                transform.localScale = medium;
                break;
            case PlayerSize.LITTLE:
                transform.localScale = little;
                break;
            default:
                break;
        }
    }
}
