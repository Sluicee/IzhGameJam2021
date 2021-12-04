using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float objectHeight; //������� ��������
    private float bottom; //������ ����� ��������
    private float top; //������� ����� ��������

    [Header("Sbstacles")]
    [SerializeField] private float obstacleCD; // �� �� ����� ����������
    [SerializeField] private float obstacleLifeTime;
    private float obstacleTimeLeft; //���������� ������� ������� (��)
    private float shieldTimeLeft; //���������� ������� ������� (��)
    [SerializeField] private List<GameObject> obstacles = new List<GameObject>(); //������ �����������
    
    [Header("Shield")]
    [SerializeField] private float shieldCD; // �� �� ����� shield
    [SerializeField] private float shieldLifeTime;
    [SerializeField] private GameObject shieldPrefab; //shield

    void Start()
    {
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y;
        top = objectHeight / 2;
        bottom = -objectHeight / 2;
        obstacleTimeLeft = obstacleCD;
        //������� �� ������� ��������� ������
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
