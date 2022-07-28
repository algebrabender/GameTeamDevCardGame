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

    public GameObject cardPrefab1 = null;
    public GameObject cardPrefab2 = null;
    public GameObject cardPrefab3 = null;
    public GameObject teenagerCardPrefab = null;
    public GameObject policemanCardPrefab = null;
    public GameObject mayorCardPrefab = null;
    public Canvas canvas = null;

    public bool isPlayable = false;
    internal int lastPlayedLevel = 0; //level - 1
    private int enemiesPerLevelTakenOut = 0;
    private bool newEnemy = false;

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

        playerDeck.Create();
        enemyDeck.CreateEnemyDeck(lastPlayedLevel);

        StartCoroutine(DealHands());
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
        if (!newEnemy)
        {
            Card card = AIChooseCard();

            StartCoroutine(UseEnemyCard(card));

            enemy.strength = enemy.strength + 1 > enemy.maxStrength ? enemy.maxStrength : enemy.strength + 1;

            player.UpdateHealth();
            player.UpdateStrength();

            enemy.UpdateHealth();
            enemy.UpdateStrength();
        }

        isPlayable = true;
    }

    internal IEnumerator CheckIfEnemyTakenOut()
    {
        switch (lastPlayedLevel)
        {
            case 0:
                if (enemy.health <= 0)
                {
                    if (enemiesPerLevelTakenOut < 2)
                    {
                        newEnemy = true;
                        enemiesPerLevelTakenOut++;
                        enemy.enemyImage.sprite = enemy.level1Enemies[enemiesPerLevelTakenOut];
                        enemy.maxHealth = enemy.health = 2;
                        enemy.maxStrength = enemy.strength = 4;
                        enemysHand.ClearHand();
                        enemyDeck.CreateEnemyDeck(lastPlayedLevel);
                        for (int i = 0; i < 3; i++)
                        {
                            enemyDeck.DealCard(enemysHand);

                            //yield return new WaitForSeconds(1.0f);
                        }
                        
                    }
                    else
                    {
                        newEnemy = true;
                        enemiesPerLevelTakenOut = 0;
                        lastPlayedLevel++;
                        backgroundImage.sprite = levelBackgrounds[lastPlayedLevel];
                        enemy.enemyImage.sprite = enemy.level2Enemies[0];
                        enemy.maxHealth = enemy.health = 5;
                        enemy.maxStrength = enemy.strength = 4;
                        enemysHand.ClearHand();
                        enemyDeck.CreateEnemyDeck(lastPlayedLevel);
                        for (int i = 0; i < 3; i++)
                        {
                            enemyDeck.DealCard(enemysHand);

                            //yield return new WaitForSeconds(1.0f);
                        }
                    }
                }
                else
                    newEnemy = false;
                break;
            case 1:
                if (enemy.health <= 0)
                {
                    if (enemiesPerLevelTakenOut == 0)
                    {
                        newEnemy = true;
                        enemiesPerLevelTakenOut++;
                        enemy.enemyImage.sprite = enemy.level2Enemies[1];
                        enemy.maxHealth = enemy.health = 5;
                        enemy.maxStrength = enemy.strength = 4;
                        enemysHand.ClearHand();
                        enemyDeck.CreateEnemyDeck(lastPlayedLevel);
                        for (int i = 0; i < 3; i++)
                        {
                            enemyDeck.DealCard(enemysHand);

                            //yield return new WaitForSeconds(1.0f);
                        }
                    }
                    else
                    {
                        newEnemy = true;
                        enemiesPerLevelTakenOut = 0;
                        lastPlayedLevel++;
                        backgroundImage.sprite = levelBackgrounds[lastPlayedLevel];
                        enemy.enemyImage.sprite = enemy.level3Enemy;
                        enemy.maxHealth = enemy.health = 15;
                        enemy.maxStrength = enemy.strength = 5;
                        enemysHand.ClearHand();
                        enemyDeck.CreateEnemyDeck(lastPlayedLevel);
                        for (int i = 0; i < 3; i++)
                        {
                            enemyDeck.DealCard(enemysHand);

                            //yield return new WaitForSeconds(1.0f);
                        }
                    }
                }
                else
                    newEnemy = false;
                break;
            case 2:
                if (enemy.health <= 0)
                    StartCoroutine(GameWin());
                break;
        }

        enemy.UpdateMaxHealth();
        enemy.UpdateHealth();
        enemy.UpdateMaxStrength();
        enemy.UpdateStrength();

        yield return new WaitForSeconds(1.0f);
    }

    internal void CheckIfGameOver()
    {
        if (player.health <= 0 || player.strength <= 0)
            StartCoroutine(GameOver());
    }

    internal void GameOverDueCards()
    {
        StartCoroutine(GameOver());
    }

    internal IEnumerator GameWin()
    {
        //transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(5);
    }

    internal IEnumerator GameOver()
    {
        //transition.SetTrigger("Start");

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
                    player.strength = player.strength + cardBeingPlayed.cardData.strength > player.maxStrength ? player.maxStrength : player.strength + cardBeingPlayed.cardData.strength;
                    
                    enemy.health -= cardBeingPlayed.cardData.damage;
                    StartCoroutine(CheckIfEnemyTakenOut());

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
                        //for cat it will just add 0 so basically not doing anything
                        //-> less if checks
                        player.maxStrength += cardBeingPlayed.cardData.maxStrenght;
                        player.maxHealth += cardBeingPlayed.cardData.maxHealth;

                        player.strength = player.strength + cardBeingPlayed.cardData.strength > player.maxStrength ? player.maxStrength : player.strength + cardBeingPlayed.cardData.strength;
                        player.health = player.health + cardBeingPlayed.cardData.health > player.maxHealth ? player.maxHealth : player.health + cardBeingPlayed.cardData.health;

                        enemy.health -= cardBeingPlayed.cardData.damage;
                        StartCoroutine(CheckIfEnemyTakenOut());

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

                        //adds for aid for rest is just + 0
                        //adds for tobacco for rest is just + 0
                        player.maxHealth += cardBeingPlayed.cardData.maxHealth;

                        player.strength = player.strength + cardBeingPlayed.cardData.strength > player.maxStrength ? player.maxStrength : player.strength + cardBeingPlayed.cardData.strength;
                        player.health = player.health + cardBeingPlayed.cardData.health > player.maxHealth ? player.maxHealth : player.health + cardBeingPlayed.cardData.health;

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

            player.strength = player.strength + 1 > player.maxStrength ? player.maxStrength : player.strength + 1;

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

        List<Card> availableCards = new List<Card>();

        for (int i = 0; i < 3; i++)
        {
            if (enemysHand.cards[i].cardData.strength + enemy.strength > -1)
                availableCards.Add(enemysHand.cards[i]);
        }

        if (availableCards.Count == 0)
        {
            return card;
        }

        //rules for playing for each enemy
        switch (lastPlayedLevel)
        {
            case 0: //teenagers play any card
                card = availableCards[UnityEngine.Random.Range(0, availableCards.Count)];
                break;
            case 1: //police officer cards have ranking
                for (int i = 0; i < availableCards.Count; i++)
                {
                    if (player.health - availableCards[i].cardData.damage <= 0)
                        card = availableCards[i];
                    else if (availableCards[i].cardData.cardTitle == "Fine for Causing Disturbance")
                        card = availableCards[i];
                    else if (availableCards[i].cardData.damage + availableCards[i].cardData.maxStrenght == 0)
                        card = availableCards[i];
                }
                if (card == null)
                    card = availableCards[UnityEngine.Random.Range(0, availableCards.Count)];
                break;
            case 2: //mayor cards have ranking
                for (int i = 0; i < availableCards.Count; i++)
                {
                    if (player.health - availableCards[i].cardData.damage <= 0)
                        card = availableCards[i];
                    else if (availableCards[i].cardData.cardTitle == "Propaganda II")
                        card = availableCards[i];
                    else if (availableCards[i].cardData.blackStrength == -2 || availableCards[i].cardData.damage == 4)
                        card = availableCards[i];
                }
                if (card == null)
                {
                    card = availableCards[UnityEngine.Random.Range(0, availableCards.Count)];

                    if (card.cardData.cardTitle == "First Aid" && enemy.health > enemy.maxHealth - 3
                        && availableCards.Count > 1)
                    {
                        availableCards.Remove(card);
                        card = availableCards[UnityEngine.Random.Range(0, availableCards.Count)];
                    }
                }
                break;
        }

        return card;
    }

    internal IEnumerator UseEnemyCard(Card card)
    {
        yield return new WaitForSeconds(0.5f);

        if (card != null)
        {
            TurnCard(card);

            yield return new WaitForSeconds(1.5f);

            //if any stat is 0 it wont have any effect and we take care of unneccesary ifs
            if (card.cardData.damage != 0 || card.cardData.blackStrength != 0)
                player.hitImage.gameObject.SetActive(true);

            player.health -= card.cardData.damage;
            player.strength -= card.cardData.blackStrength;

            CheckIfGameOver();

            enemy.health = enemy.health + card.cardData.health > enemy.maxHealth ? enemy.maxHealth : enemy.health + card.cardData.health;
            enemy.strength = enemy.strength + card.cardData.strength > enemy.maxStrength ? enemy.maxStrength : enemy.strength + card.cardData.strength;

            enemysHand.RemoveCard(card);

            yield return new WaitForSeconds(0.5f);

            player.hitImage.gameObject.SetActive(false);
            player.UpdateHealth();
            player.UpdateStrength();

            enemy.UpdateHealth();
            enemy.UpdateStrength();
        }
    }

    internal void TurnCard(Card card)
    {
        Animator animator = card.GetComponentInChildren<Animator>();

        if (animator)
            animator.SetTrigger("Cardflip");
        else
            Debug.LogError("No animator found");
    }

    #endregion

    #region UI Buttons

    public void RestButton()
    {
        player.health = player.health + 1 > player.maxHealth ? player.maxHealth : player.health + 1;
        player.strength = player.strength + 1 > player.maxStrength ? player.maxStrength : player.strength + 1;

        player.UpdateHealth();
        player.UpdateStrength();

        isPlayable = false;
        EnemyTurn();
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
