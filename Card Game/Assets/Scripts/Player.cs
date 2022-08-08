using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public Image playerImage = null;
    public Image hitImage = null;

    public Text maxStrengthText = null;
    public Text strengthText = null;
    public Text maxHealthText = null;
    public Text healthText = null;

    internal int maxHealth = 10;
    internal int health = 5; //current health
    internal int maxStrength = 6;
    internal int strength = 4; //current strength

    private Animator animator = null;

    void Start()
    {
        animator = GetComponent<Animator>();
        UpdateMaxHealth();
        UpdateHealth();
        UpdateMaxStrength();
        UpdateStrength();
    }

    internal void UpdateMaxHealth()
    {
        maxHealthText.text = maxHealth.ToString();
    }

    internal void UpdateHealth()
    {
        if (health > 0)
            healthText.text = health.ToString();
    }

    internal void UpdateMaxStrength()
    {
        maxStrengthText.text = maxStrength.ToString();
    }

    internal void UpdateStrength()
    {
        strengthText.text = strength.ToString();
    }

    internal void PlayHitAnim()
    {
        if (animator != null)
            animator.SetTrigger("Hit");
    }

    /*
    internal void PlayDealSound()
    {
        AudioManager.instance.PlayDealAudio();
    }
    */
}
