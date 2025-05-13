using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    public ObjectType objectType;  // тип объекта, устанавливается в инспекторе (RedCube или BlueSphere)

    void OnMouseDown()
    {
        // Вызывается при клике мыши по объекту с коллайдером:contentReference[oaicite:3]{index=3}
        QuestManager.Instance.RegisterObjectDestroyed(objectType);
        Destroy(gameObject);  // уничтожаем этот объект
    }

    void OnCollisionEnter(Collision collision)
    {
        // Вызывается при физическом столкновении с другим объектом
        if (collision.gameObject.CompareTag("Killable"))
        {
            // Столкновение с другим "убиваемым" объектом
            QuestManager.Instance.RegisterObjectDestroyed(objectType);
            Destroy(gameObject);
            // Примечание: если хотим, чтобы оба объекта уничтожались при столкновении,
            // можно дополнительно уничтожить второй объект:
            // Destroy(collision.gameObject);
            // Однако у второго объекта свой скрипт тоже вызовет уничтожение, поэтому
            // двойной вызов обычно не требуется.
        }
    }

    // Альтернатива: использование триггеров вместо физических коллизий
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Killable"))
        {
            QuestManager.Instance.RegisterObjectDestroyed(objectType);
            Destroy(gameObject);
        }
    }
}
