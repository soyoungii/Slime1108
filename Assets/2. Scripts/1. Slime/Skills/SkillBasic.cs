using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SkillBasic : MonoBehaviour
{
    [Header("�⺻ ��ų ����")]
    protected float skillDamage;
    protected float cooldown; //��Ÿ��
    protected bool isSkillActive = false; //��ų Ȱ��ȭ
    public bool isPossible = false;  //��ų �ر�

    [Header("UI ����")]
    public Image skillIcon;  // ��ų ������ �̹���
    protected bool isCooldown = false;
    protected float cooldownTimeLeft;

    protected virtual void Start()
    {
        skillDamage = GameManager.Instance.slime.damage;
        if (skillIcon != null)
        {
            skillIcon.type = Image.Type.Filled;
            skillIcon.fillMethod = Image.FillMethod.Radial360;
            skillIcon.fillOrigin = (int)Image.Origin360.Top;
            skillIcon.fillClockwise = false;
        }
    }
    protected virtual void Update()
    {
        if (isCooldown)
        {
            ApplyCooldown();
        }

    }
    public abstract void ActivateSkill();
    protected abstract IEnumerator SkillRoutine();

    protected void ApplyCooldown()
    {
        cooldownTimeLeft -= Time.deltaTime;

        if (skillIcon != null)
        {
            skillIcon.fillAmount = cooldownTimeLeft / cooldown;
        }

        if (cooldownTimeLeft <= 0)
        {
            isCooldown = false;
            skillIcon.fillAmount = 1f;
        }
    }

    protected void StartCooldown()
    {
        isCooldown = true;
        cooldownTimeLeft = cooldown;
    }
}
