using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerScript : NetworkBehaviour
{

    public float movementSpeed = 10.0f;
    public float rotationSpeed = 10.0f;
    public float jumpHeight = 7.0f;
    private bool isGrounded = false;
    private Rigidbody rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();

        AudioListener audioListener = GetComponentInChildren<AudioListener>();
        Camera camera = GetComponentInChildren<Camera>();

        if (isLocalPlayer)
        {
            //enable everything
            camera.enabled = true;
            audioListener.enabled = true;
        }
        else
        {
            //disable everything
            camera.enabled = false;
            audioListener.enabled = false;
        }
    }
    private void Move(KeyCode _key)
    {
        Vector3 posistion = rigid.position;
        Quaternion rotation = rigid.rotation;

        switch (_key)
        {
            case KeyCode.W:
                posistion += transform.forward * movementSpeed * Time.deltaTime;
                break;
            case KeyCode.S:
                posistion += -transform.forward * movementSpeed * Time.deltaTime;
                break;
            case KeyCode.A:
                // rotation *= Quaternion.AngleAxis(-rotationSpeed, Vector3.up);
                posistion += -transform.right * movementSpeed * Time.deltaTime;
                break;
            case KeyCode.D:
                // rotation *= Quaternion.AngleAxis(rotationSpeed, Vector3.up);
                posistion += transform.right * movementSpeed * Time.deltaTime;
                break;
            case KeyCode.Space:
                if (isGrounded)
                {
                    rigid.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
                    isGrounded = false;
                }
                break;
        }
        rigid.MovePosition(posistion);
        rigid.MoveRotation(rotation);

    }
    void HandleInput()
    {
        KeyCode[] keys =
        {
            KeyCode.W,
            KeyCode.S,
            KeyCode.A,
            KeyCode.D,
            KeyCode.Space
        };
        foreach (var key in keys)
        {
            if (Input.GetKey(key))
            {
                Move(key);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            HandleInput();
        }
    }
}
