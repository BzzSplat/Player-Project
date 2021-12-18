using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    public Transform player;
    public float Sensitivity;
    float xRotation = 0f;
    bool freeCursor = false;
    public bool lockCamera = false;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 100, 300, 300), "CONTROLS:\nTab - Free Mouse\nLMB - Throw Rock\nRMB - Throw Fireball");
    }

    void Update()
    {
        if (lockCamera)
            return;

        float mouseX = Input.GetAxis("Mouse X") * Sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * Sensitivity * Time.deltaTime;

        xRotation -= mouseY * Sensitivity;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * mouseX * Sensitivity);

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (freeCursor)
            {
                freeMouse();
            }
            else
            {
                trapMouse();
            }
        }

    }

    public void freeMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        freeCursor = false;
    }
    public void trapMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        freeCursor = true;
    }
}
