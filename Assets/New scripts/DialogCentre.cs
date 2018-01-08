using UnityEngine;
using UnityEngine.UI;

public class DialogCentre : MonoBehaviour {

    Text text;


    public GameObject message;


	// Use this for initialization
	void Start () {
        text = message.GetComponentInChildren<Text>();
             
	}
	

    public void ShowMessage(string mes)
    {
        GameObject newMessage = GameObject.Instantiate(message);
        newMessage.GetComponentInChildren<Text>().text = mes;
        newMessage.transform.SetParent(transform);

        newMessage.GetComponent<RevealMessage>().ChangeLabelState(true);

    }
}
