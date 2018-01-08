using UnityEngine;
using UnityEngine.UI;

public class information : MonoBehaviour {

    private string winnerName;

    public string WinnerName
    {
        get
        {
            return winnerName;
        }

        set
        {
            winnerName = value;
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 2)
        {
            GameObject.Find("Info").GetComponentsInChildren<Text>()[0].text = WinnerName;
            GameObject.Find("Info").GetComponentInChildren<Button>().onClick.AddListener(ToMainMenu);

        }
        else if (level == 0) {
            GameObject.Destroy(gameObject);
        }
    }

    public void ToMainMenu()
    {

        UnityEngine.SceneManagement.SceneManager.LoadScene(0);

    }

}
