using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Vector2 movimento;
    public float speed = 5f;
    Rigidbody2D rb;

    Animator anim;
    Vector3 mouseWorldPosition;
    [SerializeField] private Camera mainCamera;
    public LayerMask DefaltLayer;
    public Color gizmoColor = Color.green;
    Vector2 direction;
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
       Physics2D.LinecastAll(transform.position, direction, DefaltLayer);
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
    public void SetMouse(InputAction.CallbackContext value)
    {
        var look = value.ReadValue<Vector2>();
        direction = Vector2.ClampMagnitude(mainCamera.ScreenToWorldPoint(look), 5f);
        mouseWorldPosition = mainCamera.ScreenToWorldPoint(look);
        // Vetor que transforma o ponto do espaço da tela no espaço mundial
        mouseWorldPosition.z = 0f;
    }
    public void SetSensor(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawLine(transform.position, direction);
    }
}
