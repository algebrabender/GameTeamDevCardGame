using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour
{ 
    public List<Sprite> level1Enemies = new List<Sprite>(3);
    public List<Sprite> level2Enemies = new List<Sprite>(2);
    public Sprite level3Enemy = null;

    public Image enemyImage = null;
    public Image hitImage = null;

    public Text maxStrengthText = null;
    public Text strengthText = null;
    public Text maxHealthText = null;
    public Text healthText = null;

    internal int maxHealth;
    internal int health; //current health
    internal int maxStrength;
    internal int strength; //current health

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

    internal void PlayDealSound()
    {
        AudioManager.instance.PlayDealAudio();
    }
}
