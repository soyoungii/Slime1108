using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Sphere : MonoBehaviour
{
    public GameObject spherePrefab;
    public Image cooldownImage;
    private float cooldown = 3f;
    private Slime slime;

    private float sphereSpeed = 0.4f;
    private float pierce = 10;

    private void Awake()
    {
        slime = FindObjectOfType<Slime>();
    }

    private void Update()
    {
        transform.Translate(Vector2.right * sphereSpeed * Time.deltaTime);
    }
    public void StartSkill()
    {
        StartCoroutine(SphereCoroutine());
    }

    private IEnumerator SphereCoroutine()
    {
        while (true)
        {
            cooldownImage.fillAmount = 1f;
            GameObject sphere = Instantiate(spherePrefab, slime.transform.position, Quaternion.identity);
            Sphere sphereScript = sphere.GetComponent<Sphere>();
            if (sphereScript != null)
            {
                //sphereScript.SetProperties(slime.damage * 1.2f, sphereSpeed, pierce);
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

    private float damage;
    private float speed;

    public void SetProperties(float damageAmount, float moveSpeed, int pierceCount)
    {
        damage = damageAmount;
        speed = moveSpeed;
        pierce = pierceCount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Monster>(out Monster monster))
        {
            monster.TakeDamage(damage);
            pierce--;
            if (pierce <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
