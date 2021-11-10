using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InimigoPrototipo : MonoBehaviour
{
    public float health;
    private float m_health;
    public bool boss;
    public GameObject healthBar;
    public Slider slider;
    public Slider sliderBack;
    public EnemyManager enemyManager;
    public DieState dieState;
    public UnityEvent events;

    private Animator animator => GetComponent<Animator>();
    private float inv;

    private void Start()
    {
        slider.maxValue = health;
        sliderBack.maxValue = health;
        healthBar.SetActive(false);
    }

    private void Update()
    {
        if (inv > 0)
        {
            inv -= Time.deltaTime;
        }

        slider.value = health;
        sliderBack.value = m_health;

        if (health > 0)
        {          
            if(!boss) healthBar.transform.LookAt(Camera.main.transform);

            if(m_health > health)
            {
                m_health -= 1.2f * Time.deltaTime;
            }
        }
        if(health <= 0)
        {
            health = 0;
            m_health = 0;
            if (boss)
            {
                enemyManager.currentState = dieState;
                events.Invoke();
            }
            else
            {
                enemyManager.currentState = dieState;

                //Destroy(gameObject);

            }

        }

    }

    //public void PlayAnimation(int value)
    //{
    //    Debug.Log("Play damage");
    //    animator.SetInteger("Animation", value);
    //}

    //public void ResetAnimation()
    //{
    //    animator.SetInteger("Animation", 0);
    //}

    public void Damage(int damage)
    {
        if (inv <= 0)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Espada hit 2");
            m_health = health;
            health -= damage;
            inv = 0.5f;
        }
        healthBar.SetActive(true);
    }
}
