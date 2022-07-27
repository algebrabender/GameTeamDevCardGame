using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "CardGame/Card")]
public class CardData : ScriptableObject
{
    [Header("Text Boxes")]
    public string cardTitle;
    public string description;
    public string specialEffect;

    [Header("Effect values")]
    public int damage;         // damage to opponent health
    public int blackStrength;  // damage/boost to Dr. Schmuck's strength
    public int strength;       // cost/boost to strength of player 
    public int health;         // cost/boost to health of player

    [Header("Special Effect values")]
    public int maxStrenght;    // change to maxStrenght
    public int maxHealth;      // change to maxHealth

    [Header("Card Sprites")]
    public Sprite cardFront;
    public Sprite cardBack;
    public Sprite cardImage;
//<<<<<<< HEAD
    
//=======
//>>>>>>> 173d9ed0f431f7299e848adfcbebae25a1040152
//=======
//>>>>>>> d728453794132243379596dafdf3996b2f19c784

    [Header("")]
    public int numberInDeck;

    [Header("Ticking Plushie int value")]
    public int numberOfTurnsSkippedAfter; // for Ticking Plushie card

}
