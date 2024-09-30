using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSpearWallGrab : MonoBehaviour
{
    [SerializeField] Vector2 _size;
    [SerializeField] Transform _checkPos;
    [SerializeField] LayerMask _ground;
    [SerializeField] HangableTile _tiles;
    public UnityEvent<Vector3Int, bool> OnClimb;
    Collider2D[] _results=new Collider2D[4];
    private bool _canGrab = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_canGrab)
        {
            if (Physics2D.OverlapBoxNonAlloc(_checkPos.position, _size, 0, _results, _ground) > 0)
            {
                if (_tiles.GetTileAtPos(new Vector3(_checkPos.position.x + 0.8f, _checkPos.position.y), out Vector3Int tilePos))
                {
                    _canGrab = false;
                    Logger.Log("RIGHT");
                    OnClimb?.Invoke(tilePos, false);
                }
                else if (_tiles.GetTileAtPos(new Vector3(_checkPos.position.x - 0.8f, _checkPos.position.y), out tilePos))
                {
                    _canGrab = false;
                    Logger.Log("FSAF");
                    OnClimb?.Invoke(tilePos, true);
                }
            }
        }
    }
    public void ResetWallGrab()
    {
        _canGrab = true;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(_checkPos.position, _size);
    }
}
