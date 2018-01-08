using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour {

    // Use this for initialization
    int money;
    PlayerStuff player;
    DialogCentre dia;
    Text tableMoney;
    

    Text addMoney;

    public int MoneyAmount
    {
        get { return money; }
        private set { money = value; tableMoney.text = money + "$"; }
    }

    public bool IsMoneyGreaterThan(int amount)
    {
        return amount <= money;
    }

    void Start () {
        player = GetComponent<PlayerStuff>();
        dia = PlayerStuff.hud.GetComponentInChildren<DialogCentre>();
        addMoney = player.tablePlayer.GetComponentsInChildren<Text>(true)[2];
        tableMoney = player.tablePlayer.GetComponentsInChildren<Text>()[1];
        MoneyAmount = Assets.New_scripts.GameSettings.startMoney;


    }

    public bool Transaction(int amount)
    {
        if (money >= amount)
        {
            MoneyAmount -= amount;
            if (amount >= 0)
                addMoney.text = "-" + amount + "$";
            else
                addMoney.text = "+" + (-amount) + "$";
            print(amount);
            addMoney.GetComponent<RevealText>().ChangeLabelState(true);
            return true;
        }
        dia.ShowMessage("Недостаточно средств");
        return false;
    }


 
}
