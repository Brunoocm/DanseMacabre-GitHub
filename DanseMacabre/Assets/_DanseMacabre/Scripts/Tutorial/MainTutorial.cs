using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTutorial : MonoBehaviour
{
    [Header("Movimentacao")]
    public GameObject moveObj;
    private GameObject m_moveObj;
    private GameObject W, A, S, D;
    private bool moveTutorial;

    [Header("Sword")]
    public bool hasSword;

    void Start()
    {
        StartMovementTutorial();
    }

    void Update()
    {
        DesactiveKeyCode();
    }

    void StartMovementTutorial()
    {
        Instantiate(moveObj);

        m_moveObj = GameObject.Find("MovimentacaoTutorial");

        W = GameObject.Find("W");
        A = GameObject.Find("A");
        S = GameObject.Find("S");
        D = GameObject.Find("D");
    }

    void DesactiveKeyCode()
    {
        if (!moveTutorial)
        {
            if (Input.GetKey(KeyCode.W)) W.SetActive(false);

            if (Input.GetKey(KeyCode.A)) A.SetActive(false);

            if (Input.GetKey(KeyCode.S)) S.SetActive(false);

            if (Input.GetKey(KeyCode.D)) D.SetActive(false);

            if(!W.activeSelf && !A.activeSelf && !S.activeSelf && !D.activeSelf)
            {
                moveTutorial = true;
                m_moveObj.SetActive(false);
            }
        }
    }

}
