using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Respawner : MonoBehaviour
{
    public Transform RespawnTarget;
    public GameObject Player;


    void OnTriggerEnter(Collider other) 
    {
        Player.transform.position = RespawnTarget.transform.position;
    }
}
