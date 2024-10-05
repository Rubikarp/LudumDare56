using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Vitesse de déplacement du joueur

    private Vector2 movement;

    void Update()
    {
        // Capturer les entrées clavier (ZQSD) pour le mouvement
        movement.x = 0f;
        movement.y = 0f;

        if (Input.GetKey(KeyCode.Z))
        {
            movement.y = 1f; // Aller vers le haut
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement.y = -1f; // Aller vers le bas
        }
        if (Input.GetKey(KeyCode.Q))
        {
            movement.x = -1f; // Aller à gauche
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement.x = 1f; // Aller à droite
        }
    }

    void FixedUpdate()
    {
        // Appliquer le mouvement au joueur
        transform.Translate(movement * moveSpeed * Time.fixedDeltaTime);
    }
}
