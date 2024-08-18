using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;


public class GodNotification : MonoBehaviour
{
    [SerializeField] private TMP_Text _messageText;
    [SerializeField] private Transform _parent;
    [SerializeField] private CanvasGroup _canvasGroup;

    private Queue<GodNotificationData> _queue = new();
    private GodNotificationData[] _godNotifications;
    private List<GodNotificationData> _unsendGodNotification;

    private void Start()
    {
        _canvasGroup.alpha = 0;
        _parent.localPosition = Vector3.up * 100;

        _godNotifications = Resources.LoadAll<GodNotificationData>("godnot");
        _unsendGodNotification = _godNotifications.ToList();

        foreach (GodNotificationData godNotificationData in _godNotifications) {
            godNotificationData.notificationCheck.OnEnable();
        }

        StartCoroutine(NotificationQueueRoutine());
    }

    private void OnDestroy()
    {
        foreach (GodNotificationData godNotificationData in _godNotifications) {
            godNotificationData.notificationCheck.OnDisable();
        }
    }

    private void Update()
    {
        List<GodNotificationData> notificationsToRemove = new();
        foreach (GodNotificationData godNotificationData in _unsendGodNotification) {
            if (!godNotificationData.notificationCheck.CanSee()) continue;
            notificationsToRemove.Add(godNotificationData);
            _queue.Enqueue(godNotificationData);
        }

        foreach (GodNotificationData godNotificationData in notificationsToRemove) {
            _unsendGodNotification.Remove(godNotificationData);
        }
    }

    private IEnumerator NotificationQueueRoutine()
    {
        while (true) {
            if (_queue.TryDequeue(out GodNotificationData result)) {
                yield return NotificationRoutine(result);
                yield return new WaitForSeconds(.5f);
            } else {
                yield return null;
            }
        }
    }

    private IEnumerator NotificationRoutine(GodNotificationData godNotificationData)
    {
        _messageText.text = godNotificationData.message;

        for (float t = 0; t < 1; t += Time.deltaTime * 3f) {
            _parent.localPosition = Vector3.up * Mathf.Lerp(100, -50, t);
            _canvasGroup.alpha = Mathf.Lerp(0, 1, t);
            yield return null;
        }

        yield return new WaitForSeconds(godNotificationData.stayTime);

        for (float t = 0; t < 1; t += Time.deltaTime * 3f) {
            _parent.localPosition = Vector3.up * Mathf.Lerp(-50, 100, t);
            _canvasGroup.alpha = Mathf.Lerp(1, 0, t);
            yield return null;
        }
    }
}