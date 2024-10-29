using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 450f; // Скорость пули
    private float damage;      // Урон пули
    public Rigidbody rb;
    public GameObject shooter;
    public MonoBehaviour shooterTeam;

    void Start()
    {
        rb.velocity = transform.forward * speed; // Направляем пулю вперед
        Destroy(gameObject, 1f);  // Уничтожаем пулю через 2 секунды
    }

    public void SetDamage(float damageAmount)
    {
        damage = damageAmount;  // Устанавливаем урон, полученный от оружия
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Проверка, чтобы игроки одной команды не наносили урон друг другу
        if ((shooterTeam is RedTeam && collision.gameObject.GetComponent<BlueTeam>() != null) ||
        (shooterTeam is BlueTeam && collision.gameObject.GetComponent<RedTeam>() != null))
        {
            // Проверка на попадание в игрока
            Health health = collision.gameObject.GetComponent<Health>();
            if (health != null)
            {
                // Передаем тег команды атакующего игрока
                health.TakeDamage(damage, shooterTeam);
            }
        }

        // В любом случае, пуля уничтожается при столкновении с чем-либо
        Destroy(gameObject);
    }

}
