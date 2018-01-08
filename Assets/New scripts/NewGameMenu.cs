
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.New_scripts;

public class NewGameMenu : MonoBehaviour
{

    public GameObject rootMenu;
    public Text quantityPl;
    Slider slider;
    VerticalLayoutGroup table;
    public InputField playerPrefab;

    private void Start()
    {
        slider = GetComponentInChildren<Slider>();
        table = GetComponentInChildren<VerticalLayoutGroup>();
        TablePlayers();
        ChangeNumberOfPlayers();
    }
    public void ToRootMenu()
    {
        gameObject.SetActive(false);
        rootMenu.SetActive(true);
        
    }

    public void StartGame()
    {
        if (CheckNames())
        {
            GameSettings.NumberOfPlayers = (int)GetComponentInChildren<Slider>().value;
            GameSettings.standartNames = new List<string>();
            foreach (Transform item in table.transform)
            {
                GameSettings.standartNames.Add(item.GetComponentsInChildren<Text>()[1].text);
            }
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }

    }

    bool CheckNames()
    {
        
        foreach (Transform player in table.transform)
        {
            if (player.GetComponentsInChildren<Text>()[1].text == "")
            {
                GameObject.Find("FillNames").GetComponent<Blink>().enabled = true;
                return false;

            }
        }
        return true;
    }
    public void ChangeNumberOfPlayers()
    {
        quantityPl.text = GetComponentInChildren<Slider>().value.ToString();
    }


    public void TablePlayers()
    {
        if ((int)slider.value > table.transform.childCount)
        {
            for (int i = table.transform.childCount; i < (int)slider.value; i++)
            {
                InputField newPl = GameObject.Instantiate(playerPrefab);

                newPl.transform.SetParent(table.transform);
            }

        }
        else
        {
            for (int i = table.transform.childCount; i > (int)slider.value; i--)
            {
                Transform s = table.transform.GetChild(i - 1);

                GameObject.Destroy(s.gameObject);
            }
        }
    }

    public void FillNames()
    {
        for (int i = 0; i < table.transform.childCount; i++)
        {
            table.transform.GetChild(i).GetComponentsInChildren<Text>()[1].text = GameSettings.standartNames[i];
            table.transform.GetChild(i).GetComponentsInChildren<Text>()[1].text = "klasdjf";

        }
        print("filled");
        
    }
}
