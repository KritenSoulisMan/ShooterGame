using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
#if UNITY_STANDALONE_WIN
using System.Windows.Forms;
#endif

public class AvatarLoader : MonoBehaviour
{
    public UnityEngine.UI.Button avatarButton;            // Кнопка для загрузки аватарки
    public Image avatarImage;                             // Основное изображение аватарки
    public Image secondaryAvatarImage;                    // Второе изображение, которое тоже нужно обновить
    public TMP_Text errorText;                            // Текст для ошибок (например, если файл слишком большой или неверного формата)

    private const int maxFileSize = 1048576;              // 1 MB в байтах
    private string savePath;

    void Start()
    {
        // Устанавливаем путь к папке "Мои документы\NameGame\AvSave"
        string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        savePath = Path.Combine(documentsPath, "NameGame", "AvSave");

        // Создаем папки, если их нет
        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }

        avatarButton.onClick.AddListener(OpenFileExplorer);

        // Загрузка аватара из сохранённого файла, если он существует
        LoadSavedAvatar();
    }

    private void OpenFileExplorer()
    {
#if UNITY_STANDALONE_WIN
        OpenFileDialog dialog = new OpenFileDialog();
        dialog.Filter = "Файл картинки (*.png;*.gif)|*.png;*.gif";
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            LoadAvatar(dialog.FileName);
        }
#else
        errortext.text = "Выбор аватара доступен только на Windows в исполняемом режиме.";
        Debug.LogWarning("Выбор аватара доступен только на Windows.");
#endif
    }

    private void LoadAvatar(string path)
    {
        FileInfo fileInfo = new FileInfo(path);
        if (fileInfo.Length > maxFileSize)
        {
            errorText.text = "Ошибка: файл превышает 1 МБ.";
            return;
        }

        // Загружаем файл как массив байтов
        byte[] fileData = File.ReadAllBytes(path);
        Texture2D texture = new Texture2D(2, 2);

        if (texture.LoadImage(fileData))
        {
            // Создаём спрайт из текстуры
            Sprite newAvatarSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

            // Обновляем изображения
            avatarImage.sprite = newAvatarSprite;
            secondaryAvatarImage.sprite = newAvatarSprite;
            errorText.text = "";

            // Сохраняем аватарку на диск
            SaveAvatarToDisk(texture);
        }
        else
        {
            errorText.text = "Ошибка: неподдерживаемый формат файла.";
        }
    }

    private void SaveAvatarToDisk(Texture2D texture)
    {
        string avatarFilePath = Path.Combine(savePath, "avatar.png");
        byte[] pngData = texture.EncodeToPNG();

        if (pngData != null)
        {
            File.WriteAllBytes(avatarFilePath, pngData);
            Debug.Log("Аватарка сохранена по пути: " + avatarFilePath);
        }
        else
        {
            Debug.LogError("Ошибка: не удалось сохранить аватарку.");
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
