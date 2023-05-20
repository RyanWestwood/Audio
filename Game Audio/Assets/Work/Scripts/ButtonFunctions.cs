using UnityEngine;

public class ButtonFunctions : MonoBehaviour
{
    public GameObject menu;
    public ControlSwitch camera_script;

    public void Continue()
    {
        camera_script.Toggle();
        menu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
