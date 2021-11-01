using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth;
    public float health;
    public float stamina;

    private float m_health;

    public InputSystem inputSystem;

    public Slider slider;
    public Slider sliderBack;

    public bool isLock;
    private float inv;


    Animator anim;
    Rigidbody rb;
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        inputSystem = GetComponent<InputSystem>();
        slider.maxValue = maxHealth;
        sliderBack.maxValue = maxHealth;
    }

    private void Update()
    {
        HealthBar();
    }
    void HealthBar()
    {
        //if (healthSystem.isLock)
        //    return;
        if (inv > 0)
        {
            inv -= Time.deltaTime;
        }

        slider.value = health;
        sliderBack.value = m_health;

        if (health > 0)
        {
            if (m_health > health)
            {
                m_health -= 1.2f * Time.deltaTime;
            }
        }
        if (health <= 0)
        {
            health = 0;
            m_health = 0;

            //MORRE MORRE MORRE

        }
    }
    public void TakeDamage(int dano)
    {
        if (inv <= 0)
        {
            inputSystem.enabled = false;
            m_health = health;
            health -= dano;
            PlayTargetAnimation("Falling", true);
            inv = 1;
        }
    }
    public void PlayTargetAnimation(string targetAnim, bool isInteracting)
    {
        anim.applyRootMotion = false;
        anim.SetBool("Base Layer", isInteracting);
        anim.CrossFade(targetAnim, 0.2f);
    }

    public void EnableMove()
    {
        inputSystem.enabled = true;

    }
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }
    public void LoadPlayer()
    {
        PlayerSave data = SaveSystem.LoadPlayer();

        health = data.heath;
        stamina = data.stamina;

        Vector3 pos;
        pos.x = data.playerPos[0];
        pos.y = data.playerPos[1];
        pos.z = data.playerPos[2];
        transform.position = pos;
    }
}
