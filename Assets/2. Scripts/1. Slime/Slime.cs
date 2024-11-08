using Lean.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Slime : MonoBehaviour
{
    [Header("�⺻ ����")]
    public float damage = 1; //���ݷ� -> ������ 1 ����

    public float maxHp = 5; //�ִ� ü�� -> ������ 5 ����
    public float currentHp; //����ü��
    public float hpRecover = 0; //ü�� ȸ���� -> ������ 0.6 ����

    public float critical = 0; //ġ��Ÿ Ȯ�� -> Max��: 100 -> ������ 1%����
    public float criticalDamage = 100; //ġ��Ÿ ���� -> ������ 1% ����

    public float attackSpeed = 1; //���ݼӵ� ������ 0.1 ���� -> ������ 0.1 ���� / ���� 2���Ǹ� 1�ʿ� 2��
    public float doubleShot = 0; //���� -> ���� Ƚ���� ����? -> ������ 1%����

    public float gold = 0;

    [Header("���� ����")]
    public int damageLevel = 1;
    public int hpLevel = 1;
    public int hpRecoverLevel = 1;
    public int criticalLevel = 1;
    public int criDamLevel = 1;
    public int atkSpeedLevel = 1;
    public int dShotLevel = 1;

    [Header("UI")]
    public Text goldText;
    public Text myDamageText;

    [Header("��ƼŬ")]
    public ParticleSystem hpRecoverParticle;
    public ParticleSystem levelupParticle;
    public ParticleSystem respawnEffect;

    [Header("����ü")]
    public GameObject projectilePrefab;
    public float detectionRange = 6f; // Ž�� ����
    public float projectileSpeed = 10f;
    private float nextFireTime;

    private Vector3 respawnPosition;
    private Spawner spawner;
    private void Start()
    {
        currentHp = maxHp;
        StartCoroutine(HpRecoveryStart());
    }
    private void Update()
    {
        UIManager.Instance.gold.text = gold.ToString();
        UIManager.Instance.myDamage.text = damage.ToString();

        UIManager.Instance.damageText.text = damage.ToString();
        UIManager.Instance.hpText.text = maxHp.ToString();
        UIManager.Instance.hpRecoverText.text = hpRecover.ToString();
        UIManager.Instance.criticalText.text = critical.ToString();
        UIManager.Instance.criDamText.text = criticalDamage.ToString();
        UIManager.Instance.atkSpeedText.text = attackSpeed.ToString();
        UIManager.Instance.dShotText.text = doubleShot.ToString();


        UIManager.Instance.damageLevel.text = damageLevel.ToString();
        UIManager.Instance.hpLevel.text = hpLevel.ToString();
        UIManager.Instance.hpRecoverLevel.text = hpRecoverLevel.ToString();
        UIManager.Instance.criticalLevel.text = criticalLevel.ToString();
        UIManager.Instance.criDamLevel.text = criDamLevel.ToString();
        UIManager.Instance.atkSpeedLevel.text = atkSpeedLevel.ToString();
        UIManager.Instance.dShotLevel.text = dShotLevel.ToString();

        UIManager.Instance.damageGold.text = ((damageLevel + 1) * 10).ToString();
        UIManager.Instance.hpGold.text = ((hpLevel + 1) * 10).ToString();
        UIManager.Instance.hpRecoverGold.text = ((hpRecoverLevel + 1) * 10).ToString();
        UIManager.Instance.criticalGold.text = ((criticalLevel + 1) * 10).ToString();
        UIManager.Instance.criDamGold.text = ((criDamLevel + 1) * 10).ToString();
        UIManager.Instance.atkSpeedGold.text = ((atkSpeedLevel + 1) * 10).ToString();
        UIManager.Instance.dShotGold.text = ((dShotLevel + 1) * 10).ToString();



        if (Time.time >= nextFireTime)
        {
            AttackNearestMonster();
        }
    }

    private IEnumerator AutoAttackRoutine()
    {
        while (true)
        {
            if (Time.time >= nextFireTime)
            {
                AttackNearestMonster();
            }
            yield return null;
        }
    }

    private void AttackNearestMonster()
    {
        Monster nearestMonster = null;
        float nearestDistance = detectionRange;

        foreach (Monster monster in FindObjectsOfType<Monster>())
        {
            float distance = Vector2.Distance(transform.position, monster.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestMonster = monster;
            }
        }

        if (nearestMonster != null)
        {
            FireProjectile(nearestMonster.transform.position);
            nextFireTime = Time.time + (1f / attackSpeed);
        }
    }

    private void FireProjectile(Vector3 targetPosition)
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectileScript = projectile.GetComponent<Projectile>();

        // ũ��Ƽ�� ���
        bool isCritical = Random.Range(0f, 100f) < critical;
        float finalDamage = damage;
        if (isCritical)
        {
            finalDamage *= (criticalDamage / 100f); // ġ��Ÿ ���� ���� ����
        }

        projectileScript.Initialize(transform.position, targetPosition, projectileSpeed, finalDamage);

        // ���� ó��
        if (Random.Range(0f, 100f) < doubleShot)
        {
            StartCoroutine(FireSecondProjectile(targetPosition, finalDamage));
        }
    }

    private IEnumerator FireSecondProjectile(Vector2 targetPosition, float damage)
    {
        yield return new WaitForSeconds(0.1f);
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.Initialize(transform.position, targetPosition, projectileSpeed, damage);
    }


    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (spawner != null)
        {
           spawner.ResetMonsterStats();
        }

        Monster[] allMonsters = FindObjectsOfType<Monster>();
        foreach (Monster monster in allMonsters)
        {
            monster.SlimeDie();
        }

        currentHp = maxHp;
        transform.position = respawnPosition;

        if (respawnEffect != null)
        {
            Instantiate(respawnEffect, transform.position, Quaternion.identity);
        }
    }
  
    public void DamageLevelUp()
    {
        if (gold >= (damageLevel + 1) * 10)
        {
            damage += 1;
            gold -= (damageLevel + 1) * 10;
            damageLevel++;
            LevelUpEffect();
        }

        else
        {
            UIManager.Instance.upgradeNoGold.SetActive(true);
        }
    }

    public void HpLevelUp()
    {
        if (gold >= (hpLevel + 1) * 10)
        {
            maxHp += 5;
            gold -= (hpLevel + 1) * 10;
            hpLevel++;
            LevelUpEffect();
        }

        else
        {
            UIManager.Instance.upgradeNoGold.SetActive(true);
        }
    }

    public void HpRecoverLevelUp()
    {
        if (gold >= (hpRecoverLevel + 1) * 10)
        {
            hpRecover += 0.6f;
            gold -= (hpRecoverLevel + 1) * 10;
            hpRecoverLevel++;
            LevelUpEffect();
            StartCoroutine(HpRecoveryStart());
        }

        else
        {
            UIManager.Instance.upgradeNoGold.SetActive(true);
        }
    }

    public void CriticalChanceLevelUp()
    {
        if (gold >= (criticalLevel + 1) * 10)
        {
            critical = Mathf.Min(100f, critical + 1f); // �ִ� 100%�� ����
            gold -= (criticalLevel + 1) * 10;
            criticalLevel++;
            LevelUpEffect();
        }
        else
        {
            UIManager.Instance.upgradeNoGold.SetActive(true);
        }
    }

    public void criDamLevelUp()
    {
        if (gold >= (criDamLevel + 1) * 10)
        {
            criticalDamage += 1;
            gold -= (criDamLevel + 1) * 10;
            criDamLevel++;
            LevelUpEffect();
        }

        else
        {
            UIManager.Instance.upgradeNoGold.SetActive(true);
        }
    }

    public void AttackSpeedLevelUp()
    {
        if (gold >= (atkSpeedLevel + 1) * 10)
        {
            attackSpeed += 0.1f;
            gold -= (atkSpeedLevel + 1) * 10;
            atkSpeedLevel++;
            LevelUpEffect();
        }
        else
        {
            UIManager.Instance.upgradeNoGold.SetActive(true);
        }
    }

    public void DoubleShotLevelUp()
    {
        if (gold >= (dShotLevel + 1) * 10)
        {
            doubleShot = Mathf.Min(100f, doubleShot + 1f); // �ִ� 100%�� ����
            gold -= (dShotLevel + 1) * 10;
            dShotLevel++;
            LevelUpEffect();
        }
        else
        {
            UIManager.Instance.upgradeNoGold.SetActive(true);
        }
    }

    private IEnumerator HpRecoveryStart()
    {
        while (true)
        {
            if (currentHp < maxHp)
            {
                currentHp = Mathf.Min(maxHp, currentHp + hpRecover);
                if (hpRecoverParticle != null)
                {
                    var hpUp = Instantiate(hpRecoverParticle, transform.position, Quaternion.identity);
                    hpUp.Play();
                    Destroy(hpUp.gameObject, 1f);
                }
            }
            yield return new WaitForSeconds(5f);
        }
    }

    private void LevelUpEffect()
    {
        var particle = Instantiate(levelupParticle, transform.position, Quaternion.identity);
        particle.Play();
        Destroy(particle.gameObject, 2f);
    }

    public void StarlightUnlock()
    {
        if(gold >= 20)
        {
            Destroy(UIManager.Instance.lockStarlight.gameObject);
            Destroy(UIManager.Instance.starlightUnlock.gameObject);
            //��Ÿ����Ʈ ��ų �ڷ�ƾ ����
        }

       else
        {
            UIManager.Instance.skillNoGold.SetActive(true);
        }
    }

    public  void SphereUnlock()
    {
        if(gold>=10)
        {
            Destroy(UIManager.Instance.lockSphere.gameObject);
            Destroy(UIManager.Instance.sphereUnlock.gameObject);
            //���̵� ��ų �ڷ�ƾ ����
        }

        else
        {
            UIManager.Instance.skillNoGold.SetActive(true);
        }
    }

    public void MeteorUnlock()
    {
        if(gold>=30)
        {
            Destroy(UIManager.Instance.lockMeteor.gameObject);
            Destroy(UIManager.Instance.meteorUnlock.gameObject);
            //���׿� ��ų �ڷ�ƾ ����
        }

        else
        {
            UIManager.Instance.skillNoGold.SetActive(true);
        }
    }

    public void ThunderUnlock()
    {
        if (gold >= 40)
        {
            Destroy(UIManager.Instance.lockThunder.gameObject);
            Destroy(UIManager.Instance.thunderUnlock.gameObject);
            //���� ��ų �ڷ�ƾ ����
        }

        else
        {
            UIManager.Instance.skillNoGold.SetActive(true);
        }
    }

    public void AngerUnlock()
    {
        if (gold >= 10)
        {
            Destroy(UIManager.Instance.lockAnger.gameObject);
            Destroy(UIManager.Instance.angerUnlock.gameObject);
            //�г� ��ų �ڷ�ƾ ����
        }

        else
        {
            UIManager.Instance.skillNoGold.SetActive(true);
        }
    }
}




