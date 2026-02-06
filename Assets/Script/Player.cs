using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Vector2 movimento;
    public float speed = 5f;
    Rigidbody2D rb;
    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void SetMovimento(InputAction.CallbackContext context)
    {
        movimento = context.ReadValue<Vector2>();
    }
    public void Movimento()
    {
        rb.linearVelocity = new Vector2(movimento.x * speed, movimento.y * speed);
    }
    void FixedUpdate()
    {
        Movimento();
    }
}
