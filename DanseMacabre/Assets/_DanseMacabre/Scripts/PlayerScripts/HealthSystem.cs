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

    [Header("STAMINA")]
    public float maxStamina;
    public float stamina;
    [HideInInspector] public float m_stamina;

    public int attackStamina;
    public int defStamina;
    public bool recovery;

    public Slider sliderStamina;
    public Slider sliderBackStamina;

    public Image fillArea;
    Color colorMain;
  

    public InputSystem inputSystem;
    public CombatSystem combatSystem;
    public RespawnSystem respawnSystem;
    public bool isLock;

    private float inv;


    Animator anim;
    Rigidbody rb;
    private void Start()
    {
        respawnSystem = FindObjectOfType<RespawnSystem>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        inputSystem = GetComponent<InputSystem>();
        colorMain = fillArea.color;
        slider.maxValue = maxHealth;
        sliderBack.maxValue = maxHealth;  

        sliderStamina.maxValue = maxStamina;
        sliderBackStamina.maxValue = maxStamina;
    }

    private void Update()
    {
        RecoveryTime();
        HealthBar();
        StaminaBar();

        if(m_stamina <= stamina && stamina < maxStamina)
        {
            stamina += 5.5f * Time.deltaTime;
            m_stamina += 5.5f * Time.deltaTime;
        }
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
            respawnSystem.DieFunction();
            //MORRE MORRE MORRE

        }
    }
    void StaminaBar()
    {
        sliderStamina.value = stamina;
        sliderBackStamina.value = m_stamina;

        if (stamina >= maxStamina)
        {
            recovery = false;
        }
        else if (stamina > 0)
        {
            if (m_stamina > stamina)
            {
                m_stamina -= 1.2f * Time.deltaTime;
            }
        }
        else if (stamina <= 0)
        {
            recovery = true;
        }
        
    }

    void RecoveryTime()
    {
        if (recovery)
        {
            fillArea.color = Color.red;
            stamina += 5.5f * Time.deltaTime;
            m_stamina += 5.5f * Time.deltaTime;
        }
        else
        {
            fillArea.color = colorMain;

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

    public void WalkSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/PassosNovos", GetComponent<Transform>().position);

    }
}
