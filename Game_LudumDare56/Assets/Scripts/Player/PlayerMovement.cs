using UnityEngine;
using NaughtyAttributes;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField, ReadOnly]
    private Vector2 movement;
    private Rigidbody2D rigidBody;
    private PlayerData playerData;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.gravityScale = 0;
    }

    private void Start()
    {
        playerData = PlayerData.Instance;
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

        rigidBody.velocity = movement * playerData.defaultSpeed;
    }
}
