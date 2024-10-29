using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 32f;               // ���� �� ��������
    public float range = 100f;               // ��������� ��������

    [HideInInspector] 
    public int currentAmmo;                  // ������� ����� ��������
    public int maxAmmo = 30;                 // ������������ ������ ��������
    public int totalAmmo = 120;              // ����� ���������� ��������

    public Camera fpsCam;                    // ������ ������ ��� ����������� �����������
    public GameObject bulletPrefab;          // ������ ����
    public Transform firePoint;              // �����, ������ ����������� ����

    void Start()
    {
        currentAmmo = maxAmmo; // �������� � ������� ��������
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && currentAmmo > 0)
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))  // �����������
        {
            Reload();
        }
    }

    void Shoot()
    {
        if (currentAmmo > 0)
        {
            currentAmmo--;  // ��������� ���������� ��������

            // ������� ��� �������� �� �����
            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);
                Health health = hit.transform.GetComponent<Health>();
                if (health != null)
                {
                    // �������� ��������� �������
                    health.TakeDamage(damage, GetComponent<MonoBehaviour>());
                }
            }

            // ������� ����
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.SetDamage(damage);  // �������� ���� ����
                bulletScript.shooterTeam = GetComponent<MonoBehaviour>();  // �������� ������ ������� ����
            }
        }
        else
        {
            Reload();
        }
    }

    void Reload()
    {
        // ������������ �����������.
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
        Debug.Log("������ ������������!");
    }
}
