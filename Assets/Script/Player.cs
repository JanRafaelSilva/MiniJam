using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.IO.Compression;
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
    public ParticleSystem bolhas;

    public float rotacaoMax = 20f;
    float velRotacao;
    public float smoothTime = 0.5f;
    public float OffsetRadius;

    public GameObject sonda;
    public float timerSonda;
    Vector2 mousePosition;
    public float x;
    public float y;
    public bool OnSonda = false;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleAnimation();
        HandleFlip();
        HandleParticles();
        if (OnSonda)
        {
            timerSonda += Time.deltaTime;
            var controle = sonda.gameObject.GetComponent<Sonda>();
            controle.direction(x, y);
            if (timerSonda >= 0.2f)
            {
                Instantiate(sonda, transform.position, Quaternion.identity);
                timerSonda = 0f;
            }
        }
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

        float anguloAlvo = movimento.y * rotacaoMax;

        if (transform.localScale.x < 0) anguloAlvo = -anguloAlvo;

        float zAtual = transform.eulerAngles.z;
        if (zAtual > 180) zAtual -= 360;

        float zNovo = Mathf.SmoothDampAngle(zAtual, anguloAlvo, ref velRotacao, smoothTime);
        transform.rotation = Quaternion.Euler(0, 0, zNovo);
    }

    void HandleAnimation()
    {
        if (anim == null) return;

        float velocidadeReal = rb.linearVelocity.magnitude;

        anim.speed = Mathf.Lerp(anim.speed, velocidadeReal / 2, Time.deltaTime * 5f);
    }

    public Vector3 GetMouseWorldPosition()
    {
        return mouseWorldPosition;
    }

    public void SetMouse(InputAction.CallbackContext value)
    {
        var look = value.ReadValue<Vector2>();
        mouseWorldPosition = mainCamera.ScreenToWorldPoint(look);
        // Vetor que transforma o ponto do espaço da tela no espaço mundial
        mouseWorldPosition.z = 0f;
        x = mouseWorldPosition.x;
        y = mouseWorldPosition.y;
    }   
    public void SetSensor(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            OnSonda = true;
        }
        if(value.canceled)
        {
            OnSonda = false;
            timerSonda = 0f;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
    }
    void HandleFlip()
    {
        float escalaAntiga = transform.localScale.x;
        var linear = bolhas.velocityOverLifetime;
        var force = bolhas.forceOverLifetime;

        if (movimento.x > 0.1f) 
        {
            transform.localScale = new Vector3(1, 1, 1);
            
            linear.x = 0.1f;
            force.x = 0.1f;
        }
        else if (movimento.x < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            linear.x = -0.1f;
            force.x = -0.1f;
        }

        if (transform.localScale.x != escalaAntiga)
        {
            Vector3 rot = transform.eulerAngles;
            float z = rot.z;
            if (z > 180) z -= 360;

            transform.rotation = Quaternion.Euler(0, 0, -z);

            velRotacao = 0;
        }

    }

    void HandleParticles()
    {
        if (bolhas == null) return;

        var emission = bolhas.emission;

        if (rb.linearVelocity.magnitude > 0.1f)
        {
            emission.rateOverTime = rb.linearVelocity.magnitude + 5;
        }
        else
        {
            emission.rateOverTime = 0;
        }
    }
}
