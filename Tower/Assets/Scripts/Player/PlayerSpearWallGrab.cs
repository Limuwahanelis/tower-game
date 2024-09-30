using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSpearWallGrab : MonoBehaviour
{
    [SerializeField] Vector2 _size;
    [SerializeField] Transform _checkPos;
    [SerializeField] LayerMask _ground;
    [SerializeField] HangableTile _tiles;
    [SerializeField] PlayerMovement _playerMovement;
    public UnityEvent<Vector3Int, bool> OnClimb;
    Collider2D[] _results=new Collider2D[4];
    private bool _canGrab = false;
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
                
                if (_tiles.GetTileAtPos(new Vector3(_checkPos.position.x + 0.4f, _checkPos.position.y), out Vector3Int tilePos))
                {
                    if (_playerMovement.FlipSide == ((int)PlayerMovement.playerDirection.LEFT)) return;
                    Logger.Log(_checkPos.position.y - tilePos.y);
                    if (_checkPos.position.y - tilePos.y > 0.8f)
                    {
                        _canGrab = false;
                        Logger.Log("RIGHT");
                        OnClimb?.Invoke(tilePos, false);
                    }
                }
                else if (_tiles.GetTileAtPos(new Vector3(_checkPos.position.x - 0.4f, _checkPos.position.y), out tilePos))
                {
                    if (_playerMovement.FlipSide == ((int)PlayerMovement.playerDirection.RIGHT)) return;
                    Logger.Log(_checkPos.position.y - tilePos.y);
                    if (_checkPos.position.y - tilePos.y > 0.8f)
                    {
                        _canGrab = false;
                        Logger.Log("FSAF");
                        OnClimb?.Invoke(tilePos, true);
                    }
                }
            }
        }
    }
    public void SetWallGrab(bool value)
    {
        _canGrab = value;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(_checkPos.position, _size);
    }
}
