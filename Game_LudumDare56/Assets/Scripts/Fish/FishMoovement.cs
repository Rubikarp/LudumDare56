using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class FishMoovement : MonoBehaviour
{
    public float moveSpeed = 3f; // Vitesse de déplacement du poisson
    public float timeToChangeDirection = 3f; // Temps avant de changer de direction
    public FishZone moveArea;

    private Rigidbody2D rb;
    private CircleCollider2D circleCollider;

    private Vector2 targetPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    void Start()
    {
        // Générer une première direction aléatoire
        SetNextPoint();
    }

    void Update()
    {
        // Déplacement du poisson
        MoveFish();
    }

    void MoveFish()
    {
        // Déplacer le poisson dans la direction cible
        rb.velocity = (targetPosition - (Vector2)transform.position) * moveSpeed;

        // Faire tourner le poisson pour faire face à la direction du mouvement (en 2D)
        Vector2 direction = targetPosition - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Vérifier si le poisson est arrivé à la position cible
        if (Vector2.Distance(transform.position, targetPosition) < 1f)
        {
            SetNextPoint();
        }
    }

    void SetNextPoint()
    {
        targetPosition = moveArea.Area.RandomPointInRect();
    }
}
