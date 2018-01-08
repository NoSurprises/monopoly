using UnityEngine;

public class DeleteAfter : MonoBehaviour {

    public float seconds;

    float time = 0f;
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time > seconds)
            GameObject.Destroy(gameObject);
	}
}
