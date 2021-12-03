using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float speed; //скорость полёта препятсвия
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //подпись на событие изменения уровня
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * -1, 0); //движение препятсвия
    }
}
