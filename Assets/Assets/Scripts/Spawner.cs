using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float bottom; //������ ����� ��������
    private float top; //������� ����� ��������

    [SerializeField] GameController gameController;

    [Header("Sbstacles")]
    [SerializeField] private float obstacleMinCD; // �� �� ����� ����������
    [SerializeField] private float obstacleMaxCD; // �� �� ����� ����������
    [SerializeField] private float obstacleLifeTime;
    private float obstacleTimeLeft; //���������� ������� ������� (��)
    [SerializeField] private List<GameObject> obstacles = new List<GameObject>(); //������ �����������
    
    [Header("Buffs")]
    [SerializeField] private float buffMinCD; // �� �� ����� buff
    [SerializeField] private float buffMaxCD; // �� �� ����� buff
    [SerializeField] private float buffLifeTime;
    private float buffTimeLeft; //���������� ������� ������� (��)
    [SerializeField] private List<GameObject> buffPrefabs = new List<GameObject>(); //shield

    void Start()
    {
        top = transform.GetComponent<SpriteRenderer>().bounds.max.y;
        bottom = transform.GetComponent<SpriteRenderer>().bounds.min.y;
        obstacleTimeLeft = Random.Range(obstacleMinCD, obstacleMaxCD);
        buffTimeLeft = Random.Range(buffMinCD, buffMaxCD);
        //������� �� ������� ��������� ������
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
