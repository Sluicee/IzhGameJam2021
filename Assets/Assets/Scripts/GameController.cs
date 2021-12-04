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
    [Header("Health Points")]
    [SerializeField] private GameObject HP;
    [SerializeField] private RectTransform HPSpawnPoint;
    [SerializeField] private float HPGap;
    private List<GameObject> HPs = new List<GameObject>();


    //score
    [Header("Score")]
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private int scoreMultiply;
    private float score;
    private float highScore;

    private void Start()
    {
        Player.HPChange += HPChange;
        Player.Death += Death;
        drawHPs();
    }

    private void OnDestroy()
    {
        Player.HPChange -= HPChange;
    }

    private void Update()
    {
        score += Time.deltaTime * scoreMultiply;
        scoreText.SetText("Score: " + Mathf.Round(score).ToString());
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

    private void Death()
    {
        if (score > highScore) { 
            highScore = Mathf.Round(score);
            highScoreText.SetText("High Score: " + highScore.ToString());
        }
        PlayerPrefs.SetFloat("HighScore", highScore);
    }
}
