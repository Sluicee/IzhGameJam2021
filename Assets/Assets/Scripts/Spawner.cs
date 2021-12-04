using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float bottom; //������ ����� ��������
    private float top; //������� ����� ��������

    [Header("Sbstacles")]
    [SerializeField] private float obstacleCD; // �� �� ����� ����������
    [SerializeField] private float obstacleLifeTime;
    private float obstacleTimeLeft; //���������� ������� ������� (��)
    private float shieldTimeLeft; //���������� ������� ������� (��)
    [SerializeField] private List<GameObject> obstacles = new List<GameObject>(); //������ �����������
    
    [Header("Shield")]
    [SerializeField] private float shieldMinCD; // �� �� ����� shield
    [SerializeField] private float shieldMaxCD; // �� �� ����� shield
    [SerializeField] private float shieldLifeTime;
    [SerializeField] private GameObject shieldPrefab; //shield

    [Header("HP")]
    [SerializeField] private float HPMinCD; // �� �� ����� hp
    [SerializeField] private float HPMaxCD; // �� �� ����� hp
    [SerializeField] private float HPLifeTime;
    [SerializeField] private GameObject HPPrefab; //hp

    [Header("Mana")]
    [SerializeField] private float manaMinCD; // �� �� ����� mana
    [SerializeField] private float manaMaxCD; // �� �� ����� mana
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
        //������� �� ������� ��������� ������
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
