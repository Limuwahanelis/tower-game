using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChecks : MonoBehaviour
{
    [SerializeField] LayerMask ground;
    [SerializeField] float groundCheckWidth;
    [SerializeField] float groundCheckHeight;
    [SerializeField] Transform groundCheckPos;
    [SerializeField] Transform ceilingCheckPos;
    [SerializeField] float ceilingCheckWidth;
    [SerializeField] float ceilingCheckHeight;

    private bool _isOnGround;
    private bool _isNearCeiling;
    private bool _isRayHittingGround;
    public bool IsOnGround => _isOnGround;
    public bool IsNearCeiling => _isNearCeiling;
    public bool IsRay=>_isRayHittingGround;
    // Start is called before the first frame update
    void Start()
    {
       // _player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit=Physics2D.BoxCast(transform.position, new Vector2(groundCheckWidth, groundCheckHeight), 0, -transform.up, 1.26f,ground);
        if(hit)
        {
            _isRayHittingGround = true;
        }
        else
        {
            _isRayHittingGround = false;
        }



        Collider2D[] colliders = Physics2D.OverlapBoxAll(groundCheckPos.position, new Vector2(groundCheckWidth, groundCheckHeight), 0, ground);
        _isOnGround = colliders.Length > 0;
        //colliders = Physics2D.OverlapBoxAll(ceilingCheckPos.position, new Vector2(ceilingCheckWidth, ceilingCheckHeight), 0, ground);
        //_isNearCeiling = colliders.Length > 0;

    }

    private void OnDrawGizmos()
    {
        if (ceilingCheckPos != null) Gizmos.DrawWireCube(ceilingCheckPos.position, new Vector3(ceilingCheckWidth, ceilingCheckHeight));
        if (groundCheckPos != null) Gizmos.DrawWireCube(groundCheckPos.position, new Vector3(groundCheckWidth, groundCheckHeight));
        Gizmos.DrawRay(transform.position, -transform.up * 1.4f);
    }

}
