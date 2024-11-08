using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Meteor : MonoBehaviour
{
    public GameObject meteorPrefab;
    public Image cooldownImage;
    private float cooldown = 3f;
    private Slime slime;
    private float meteorSpeed = 10f;

    private void Awake()
    {
        slime = FindObjectOfType<Slime>();
    }

    public void StartSkill()
    {
        StartCoroutine(MeteorCoroutine());
    }

    private IEnumerator MeteorCoroutine()
    {
        while (true)
        {
            cooldownImage.fillAmount = 1f;
            Monster target = FindNearestMonster();
            if (target != null)
            {
                // ���׿��� �������� �⺻ ��ġ�� ����
                GameObject meteor = Instantiate(meteorPrefab, meteorPrefab.transform.position, Quaternion.identity);

                // ���׿� �������� ���� �ڷ�ƾ ����
                StartCoroutine(MoveMeteor(meteor, target.transform.position));
            }
            yield return StartCoroutine(Cooldown());
        }
    }

    private IEnumerator MoveMeteor(GameObject meteor, Vector3 targetPosition)
    {
        Vector3 startPos = meteor.transform.position;
        float journeyLength = Vector3.Distance(startPos, targetPosition);
        float startTime = Time.time;

        while (meteor != null)
        {
            float distanceCovered = (Time.time - startTime) * meteorSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;

            if (fractionOfJourney >= 1f)
            {
                if (meteor != null)
                {
                    Collider2D[] hits = Physics2D.OverlapCircleAll(targetPosition, 1f);
                    foreach (Collider2D hit in hits)
                    {
                        if (hit.TryGetComponent<Monster>(out Monster monster))
                        {
                            monster.TakeDamage(slime.damage * 12f);
                        }
                    }
                    Destroy(meteor);
                }
                break;
            }

            if (meteor != null)
            {
                meteor.transform.position = Vector3.Lerp(startPos, targetPosition, fractionOfJourney);
                meteor.transform.Rotate(0, 0, 360 * Time.deltaTime);
            }

            yield return null;
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

    private Monster FindNearestMonster()
    {
        Monster nearestMonster = null;
        float nearestDistance = float.MaxValue;

        foreach (Monster monster in FindObjectsOfType<Monster>())
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
