using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class HangableTile : MonoBehaviour
{
   // public
    private Tilemap _tilemap;
    // Start is called before the first frame update
    void Start()
    {
        _tilemap = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3Int tilePos=_tilemap.WorldToCell(new Vector3(collision.bounds.center.x+0.8f,collision.transform.position.y));
        if (_tilemap.GetTile(tilePos))
        {
           // Logger.Log(tilePos);
        }
        else if(_tilemap.GetTile(_tilemap.WorldToCell(new Vector3(collision.bounds.center.x - 0.8f, collision.transform.position.y))))
        {
            tilePos = _tilemap.WorldToCell(new Vector3(collision.bounds.center.x - 0.8f, collision.transform.position.y));
           // Logger.Log(tilePos);
           // OnClimb?.Invoke(tilePos, true);
        }
    }
    public bool GetTileAtPos(Vector3 pos,out Vector3Int tilePos)
    {
        tilePos = _tilemap.WorldToCell(pos);
        if (_tilemap.GetTile(tilePos)) return true;
        else return false;
    }
}
