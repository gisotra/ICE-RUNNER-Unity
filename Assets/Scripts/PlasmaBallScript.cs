using System;
using UnityEngine;

public class PlasmaBallScript : MonoBehaviour
{
    public LogicScript ls; 
    private float maxHeight = 0.58f;
    private float minHeight = -2.12f;

    private float XOffset = 1.58f;
    private float previousXOffset = 0.0f;

    public float time = 0.0f; //vou utilizar sin() para efeito de parábola 

    void Start()
    {
        ls = GameObject.FindGameObjectWithTag("Main Logic").GetComponent<LogicScript>();
    }

    void Onable()
    {
        time = 0.0f;
        previousXOffset = XOffset;        
    }

    // Update is called once per frame
    void Update()
    {
        if (!ls.game_is_running) {
            return;
        }
        time += Time.deltaTime;
        float t = (float) ((Math.Sin(time * 6) + 1.0f)/ 2);
        float u = (float) ((Math.Cos(time * 6) + 1.0f)/ 2);
        float linearT = Mathf.Lerp(maxHeight, minHeight, t); // eu utilizar sin e cos gera uma movimentação circular
        float linearX = Mathf.Lerp(XOffset, -XOffset, u);
        float deltaX = linearX - previousXOffset;
        previousXOffset = linearX;


        transform.position = new Vector3(transform.position.x  + deltaX, linearT, transform.position.z);
        
    }
}
