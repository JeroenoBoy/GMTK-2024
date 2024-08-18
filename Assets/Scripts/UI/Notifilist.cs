using UnityEngine;


public class Notifilist : MonoBehaviour
{
    [SerializeField] private GameObject notificationTemplate;
    [SerializeField] private Transform parenting;

    private void OnEnable()
    {
        EventBus.instance.onNeedBalanceLost += HandleNeedBalanceLossed;
    }

    private void OnDisable()
    {
        EventBus.instance.onNeedBalanceLost -= HandleNeedBalanceLossed;
    }

    private void HandleNeedBalanceLossed(Need need)
    {
        GameObject currentNeed = Instantiate(notificationTemplate, transform.position, Quaternion.identity, parenting);
        currentNeed.GetComponent<notification>().SetNotification(need);
    }
}