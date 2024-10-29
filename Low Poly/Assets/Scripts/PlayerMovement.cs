using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller; // ������ �� ��������� CharacterController

    // ���� ������ � �������� ������������
    public float speed = 9f;               // �������� ������������
    public float jump = 5f;                // ���� ������

    // ��������� ����������
    public float gravity = -9.81f;         // ����������
    public float groundDistance = 0.4f;    // ���������� ��� �������� �����
    
    // ����� ������� � ����� � ���� �����
    public Transform groundCheck;          // ����� �������� �����
    public LayerMask groundMask;           // ���� ��� ����� (Ground)

    Vector3 velocity;
    bool isGrounded;


    void Update()
    {
        // ���������, �� ����� �� �����
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // ��������� ��������� � �����
        }

        // �������� ������
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // ������
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jump * -2f * gravity);
        }

        // ��������� ����������
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
