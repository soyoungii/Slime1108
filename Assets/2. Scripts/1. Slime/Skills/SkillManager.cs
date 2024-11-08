using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public Starlight starlight;
    public Sphere sphere;
    public Meteor meteor;
    public Thunder thunder;
    public Anger anger;
    public void UnlockStarlight()
    {
        if (GameManager.Instance.slime.gold >= 20)
        {
            GameManager.Instance.slime.gold -= 20;
            Destroy(UIManager.Instance.lockStarlight.gameObject);
            Destroy(UIManager.Instance.starlightBottom.gameObject);
            Destroy(UIManager.Instance.starlightGoldImage.gameObject);
            UIManager.Instance.starlightUnlock.SetActive(false);
            starlight.StartSkill();
            UIManager.Instance.starlightTop.text = "�رݵ� ��ų�Դϴ�".ToString();
            UIManager.Instance.starlightCloseScreen.text = "â�ݱ�".ToString();
        }
        else
        {
            UIManager.Instance.starlightUnlock.SetActive(false);
            UIManager.Instance.skillNoGold.SetActive(true);
        }
    }

public void UnlockSphere()
    {
        if (GameManager.Instance.slime.gold >= 10)
        {
            GameManager.Instance.slime.gold -= 10;
            Destroy(UIManager.Instance.lockSphere.gameObject);
            Destroy(UIManager.Instance.sphereBottom.gameObject);
            Destroy(UIManager.Instance.sphereGoldImage.gameObject);
            sphere.StartSkill();
            UIManager.Instance.sphereUnlock.SetActive(false);
            UIManager.Instance.sphereTop.text = "�رݵ� ��ų�Դϴ�".ToString();
            UIManager.Instance.sphereCloseScreen.text = "â�ݱ�".ToString();
        }
        else
        {
            UIManager.Instance.sphereUnlock.SetActive(false);
            UIManager.Instance.skillNoGold.SetActive(true);
        }
    }

    public void UnlockMeteor()
    {
        if (GameManager.Instance.slime.gold >= 30)
        {
            GameManager.Instance.slime.gold -= 30;
            Destroy(UIManager.Instance.lockMeteor.gameObject);
            Destroy(UIManager.Instance.meteorBottom.gameObject);
            Destroy(UIManager.Instance.meteorGoldImage.gameObject);
            meteor.StartSkill();
            UIManager.Instance.meteorUnlock.SetActive(false);
            UIManager.Instance.meteorTop.text = "�رݵ� ��ų�Դϴ�".ToString();
            UIManager.Instance.meteorCloseScreen.text = "â�ݱ�".ToString();
        }
        else
        {
            UIManager.Instance.meteorUnlock.SetActive(false);
            UIManager.Instance.skillNoGold.SetActive(true);
        }
    }

    public void UnlockThunder()
    {
        if (GameManager.Instance.slime.gold >= 40)
        {
            GameManager.Instance.slime.gold -= 40;
            Destroy(UIManager.Instance.lockThunder.gameObject);
            Destroy(UIManager.Instance.thunderBottom.gameObject);
            Destroy(UIManager.Instance.thunderGoldImage.gameObject);
            thunder.StartSkill();
            UIManager.Instance.thunderUnlock.SetActive(false);
            UIManager.Instance.thunderTop.text = "�رݵ� ��ų�Դϴ�".ToString();
            UIManager.Instance.thunderCloseScreen.text = "â�ݱ�".ToString();
        }
        else
        {
            UIManager.Instance.thunderUnlock.SetActive(false);
            UIManager.Instance.skillNoGold.SetActive(true);
        }
    }


    public void UnlockAnger()
    {
        if (GameManager.Instance.slime.gold >= 10)
        {
            GameManager.Instance.slime.gold -= 10;
            Destroy(UIManager.Instance.lockAnger.gameObject);
            Destroy(UIManager.Instance.angerBottom.gameObject);
            Destroy(UIManager.Instance.angerGoldImage.gameObject);
            anger.StartSkill();
            UIManager.Instance.angerUnlock.SetActive(false);
            UIManager.Instance.angerTop.text = "�رݵ� ��ų�Դϴ�".ToString();
            UIManager.Instance.angerCloseScreen.text = "â�ݱ�".ToString();
        }
        else
        {
            UIManager.Instance.angerUnlock.SetActive(false);
            UIManager.Instance.skillNoGold.SetActive(true);
        }
    }
}


