using UnityEngine;
using UnityEngine.InputSystem;

public class Quitapplication : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.escapeKey.isPressed)
        {
            Debug.Log("esc key is pressed!");
            Application.Quit();
        }
    }
}
