using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LookScript : NetworkBehaviour
{
    public float mouseSensitivity = 30.0f;

    public float minY = -60f;
    public float maxY = 90f;

    private float yaw = 0f;
    private float pitch = 0f;

    private GameObject mainCam;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Camera cam = GetComponentInChildren<Camera>();
        if (cam != null)
        {
            mainCam = cam.gameObject;
        }
    }

    private void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void HandleInput()
    {
        yaw   += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        pitch += Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        pitch = Mathf.Clamp(pitch, minY, maxY);
        
    }
    void Update()
    {
        if (isLocalPlayer)
        {
            HandleInput();
        }
    }
    private void LateUpdate()
    {
        if (isLocalPlayer)
        {
            transform.localEulerAngles = new Vector3(0, yaw, 0);
            mainCam.transform.localEulerAngles = new Vector3(-pitch, 0, 0);
        }
    }
}
