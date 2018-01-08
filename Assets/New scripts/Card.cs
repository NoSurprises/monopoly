using System.Collections.Generic;
using UnityEngine;
using Assets.New_scripts;

public class Card : MonoBehaviour {

    Color _color;
    TextMesh upgLevel;
    Turn turn;
    public Color Color { get { return _color; } set { _color = value; RefreshMaterial();  } }
    public int upgradeLevel { get { return _upgradeLevel; } set { _upgradeLevel = value; upgLevel.text = new string('*', _upgradeLevel); } }
    public int cost { get { return _cost; } set { _cost = value; if (costText != null) costText.text = _cost + "$"; } }
    public int index;
    public string label;

    public bool isMonopoly;
    Animator anim;
    TextMesh costText;
    TextMesh labelText;
    
    public PlayerStuff owner;
    public bool canBuy;

    hudChange hud;
    Trade trade;
    public int startValue;
    public int upgradeCost;

    public int _cost;
    int _upgradeLevel = 0;


    private void Start()
    {
        owner = null;
        isMonopoly = false;
        hud = GameObject.Find("HUD").GetComponentInChildren<hudChange>();
        upgLevel = GetComponentInChildren<TextMesh>();
        if (canBuy)
        {
            costText = GetComponentsInChildren<TextMesh>()[1];
            costText.text = _cost + "$";

            labelText = GetComponentsInChildren<TextMesh>()[2];
            labelText.text = label;
            Color tmpColor = labelText.color;
            tmpColor.a = 0f;
            labelText.color = tmpColor;
        }
        turn = GameObject.Find("players").GetComponent<Turn>();

        // Индексы монополий
        List<int> monopolyInds = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 15 };
        if (monopolyInds.Contains(index))
            canBuy = true;
        // 
        try
        {
            anim = GetComponent<Animator>();

        }
        catch (System.Exception)
        {
            anim = null;
        }

        trade = hud.GetComponentInChildren<Trade>();
    }
    private void OnMouseEnter()
    {
        if (hud.tradeMenu.IsActive())
        {

        }
        else if (canBuy && isMonopoly) 
        {
            anim.SetBool("isMouseOver", true);

        }
    }

    private void OnMouseExit()
    {
        if(canBuy) anim.SetBool("isMouseOver", false);
    }


    private void OnMouseDown()
    {
        if (hud.tradeMenu.IsActive() && owner == turn.player)
        {
            if (!turn.player.tablePlayer.GetComponent<TradeCenter>().added.Contains(this))
                turn.player.tablePlayer.GetComponent<TradeCenter>().Add(this);
        }
        SellCard();

    }

    internal void ChangeLabelState(bool reveal)
    {
        labelText.GetComponent<RevealLable>().ChangeLabelState(reveal);
    }

    public void SellCard()
    {
        if (owner == turn.player)
        {
            if (upgradeLevel > 0 && owner == turn.player && owner.GetComponent<Property>().CheckForSell(this))
            {
                upgradeLevel--;
                upgradeCost -= 50;
                cost /= 2;
            }
            else if (upgradeLevel == 0)
            {
                // заложить
                owner.GetComponent<Money>().Transaction(-cost/2);
                cost = 0;
                upgradeCost = startValue ;
            }
        }
    }

    public void RefreshMaterial()
    {
        this.GetComponent<Renderer>().material.color = this.Color;
    }
    
    public void Upgrade()
    {
        if (upgradeLevel < GameSettings.maxUpgradeLevel)
        {
            upgradeLevel++;
            this.cost*=2;
        }
    }
}
