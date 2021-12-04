using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] private int level;
    //событие изменение уровня, на которое подписаны obstacle and spawner
    
    [SerializeField] private Player player;

    //HP
    [SerializeField] private GameObject HP;
    [SerializeField] private RectTransform HPSpawnPoint;
    [SerializeField] private float HPGap;
    private List<GameObject> HPs = new List<GameObject>();

    //score
    [SerializeField] private TextMeshPro highScore;
    [SerializeField] private TextMeshPro scoreText;
    private int score;

    private void Start()
    {
        Player.HPChange += HPChange;
        drawHPs();
    }

    private void Update()
    {
        score += (int) Time.deltaTime;
        scoreText.SetText("Score: " + score.ToString());
    }

    private void HPChange()
    {
        foreach (GameObject hp in HPs)
        {
            Destroy(hp);
        }
        drawHPs();
    }

    private void drawHPs()
    {
        for (int i = 0; i < player.hp; i++)
        {
            GameObject hp = Instantiate(HP);
            hp.transform.position = new Vector3(hp.transform.position.x + i * HPGap, hp.transform.position.y, hp.transform.position.z);
            hp.transform.SetParent(HPSpawnPoint.transform, false);
            HPs.Add(hp);
        }
    }
}
