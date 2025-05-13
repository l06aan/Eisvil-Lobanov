using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum ObjectType { RedCube, BlueSphere }

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    private float timeElapsed = 0f;
    private bool task1Complete = false;
    private int destroyedAnyCount = 0;
    private bool task2Complete = false;
    private int destroyedBlueCount = 0;
    private bool task3Complete = false;

    private float task1GoalTime = 120f;   // 2 минуты в секундах
    private int task2GoalCount = 10;
    private int task3GoalCount = 10;

    // Используем TextMeshPro для отображения текста заданий
    public TextMeshProUGUI task1Text;
    public TextMeshProUGUI task2Text;
    public TextMeshProUGUI task3Text;

    // Галочки выполнения заданий
    public Image task1Checkmark;
    public Image task2Checkmark;
    public Image task3Checkmark;

    void Awake()
    {
        // Реализация Singleton
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        // Инициализация UI при старте
        task1Text.text = $"Задание 1: пробыть в игре 2 минуты – 0/{Mathf.FloorToInt(task1GoalTime)} c";
        task2Text.text = $"Задание 2: уничтожить {task2GoalCount} объектов – 0/{task2GoalCount}";
        task3Text.text = $"Задание 3: уничтожить {task3GoalCount} синих сфер – 0/{task3GoalCount}";

        if (task1Checkmark != null) task1Checkmark.enabled = false;
        if (task2Checkmark != null) task2Checkmark.enabled = false;
        if (task3Checkmark != null) task3Checkmark.enabled = false;
    }

    void Update()
    {
        // Обновление задания 1 (время)
        if (!task1Complete)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= task1GoalTime)
            {
                timeElapsed = task1GoalTime;
                task1Complete = true;
                if (task1Checkmark != null) task1Checkmark.enabled = true;
                task1Text.color = Color.green;
            }
            int seconds = Mathf.FloorToInt(timeElapsed);
            task1Text.text = $"Задание 1: пробыть в игре 2 минуты – {seconds}/{Mathf.FloorToInt(task1GoalTime)} c";
        }
    }

    // Вызывается при уничтожении любого объекта
    public void RegisterObjectDestroyed(ObjectType type)
    {
        // Задание 2: уничтожить любое количество объектов
        if (!task2Complete)
        {
            destroyedAnyCount++;
            if (destroyedAnyCount >= task2GoalCount)
            {
                task2Complete = true;
                if (task2Checkmark != null) task2Checkmark.enabled = true;
                task2Text.color = Color.green;
            }
            task2Text.text = $"Задание 2: уничтожить {task2GoalCount} объектов – {destroyedAnyCount}/{task2GoalCount}";
        }

        // Задание 3: уничтожить объекты определенного типа
        if (!task3Complete && type == ObjectType.BlueSphere)
        {
            destroyedBlueCount++;
            if (destroyedBlueCount >= task3GoalCount)
            {
                task3Complete = true;
                if (task3Checkmark != null) task3Checkmark.enabled = true;
                task3Text.color = Color.green;
            }
            task3Text.text = $"Задание 3: уничтожить {task3GoalCount} синих сфер – {destroyedBlueCount}/{task3GoalCount}";
        }
    }
}
