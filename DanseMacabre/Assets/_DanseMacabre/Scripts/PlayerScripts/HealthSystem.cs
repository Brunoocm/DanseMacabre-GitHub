using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [Header("HEALTH")]
    public float maxHealth;
    public float health;

    private float m_health;

    public Slider slider;
    public Slider sliderBack;

    [Header("HEALTH")]
    public float maxStamina;
    public float stamina;
    [HideInInspector] public float m_stamina;

    public int attackStamina;
    public int defStamina;

    public Slider sliderStamina;
    public Slider sliderBackStamina;

    public InputSystem inputSystem;
    public CombatSystem combatSystem;
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

        sliderStamina.maxValue = maxStamina;
        sliderBackStamina.maxValue = maxStamina;
    }

    private void Update()
    {
        HealthBar();
        StaminaBar();
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
    void StaminaBar()
    {
        sliderStamina.value = stamina;
        sliderBackStamina.value = m_stamina;

        if (stamina > 0)
        {
            if (m_stamina > stamina)
            {
                m_stamina -= 1.2f * Time.deltaTime;
            }
        }
        if (stamina <= 0)
        {

        }
    }
    public void TakeDamage(int dano)
    {
        if (!combatSystem.blocking)
        {
            if (inv <= 0)
            {
                m_health = health;
                health -= dano;
                PlayTargetAnimation("Falling", true);
                inv = 3;
            }
        }
        else
        {
            PlayTargetAnimation("Shield", true);
            m_stamina = stamina;
            stamina -= defStamina;
        }
    }
    public void PlayTargetAnimation(string targetAnim, bool isInteracting)
    {
        anim.applyRootMotion = false;
        //anim.SetBool("Base Layer", isInteracting);
        anim.CrossFade(targetAnim, 0.2f);
    }

    public void EnableMove()
    {
        inputSystem.enabled = true;

    } 
    public void DesableMove()
    {
        inputSystem.enabled = false;

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
