using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    private int monsterCount = 10; // �� ���� ������ ���� ��

    public GameObject monsterPrefab; // �� ������
    public float spawnInterval = 3f; 
    private float baseHp = 1f; 
    private float baseDamage = 1f;
    private int waveCount = 0;  

    private float spawnStartX = 3f; // ù ��° ���� ���� ��ġ
    public float monsterSpacing = 0.5f; // ���� �� ����

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");

            if (monsters.Length == 0)
            {
                waveCount++;
                Spawn(monsterCount); // ���� ���� ����ŭ ����
            }

            yield return new WaitForSeconds(spawnInterval); // ���� ����
        }
    }

    private void Spawn(int count)
    {
        for (int i = 0; i < count; i++)
        {
            float y = Random.Range(0.45f, 0.5f);
            float x = spawnStartX + (i * monsterSpacing);

            Vector2 spawnPos = new Vector2(x, y);
            GameObject monsterObject = Instantiate(monsterPrefab, spawnPos, Quaternion.identity);

            Monster monster = monsterObject.GetComponent<Monster>();
            if (monster != null)
            {
                float hp = baseHp * (1 + waveCount * 0.1f); // ���̺긶�� ü�� ����
                float damage = baseDamage * (1 + waveCount * 0.1f); // ���̺긶�� ������ ����
                monster.SetHp(hp);
                monster.SetDamage(damage);
            }
        }
    }

    public void ResetMonsterStats()
    {
        waveCount = 0;
    }
}
