using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float bottom; //нижн€€ точка спавнера
    private float top; //верхн€€ точка спавнера

    [SerializeField] GameController gameController;

    [Header("Sbstacles")]
    [SerializeField] private float obstacleMinCD; // кд на спавн преп€тсвий
    [SerializeField] private float obstacleMaxCD; // кд на спавн преп€тсвий
    [SerializeField] private float obstacleLifeTime;
    private float obstacleTimeLeft; //внутренний счЄтчик времени (кд)
    [SerializeField] private List<GameObject> obstacles = new List<GameObject>(); //список преп€тствий
    
    [Header("Buffs")]
    [SerializeField] private float buffMinCD; // кд на спавн buff
    [SerializeField] private float buffMaxCD; // кд на спавн buff
    [SerializeField] private float buffLifeTime;
    private float buffTimeLeft; //внутренний счЄтчик времени (кд)
    [SerializeField] private List<GameObject> buffPrefabs = new List<GameObject>(); //shield

    void Start()
    {
        top = transform.GetComponent<SpriteRenderer>().bounds.max.y;
        bottom = transform.GetComponent<SpriteRenderer>().bounds.min.y;
        obstacleTimeLeft = Random.Range(obstacleMinCD, obstacleMaxCD);
        buffTimeLeft = Random.Range(buffMinCD, buffMaxCD);
        //подпись на событие изменени€ уровн€
    }

    void Update()
    {
        if (gameController.gameStarted)
        {
            obstacleTimeLeft -= Time.deltaTime;
            buffTimeLeft -= Time.deltaTime;
        }
        

        if (obstacleTimeLeft <= 0)
        {
            GameObject obstacle;
            obstacle = Instantiate(obstacles[Random.Range(0, obstacles.Count)], new Vector2(transform.position.x, Random.Range(top, bottom)), Quaternion.identity);
            obstacleTimeLeft = Random.Range(obstacleMinCD / gameController.level, obstacleMaxCD / gameController.level);
            Destroy(obstacle, obstacleLifeTime / gameController.level);
        }

        if (buffTimeLeft <= 0)
        {
            GameObject buff;
            buff = Instantiate(buffPrefabs[Random.Range(0, buffPrefabs.Count)], new Vector2(transform.position.x, Random.Range(top, bottom)), Quaternion.identity);
            buffTimeLeft = Random.Range(buffMinCD / gameController.level, buffMaxCD / gameController.level);
            Destroy(buff, buffLifeTime / gameController.level);
        }
    }
}
