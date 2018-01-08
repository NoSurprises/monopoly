using System;
using UnityEngine;
using UnityEngine.UI;

public class Trade : MonoBehaviour
{
    public PlayerStuff secondTrader;

    public Button tradeCancel;
    public Button okFirst;
    public Button okSecond;

    Turn turn;

    public bool firstFinished;
    public bool secondFinished;

    public VerticalLayoutGroup list1;
    public VerticalLayoutGroup list2;

    public Color finishedColor;
    public Color notFinishedColor;
    public Image warningWindow;

    public Button warningOk;
    public Button warningReject;

    public Button addMoney1;
    public Button addMoney2;

    public Text moneyAmount1;
    public Text moneyAmount2;

    public InputField money1;
    public InputField money2;

    // Use this for initialization
    void Start()
    {
        warningWindow.gameObject.SetActive(false);

        turn = GameObject.Find("players").GetComponent<Turn>();
        okFirst.onClick.AddListener(() =>
       {
           firstFinished = !firstFinished;
           turn.player.tablePlayer.GetComponent<TradeCenter>().firstFinished = firstFinished;
           ColorBlock newColors = okFirst.colors;
           if (firstFinished)
           {

               newColors.normalColor = finishedColor;
               newColors.highlightedColor = finishedColor;
               okFirst.colors = newColors;

           }
           else
           {
               newColors.normalColor = notFinishedColor;
               newColors.highlightedColor = notFinishedColor;

               okFirst.colors = newColors;
           }
       });

        okSecond.onClick.AddListener(() =>
       {
           if (firstFinished)
           {


               secondFinished = !secondFinished;

               warningWindow.gameObject.SetActive(true);
           }
       });

        warningOk.onClick.AddListener(() =>
       {
           //добавить карты, деньги
           foreach (Transform item in list1.transform)
           {
               turn.player.GetComponent<Property>().RemoveCard(item.GetComponent<tradeCard>().card);
               secondTrader.GetComponent<Property>().AddCard(item.GetComponent<tradeCard>().card);
               GameObject.DestroyObject(item.gameObject);
           }
           foreach (Transform item in list2.transform)
           {
               secondTrader.GetComponent<Property>().RemoveCard(item.GetComponent<tradeCard>().card);
               turn.player.GetComponent<Property>().AddCard(item.GetComponent<tradeCard>().card);
               GameObject.DestroyObject(item.gameObject);

           }
 
           turn.player.GetComponent<Money>().Transaction(Convert.ToInt32(moneyAmount1.text.Split(' ')[1].TrimEnd('$')) + -(Convert.ToInt32(moneyAmount2.text.Split(' ')[1].TrimEnd('$'))));
           secondTrader.GetComponent<Money>().Transaction(-(Convert.ToInt32(moneyAmount1.text.Split(' ')[1].TrimEnd('$'))) + Convert.ToInt32(moneyAmount2.text.Split(' ')[1].TrimEnd('$')));




           // reset
           firstFinished = false;
           turn.player.tablePlayer.GetComponent<TradeCenter>().firstFinished = false;
           turn.player.tablePlayer.GetComponent<TradeCenter>().ResetList();
           secondFinished = false;
           money1.text = string.Empty;
           money2.text = string.Empty;
           moneyAmount1.text = moneyAmount2.text = "Деньгами: 0$";

           ColorBlock newColors = okFirst.colors;
           newColors.normalColor = notFinishedColor;
           newColors.highlightedColor = notFinishedColor;
           okFirst.colors = newColors;

           warningWindow.gameObject.SetActive(false);

           gameObject.SetActive(false);


       });

        warningReject.onClick.AddListener(() =>
        {
            warningWindow.gameObject.SetActive(false);

        });

        addMoney1.onClick.AddListener(() =>
        {
            if (turn.player.GetComponent<Money>().IsMoneyGreaterThan((string.IsNullOrEmpty(money1.text) ? 0 : int.Parse(money1.text))))
            {
                int money = Convert.ToInt32(moneyAmount1.text.Split(' ')[1].TrimEnd('$'));
                moneyAmount1.text = "Деньгами: " + (money + (string.IsNullOrEmpty(money1.text) ? 0 : int.Parse(money1.text))) + "$";
                money1.text = string.Empty;
            }

        });

        addMoney2.onClick.AddListener(() =>
       {
           if (secondTrader.GetComponent<Money>().IsMoneyGreaterThan((string.IsNullOrEmpty(money2.text) ? 0 : int.Parse(money2.text))))
           {
               int money = Convert.ToInt32(moneyAmount2.text.Split(' ')[1].TrimEnd('$'));

               moneyAmount2.text = "Деньгами: " + (money + (string.IsNullOrEmpty(money2.text) ? 0 : int.Parse(money2.text))) + "$";
               money2.text = string.Empty;
           }

       });


        money1.onValidateInput += (string input, int charIndex, char c) => "0123456789".Contains(c.ToString()) ? c : '\0';
        money2.onValidateInput += (string input, int charIndex, char c) => "0123456789".Contains(c.ToString()) ? c : '\0';

        tradeCancel.onClick.AddListener(() =>
        { // reset
            firstFinished = false;
            turn.player.tablePlayer.GetComponent<TradeCenter>().firstFinished = false;
            turn.player.tablePlayer.GetComponent<TradeCenter>().ResetList();
            secondFinished = false;
            money1.text = string.Empty;
            money2.text = string.Empty;
            moneyAmount1.text = moneyAmount2.text = "Деньгами: 0$";

            ColorBlock newColors = okFirst.colors;
            newColors.normalColor = notFinishedColor;
            newColors.highlightedColor = notFinishedColor;
            okFirst.colors = newColors;

            warningWindow.gameObject.SetActive(false);

            gameObject.SetActive(false);


        });

    }

    

}
