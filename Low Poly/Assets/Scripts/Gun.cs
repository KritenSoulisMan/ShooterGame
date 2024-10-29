using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 32f;               // Урон от выстрела
    public float range = 100f;               // Дальность стрельбы

    [HideInInspector] 
    public int currentAmmo;                  // Текущий запас патронов
    public int maxAmmo = 30;                 // Максимальный размер магазина
    public int totalAmmo = 120;              // Общее количество патронов

    public Camera fpsCam;                    // Камера игрока для определения направления
    public GameObject bulletPrefab;          // Префаб пули
    public Transform firePoint;              // Точка, откуда выпускается пуля

    void Start()
    {
        currentAmmo = maxAmmo; // Начинаем с полного магазина
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && currentAmmo > 0)
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))  // Перезарядка
        {
            Reload();
        }
    }

    void Shoot()
    {
        if (currentAmmo > 0)
        {
            currentAmmo--;  // Уменьшаем количество патронов

            // Рэйкаст для стрельбы по целям
            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);
                Health health = hit.transform.GetComponent<Health>();
                if (health != null)
                {
                    // Передаем компонент команды
                    health.TakeDamage(damage, GetComponent<MonoBehaviour>());
                }
            }

            // Спавним пулю
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.SetDamage(damage);  // Передаем урон пуле
                bulletScript.shooterTeam = GetComponent<MonoBehaviour>();  // Передаем объект стрелка пуле
            }
        }
        else
        {
            Reload();
        }
    }

    void Reload()
    {
        // Моментальная перезарядка.
        int ammoNeeded = maxAmmo - currentAmmo;
        if (totalAmmo >= ammoNeeded)
        {
            totalAmmo -= ammoNeeded;
            currentAmmo = maxAmmo;
        }
        else
        {
            currentAmmo += totalAmmo;
            totalAmmo = 0;
        }
        Debug.Log("Оружее перезаряжено!");
    }
}
