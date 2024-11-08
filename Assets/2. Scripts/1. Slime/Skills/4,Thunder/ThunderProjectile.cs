using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderProjectile : MonoBehaviour
{
    private float thunderDamage;

    public void Initialize(float damage)
    {
        thunderDamage = damage;
        Destroy(gameObject, 1f); // 1√  »ƒ ∫≠∂Ù ¿Ã∆Â∆Æ ¡¶∞≈
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Monster>(out Monster monster))
        {
            monster.TakeDamage(thunderDamage);
        }
    }
}
