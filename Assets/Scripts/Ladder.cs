using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ladder : MonoBehaviour
{
    PlayerInput Player;

    private void OnTriggerStay2D(Collider2D collision)
   
    {
        Player = collision.GetComponent<PlayerInput>();
        if(Input.GetKey(KeyCode.UpArrow))
        {
            collision.GetComponent<Rigidbody2D>().velocity = new Vector3(0,10,0);
        }
    }
}
