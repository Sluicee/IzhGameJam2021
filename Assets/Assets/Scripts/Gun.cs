using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletForce;
    [SerializeField] private float bulletLifeTime;
    [SerializeField] private Player player;
    private Transform firePoint;

    public delegate void ShootDelegete();
    public static event ShootDelegete ShootEvent;

    private void Start()
    {
        firePoint = transform;
    }

    public void Shoot()
    {
        
        if (player.mana > 0)
        { 
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rigidbody2D = bullet.GetComponent<Rigidbody2D>();
            rigidbody2D.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
            Destroy(bullet, bulletLifeTime);
            player.mana -= 1;
        }
        ShootEvent?.Invoke();

    }
}
