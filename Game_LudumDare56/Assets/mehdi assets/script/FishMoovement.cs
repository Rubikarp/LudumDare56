using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMoovement : MonoBehaviour
{
    public float moveSpeed = 3f; // Vitesse de déplacement du poisson
    public float timeToChangeDirection = 3f; // Temps avant de changer de direction
    public Vector2 movementBoundsMin; // Limite inférieure de la zone de déplacement (en 2D)
    public Vector2 movementBoundsMax; // Limite supérieure de la zone de déplacement (en 2D)

    private Vector2 targetDirection; // Direction dans laquelle le poisson se déplace
    private float changeDirectionTimer;

    void Start()
    {
        // Générer une première direction aléatoire
        GenerateNewDirection();
    }

    void Update()
    {
        // Déplacement du poisson
        MoveFish();

        // Changer de direction après un certain temps
        changeDirectionTimer += Time.deltaTime;
        if (changeDirectionTimer > timeToChangeDirection)
        {
            GenerateNewDirection();
        }

        // Vérifier si le poisson sort des limites et ajuster la direction
        CheckBounds();
    }

    void MoveFish()
    {
        // Déplacer le poisson dans la direction cible
        transform.Translate(targetDirection * moveSpeed * Time.deltaTime);

        // Faire tourner le poisson pour faire face à la direction du mouvement (en 2D)
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
    }

    void GenerateNewDirection()
    {
        // Générer une direction aléatoire en 2D
        targetDirection = new Vector2(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ).normalized;

        // Reset du timer de changement de direction
        changeDirectionTimer = 0f;
    }

    void CheckBounds()
    {
        Vector2 pos = transform.position;

        // Si le poisson sort des limites, inverser la direction correspondante
        if (pos.x < movementBoundsMin.x || pos.x > movementBoundsMax.x)
        {
            targetDirection.x = -targetDirection.x;
        }

        if (pos.y < movementBoundsMin.y || pos.y > movementBoundsMax.y)
        {
            targetDirection.y = -targetDirection.y;
        }
    }
}
