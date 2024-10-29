using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 450f; // �������� ����
    private float damage;      // ���� ����
    public Rigidbody rb;
    public GameObject shooter;
    public MonoBehaviour shooterTeam;

    void Start()
    {
        rb.velocity = transform.forward * speed; // ���������� ���� ������
        Destroy(gameObject, 1f);  // ���������� ���� ����� 2 �������
    }

    public void SetDamage(float damageAmount)
    {
        damage = damageAmount;  // ������������� ����, ���������� �� ������
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ��������, ����� ������ ����� ������� �� �������� ���� ���� �����
        if ((shooterTeam is RedTeam && collision.gameObject.GetComponent<BlueTeam>() != null) ||
        (shooterTeam is BlueTeam && collision.gameObject.GetComponent<RedTeam>() != null))
        {
            // �������� �� ��������� � ������
            Health health = collision.gameObject.GetComponent<Health>();
            if (health != null)
            {
                // �������� ��� ������� ���������� ������
                health.TakeDamage(damage, shooterTeam);
            }
        }

        // � ����� ������, ���� ������������ ��� ������������ � ���-����
        Destroy(gameObject);
    }

}
