using System;
using Unity.VisualScripting;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    /*a diferença de comportamento do pássaro para os outros obstáculos é que ele oscila verticalmente,
    independente disso, a propriedade de spawn e pooling é responsabilidade do Obstacle component, e não 
    do pássaro. 
     */ 
    public LogicScript ls; 
    private float maxHeight = 0.78f;
    private float minHeight = -2.27f;

    public float time = 0.0f; //vou utilizar sin() para efeito de parábola 

    void Start()
    {
        ls = GameObject.FindGameObjectWithTag("Main Logic").GetComponent<LogicScript>();
    }

    void Update()
    {
        if (!ls.game_is_running) {
            return;
        }
        time += Time.deltaTime;
        float t = (float) ((Math.Sin(time * 2.3) + 1.0f)/ 2);
        float linearT = Mathf.Lerp(maxHeight, minHeight, t);

        transform.position = new Vector3(transform.position.x, linearT, transform.position.z);

    }
}
