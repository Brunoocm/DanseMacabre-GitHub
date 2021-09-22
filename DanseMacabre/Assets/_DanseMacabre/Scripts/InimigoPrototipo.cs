using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InimigoPrototipo : MonoBehaviour
{
    public float health;
    private float m_health;
    public GameObject healthBar;
    public Slider slider;
    public Slider sliderBack;
    private Animator animator => GetComponent<Animator>();

    private void Start()
    {
        slider.maxValue = health;
        sliderBack.maxValue = health;
        healthBar.SetActive(false);
    }

    private void Update()
    {
        if(health > 0)
        {
            slider.value = health;
            sliderBack.value = m_health;
            healthBar.transform.LookAt(Camera.main.transform);

            if(m_health > health)
            {
                m_health -= 1.2f * Time.deltaTime;
            }
        }
        if(health <= 0)
        {
            Destroy(gameObject);
        }

    }

    public void PlayAnimation(int value)
    {
        Debug.Log("Play damage");
        animator.SetInteger("Animation", value);
    }

    public void ResetAnimation()
    {
        animator.SetInteger("Animation", 0);
    }

    public void Damage(int damage)
    {
        m_health = health;
        health -= damage;
        healthBar.SetActive(true);
    }
}
