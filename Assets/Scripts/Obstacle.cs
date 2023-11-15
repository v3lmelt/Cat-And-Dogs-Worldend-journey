using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(ObstacleDamage))]
public class Obstacle : MonoBehaviour
{
    ObstacleDamage damageable;
    Rigidbody2D rb;
    Animator animator;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        damageable = GetComponent<ObstacleDamage>();
    }

    // Update is called once per frame
 
    public void onHit(int damage, Vector2 knockback)
    {
       //
    }
}
