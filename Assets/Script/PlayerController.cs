using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 0.5f;

    private float fireCooldown = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = fireRate;
        }

        if (fireCooldown > 0f)
            fireCooldown -= Time.deltaTime;
    }

    void Shoot()
    {
        Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
    }
}
