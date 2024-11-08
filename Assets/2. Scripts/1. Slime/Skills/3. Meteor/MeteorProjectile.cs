using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorProjectile : MonoBehaviour
{
    private float meteorDamage;
    private Vector3 targetPos;
    private float moveSpeed = 10f;

    public void Initialize(float damage)
    {
        meteorDamage = damage;
        // 가장 가까운 몬스터 찾기
        FindTarget();
    }

    private void FindTarget()
    {
        float nearestDistance = Mathf.Infinity;
        Vector3 nearestMonsterPos = Vector3.zero;

        foreach (Monster monster in GameManager.Instance.monsters)
        {
            if (monster != null)
            {
                float distance = Vector2.Distance(transform.position, monster.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestMonsterPos = monster.transform.position;
                }
            }
        }

        targetPos = nearestMonsterPos;
    }

    private void Update()
    {
        if (targetPos != Vector3.zero)
        {
            // 타겟을 향해 이동
            Vector3 direction = (targetPos - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            // 타겟에 도달했는지 확인
            if (Vector3.Distance(transform.position, targetPos) < 0.1f)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Monster>(out Monster monster))
        {
            monster.TakeDamage(meteorDamage);
            Destroy(gameObject);
        }
    }
}
