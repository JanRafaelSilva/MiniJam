using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.PlayerSettings;
public class Mapa : MonoBehaviour
{
    private Tilemap tileMap;
    
    private void Start()
    {
        tileMap = GetComponent<Tilemap>();
        //Vector3Int cell = tileMap.WorldToCell(every);
        //tileMap.SetColor(cell);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Sonda"))
        {
            for(int quantidade = 0; quantidade < collision.contactCount; quantidade++) { 
            ContactPoint2D contact = collision.GetContact(quantidade);
            Vector3 pos = contact.point - (contact.normal / 2f);
            Vector3Int cell = tileMap.WorldToCell(pos);
            if (tileMap.HasTile(cell))
                {
                    tileMap.RemoveTileFlags(new Vector3Int(cell.x, cell.y, 0), TileFlags.LockColor);
                    tileMap.SetColor(new Vector3Int(cell.x, cell.y, 0), Color.blue);
                    Destroy(collision.gameObject);
                }
            }
        }

        }
    }

//private ContactPoint[] contacts;
//Vector3Int cell = tilemap.WorldToCell(pos);
//+private float[,] blockTempDataMap = new float[250, 150];

/* tileMap = gameObject.GetComponent<Tilemap>();

 for (int y = 0; y < blockTempDataMap.GetLength(0); y++)
 {
     for (int x = 0; x < blockTempDataMap.GetLength(1); x++)
     {
         tileMap.SetColor(new Vector3Int(y, x, 0), Color.white);
     }
 }
if (collision.gameObject.CompareTag("Sonda"))
{
    Debug.Log("bateu lenda");
    Transform controle = collision.transform;
    for (int x = 0; x < controle.position.x; x++)
    {
        //Vector3Int loc = x.CellCoordinates;
        for (int y = 0; y < controle.position.y; y++)
        {
            tileMap.RemoveTileFlags(new Vector3Int(x, y, 0), TileFlags.LockColor);
            tileMap.SetColor(new Vector3Int(x, y, 0), Color.white);
        }
    }
}*/