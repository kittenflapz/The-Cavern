using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject winMenu;

    bool canWin = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Awake()
    {

        // fix input fields in webgl build
#if !UNITY_EDITOR && UNITY_WEBGL
            WebGLInput.captureAllKeyboardInput = true;
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnWin()
    {
        winMenu.SetActive(true);
    }
}
