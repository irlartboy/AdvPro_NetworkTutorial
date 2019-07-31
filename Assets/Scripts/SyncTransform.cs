using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SyncTransform : NetworkBehaviour
{
    public float lerpRate = 15;

    [SyncVar] private Vector3 syncPosition;
    [SyncVar] private Quaternion syncRotation;

    public float positionThreshold = 0.5f;
    public float rotationThershold = 5.0f;

    private Vector3 lastPos;
    private Quaternion lastRot;

    private Rigidbody rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void LerpPosition()
    {
        if (!isLocalPlayer)
        {
            rigid.position = Vector3.Lerp(rigid.position, syncPosition, Time.deltaTime * lerpRate);
        }
        
    }
    void LerpRotation()
    {
        if (!isLocalPlayer)
        {
            rigid.rotation = Quaternion.Lerp(rigid.rotation, syncRotation, Time.deltaTime * lerpRate);
        }
    }
    [Command] void CmdSendPositionToServer(Vector3 _pos)
    {
        syncPosition = _pos;
        Debug.Log("Pos Cmd");
    }
    [Command] void CmdSendRotationToServer(Quaternion _rot)
    {
        syncRotation = _rot;
        Debug.Log("Rot Cmd");
    }
    [ClientCallback] void TransmitPosition()
    {
        if (isLocalPlayer)
        {
            CmdSendPositionToServer(rigid.position);
            lastPos = rigid.position;
        }
    }
    [ClientCallback] void TransmitRotation()
    {
        if (isLocalPlayer)
        {
            CmdSendRotationToServer(rigid.rotation);
            lastRot = rigid.rotation;
        }
    }
    void FixedUpdate()
    {
        TransmitPosition();
        LerpPosition();

        TransmitRotation();
        LerpRotation();
    }
}
