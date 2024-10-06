using UnityEngine;

public class FishZone : MonoBehaviour
{
    [SerializeField] private Color gizmoColor = Color.blue;
    public Rect Area
    {
        get
        {
            return new Rect(
                transform.position.x - transform.localScale.x * 0.5f,
                transform.position.y - transform.localScale.y * 0.5f,
                transform.localScale.x,
                transform.localScale.y
            );
        }
    }

    void OnDrawGizmos()
    {
        // Dessiner un cube représentant la zone de déplacement des poissons
        var color = Gizmos.color;
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
        Gizmos.color = color;
    }
}