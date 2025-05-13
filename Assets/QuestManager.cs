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

    private float task1GoalTime = 120f;   // 2 ������ � ��������
    private int task2GoalCount = 10;
    private int task3GoalCount = 10;

    // ���������� TextMeshPro ��� ����������� ������ �������
    public TextMeshProUGUI task1Text;
    public TextMeshProUGUI task2Text;
    public TextMeshProUGUI task3Text;

    // ������� ���������� �������
    public Image task1Checkmark;
    public Image task2Checkmark;
    public Image task3Checkmark;

    void Awake()
    {
        // ���������� Singleton
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        // ������������� UI ��� ������
        task1Text.text = $"������� 1: ������� � ���� 2 ������ � 0/{Mathf.FloorToInt(task1GoalTime)} c";
        task2Text.text = $"������� 2: ���������� {task2GoalCount} �������� � 0/{task2GoalCount}";
        task3Text.text = $"������� 3: ���������� {task3GoalCount} ����� ���� � 0/{task3GoalCount}";

        if (task1Checkmark != null) task1Checkmark.enabled = false;
        if (task2Checkmark != null) task2Checkmark.enabled = false;
        if (task3Checkmark != null) task3Checkmark.enabled = false;
    }

    void Update()
    {
        // ���������� ������� 1 (�����)
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
            task1Text.text = $"������� 1: ������� � ���� 2 ������ � {seconds}/{Mathf.FloorToInt(task1GoalTime)} c";
        }
    }

    // ���������� ��� ����������� ������ �������
    public void RegisterObjectDestroyed(ObjectType type)
    {
        // ������� 2: ���������� ����� ���������� ��������
        if (!task2Complete)
        {
            destroyedAnyCount++;
            if (destroyedAnyCount >= task2GoalCount)
            {
                task2Complete = true;
                if (task2Checkmark != null) task2Checkmark.enabled = true;
                task2Text.color = Color.green;
            }
            task2Text.text = $"������� 2: ���������� {task2GoalCount} �������� � {destroyedAnyCount}/{task2GoalCount}";
        }

        // ������� 3: ���������� ������� ������������� ����
        if (!task3Complete && type == ObjectType.BlueSphere)
        {
            destroyedBlueCount++;
            if (destroyedBlueCount >= task3GoalCount)
            {
                task3Complete = true;
                if (task3Checkmark != null) task3Checkmark.enabled = true;
                task3Text.color = Color.green;
            }
            task3Text.text = $"������� 3: ���������� {task3GoalCount} ����� ���� � {destroyedBlueCount}/{task3GoalCount}";
        }
    }
}
