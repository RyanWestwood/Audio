using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunctions : MonoBehaviour
{
    public GameObject menu;
    public ControlSwitch camera_script;

    public void Continue()
    {
        Debug.Log("Continue Clicked!\n");
        camera_script.Toggle();
        menu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
