using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.New_scripts;
using System.Reflection;
using System.Text;
using System;
using System.IO;

public class Turn : MonoBehaviour
{
    // Debug
    Operations operations;
    //

    public hudChange hud;
    public GameObject gameInfo;
    List<PlayerStuff> players;
    PlayerStuff _player;
    public PlayerStuff player { get { return _player; } set {
            if (player != null)
                hud.SetTransparency(player.tablePlayer.GetComponent<Image>(), 0.2f);
            _player = value;
            hud.SetTransparency(player.tablePlayer.GetComponent<Image>(), 1); } }
    public GameObject prefabPlayer;
    public GameObject prefabPanelPlayer;
    public GameObject table;
    public Color[] playerColors;

    public GameObject particles;

    int currentPlayerIndex;

    List<string> names = GameSettings.standartNames;

    public static string GetParamName(System.Reflection.MethodInfo method)
    {
        StringBuilder retVal = new StringBuilder();
        int index = 0;
        while (method.GetParameters().Length > index)
        {
            
                retVal.Append(method.GetParameters()[index++].ParameterType + " " + method.GetParameters()[index-1].Name + ",");
           
        }
        if (retVal.ToString() == string.Empty)
            return "-";
        return retVal.ToString() ;
    }

    void Start()
    {



        Assembly thisAssembly = Assembly.GetExecutingAssembly();
        StringBuilder OutputText;

        OutputText = new StringBuilder();

        print("Start information");
        foreach (Type t in thisAssembly.GetTypes())
        {
            OutputText.AppendLine("\n FIELDS FOR:" + t.FullName + "\r\n");
            MemberInfo[] Members = t.GetMembers();
            FieldInfo[] fields = t.GetFields(BindingFlags.Instance| BindingFlags.NonPublic | BindingFlags.Public );
            PropertyInfo[] properties = t.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            MethodInfo[] methods = t.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly);
            OutputText.Append(fields.Length);
           
            foreach (FieldInfo NextMember in fields)
            {
                
                if (NextMember.IsPublic)
                {
                    OutputText.AppendLine("\n\t" + NextMember.Name + "\tpublic\t" + NextMember.FieldType);

                }
                else
                {
                    OutputText.AppendLine("\n\t" + NextMember.Name + "\tprivate\t" + NextMember.FieldType);

                }
            }
            OutputText.Append("\nPROPERTIES FOR " + t.FullName + "\r\n");
            foreach (PropertyInfo NextMember in properties)
            {


                OutputText.AppendLine("\n\t" + NextMember.Name + "\tpublic\t" + NextMember.PropertyType + "\tget/set");


            }
            OutputText.Append("\n METHODS FOR " + t.FullName + "\r\n");
            foreach (MethodInfo NextMember in methods)
            {
                
                    if (NextMember.IsPublic)
                    {
                        OutputText.AppendLine("\n\t" + NextMember.Name + "\tpublic\t" + NextMember.ReturnType + "\t" + GetParamName(NextMember));

                    }
                    else
                    {
                        OutputText.AppendLine("\n\t" + NextMember.Name + "\tprivate\t" + NextMember.ReturnType + "\t" + GetParamName(NextMember));

                    }

                
            }
        }
        print(OutputText);
        using (FileStream fs = new FileStream(@"c:\Users\Nick\Desktop\info.txt", FileMode.Create
            ))
        {
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine(OutputText);
            }
        }
        print("End information");











    operations = GetComponent<Operations>();
        players = new List<PlayerStuff>();

        for (int i = 0; i < GameSettings.NumberOfPlayers; i++)
        {
            var newPlayer = GameObject.Instantiate(prefabPlayer);
            newPlayer.name = names[i];
            newPlayer.GetComponent<PlayerStuff>().color = playerColors[i];
            newPlayer.transform.SetParent(transform);

            var newPanelPlayer = GameObject.Instantiate(prefabPanelPlayer);
            var texts = newPanelPlayer.GetComponentsInChildren<Text>();
            texts[0].text = newPlayer.name;
            texts[1].text = newPlayer.GetComponent<Money>().MoneyAmount.ToString();
            newPanelPlayer.transform.SetParent(table.transform);
            newPanelPlayer.GetComponent<Image>().color = playerColors[i];
            newPanelPlayer.GetComponent<TradeCenter>().player = newPlayer.GetComponent<PlayerStuff>();
            newPlayer.GetComponent<PlayerStuff>().tablePlayer = newPanelPlayer;

            players.Add(newPlayer.GetComponent<PlayerStuff>());
             
        }
        
        currentPlayerIndex = 0;
        player = players[currentPlayerIndex];

        foreach (var item in players)
        {
            player = players[++currentPlayerIndex % players.Count];
            item.nextPlayer = player;
        }
        hud.HideAll();
         
        hud.ShowRoll();
         
    }

    public void RemovePlayer(PlayerStuff player)
    {
        if (players.Count == 2)
        {
            // winner is ----  player.nextPlayer
            print("winner is " + player.nextPlayer);
            
            GameObject.DontDestroyOnLoad(gameInfo);
            gameInfo.GetComponent<information>().WinnerName = player.nextPlayer.name;
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        }
        else
        {
            foreach (var item in players)
            {
                if (item.nextPlayer == player)
                {
                    item.nextPlayer = player.nextPlayer;
                }
            }

            players.Remove(player);

            player.tablePlayer.SetActive(false);
            player.gameObject.SetActive(false);

        }
    }

     
}
