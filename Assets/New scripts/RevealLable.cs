using UnityEngine;

public class RevealLable : MonoBehaviour {

    public float time;
    float currentChangeTime;
    bool changing;
    private bool _visible;
    TextMesh text;
    Color color;


    public bool makeVisible
    {
        get { return _visible; }
        set {
            _visible = value;
            changing = true;
        }
    }

    private void Start()
    {
        text = GetComponent<TextMesh>();
        time = 1f;
        

    }

    // Update is called once per frame
    void Update () {
		if (changing )
        {
            currentChangeTime += Time.deltaTime;
            
            color = text.color;
            if (makeVisible)
                color.a = Mathf.Lerp(0f, 1f, currentChangeTime / time);
            else
            {
                color.a = Mathf.Lerp(1f, 0f, currentChangeTime / time);

            }
            text.color = color;
            if (currentChangeTime >= time)
            {
                changing = false;
                currentChangeTime = 0f;
            }
        }
	}

    internal void ChangeLabelState(bool reveal)
    {
        makeVisible = reveal;
        changing = true;
    }
}
