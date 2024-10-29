using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;  // Чувствительность мыши
    public Transform playerBody;           // Ссылка на тело игрока
    public Transform weaponTransform;      // Ссылка на оружие
    public Transform cameraTransform;      // Ссылка на камеру игрока

    public Vector3 weaponOffset = new Vector3(0.5f, -1.3f, 0.9f); // Смещение оружия
    public Vector3 weaponRotationOffset = new Vector3(0f, 90f, 0f); // Смещение угла поворота оружия

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Скрываем курсор
    }

    void Update()
    {
        // Получаем движение мыши
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Ограничиваем вращение по оси X (вверх-вниз)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Поворачиваем камеру по оси X
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Поворачиваем тело игрока по оси Y (влево-вправо)
        playerBody.Rotate(Vector3.up * mouseX);

        // Смещение оружия и поворот с учетом cameraTransform и weaponRotationOffset
        weaponTransform.position = cameraTransform.position + cameraTransform.TransformDirection(weaponOffset);
        weaponTransform.rotation = cameraTransform.rotation * Quaternion.Euler(weaponRotationOffset);
    }
}
