using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float objectHeight; //размеры спавнера
    private float bottom; //нижн€€ точка спавнера
    private float top; //верхн€€ точка спавнера

    [Header("Sbstacles")]
    [SerializeField] private float obstacleCD; // кд на спавн преп€тсвий
    [SerializeField] private float obstacleLifeTime;
    private float obstacleTimeLeft; //внутренний счЄтчик времени (кд)
    private float shieldTimeLeft; //внутренний счЄтчик времени (кд)
    [SerializeField] private List<GameObject> obstacles = new List<GameObject>(); //список преп€тствий
    
    [Header("Shield")]
    [SerializeField] private float shieldCD; // кд на спавн shield
    [SerializeField] private float shieldLifeTime;
    [SerializeField] private GameObject shieldPrefab; //shield

    void Start()
    {
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y;
        top = objectHeight / 2;
        bottom = -objectHeight / 2;
        obstacleTimeLeft = obstacleCD;
        //подпись на событие изменени€ уровн€
    }

    void Update()
    {
        obstacleTimeLeft -= Time.deltaTime;
        shieldTimeLeft -= Time.deltaTime;
        if (obstacleTimeLeft <= 0)
        {
            GameObject obstacle;
            obstacle = Instantiate(obstacles[Random.Range(0, obstacles.Count)], new Vector2(transform.position.x, Random.Range(top, bottom)), Quaternion.identity);
            obstacleTimeLeft = obstacleCD;
            Destroy(obstacle, obstacleLifeTime);
        }

        if (shieldTimeLeft <= 0)
        {
            GameObject shield;
            shield = Instantiate(shieldPrefab, new Vector2(transform.position.x, Random.Range(top, bottom)), Quaternion.identity);
            shieldTimeLeft = shieldCD;
            Destroy(shield, shieldLifeTime);
        }
    }
}
