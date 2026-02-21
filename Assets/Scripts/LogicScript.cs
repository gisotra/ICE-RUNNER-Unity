using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LogicScript : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject MenuScreen;
    public GameObject player;
    public GameObject om; //Obstacle Manager
    public bool game_is_running = false;
    public Text scorePoints;
    public float scoreValue = 0f;
    public float factor_speed = 1.0f;
    public float max_factor_speed = 2.8f;
    public int nextMilestone = 750;

    // Update is called once per frame
    void Update()
    {
        if (!game_is_running) {
            return;
        }
        scoreValue += Time.deltaTime * 100;
        scorePoints.text = Mathf.RoundToInt(scoreValue).ToString();

        if(scoreValue >= nextMilestone)
        {
            if (factor_speed < max_factor_speed)
            {
                factor_speed += 0.15f;
                if (factor_speed > max_factor_speed)
                {
                    factor_speed = max_factor_speed;
                }
            } 
            
            nextMilestone += 750 + (int)(scoreValue * 0.1f); //cast para transformar em um valor inteiro
        }

        
    }

    public void StartGame()
    {
        game_is_running = true;
        player.SetActive(true);
        om.SetActive(true);
        MenuScreen.SetActive(false);
        RestartLevel();
        scoreValue = 0.0f;
    }

    public void RestartLevel()
    {
        player.GetComponent<PlayerScript>().RestartPosition();
        om.GetComponent<ObstacleManager>().ObstacleReposition();
        gameOverScreen.SetActive(false);
        scoreValue = 0.0f;
        factor_speed = 1.0f;
        nextMilestone = 750;
        game_is_running = true;

    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        scoreValue = 0.0f;
    }

    public void GoToMenu()
    {
        game_is_running = false;
        player.SetActive(false);
        om.SetActive(false);
        gameOverScreen.SetActive(false);
        MenuScreen.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
