using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShootScript : NetworkBehaviour
{
    public float fireRate = 1f;
    public float range = 100f;
    public LayerMask mask;

    private float fireFactor = 0f;
    private GameObject mainCamera;
    void Start()
    {
        Camera mainCamera = GetComponentInChildren<Camera>();
    }

    [Command] void Cmd_PlayerShot(string _id)
    {
        Debug.Log("Player" +_id +"has been shot");
    }
   /* [Client] void Shoot()
    {
        if (Physics.Raycast())
        {

        }
    }*/
    void Update()
    {
        
    }
}
