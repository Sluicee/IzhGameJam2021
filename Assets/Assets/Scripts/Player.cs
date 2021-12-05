using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int mana;
    public int hp;
    [SerializeField] private int maxHP;
    [SerializeField] private int maxMana;
    public bool deff = false;
    [SerializeField] private GameObject shield;

    public delegate void PlayerDelegate();
    public static event PlayerDelegate Death;
    public static event PlayerDelegate HPChange;
    public static event PlayerDelegate ManaChange;
    public static event PlayerDelegate CoinCollected;

    [Header("Sounds")]
    [SerializeField] private AudioSource punchSound;
    [SerializeField] private AudioSource buffPickUp;
    [SerializeField] private AudioSource shieldBreak;

    private void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Obstacle":
                if (deff)
                {
                    shield.SetActive(false);
                    deff = false;
                    shieldBreak.Play();
                }
                else
                {
                    hp--;
                    punchSound.Play();
                    HPChange?.Invoke();
                }
                Destroy(collision.gameObject);
                if (hp == 0)
                {
                    Death?.Invoke();
                    Debug.Log("Death");
                }
                break;
            case "BuffShield":
                deff = true;
                buffPickUp.Play();
                shield.SetActive(true);
                Destroy(collision.gameObject);
                break;
            case "BuffMana":
                if (mana < maxMana)
                {
                    mana++;
                    buffPickUp.Play();
                    ManaChange?.Invoke();
                }
                Destroy(collision.gameObject);
                break;
            case "BuffHP":
                if (hp < maxHP)
                {
                    hp++;
                    buffPickUp.Play();
                    HPChange?.Invoke();
                }
                Destroy(collision.gameObject);
                break;
            case "BuffCoin":
                buffPickUp.Play();
                CoinCollected?.Invoke();
                Destroy(collision.gameObject);
                break;
        }
        
    }
}
