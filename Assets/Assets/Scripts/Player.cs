using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int mana;
    public int hp;

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
                hp--;
                HPChange();
                Destroy(collision.gameObject);
                break;
        }
        if (hp == 0)
        {
            //Death();
            Debug.Log("Death");
        }
    }
}
