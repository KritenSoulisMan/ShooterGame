using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;  // ���������������� ����
    public Transform playerBody;           // ������ �� ���� ������
    public Transform weaponTransform;      // ������ �� ������
    public Transform cameraTransform;      // ������ �� ������ ������

    public Vector3 weaponOffset = new Vector3(0.5f, -1.3f, 0.9f); // �������� ������
    public Vector3 weaponRotationOffset = new Vector3(0f, 90f, 0f); // �������� ���� �������� ������

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // �������� ������
    }

    void Update()
    {
        // �������� �������� ����
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // ������������ �������� �� ��� X (�����-����)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // ������������ ������ �� ��� X
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // ������������ ���� ������ �� ��� Y (�����-������)
        playerBody.Rotate(Vector3.up * mouseX);

        // �������� ������ � ������� � ������ cameraTransform � weaponRotationOffset
        weaponTransform.position = cameraTransform.position + cameraTransform.TransformDirection(weaponOffset);
        weaponTransform.rotation = cameraTransform.rotation * Quaternion.Euler(weaponRotationOffset);
    }
}
