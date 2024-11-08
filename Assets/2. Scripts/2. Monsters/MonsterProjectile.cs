using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterProjectile : MonoBehaviour
{
    //private float rotateSpeed = 350f;
    //private float damage;
    //private Vector2 direction;
    //private float speed;
    //private bool hasHit = false;

    //public void Initialize(Vector2 direction, float speed, float damage)
    //{
    //    this.direction = direction;
    //    this.speed = speed;
    //    this.damage = damage;
    //}

    //private void Update()
    //{
    //    transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);

    //    if (hasHit) return;
    //    transform.Translate(direction * speed * Time.deltaTime);
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (hasHit) return;

    //    if (collision.CompareTag("Player"))
    //    {
    //        hasHit = true;
    //        Slime slime = collision.GetComponent<Slime>();
    //        if (slime != null)
    //        {
    //            slime.TakeDamage(damage);
    //        }
    //        Destroy(gameObject);
    //    }
    //    else if (!collision.CompareTag("Monster") && !collision.CompareTag("ProjectilePassthrough"))
    //    {
    //        hasHit = true;
    //        Destroy(gameObject);
    //    }
    //}
}
