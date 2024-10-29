using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    public float health = 100f;
    public TMP_Text healthText; // ������ �� UI ������� ��� ��������

    void Start()
    {
        // ��������� ��������� �������� �������� �� UI
        UpdateHealthUI();
    }

    public void TakeDamage(float amount, MonoBehaviour attackerTeam)
    {
        // ��������, ����� ���� �������� ������ �� ��������� �������
        if ((attackerTeam is RedTeam && GetComponent<BlueTeam>() != null) ||
        (attackerTeam is BlueTeam && GetComponent<RedTeam>() != null))
        {
            health -= amount;
            if (health < 0f) health = 0f;

            // ��������� UI ����� ��������� �����
            UpdateHealthUI();

            if (health <= 0f)
            {
                Die();
            }
        }
    }

    void UpdateHealthUI()
    {
        // ��������� ��������� ���� � ������� ��������� ��������
        healthText.text = "HP: " + health.ToString();
    }

    void Die()
    {
        // ����� ����� �������� ����� �������� ��� ������ (��������, ������������ �����)
        Destroy(gameObject); // ����������� �������
    }
}
