using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0; // Выбранное оружие, начиная с пистолета

    void Start()
    {
        SelectWeapon();
    }

    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        if (Input.GetKeyDown(KeyCode.Alpha1)) // Нож на цифру 1
        {
            selectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) // Пистолет на цифру 2
        {
            selectedWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) // Автомат на цифру 3
        {
            selectedWeapon = 2;
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            // Активируем только выбранное оружие
            weapon.gameObject.SetActive(i == selectedWeapon);

            if (i == selectedWeapon)
            {
                // Устанавливаем поворот на 90 градусов по оси Y
                weapon.localRotation = Quaternion.Euler(0, 90, 0);
            }

            i++;
        }
    }
}
