using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float countDown; // кд на спавн преп€тсвий
    private float objectHeight; //размеры спавнера
    private float bottom; //нижн€€ точка спавнера
    private float top; //верхн€€ точка спавнера
    private float timeLeft; //внутренний счЄтчик времени (кд)
    [SerializeField] List<GameObject> obstacles = new List<GameObject>(); //список преп€тствий

    void Start()
    {
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y;
        top = objectHeight / 2;
        bottom = -objectHeight / 2;
        timeLeft = countDown;
        //подпись на событие изменени€ уровн€
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            GameObject obstacle;
            obstacle = Instantiate(obstacles[Random.Range(0, 1)], new Vector2(transform.position.x, Random.Range(top, bottom)), Quaternion.identity);
            timeLeft = countDown;
        }
    }
}
