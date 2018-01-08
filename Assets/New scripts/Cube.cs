using UnityEngine;

public class Cube : MonoBehaviour
{

    public Vector3 startPos;
    Rigidbody rb;
    public delegate void Finish(int val);
    public event Finish cubeRolled;
    bool finished;
    float time = 3f;
    float currTime;



    private void Start()
    {
        startPos = transform.position;


    }

    void AddImpulse()
    {
        rb.AddForce(Vector3.down/2, ForceMode.Impulse);
    }
    void AddRotation()
    {
        rb.AddTorque(Random.Range(-100f, 100f), Random.Range(-100f, 100f), Random.Range(-100f, 100f));
    }



    private void OnEnable()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        if (startPos != Vector3.zero) transform.position = startPos;
        transform.rotation = Random.rotation;
        AddImpulse();
        AddRotation();
        finished = false;
        currTime = 0f;

    }

    private void Update()
    {
        AddImpulse();
        currTime += Time.deltaTime;
        if (!finished && currTime > time)
        {

            cubeRolled(GetNumber());

            finished = true;

        }
    }
    public int GetNumber()
    {

        if (transform.up == Vector3.up)
            return 1;
        if (transform.up == Vector3.down)
            return 6;

        if (transform.forward == Vector3.up)
            return 4;
        if (transform.forward == Vector3.down)
            return 3;

        if (transform.right == Vector3.up)
            return 5;

        return 2;
        
    }
}


