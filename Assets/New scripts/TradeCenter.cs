using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeCenter : MonoBehaviour
{

    hudChange hud;
    Button button;
    public PlayerStuff player;
    GameObject tradePrefab;
    Turn turn;
    public bool firstFinished;
    public bool secondFinished;
    public List<Card> added;  

    Trade trade;
    
    VerticalLayoutGroup firstLG;
    VerticalLayoutGroup secondLG;
    void Start()
    {
        trade = transform.parent.parent.GetComponentInChildren<Trade>(true);

        turn = GameObject.Find("players").GetComponent<Turn>();
        firstLG = PlayerStuff.hud.tradeMenu.GetComponentsInChildren<VerticalLayoutGroup>()[0];
        secondLG = PlayerStuff.hud.tradeMenu.GetComponentsInChildren<VerticalLayoutGroup>()[1];
        hud = transform.parent.parent.GetComponent<hudChange>();
        tradePrefab = hud.tradePrefab;

        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            if (turn.player == player) // нажатие на себя
            {
                print(player  + " is not null");
                
                hud.EnableBancruptMenu(player);
            }
            else hud.EnableTradeMenu(player);
        });

        added = new List<Card>();
    }

    internal void Add(Card card)
    {

        

        if (!firstFinished && card.owner == turn.player)
        {
            var newTrade = Instantiate(tradePrefab);
            newTrade.GetComponent<Button>().onClick.AddListener(() => { added.Remove(newTrade.GetComponent<tradeCard>().card); GameObject.Destroy(newTrade); });
            newTrade.GetComponent<tradeCard>().card = card;
            newTrade.GetComponent<Text>().text = card.label + " " + card.cost + "$";
            newTrade.transform.SetParent(firstLG.transform);
            added.Add(card);

        }

        else if (firstFinished && card.owner == trade.secondTrader)
        {
            var newTrade = Instantiate(tradePrefab);
            newTrade.GetComponent<Button>().onClick.AddListener(() => { added.Remove(newTrade.GetComponent<tradeCard>().card); GameObject.Destroy(newTrade); });
            newTrade.GetComponent<tradeCard>().card = card;
            newTrade.GetComponent<Text>().text = card.label + " " + card.cost + "$";

            newTrade.transform.SetParent(secondLG.transform);
            added.Add(card);

        }

    }

    public void ResetList()
    {
        added = new List<Card>();
    }
}
