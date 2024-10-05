using UnityEngine;
using NaughtyAttributes;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [field: SerializeField, Range(1, 10)]
    public float MoveSpeed { get; private set; } = 5f; // Vitesse de déplacement

    [SerializeField, ReadOnly]
    private Vector2 movement;
    private Rigidbody2D rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.gravityScale = 0;
    }

    void Update()
    {
        movement = Vector2.zero;

        movement += Vector2.right * Input.GetAxis("Horizontal");
        movement += Vector2.up * Input.GetAxis("Vertical");

        if(movement.magnitude > 1)
        {
            movement.Normalize();
        }

        rigidBody.velocity = movement * MoveSpeed;
    }
}
