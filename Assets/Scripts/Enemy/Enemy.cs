using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Spaceship
{
    [SerializeField] private Rigidbody rigidBody;
    private void Move()
    {
        if (spaceshipData.speed > 0)
        {
            transform.Translate(Vector3.up * (spaceshipData.speed * Time.deltaTime),Space.Self);
        }
    }

    private new void Update()
    {
        base.Update();

        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            OnTakeDamage(10); // TODO Get player power
            Destroy(other.gameObject);

        }
    }
}
