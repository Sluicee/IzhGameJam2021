using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int level { get; private set; }
    
    [SerializeField] private Player player;

    public bool gameStarted { get; private set; }
    [SerializeField] private GameObject GUI;
    [SerializeField] private GameObject Menu;
    [SerializeField] private GameObject endScreen;
    public delegate void GameControllerDelegate();
    public static event GameControllerDelegate GameStarted;

    [Header("Sounds")]
    [SerializeField] private AudioSource bgMusic;
    [SerializeField] private AudioSource loseSound;

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
    [SerializeField] private TMP_Text endHighScoreText;
    [SerializeField] private TMP_Text endScoreText;
    [SerializeField] private int scoreMultiply;
    private float scoreMuliplyTimeLeft = 0;
    private float scoreBonusMuliply = 1;
    private bool scoreBonusMuliplyActive = false;
    private float score;
    private float highScore;
    private float levelProgress = 0;

    [Header("Camera")]
    [SerializeField] private Animator cameraAnimator;

    private void Start()
    {
        gameStarted = false;
        level = 1;
        Player.HPChange += HPChange;
        Player.Death += Death;
        Player.ManaChange += ManaChange;
        Gun.ShootEvent += ManaChange;
        Player.CoinCollected += CoinCollected;
        highScoreText.SetText("High Score: " + PlayerPrefs.GetFloat("HighScore", 0));
        drawHPs();
        drawManas();
    }

    private void OnDestroy()
    {
        Player.HPChange -= HPChange;
        Player.Death -= Death;
        Player.ManaChange -= ManaChange;
        Gun.ShootEvent -= ManaChange;
        Player.CoinCollected -= CoinCollected;
    }

    private void Update()
    {
        if (gameStarted)
        {
            if (scoreBonusMuliplyActive)
            {
                scoreMuliplyTimeLeft -= Time.deltaTime;
                if (scoreMuliplyTimeLeft <= 0)
                {
                    scoreMuliplyTimeLeft = 0;
                    scoreBonusMuliplyActive = false;
                    scoreBonusMuliply = 1;
                }
            }
            score += Time.deltaTime * scoreMultiply * scoreBonusMuliply;
            levelProgress += Time.deltaTime * scoreMultiply * scoreBonusMuliply;
            scoreText.SetText("Score: " + Mathf.Round(score).ToString());
            if (levelProgress >= 300 && level < 4)
            { 
                levelProgress = 0;
                level++;
            }
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene("Game");
    }

    public void StartGame()
    {
        cameraAnimator.Play("CameraZoom");
        Menu.SetActive(false);
        StartCoroutine(StartFade(bgMusic, 2, 0.6f));
        StartCoroutine(wait());
        
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(cameraAnimator.GetCurrentAnimatorStateInfo(0).length);
        GameStarted?.Invoke();
        GUI.SetActive(true);
        gameStarted = true;
    }

    private void CoinCollected()
    {
        scoreBonusMuliplyActive = true;
        scoreMuliplyTimeLeft = 10;
        scoreBonusMuliply = 2;
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
        gameStarted = false;
        StartCoroutine(StartFade(bgMusic, 0.5f, 0));
        loseSound.Play();
        if (score > highScore) { 
            highScore = Mathf.Round(score);
            highScoreText.SetText("High Score: " + highScore.ToString());
        }
        PlayerPrefs.SetFloat("HighScore", highScore);
        GUI.SetActive(false);
        endScreen.SetActive(true);
        endScoreText.SetText("Score: " + Mathf.Round(score).ToString());
        endHighScoreText.SetText("High Score: " + PlayerPrefs.GetFloat("HighScore", 0));
    }
    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}
