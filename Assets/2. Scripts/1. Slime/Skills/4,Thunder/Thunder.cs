using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Thunder : MonoBehaviour
{
    public GameObject thunderPrefab;
    public Image cooldownImage;
    private float cooldown = 5f;
    private float detectionRange = 4f;
    private Slime slime;

    private void Awake()
    {
        slime = FindObjectOfType<Slime>();
    }

    public void StartSkill()
    {
        StartCoroutine(ThunderCoroutine());
    }

    //private IEnumerator ThunderCoroutine()
    //{
    //    while (true)
    //    {
    //        cooldownImage.fillAmount = 1f;
    //        for (int i = 0; i < 8; i++)
    //        {
    //            Monster target = FindNearestMonsterInRange();
    //            if (target != null)
    //            {
    //                Vector3 spawnPosition = target.transform.position + Vector3.up * 5f;
    //                GameObject thunder = Instantiate(thunderPrefab, spawnPosition, Quaternion.identity);
    //                target.TakeDamage(slime.damage);
    //                Destroy(thunder, 1f);
    //            }
    //            yield return new WaitForSeconds(0.5f);
    //        }
    //        yield return StartCoroutine(Cooldown());
    //    }
    //}

    private IEnumerator ThunderCoroutine()
    {
        while (true)
        {
            cooldownImage.fillAmount = 1f;
            for (int i = 0; i < 8; i++)
            {
                Monster target = FindNearestMonsterInRange();
                if (target != null)
                {
                    Vector3 spawnPosition = new Vector3(
                        target.transform.position.x,
                        target.transform.position.y,
                        target.transform.position.z
                    );
                    GameObject thunder = Instantiate(thunderPrefab, spawnPosition, Quaternion.identity);
                    target.TakeDamage(slime.damage);
                    Destroy(thunder, 1f);
                }
                yield return new WaitForSeconds(0.5f);
            }
            yield return StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
    {
        float elapsed = 0f;
        while (elapsed < cooldown)
        {
            elapsed += Time.deltaTime;
            cooldownImage.fillAmount = 1f - (elapsed / cooldown);
            yield return null;
        }
    }

    private Monster FindNearestMonsterInRange()
    {
        Monster nearestMonster = null;
        float nearestDistance = detectionRange;
        Monster[] monsters = FindObjectsOfType<Monster>();

        foreach (Monster monster in monsters)
        {
            float distance = Vector2.Distance(slime.transform.position, monster.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestMonster = monster;
            }
        }

        return nearestMonster;
    }
}
