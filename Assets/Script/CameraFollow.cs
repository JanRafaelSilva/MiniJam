using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;

    public float smoothTime = 0.3f;

    public float mouseInfluence = 0.3f;
    public float maxMouseDistance = 3f;


    Vector3 offset;
    public Vector3 velocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = new Vector3(0, 0, -10);
        velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        if (player == null) return;

        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, 0));
        mousePos.z = 0;

        Vector3 direction = mousePos - player.transform.position;

        Vector3 targetMouseOffset = Vector3.ClampMagnitude(direction * mouseInfluence, maxMouseDistance);

        Vector3 targetPosition = player.transform.position + targetMouseOffset + offset;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        
    }
}
