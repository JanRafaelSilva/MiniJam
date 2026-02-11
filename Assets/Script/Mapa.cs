using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Mapa : MonoBehaviour
{
    private Tilemap tileMap;
    private void Start()
    {
        tileMap = GetComponent<Tilemap>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Sonda"))
        {
            Debug.Log("bateu lenda");
            Vector3 pos = collision.transform.position;
            Vector3Int cell = tileMap.WorldToCell(pos);
            tileMap.RemoveTileFlags(new Vector3Int(cell.x,cell.y,0), TileFlags.LockColor);
            tileMap.SetColor(new Vector3Int(cell.x, cell.y, 0), Color.white);
        }
    }
}
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