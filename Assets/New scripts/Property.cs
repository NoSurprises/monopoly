using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Property : MonoBehaviour {

    List<Card> property;
    Cards cards;
    Dictionary<int, List<Card>> propUpgrades;

    GameObject particles;
    private void Start()
    {
        property = new List<Card>();
        propUpgrades = new Dictionary<int, List<Card>>();
        cards = GameObject.Find("Cards").GetComponent<Cards>();

        particles = GameObject.Find("players").GetComponent<Turn>().particles;
        

    }
    public int CountProperty()
    {
        return property.Count;
    }
    public void AddCard()
    {
        PlayerStuff player = GetComponent<PlayerStuff>();
        property.Add(player.targetCard);


        var p= GameObject.Instantiate(particles);
        p.transform.SetParent(player.targetCard.transform);
        p.transform.position = player.targetCard.transform.position;

        player.targetCard.Color = player.color;
        player.targetCard.owner = player;


        print("Количество карт данной монополии - " + property.Where(x => x.index == player.targetCard.index).ToArray().Length + ". Необходимо - " + cards.fullMonopolyQuants[player.targetCard.index]);
        
        if (property.Where(x => x.index == player.targetCard.index).ToArray().Length == cards.fullMonopolyQuants[player.targetCard.index])
        {
            property.Where(x => x.index == player.targetCard.index).Select(x => x.isMonopoly = true).ToList();
            print("Монополия создана у игрока " + player);
        }

        if (!propUpgrades.ContainsKey(player.targetCard.index))
        {
            propUpgrades.Add(player.targetCard.index, new List<Card>());
        }
        propUpgrades[player.targetCard.index].Add(player.targetCard);


    }

    // works fine
    public bool CheckForUpgrade(Card card)
    {
        return card.upgradeLevel <= (propUpgrades[card.index].Select(x => x.upgradeLevel).ToList().Max());
    }

    // works fine
    public bool CheckForSell(Card card)
    {
        return card.upgradeLevel >= (propUpgrades[card.index].Select(x => x.upgradeLevel).ToList().Max());

    }

   

    public void AddCard(Card card)
    {

        PlayerStuff player = GetComponent<PlayerStuff>();
        property.Add(card);
        card.Color = player.color;
        card.owner = player;

        var p = GameObject.Instantiate(particles);
        p.transform.SetParent(card.transform);
        p.transform.position = card.transform.position;
        print("Количество карт данной монополии - " + property.Where(x => x.index == card.index).ToArray().Length + ". Необходимо - " + cards.fullMonopolyQuants[card.index]);

        if (property.Where(x => x.index == card.index).ToArray().Length == cards.fullMonopolyQuants[card.index])
        {
            property.Where(x => x.index == card.index).Select(x => x.isMonopoly = true).ToList();
            print("Монополия создана у игрока " + player);
        }

        if (!propUpgrades.ContainsKey(card.index))
        {
            propUpgrades.Add(card.index, new List<Card>());
        }
        propUpgrades[card.index].Add(card);
    }
    
    public int SellAllCards()
    {
        int money = 0;
        while (property.Count != 0) { 
            money += property[0].startValue / 2;
            RemoveCard(property[0]);
            

        }
        return money;
    }


    public void RemoveCard()
    {
        property.Remove(GameObject.Find("players").GetComponent<Turn>().player.targetCard);
    }

    public void RemoveCard(Card card)
    {
        if (card.isMonopoly)
        {
            // убрать у всех карт того же типа монополию
            propUpgrades[card.index].Select(x => x.isMonopoly = false).ToList();
        }
        property.Remove(card);
        card.Color = Color.white;
        card.owner = null;

        propUpgrades[card.index].Remove(card);
        
        
    }
    
}
