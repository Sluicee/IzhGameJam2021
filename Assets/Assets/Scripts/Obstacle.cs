using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float speed; //скорость полёта препятсвия
    private Rigidbody2D rb;
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameController.levelUP += levelChange;
    }

    private void OnDestroy()
    {
        GameController.levelUP -= levelChange;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * -1, 0); //движение препятсвия
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            if (audioSource != null)
            {
                audioSource.Play();
            }
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }

    private void levelChange()
    {
        speed++;
    }
}
