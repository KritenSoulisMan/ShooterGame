using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    public float health = 100f;
    public TMP_Text healthText; // Ссылка на UI элемент для здоровья

    void Start()
    {
        // Обновляем начальное значение здоровья на UI
        UpdateHealthUI();
    }

    public void TakeDamage(float amount, MonoBehaviour attackerTeam)
    {
        // Проверка, чтобы урон проходил только по вражеской команде
        if ((attackerTeam is RedTeam && GetComponent<BlueTeam>() != null) ||
        (attackerTeam is BlueTeam && GetComponent<RedTeam>() != null))
        {
            health -= amount;
            if (health < 0f) health = 0f;

            // Обновляем UI после получения урона
            UpdateHealthUI();

            if (health <= 0f)
            {
                Die();
            }
        }
    }

    void UpdateHealthUI()
    {
        // Обновляем текстовое поле с текущим значением здоровья
        healthText.text = "HP: " + health.ToString();
    }

    void Die()
    {
        // Здесь можно добавить любые действия при смерти (например, перезагрузка сцены)
        Destroy(gameObject); // Уничтожение объекта
    }
}
