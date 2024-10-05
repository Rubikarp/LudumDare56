using UnityEngine;
public class ContextualInteraction : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Clic gauche
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Logique d'interaction basée sur l'objet touché
                if (hit.collider.CompareTag("Interactable"))
                {
                    InteractWithObject(hit.collider.gameObject);
                }
            }
        }
    }

    void InteractWithObject(GameObject obj)
    {
        // Implémente la logique d'interaction ici
        Debug.Log("Interaction avec : " + obj.name);
    }
}