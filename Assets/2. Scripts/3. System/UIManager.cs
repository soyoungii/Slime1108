using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class UIManager : SingletonManager<UIManager>
{
    [Header("ȭ�� ���� ��� ������ ����")]
    public Text gold; //���� ���(���� ���)
    public Text myDamage; //���� �÷��̾��� ���ݷ�(���� ���)

    [Header("��ȭâ ������ ����")]
    public Text damageLevel; //��ȭ ȭ�� �� �÷��̾��� ������ ����
    public Text damageText; //��ȭ ȭ�� �� �÷��̾��� ������(���ݷ�)
    public Text damageGold; //��ȭ ȭ�� �� ������ ��ȭ�� �ʿ��� ���

    [Header("��ȭâ ü�� ����")]
    public Text hpLevel; //��ȭ ȭ�� �� �÷��̾��� Hp ����
    public Text hpText; //��ȭ ȭ�� �� �÷��̾��� Max Hp
    public Text hpGold; //��ȭ ȭ�� �� hp ��ȭ�� �ʿ��� ���

    [Header("��ȭâ ü�� ȸ�� ����")]
    public Text hpRecoverLevel; //��ȭ ȭ�� �� �÷��̾��� ü�� ȸ�� ����
    public Text hpRecoverText; //��ȭ ȭ�� �� �÷��̾��� ü�� ȸ����
    public Text hpRecoverGold; //��ȭ ȭ�� �� ü��ȸ�� ��ȭ�� �ʿ��� ���

    [Header("��ȭâ ġ��Ÿ Ȯ�� ����")]
    public Text criticalLevel; //��ȭ ȭ�� �� �÷��̾��� ġ��Ÿ Ȯ�� ����
    public Text criticalText; //��ȭ ȭ�� �� �÷��̾��� ġ��Ÿ Ȯ��
    public Text criticalGold; //��ȭ ȭ�� �� ġ��Ÿ Ȯ�� ��ȭ�� �ʿ��� ���

    [Header("��ȭâ ġ��Ÿ ���� ����")]
    public Text criDamLevel; //��ȭ ȭ�� �� �÷��̾��� ġ��Ÿ ���� ����
    public Text criDamText; //��ȭ ȭ��  �� �÷��̾��� ġ��Ÿ ����
    public Text criDamGold; //��ȭ ȭ�� �� ġ��Ÿ ���� ��ȭ�� �ʿ��� ���

    [Header("��ȭâ ���� �ӵ� ����")]
    public Text atkSpeedLevel; //��ȭ ȭ�� �� �÷��̾��� ���� �ӵ� ����
    public Text atkSpeedText; //��ȭ ȭ�� �� �÷��̾��� ���� �ӵ�
    public Text atkSpeedGold; //��ȭ ȭ�� �� ���� �ӵ� ��ȭ�� �ʿ��� ���

    [Header("��ȭâ ���� ����")]
    public Text dShotLevel; //��ȭ ȭ�� �� �÷��̾��� ���� ����
    public Text dShotText; //��ȭ ȭ�� �� �÷��̾��� ���� Ȯ��
    public Text dShotGold; //��ȭ ȭ�� �� ���� ��ȭ�� �ʿ��� ���

    [Header("��ų �ڹ��� �̹���")]
    public GameObject lockStarlight; //��Ÿ����Ʈ ��ų �ڹ��� �̹���
    public GameObject lockSphere;
    public GameObject lockMeteor;
    public GameObject lockThunder;
    public GameObject lockAnger;

    [Header("��ų �ر�â UI")]
    public GameObject starlightUnlock; // ��Ÿ����Ʈ ��ų �ر�â UI
    public GameObject sphereUnlock; //��ü ��ų �ر�â UI
    public GameObject meteorUnlock; //���׿� �ر�â UI
    public GameObject thunderUnlock; //���� �ر�â UI
    public GameObject angerUnlock; //�г� �ر�â UI

    [Header("��ų �ر�â ��� �̹���(�ر� �� ����)")]
    public GameObject starlightGoldImage; //��Ÿ����Ʈ ��� �̹���(�ر� �� ������)
    public GameObject sphereGoldImage;
    public GameObject meteorGoldImage;
    public GameObject thunderGoldImage;
    public GameObject angerGoldImage;

    [Header("��ų �ر�â ��� ����(�ر� �� ����)")]
    public Text starlightTop; //��Ÿ����Ʈ ��� ����(�ر� �� �����)
    public Text sphereTop;
    public Text meteorTop;
    public Text thunderTop;
    public Text angerTop;

    [Header("��ų �ر�â �ϴ� ��ư1(�ر� �� ����)")]
    public GameObject starlightBottom; //��Ÿ����Ʈ �ϴ� ��ư(�ر� �� ������)
    public GameObject sphereBottom;
    public GameObject meteorBottom;
    public GameObject thunderBottom;
    public GameObject angerBottom;

    [Header("��ų �ر�â �ϴ� ��ư2(�ر� �� ����)")]
    public Text starlightCloseScreen; //��Ÿ����Ʈ â�ݱ� ����(�ر� �� �����)
    public Text sphereCloseScreen;
    public Text meteorCloseScreen;
    public Text thunderCloseScreen;
    public Text angerCloseScreen;

    [Header("��ų �ر� �� ��ȭ ���� UI")]
    public GameObject upgradeNoGold; //��ȭ ���� UI
    public GameObject skillNoGold; //��ų �ر� ���� UI

    [Header("ȭ�� ������ ��� ��ư")]
    public GameObject speed2xText;
    public GameObject pauseexit;
}
