using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller; // Ссылка на компонент CharacterController

    // Сила прыжка и скорость передвижения
    public float speed = 9f;               // Скорость передвижения
    public float jump = 5f;                // Сила прыжка

    // Настройка гравитации
    public float gravity = -9.81f;         // Гравитация
    public float groundDistance = 0.4f;    // Расстояние для проверки земли
    
    // Точка касания с землёй и слой земли
    public Transform groundCheck;          // Точка проверки земли
    public LayerMask groundMask;           // Слой для земли (Ground)

    Vector3 velocity;
    bool isGrounded;


    void Update()
    {
        // Проверяем, на земле ли игрок
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Прижимаем персонажа к земле
        }

        // Движение игрока
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // Прыжок
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jump * -2f * gravity);
        }

        // Применяем гравитацию
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
