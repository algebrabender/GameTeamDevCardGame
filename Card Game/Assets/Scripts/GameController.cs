using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    static public GameController instance = null;

    public List<Sprite> levelBackgrounds = new List<Sprite>(3);
    public Image backgroundImage = null;

    public Deck playerDeck = new Deck();
    public Deck enemyDeck = new Deck();

    public Hand playersHand = new Hand();
    public Hand enemysHand = new Hand();

    public Player player = null;
    public Enemy enemy = null;

    public List<CardData> cards = new List<CardData>();

    public List<CardData> level1EnemyCards = new List<CardData>();
    public List<CardData> level2EnemyCards = new List<CardData>();
    public List<CardData> level3EnemyCards = new List<CardData>();

    public GameObject cardPrefab = null;
    public Canvas canvas = null;

    public bool isPlayable = false;
    internal int lastPlayedLevel = 0; //level - 1
    private int enemiesPerLevelTakenOut = 0; 

    public Animator transition = null;

    void Awake()
    {
        instance = this;
        backgroundImage.sprite = levelBackgrounds[lastPlayedLevel];

        switch (lastPlayedLevel)
        {
            case 0:
                enemy.enemyImage.sprite = enemy.level1Enemies[0];
                break;
            case 1:
                enemy.enemyImage.sprite = enemy.level2Enemies[0];
                break;
            case 2:
                enemy.enemyImage.sprite = enemy.level3Enemy;
                break;
        }

        SetUpEnemy();

        //playerDeck.Create();
        //enemyDeck.Create();

        //StartCoroutine(DealHands());
    }

    #region Game Set Up

    internal void SetUpEnemy()
    {
        switch (lastPlayedLevel)
        {
            case 0:
                enemy.enemyImage.sprite = enemy.level1Enemies[0];
                enemy.maxHealth = enemy.health = 2;
                enemy.maxStrength = enemy.strength = 4;
                break;
            case 1:
                enemy.enemyImage.sprite = enemy.level2Enemies[0];
                enemy.maxHealth = enemy.health = 5;
                enemy.maxStrength = enemy.strength = 4;
                break;
            case 2:
                enemy.enemyImage.sprite = enemy.level3Enemy;
                enemy.maxHealth = enemy.health = 15;
                enemy.maxStrength = enemy.strength = 5;
                break;
        }

        enemy.UpdateMaxHealth();
        enemy.UpdateHealth();
        enemy.UpdateMaxStrength();
        enemy.UpdateStrength();
    }

    internal IEnumerator DealHands()
    {
        yield return new WaitForSeconds(1.0f);

        for (int i = 0; i < 3; i++)
        {
            playerDeck.DealCard(playersHand);
            enemyDeck.DealCard(enemysHand);

            yield return new WaitForSeconds(1.0f);
        }

        isPlayable = true;
    }

    internal void EnemyTurn()
    {
        //TODO: check for plushie pausing turns

        Card card = AIChooseCard();

        StartCoroutine(UseEnemyCard(card));

        isPlayable = true;
    }

    internal void CheckIfEnemyTakenOut()
    {
        switch (lastPlayedLevel)
        {
            case 0:
                if (enemy.health <= 0)
                {
                    if (enemiesPerLevelTakenOut < 2)
                    {
                        enemiesPerLevelTakenOut++;
                        enemy.enemyImage.sprite = enemy.level1Enemies[enemiesPerLevelTakenOut];
                        enemy.maxHealth = enemy.health = 2;
                        enemy.maxStrength = enemy.strength = 4;
                    }
                    else
                    {
                        enemiesPerLevelTakenOut = 0;
                        lastPlayedLevel++;
                        backgroundImage.sprite = levelBackgrounds[lastPlayedLevel];
                        enemy.enemyImage.sprite = enemy.level3Enemy;
                        enemy.maxHealth = enemy.health = 5;
                        enemy.maxStrength = enemy.strength = 4;
                    }
                }
                break;
            case 1:
                if (enemy.health <= 0)
                {
                    if (enemiesPerLevelTakenOut == 0)
                    {
                        enemiesPerLevelTakenOut++;
                        enemy.enemyImage.sprite = enemy.level2Enemies[1];
                        enemy.maxHealth = enemy.health = 5;
                        enemy.maxStrength = enemy.strength = 4;
                    }
                    else
                    {
                        enemiesPerLevelTakenOut = 0;
                        lastPlayedLevel++;
                        backgroundImage.sprite = levelBackgrounds[lastPlayedLevel];
                        enemy.enemyImage.sprite = enemy.level3Enemy;
                        enemy.maxHealth = enemy.health = 15;
                        enemy.maxStrength = enemy.strength = 5;
                    }
                }
                break;
            case 2:
                if (enemy.health <= 0)
                    StartCoroutine(GameWin());
                break;
        }

        enemy.UpdateMaxHealth();
        enemy.UpdateMaxStrength();
    }

    internal IEnumerator GameWin()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(5);
    }

    internal IEnumerator GameOver()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(4);
    }

    #endregion

    #region Card Methods

    internal bool UseCard(Card cardBeingPlayed)
    {
        bool valid;

        if (cardBeingPlayed == null)
            return false;

        //if we have some effects they can be casted when the stats are updated inside ifs

        if (cardBeingPlayed.cardData.strength > 0) // coffee and drink
        {
            if (cardBeingPlayed.cardData.cardTitle == "Blackest Coffee")
            {
                player.maxStrength += cardBeingPlayed.cardData.maxStrenght;
                player.strength = player.strength + 6 > player.maxStrength ? player.maxStrength : player.strength + 6;
                
                valid = true;
            }
            else if (player.health + cardBeingPlayed.cardData.health > 0) //drink
            {
                player.strength = player.strength + 6 > player.maxStrength ? player.maxStrength : player.strength + 6;
                player.health += cardBeingPlayed.cardData.health;

                valid = true;
            }
            else
                valid = false;
        }
        else //rest of the cards 
        {
            if (cardBeingPlayed.cardData.health == 0
                && cardBeingPlayed.cardData.damage > 0) //only deals damage
            {
                if (player.strength + cardBeingPlayed.cardData.strength > 0)
                {
                    player.strength += cardBeingPlayed.cardData.strength;
                    
                    enemy.health -= cardBeingPlayed.cardData.damage;
                    

                    valid = true;
                }
                else
                    valid = false;
            }
            else //cat, plushie, aid, powder, tobacco
            {
                if (cardBeingPlayed.cardData.health < 0) //cat, powder
                {
                    if (player.strength + cardBeingPlayed.cardData.strength < 0
                        || player.health + cardBeingPlayed.cardData.health < 0)
                        valid = false;
                    else
                    {
                        player.strength += cardBeingPlayed.cardData.strength;
                        player.health += cardBeingPlayed.cardData.health;

                        //for cat it will just add 0 so basically not doing anything
                        //-> less if checks
                        player.maxStrength += cardBeingPlayed.cardData.maxStrenght;
                        player.maxHealth += cardBeingPlayed.cardData.maxHealth;

                        enemy.health -= cardBeingPlayed.cardData.damage;
                        CheckIfEnemyTakenOut();

                        valid = true;
                    }
                }
                else //aid, plushie, tobacco
                {
                    if (player.strength + cardBeingPlayed.cardData.strength > 0)
                    {
                        if (cardBeingPlayed.cardData.cardTitle == "Ticking Plushie")
                        {
                            //TODO: PAUSE ENEMY ATTACK
                        }
                        player.strength += cardBeingPlayed.cardData.strength;
                        //adds for aid for rest is just + 0
                        player.health += cardBeingPlayed.cardData.health;
                        //adds for tobacco for rest is just + 0
                        player.maxHealth += cardBeingPlayed.cardData.maxHealth;

                        valid = true;
                    }
                    else
                        valid = false;
                }    
            }
        }

        if (valid) //card is playble
        {
            isPlayable = false;

            player.UpdateMaxHealth();
            player.UpdateHealth();
            player.UpdateMaxStrength();
            player.UpdateStrength();

            enemy.UpdateHealth();
            enemy.UpdateStrength();

            playersHand.RemoveCard(cardBeingPlayed);

            EnemyTurn();
        }

        //if we have some animations they can go here

        return valid;
    }

    internal Card AIChooseCard()
    {
        Card card = null;

        switch(lastPlayedLevel)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
        }

        return card;
    }

    internal IEnumerator UseEnemyCard(Card card)
    {
        yield return new WaitForSeconds(0.5f);

        if (card != null)
        {
            //TODO: card turning animation

            yield return new WaitForSeconds(1.5f);

            //if any stat is 0 it wont have any effect and we take care of unneccesary ifs
            player.health -= card.cardData.damage;
            player.strength += card.cardData.blackStrength;
            //TODO: check if gameover

            enemy.health += card.cardData.health;
            enemy.strength += card.cardData.strength;

            enemysHand.RemoveCard(card);

            yield return new WaitForSeconds(0.5f);
        }
    }

    #endregion

    #region UI Buttons

    public void BurnCardButton()
    {

    }

    public void RestButton()
    {
        //Strength +1

    }

    public void Quit()
    {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    #endregion
}
