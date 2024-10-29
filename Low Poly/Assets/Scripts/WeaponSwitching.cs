using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0; // ��������� ������, ������� � ���������

    void Start()
    {
        SelectWeapon();
    }

    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        if (Input.GetKeyDown(KeyCode.Alpha1)) // ��� �� ����� 1
        {
            selectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) // �������� �� ����� 2
        {
            selectedWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) // ������� �� ����� 3
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
            // ���������� ������ ��������� ������
            weapon.gameObject.SetActive(i == selectedWeapon);

            if (i == selectedWeapon)
            {
                // ������������� ������� �� 90 �������� �� ��� Y
                weapon.localRotation = Quaternion.Euler(0, 90, 0);
            }

            i++;
        }
    }
}
