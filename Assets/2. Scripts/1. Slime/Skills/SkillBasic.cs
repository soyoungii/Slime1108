using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SkillBasic : MonoBehaviour
{
    [Header("기본 스킬 설정")]
    protected float skillDamage;
    protected float cooldown; //쿨타임
    protected bool isSkillActive = false; //스킬 활성화
    public bool isPossible = false;  //스킬 해금

    [Header("UI 설정")]
    public Image skillIcon;  // 스킬 아이콘 이미지
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
