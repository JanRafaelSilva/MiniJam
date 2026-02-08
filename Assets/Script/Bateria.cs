using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bateria : MonoBehaviour
{
    public Image bateriaBar;
    public int valorAtual = 1000;
    public GameObject Player;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void VidaBarMenos(int timer)
    {
            valorAtual -= timer;
            bateriaBar.fillAmount = (float)valorAtual / 1000;
            var cont = Player.gameObject.GetComponent<Player>();
            cont.receberBateria(valorAtual);
    }
}
