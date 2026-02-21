using UnityEngine;

public class Ground : MonoBehaviour
{
    public Rigidbody2D groundBody;
    public LogicScript ls;

    void Start()
    {
        ls = GameObject.FindGameObjectWithTag("Main Logic").GetComponent<LogicScript>();
    }

}
