using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public CardData cardData = null;

    public TextMeshProUGUI titleText = null;
    public TextMeshProUGUI descriptionText = null;
    public TextMeshProUGUI damageText = null;
    public TextMeshProUGUI strengthText = null;
    public TextMeshProUGUI healthText = null;
    public TextMeshProUGUI blackStrengthText = null;
    public Image cardImage = null;
    public Image cardFrontImage = null;
    public Image cardBackImage = null;

    public void Initialize()
    {
        if (cardData == null)
        {
            Debug.LogError("Card has no CardData");
            return;
        }

        titleText.text = cardData.cardTitle;
        descriptionText.text = cardData.description;
        healthText.text = cardData.health.ToString();
        damageText.text = cardData.damage.ToString();
        blackStrengthText.text = cardData.blackStrength.ToString();
        strengthText.text = cardData.strength.ToString();
        cardImage.sprite = cardData.cardImage;
        cardFrontImage.sprite = cardData.cardFront;
        cardBackImage.sprite = cardData.cardBack;

        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => { OnClick((PointerEventData)data); });
        trigger.triggers.Add(entry);
    }

    public void OnClick(BaseEventData data)
    {
        //TODO: play that card
        PointerEventData pData = (PointerEventData)data;
        Debug.Log("try");
    }
}
