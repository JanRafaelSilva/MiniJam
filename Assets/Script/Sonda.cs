using UnityEngine;
using UnityEngine.Tilemaps;

public class Sonda : MonoBehaviour
{
    public Rigidbody2D rigid;
    public float force = 1;
    public float timer;
    private GameObject Mouse;
    public Vector2 a;
    public Tilemap tilemap;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    public void Start()
    {
        
        Vector3 direcao = a - (Vector2)transform.position;
        rigid.linearVelocity = new Vector2(direcao.x, direcao.y).normalized * force;

        float rot = Mathf.Atan2(-direcao.y, -direcao.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    public void direction(float x, float y)
    {
        Vector2 direction = new Vector2(x, y);
        a = direction;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 1.5f){
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Sonda"))
        {
            //tilemap = collision.gameObject.GetComponent<Tilemap>();   
             //  tilemap = Tilemap.SetColor(Vector3(collsion.position), Color.red);
        }
    }
}
