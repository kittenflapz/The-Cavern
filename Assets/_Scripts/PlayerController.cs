using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public enum PlayerSize
{
    BIG,
    MEDIUM,
    LITTLE
}

public enum Key
{
    BIG,
    SMALL
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

    [SerializeField]
    TextMeshProUGUI pressEToPickUp;

    [SerializeField]
    Image bigKeyUIImage;
    [SerializeField]
    Image smallKeyUIImage;
    // Animation setup
    //[SerializeField]
    // Animator animator;

    // Misc gameplay setup
    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    GameObject bigKey;
    [SerializeField]
    GameObject smallKey;

    bool isPaused;
    bool hasBigKey;
    bool hasSmolKey;
    bool canGetBigKey;
    bool canGetSmolKey;

    // Stuff to do with size changing
    [SerializeField]
    PlayerSize initialPlayerSize;
    
    public PlayerSize playerSize;
    Vector3 big;
    Vector3 medium;
    Vector3 little;

    // UI setup

    [SerializeField]
    GameObject pauseMenu;

    private void Awake()
    {
        pressEToPickUp.gameObject.SetActive(false);
        bigKeyUIImage.gameObject.SetActive(false);
        smallKeyUIImage.gameObject.SetActive(false);
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
                StartCoroutine(Scale(playerTransform, big, new Vector3(playerTransform.position.x, playerTransform.position.y + 0.2f, playerTransform.position.z), 2f));
                break;
            case PlayerSize.MEDIUM:
                StartCoroutine(Scale(playerTransform, medium, new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z), 2f));
                break;
            case PlayerSize.LITTLE:
                StartCoroutine(Scale(playerTransform, little, new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z), 2f));
                transform.localScale = little;
                break;
            default:
                break;
        }
    }

    // https://answers.unity.com/questions/1752632/scale-with-coroutine-and-lerp.html

    IEnumerator Scale(Transform playerTransform, Vector3 upScale, Vector3 endPosition, float duration)
    {
        Vector3 initialScale = transform.localScale;
        Vector3 initialPosition = playerTransform.position;

        for (float time = 0; time < duration; time += Time.deltaTime)
        {
            transform.localScale = Vector3.Lerp(initialScale, upScale, time);
            playerTransform.localPosition = Vector3.Lerp(initialPosition, endPosition, time);
            yield return null;
        }
    }


    public void SetCanGetKey(Key key)
    {
        switch (key)
        {
            case Key.BIG:
                canGetBigKey = true;
                pressEToPickUp.gameObject.SetActive(true);
                break;
            case Key.SMALL:
                canGetSmolKey = true;
                pressEToPickUp.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void SetCannotGetKey()
    {
        canGetBigKey = false;
        canGetSmolKey = false;
        pressEToPickUp.gameObject.SetActive(false);
    }

    public void OnPickUp()
    {
        if (canGetSmolKey)
        {
            hasSmolKey = true;
            smallKeyUIImage.gameObject.SetActive(true);
            Destroy(smallKey.gameObject);
            pressEToPickUp.gameObject.SetActive(false);
        }
       if (canGetBigKey)
        {
            hasBigKey = true;
           bigKeyUIImage.gameObject.SetActive(true);
            Destroy(bigKey.gameObject);
            pressEToPickUp.gameObject.SetActive(false);
        }
    }
}
