using UnityEngine;
using UnityEngine.UI;

public class RootMenu : MonoBehaviour {

    public GameObject newGameMenu;
    public Image help;

    public void ToNewGame()
    {
        gameObject.SetActive(false);
        newGameMenu.SetActive(true);
    }

    public void ShowHelp()
    {
        help.gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
