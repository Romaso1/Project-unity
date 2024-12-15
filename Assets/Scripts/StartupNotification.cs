using UnityEngine;
using TMPro;

public class StartupNotification : MonoBehaviour
{
    public GameObject notificationPrefab; // Префаб з нотифікацією
    public Transform canvasTransform;    // Canvas, куди буде додаватись нотифікація

    private void Start()
    {
        // Викликаємо метод для показу нотифікації на старті
        Debug.Log("Start method called!");
        ShowNotification("Hello World");
    }

    public void ShowNotification(string message)
    {
        // Створення інстанції нотифікації
        Debug.Log("Instantiating notification prefab.");
        GameObject notification = Instantiate(notificationPrefab, canvasTransform);
        RectTransform rectTransform = notification.GetComponent<RectTransform>();

        // Отримуємо компонент TextMeshPro для зміни тексту
        var textComponent = notification.GetComponentInChildren<TMP_Text>();
        if (textComponent != null)
        {
            textComponent.text = message;
            Debug.Log("Notification text set to: " + message);
        }

        // Початкова позиція (поза екраном знизу)
        rectTransform.anchoredPosition = new Vector2(-50, -220);  // Вихідна позиція поза екраном
        Debug.Log("Initial position set to: " + rectTransform.anchoredPosition);

        // Запускаємо анімацію появи
        StartCoroutine(SlideInNotification(rectTransform, notification));
    }

    private System.Collections.IEnumerator SlideInNotification(RectTransform rectTransform, GameObject notification)
    {
        // Позиція для завершення анімації (внизу праворуч екрану)
        Vector2 targetPosition = new Vector2(-50, 50);  // Позиція праворуч внизу
        float duration = 0.5f; // Час анімації
        float elapsedTime = 0;

        // Плавний рух в бік правого нижнього кута
        while (elapsedTime < duration)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, targetPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Завершили анімацію, чекаємо 3 секунди
        Debug.Log("Notification reached target position.");
        yield return new WaitForSeconds(3f);

        // Повертаємо нотифікацію назад
        StartCoroutine(SlideOutNotification(rectTransform, notification));
    }

    private System.Collections.IEnumerator SlideOutNotification(RectTransform rectTransform, GameObject notification)
    {
        // Позиція поза екраном знизу
        Vector2 targetPosition = new Vector2(-50, -220);  // Повертаємось за межі екрану
        float duration = 0.5f; // Час анімації
        float elapsedTime = 0;

        // Плавний рух назад
        while (elapsedTime < duration)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, targetPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Видалення нотифікації після анімації
        Debug.Log("Notification removed from screen.");
        Destroy(notification);
    }
}
