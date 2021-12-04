using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int mana;
    public int hp;
    public bool deff = false;
    [SerializeField] private GameObject shield;

    public delegate void DeathDelegate();
    public static event DeathDelegate Death;

    public delegate void HPDelegate();
    public static event HPDelegate HPChange;

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
                }
                else
                {
                    hp--;
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
                shield.SetActive(true);
                Destroy(collision.gameObject);
                break;
        }
        
    }
}
