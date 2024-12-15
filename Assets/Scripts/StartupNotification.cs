using UnityEngine;
using TMPro;

public class StartupNotification : MonoBehaviour
{
    public GameObject notificationPrefab; // ������ � ������������
    public Transform canvasTransform;    // Canvas, ���� ���� ���������� �����������

    private void Start()
    {
        // ��������� ����� ��� ������ ����������� �� �����
        Debug.Log("Start method called!");
        ShowNotification("Hello World");
    }

    public void ShowNotification(string message)
    {
        // ��������� ��������� �����������
        Debug.Log("Instantiating notification prefab.");
        GameObject notification = Instantiate(notificationPrefab, canvasTransform);
        RectTransform rectTransform = notification.GetComponent<RectTransform>();

        // �������� ��������� TextMeshPro ��� ���� ������
        var textComponent = notification.GetComponentInChildren<TMP_Text>();
        if (textComponent != null)
        {
            textComponent.text = message;
            Debug.Log("Notification text set to: " + message);
        }

        // ��������� ������� (���� ������� �����)
        rectTransform.anchoredPosition = new Vector2(-50, -220);  // ������� ������� ���� �������
        Debug.Log("Initial position set to: " + rectTransform.anchoredPosition);

        // ��������� ������� �����
        StartCoroutine(SlideInNotification(rectTransform, notification));
    }

    private System.Collections.IEnumerator SlideInNotification(RectTransform rectTransform, GameObject notification)
    {
        // ������� ��� ���������� ������� (����� �������� ������)
        Vector2 targetPosition = new Vector2(-50, 50);  // ������� �������� �����
        float duration = 0.5f; // ��� �������
        float elapsedTime = 0;

        // ������� ��� � �� ������� �������� ����
        while (elapsedTime < duration)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, targetPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ��������� �������, ������ 3 �������
        Debug.Log("Notification reached target position.");
        yield return new WaitForSeconds(3f);

        // ��������� ����������� �����
        StartCoroutine(SlideOutNotification(rectTransform, notification));
    }

    private System.Collections.IEnumerator SlideOutNotification(RectTransform rectTransform, GameObject notification)
    {
        // ������� ���� ������� �����
        Vector2 targetPosition = new Vector2(-50, -220);  // ����������� �� ��� ������
        float duration = 0.5f; // ��� �������
        float elapsedTime = 0;

        // ������� ��� �����
        while (elapsedTime < duration)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, targetPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ��������� ����������� ���� �������
        Debug.Log("Notification removed from screen.");
        Destroy(notification);
    }
}
