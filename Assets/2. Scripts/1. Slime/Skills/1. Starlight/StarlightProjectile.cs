using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarlightProjectile : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Monster>(out Monster monster))
        {
            monster.TakeDamage(GameManager.Instance.slime.damage * 1.5f);
            Destroy(gameObject);
        }
    }
}
