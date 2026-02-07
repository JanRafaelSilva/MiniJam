using System.IO.Compression;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Vector2 movimento;
    public float speed = 5f;
    Rigidbody2D rb;

    Animator anim;

    public ParticleSystem bolhas;

    public float rotacaoMax = 20f;
    float velRotacao;
    public float smoothTime = 0.5f;

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
