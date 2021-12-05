using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float speed; //скорость полёта препятсвия
    [SerializeField] private GameController controller;
    private Rigidbody2D rb;
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = FindObjectOfType<GameController>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(controller.level * speed * -1, 0); //движение препятсвия
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
}
