using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "CardGame/Card")]
public class CardData : ScriptableObject
{
    public string cardTitle;
    public string description;

    public int damage;         // damage to opponent health
    public int blackStrength;  // damage to Dr. Schmuck's strength
    public int strength;       // cost/boost to strength of player 
    public int health;         // cost/boost to health of player

    public int maxStrenght;    // change to maxStrenght
    public int maxHealth;      // change to maxHealth

    public Sprite cardFront;
    public Sprite cardBack;
    public Sprite cardImage;
    public Sprite effect1Sprite;
    public Sprite effect2Sprite;
    public Sprite effect3Sprite;
    public Sprite effect4Sprite;

    public int numberInDeck;


}
