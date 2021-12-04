using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletForce;
    [SerializeField] private float bulletLifeTime;
    [SerializeField] private GameController gameController;
    private Transform firePoint;

    private void Start()
    {
        firePoint = transform;
    }

    public void Shoot()
    {
        if (gameController.mana > 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rigidbody2D = bullet.GetComponent<Rigidbody2D>();
            rigidbody2D.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
            Destroy(bullet, bulletLifeTime);
            gameController.mana -= 1;
        }
        
    }
}
