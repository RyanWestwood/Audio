using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInput : MonoBehaviour
{
    public GameObject menu;
    public ControlSwitch camera_script;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && menu.activeInHierarchy == false)
        {
            camera_script.Toggle();
            menu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}