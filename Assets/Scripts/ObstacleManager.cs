using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class ObstacleManager : MonoBehaviour
{
    /*possui a responsabilidade de:
    > Armazenar os obstáculos préviamente (pooling)
    > Apenas dita QUANDO eles devem retornar*/
    public LogicScript ls;
    private float spawnTimer = 0;
    private float nextSpawn = 0;
    [SerializeField] Obstacles[] obstacle_array; //instanciados no próprio editor


    void Awake()
    {
        ls = GameObject.FindGameObjectWithTag("Main Logic").GetComponent<LogicScript>();
        if (obstacle_array.Length != 0)
        {
            ObstacleReposition(); //organizados no nascimento do objeto
        }
        DetermineNextSpawn();
    }
    
    void Update()
    {
        if (!ls.game_is_running)
        {
            return;
        }
        // o tempo "passa mais rápido" conforme o jogo se extende
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= nextSpawn)
        {
            spawnTimer = 0;
            SpawnObstacle();
            DetermineNextSpawn();
        }
    }

    void SpawnObstacle()
    {
        bool i = false;
        do
        {
            int randomValue = UnityEngine.Random.Range(0, obstacle_array.Length);
            if (obstacle_array[randomValue].isActiveAndEnabled)
            {
                // esse obstáculo JÁ FOI spawnado, então nada acontece.
                i = false;
            }
            else {
                i = true;
                obstacle_array[randomValue].gameObject.SetActive(true);
                break;
            };


        } while (!i);

    }

    public void ObstacleReposition()
    {
        for(int i = 0; i < obstacle_array.Length; i++)
        {

            obstacle_array[i].transform.position = new Vector2(12, -2.1f);
            obstacle_array[i].gameObject.SetActive(false);
        }
    }

    public void DetermineNextSpawn()
    {
        float minTime = 1.2f;
        float maxTime = 2.8f;

        float currentMinTime = minTime / ls.factor_speed; 
        float currentMaxTime = maxTime / ls.factor_speed; //à medida que a velocidade do jogo AVANÇA, o tempo fica MENOR 

        currentMinTime = MathF.Max(currentMinTime, 0.65f);
        currentMaxTime = MathF.Max(currentMaxTime, 1.2f);  //retorno qual o maior valor dentre os dois parâmetros, ou seja, limito o QUÃO PEQUENO é o valor de espera
        
        nextSpawn = UnityEngine.Random.Range(currentMinTime, currentMaxTime);
    }
}
