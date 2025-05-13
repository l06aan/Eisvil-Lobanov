using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    public ObjectType objectType;  // ��� �������, ��������������� � ���������� (RedCube ��� BlueSphere)

    void OnMouseDown()
    {
        // ���������� ��� ����� ���� �� ������� � �����������:contentReference[oaicite:3]{index=3}
        QuestManager.Instance.RegisterObjectDestroyed(objectType);
        Destroy(gameObject);  // ���������� ���� ������
    }

    void OnCollisionEnter(Collision collision)
    {
        // ���������� ��� ���������� ������������ � ������ ��������
        if (collision.gameObject.CompareTag("Killable"))
        {
            // ������������ � ������ "���������" ��������
            QuestManager.Instance.RegisterObjectDestroyed(objectType);
            Destroy(gameObject);
            // ����������: ���� �����, ����� ��� ������� ������������ ��� ������������,
            // ����� ������������� ���������� ������ ������:
            // Destroy(collision.gameObject);
            // ������ � ������� ������� ���� ������ ���� ������� �����������, �������
            // ������� ����� ������ �� ���������.
        }
    }

    // ������������: ������������� ��������� ������ ���������� ��������
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Killable"))
        {
            QuestManager.Instance.RegisterObjectDestroyed(objectType);
            Destroy(gameObject);
        }
    }
}
