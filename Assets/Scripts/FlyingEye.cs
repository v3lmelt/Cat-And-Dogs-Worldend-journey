using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FlyingEye : MonoBehaviour
{
    public float flightSpeed = 2f;
    public float waypointReachedDistance = 0.1f;
    public DetectZone biteDetectionZone;
    public Collider2D deathCollider;
    public List<Transform> waypoints;


    Animator animator;
    Rigidbody2D rb;
    Damageable damageable;

    Transform nextWaypoint;
    int waypointNum = 0;

    public bool _hasTarget = false;

    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }

    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        damageable = GetComponent<Damageable>();
    }

    private void Start()
    {
        nextWaypoint = waypoints[waypointNum];
    }

    private void OnEnable()
    {
        damageable.damageableDeath.AddListener(onDeath);
    }

    // Update is called once per frame
    void Update()
    {
        HasTarget = biteDetectionZone.detectedColliders.Count > 0;
    }

    private void FixedUpdate()
    {
        if (damageable.IsAlive)
        {
            if (CanMove)
            {
                Flight();
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }
        /*else
        {
            rb.gravityScale = 2f;
            rb.velocity = new Vector2 (0, rb.velocity.y);
            deathCollider.enabled = true;
        }*/
    }

    private void Flight()
    {
        //飞向下一个点位
        Vector2 directionToWaypoint = (nextWaypoint.position - transform.position).normalized;

        //检查是否已经到达点位
        float distance = Vector2.Distance(nextWaypoint.position, transform.position);

        rb.velocity = directionToWaypoint * flightSpeed;
        UpdateDirection();

        //看是否我们需要换路径点
        if (distance <= waypointReachedDistance)
        {
            waypointNum++;

            if (waypointNum>=waypoints.Count)
            {
                //回到起始点
                waypointNum = 0;
            }

            nextWaypoint = waypoints[waypointNum];
        }
    }

    private void UpdateDirection()
    {
        Vector3 locScale = transform.localScale;
        if (transform.localScale.x > 0f)
        {
            if (rb.velocity.x< 0)
            {
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }
        else
        {
            if (rb.velocity.x > 0)
            {
                transform.localScale = new Vector3(-1*locScale.x,locScale.y,locScale.z);
            }
        }
    }

    public void onDeath()
    {
        rb.gravityScale = 2f;
        rb.velocity = new Vector2(0, rb.velocity.y);
        deathCollider.enabled = true;
    }
}
