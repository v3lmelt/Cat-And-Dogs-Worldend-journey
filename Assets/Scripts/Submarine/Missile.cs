using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 200f;
    public int damage = 50;

    private Transform _target;

    private void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Boss2").transform;

        if (_target == null)
        {
            Debug.LogError("Boss not found!");
        }
    }

    private void Update()
    {
        Seek();
    }

    private void Seek()
    {
        Vector2 direction = (_target.position - transform.position).normalized;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), rotationSpeed * Time.deltaTime);
        transform.Translate(Vector2.right * (speed * Time.deltaTime));
    }
    
    public void SetInitialDirection(float direction)
    {
        transform.localScale = new Vector3(direction, 1f, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boss2"))
        {
            collision.GetComponent<Tentacle>().BeHit(damage);
            Destroy(gameObject);
        }
    }
}
