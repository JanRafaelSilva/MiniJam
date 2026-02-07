using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Vector2 movimento;
    public float speed = 5f;
    Rigidbody2D rb;

    Animator anim;
    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleAnimation();
    }

    void FixedUpdate()
    {
        Movimento();
    }

    public void SetMovimento(InputAction.CallbackContext context)
    {
        movimento = context.ReadValue<Vector2>();
    }

    public void Movimento()
    {
        rb.AddForce(movimento * speed);
    }

    void HandleAnimation()
    {
        if (anim == null) return;

        float velocidadeReal = rb.linearVelocity.magnitude;

        anim.speed = Mathf.Lerp(anim.speed, velocidadeReal / 2, Time.deltaTime * 5f);
    }
}
