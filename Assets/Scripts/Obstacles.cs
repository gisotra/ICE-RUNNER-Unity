using UnityEngine;

public class Obstacles : MonoBehaviour
{
    public LogicScript ls;
    private float uniformSpeed = 3.5f;
    private int X_spawn_point = 12;
    private int X_despawn_point = -12;


    private void Awake()
    {
        ls = GameObject.FindGameObjectWithTag("Main Logic").GetComponent<LogicScript>();
    }

    void Update()
    {
        if (!ls.game_is_running){
            return;
        }
        if (isActiveAndEnabled)
        {
            transform.position -= new Vector3((uniformSpeed * ls.factor_speed) * Time.deltaTime, 0);
            if (transform.position.x <= X_despawn_point) //obstáculo ultrapassou os limites da camera
            {
                transform.position = new Vector3(X_spawn_point, transform.position.y);
                gameObject.SetActive(false);

            }
        }

    }
}
