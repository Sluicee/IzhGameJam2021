using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float bottom; //нижн€€ точка спавнера
    private float top; //верхн€€ точка спавнера

    [Header("Sbstacles")]
    [SerializeField] private float obstacleCD; // кд на спавн преп€тсвий
    [SerializeField] private float obstacleLifeTime;
    private float obstacleTimeLeft; //внутренний счЄтчик времени (кд)
    private float shieldTimeLeft; //внутренний счЄтчик времени (кд)
    [SerializeField] private List<GameObject> obstacles = new List<GameObject>(); //список преп€тствий
    
    [Header("Shield")]
    [SerializeField] private float shieldMinCD; // кд на спавн shield
    [SerializeField] private float shieldMaxCD; // кд на спавн shield
    [SerializeField] private float shieldLifeTime;
    [SerializeField] private GameObject shieldPrefab; //shield

    [Header("HP")]
    [SerializeField] private float HPMinCD; // кд на спавн hp
    [SerializeField] private float HPMaxCD; // кд на спавн hp
    [SerializeField] private float HPLifeTime;
    [SerializeField] private GameObject HPPrefab; //hp

    [Header("Mana")]
    [SerializeField] private float manaMinCD; // кд на спавн mana
    [SerializeField] private float manaMaxCD; // кд на спавн mana
    [SerializeField] private float manaLifeTime;
    [SerializeField] private GameObject manaPrefab; //mana

    void Start()
    {
        top = transform.GetComponent<SpriteRenderer>().bounds.max.y;
        bottom = transform.GetComponent<SpriteRenderer>().bounds.min.y;
        obstacleTimeLeft = obstacleCD;
        shieldTimeLeft = Random.Range(shieldMinCD, shieldMaxCD);
        HPLifeTime = Random.Range(HPMinCD, HPMaxCD);
        manaLifeTime = Random.Range(manaMinCD, manaMaxCD);
        //подпись на событие изменени€ уровн€
    }

    void Update()
    {
        obstacleTimeLeft -= Time.deltaTime;
        shieldTimeLeft -= Time.deltaTime;
        manaLifeTime -= Time.deltaTime;
        HPLifeTime -= Time.deltaTime;
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
            shieldTimeLeft = Random.Range(shieldMinCD, shieldMaxCD);
            Destroy(shield, shieldLifeTime);
        }

        if(manaLifeTime <= 0)
        {
            GameObject mana;
            mana = Instantiate(manaPrefab, new Vector2(transform.position.x, Random.Range(top, bottom)), Quaternion.identity);
            manaLifeTime = Random.Range(manaMinCD, manaMaxCD);
            Destroy(mana, manaLifeTime);
        }

        if(HPLifeTime <= 0)
        {
            GameObject hp;
            hp = Instantiate(HPPrefab, new Vector2(transform.position.x, Random.Range(top, bottom)), Quaternion.identity);
            HPLifeTime = Random.Range(HPMinCD, HPMaxCD);
            Destroy(hp,HPLifeTime);
        }
    }
}
