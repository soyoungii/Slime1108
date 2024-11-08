using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Starlight : MonoBehaviour
{
    public GameObject starlightPrefab;
    public Image cooldownImage;
    private float cooldown = 7f;
    private Slime slime;

    private void Awake()
    {
        slime = FindObjectOfType<Slime>();
    }
    public void StartSkill()
    {
        StartCoroutine(StarlightCoroutine());
    }

    private IEnumerator StarlightCoroutine()
    {
        while (true)
        {
            cooldownImage.fillAmount = 1f;
            for (int i = 0; i < 10; i++)
            {
                GameObject starlight = Instantiate(starlightPrefab, starlightPrefab.transform.position, Quaternion.identity);
                Projectile projectile = starlight.GetComponent<Projectile>();
                if (projectile != null)
                {
                    Monster target = FindNearestMonster();
                    if (target != null)
                    {
                        projectile.Initialize(starlight.transform.position, target.transform.position, 10f, slime.damage * 1.5f);
                    }
                }
                yield return new WaitForSeconds(0.1f);
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
