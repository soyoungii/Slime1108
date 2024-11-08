using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;
    internal List<Monster> monsters = new List<Monster>();   //���� �����ϴ� ��ü �� List
    internal Slime slime;  //���� �����ϴ� ������ ��ü

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(this);
            return;
        }
        DontDestroyOnLoad(gameObject);

        slime = FindObjectOfType<Slime>();
    }

    private void Start()
    {
        //slime = FindObjectOfType<Slime>();
    }

}
