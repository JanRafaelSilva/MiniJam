using UnityEngine;

public class Mapa : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Sonda"))
        {

        }
    }
}
