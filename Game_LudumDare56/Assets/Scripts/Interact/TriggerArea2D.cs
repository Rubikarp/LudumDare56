using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

[RequireComponent(typeof(BoxCollider2D))]
public class TriggerArea2D : MonoBehaviour
{
    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;
    [HorizontalLine]
    public UnityEvent onInteraction;

    [SerializeField, Tag]
    public string targetTag;

    public void Interact()
    {
        onInteraction?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag)) onTriggerEnter.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag)) onTriggerExit.Invoke();
    }
}
