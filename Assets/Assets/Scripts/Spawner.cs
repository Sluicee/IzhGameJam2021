using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float countDown; // �� �� ����� ����������
    private float objectHeight; //������� ��������
    private float bottom; //������ ����� ��������
    private float top; //������� ����� ��������
    private float timeLeft; //���������� ������� ������� (��)
    [SerializeField] List<GameObject> obstacles = new List<GameObject>(); //������ �����������

    void Start()
    {
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y;
        top = objectHeight / 2;
        bottom = -objectHeight / 2;
        timeLeft = countDown;
        //������� �� ������� ��������� ������
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
