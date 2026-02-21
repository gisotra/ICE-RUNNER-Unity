using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public LogicScript ls;
    public float base_speed;

    public float layerLength;
    public Vector3 initialPosition;
    private float traveledDist;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ls = GameObject.FindGameObjectWithTag("Main Logic").GetComponent<LogicScript>();
        initialPosition = transform.position;
        layerLength = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (!ls.game_is_running)
        {
            return;
        }

        traveledDist += (base_speed * ls.factor_speed) * Time.deltaTime;
        float sprite_offset = traveledDist % layerLength;
        transform.position = new Vector3(initialPosition.x - sprite_offset, transform.position.y, initialPosition.z);

        
    }
}
