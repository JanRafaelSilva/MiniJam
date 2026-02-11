using UnityEditor;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    public GameObject player;
    private Player playerScript;

    void Start()
    {
        playerScript = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerScript.bateria -= 20;

            AudioSource audio = GetComponent<AudioSource>();
            AudioSource.PlayClipAtPoint(audio.clip, transform.position);

            Destroy(gameObject);
        }
    }
}
