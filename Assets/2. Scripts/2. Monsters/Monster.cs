using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [Tooltip("ĳ���Ϳ��� �ִ� ���ط�")]
    public float damage = 0; //���ݷ�
    [Tooltip("���� ü��")]
    public float hp = 0.9f; //ü��
    public float moveSpeed; //�̵��ӵ�

    private Transform target; //���� ���(������)
    private Rigidbody2D rb;
    public ParticleSystem monsterDieParticle;

    Slime slime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    private void Start()
    {
        //GameManager.Instance.enemies.Add(this);
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        MonsterMove();
    }

    public void SetHp(float newHp)
    {
        hp = newHp;
    }

    public void SetDamage(float newDamage)
    {
        damage = newDamage;
    }
    public void MonsterMove()
    {
        Vector3 direction = target.position - transform.position;
        direction.Normalize();

        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    public void StopMonster()
    {
        moveSpeed = 0f;
        //Vector3.Distance(transform.position, target.position);
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
    }

    public void SlimeDie()
    {
        damage = 1;
        hp = 1;
    }

    public void Die()
    {
        Destroy(gameObject);
        GameManager.Instance.slime.gold += 10;
        var particle = Instantiate(monsterDieParticle, transform.position, Quaternion.identity);
        particle.Play();
        Destroy(particle.gameObject, 2f);
        //LeanPool.Despawn(gameObject); -> leanpool ���� �̰ɷ� �ٲٱ�
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SlimeMonDist"))
        {
            StopMonster();
        }
    }


}