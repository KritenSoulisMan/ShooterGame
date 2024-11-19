using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
#if UNITY_STANDALONE_WIN
using System.Windows.Forms;
#endif

public class AvatarLoader : MonoBehaviour
{
    public UnityEngine.UI.Button avatarButton;            // ������ ��� �������� ��������
    public Image avatarImage;                             // �������� ����������� ��������
    public Image secondaryAvatarImage;                    // ������ �����������, ������� ���� ����� ��������
    public TMP_Text errorText;                            // ����� ��� ������ (��������, ���� ���� ������� ������� ��� ��������� �������)

    private const int maxFileSize = 1048576;              // 1 MB � ������
    private string savePath;

    void Start()
    {
        // ������������� ���� � ����� "��� ���������\NameGame\AvSave"
        string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        savePath = Path.Combine(documentsPath, "NameGame", "AvSave");

        // ������� �����, ���� �� ���
        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }

        avatarButton.onClick.AddListener(OpenFileExplorer);

        // �������� ������� �� ����������� �����, ���� �� ����������
        LoadSavedAvatar();
    }

    private void OpenFileExplorer()
    {
#if UNITY_STANDALONE_WIN
        OpenFileDialog dialog = new OpenFileDialog();
        dialog.Filter = "���� �������� (*.png;*.gif)|*.png;*.gif";
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            LoadAvatar(dialog.FileName);
        }
#else
        errortext.text = "����� ������� �������� ������ �� Windows � ����������� ������.";
        Debug.LogWarning("����� ������� �������� ������ �� Windows.");
#endif
    }

    private void LoadAvatar(string path)
    {
        FileInfo fileInfo = new FileInfo(path);
        if (fileInfo.Length > maxFileSize)
        {
            errorText.text = "������: ���� ��������� 1 ��.";
            return;
        }

        // ��������� ���� ��� ������ ������
        byte[] fileData = File.ReadAllBytes(path);
        Texture2D texture = new Texture2D(2, 2);

        if (texture.LoadImage(fileData))
        {
            // ������ ������ �� ��������
            Sprite newAvatarSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

            // ��������� �����������
            avatarImage.sprite = newAvatarSprite;
            secondaryAvatarImage.sprite = newAvatarSprite;
            errorText.text = "";

            // ��������� �������� �� ����
            SaveAvatarToDisk(texture);
        }
        else
        {
            errorText.text = "������: ���������������� ������ �����.";
        }
    }

    private void SaveAvatarToDisk(Texture2D texture)
    {
        string avatarFilePath = Path.Combine(savePath, "avatar.png");
        byte[] pngData = texture.EncodeToPNG();

        if (pngData != null)
        {
            File.WriteAllBytes(avatarFilePath, pngData);
            Debug.Log("�������� ��������� �� ����: " + avatarFilePath);
        }
        else
        {
            Debug.LogError("������: �� ������� ��������� ��������.");
        }
    }

    private void LoadSavedAvatar()
    {
        string avatarFilePath = Path.Combine(savePath, "avatar.png");
        if (File.Exists(avatarFilePath))
        {
            byte[] fileData = File.ReadAllBytes(avatarFilePath);
            Texture2D texture = new Texture2D(2, 2);
            if (texture.LoadImage(fileData))
            {
                Sprite savedAvatarSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
                avatarImage.sprite = savedAvatarSprite;
                secondaryAvatarImage.sprite = savedAvatarSprite;
            }
        }
    }
}
