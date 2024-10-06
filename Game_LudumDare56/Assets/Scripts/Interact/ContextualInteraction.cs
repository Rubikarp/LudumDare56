using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ContextualInteraction : MonoBehaviour
{
    [SerializeField] private List<TriggerArea2D> triggerAreas;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            InteractWithObject();
        }
    }

    void InteractWithObject()
    {
        foreach (var trigger in triggerAreas)
        {
            trigger.Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<TriggerArea2D>() == null) return;

        var trigger = collision.GetComponent<TriggerArea2D>();
        triggerAreas.Add(trigger);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<TriggerArea2D>() == null) return;

        // Si l'objet sort de la zone de détection, on le retire de la liste
        var trigger = collision.GetComponent<TriggerArea2D>();
        if(triggerAreas.Contains(trigger)) triggerAreas.Remove(trigger);

    }
}