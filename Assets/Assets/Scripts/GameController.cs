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
    [SerializeField] private GameObject HPEmpty;
    [SerializeField] private RectTransform HPSpawnPoint;
    [SerializeField] private float HPGap;
    private List<GameObject> HPs = new List<GameObject>();

    //MANA
    [Header("Mana Points")]
    [SerializeField] private GameObject MANA;
    [SerializeField] private RectTransform manaSpawnPoint;
    [SerializeField] private float manaGap;
    private List<GameObject> manas = new List<GameObject>();


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
        Player.ManaChange += ManaChange;
        Gun.ShootEvent += ManaChange;
        highScore = PlayerPrefs.GetFloat("HighScore");
        drawHPs();
        drawManas();
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

    private void ManaChange()
    {
        foreach (GameObject mana in manas)
        {
            Destroy(mana);
        }
        drawManas();
    }

    private void drawHPs()
    {
        for (int i = 0; i < player.hp; i++)
        {
            GameObject hp = Instantiate(HP);
            hp.transform.position = new Vector3(hp.transform.position.x + i * HPGap, hp.transform.position.y, hp.transform.position.z);
            hp.transform.SetParent(HPSpawnPoint.transform, false);
            HPs.Add(hp);
            GameObject hp_empty = Instantiate(HPEmpty);
            hp_empty.transform.position = new Vector3(hp_empty.transform.position.x + i * HPGap, hp_empty.transform.position.y, hp_empty.transform.position.z);
            hp_empty.transform.SetParent(HPSpawnPoint.transform, false);
        }
    }

    private void drawManas()
    {
        for (int i = 0; i < player.mana; i++)
        {
            GameObject mana = Instantiate(MANA);
            mana.transform.position = new Vector3(mana.transform.position.x + i * manaGap, mana.transform.position.y, mana.transform.position.z);
            mana.transform.SetParent(manaSpawnPoint.transform, false);
            manas.Add(mana);
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
