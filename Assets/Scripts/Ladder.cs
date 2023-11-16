using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ladder : MonoBehaviour
{
    

    private void OnTriggerStay2D(Collider2D collision)
   
    {
        
        if(Input.GetKey(KeyCode.UpArrow) && collision.gameObject.name == "Cat")
        {
            collision.GetComponent<Rigidbody2D>().velocity = new Vector3(0,10,0);
        }
        if (Input.GetKey(KeyCode.W) && collision.gameObject.name == "Dog")
        {
            collision.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 10, 0);
        }
    }
}
