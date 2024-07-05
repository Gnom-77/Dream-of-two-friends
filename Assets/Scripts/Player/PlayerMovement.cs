using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerControlSettings _playerControlSettings;
    [Header("Movement Settings")]
    [SerializeField] private Rigidbody2D _playerRb2D;
    [SerializeField] private CapsuleCollider2D _playerCollider;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float slopeCheckDistance;
    [SerializeField] private float maxSlopeAngle;
    [SerializeField] PhysicsMaterial2D _lowFriction;
    [SerializeField] PhysicsMaterial2D _hightFriction;
    [Space(10)]
    [Header("Ground")]
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask _groundMask;
    [Space(10)]
    [Header("Coyote Time Settings")]
    [SerializeField] private float _coyoteTime;
    [Space(10)]
    [Header("Jump Buffer Time Settings")]
    [SerializeField] private float _jumpBufferTime;


    private Vector2 _targetVelocity;
    private bool _isFacingRight;
    private bool _isJump = false;
    private bool _isSmallJump = false;
    private bool isGrounded;
    private int _directionOfMovement = 0;
    // Coyote Time
    private float _coyoteTimeCounter;
    // Jump Buffer Time Settings
    private float _jumpBufferCounter;
    // Slope
    private Vector2 slopeNormalPerp;
    private bool isOnSlope;
    private bool canWalkOnSlope;
    private float slopeDownAngle;
    private float slopeSideAngle;
    private float lastSlopeAngle;
    private Vector2 capsuleColliderSize;

    #region
    private string _moveRight;
    private string _moveLeft;
    private string _jump;
    private string _attack;
    #endregion

    private void Awake()
    {
        ContolSettings();
    }

    private void Start()
    {
        capsuleColliderSize = _playerCollider.size;
        if (transform.rotation.y == 0)
            _isFacingRight = true;
        else
            _isFacingRight = false;
    }

    private void Update()
    {
        _directionOfMovement = HorizontalDirection();
        CoyoteTime();
        JumpBuffer();
        Jump();
    }


    private void FixedUpdate()
    {
        SlopeCheck();
        Movement();
    }

    private int HorizontalDirection()
    {
        if (Input.GetKey(KeyCode.D))
        {
            if (!_isFacingRight)
                Flip();
            return 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (_isFacingRight)
                Flip();
            return -1;
        }
        else
        {
            return 0;
        }
    }

    private void Jump()
    {
        if (_jumpBufferCounter > 0f && _coyoteTimeCounter > 0f)
        {
            _isJump = true;

            _jumpBufferCounter = 0f;
        }
        if (Input.GetKeyUp(KeyCode.J) && _playerRb2D.velocity.y > 0f)
        {
            _isSmallJump = true;

            _coyoteTimeCounter = 0f;
        }


    }

    private void CoyoteTime()
    {
        if (CheckGrounding())
        {
            _coyoteTimeCounter = _coyoteTime;
        }
        else
        {
            _coyoteTimeCounter -= Time.deltaTime;
        }
    }

    private void JumpBuffer()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            _jumpBufferCounter = _jumpBufferTime;
        }
        else
        {
            _jumpBufferCounter -= Time.deltaTime;
        }
    }

    private void Movement()
    {
        if(isGrounded && !isOnSlope && !_isJump)
        {
            _targetVelocity.Set(_horizontalSpeed * _directionOfMovement, 0.0f);
            _playerRb2D.velocity = _targetVelocity;
        }
        else if(isGrounded && isOnSlope && canWalkOnSlope && !_isJump)
        {
            _targetVelocity.Set(_horizontalSpeed * slopeNormalPerp.x * -_directionOfMovement, _horizontalSpeed * slopeNormalPerp.y * -_directionOfMovement);
            _playerRb2D.velocity = _targetVelocity;
        }
        else if (!isGrounded)
        {
            _targetVelocity.Set(_horizontalSpeed * _directionOfMovement, _playerRb2D.velocity.y);
            _playerRb2D.velocity = _targetVelocity;
        }

        if (_isJump)
        {
            _playerRb2D.velocity = new Vector2(_playerRb2D.velocity.x, _jumpForce);
            _isJump = false;
            Debug.Log("Jump");
        }
        if (_isSmallJump)
        {
            _playerRb2D.velocity = new Vector2(_playerRb2D.velocity.x, _playerRb2D.velocity.y * 0.5f);
            _isSmallJump = false;
            Debug.Log("Small Jump");
        }
    }


    private bool CheckGrounding()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, _groundMask);

        return isGrounded;
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    private void SlopeCheck()
    {
        Vector2 checkPos = transform.position - (Vector3)(new Vector2(0.0f, capsuleColliderSize.y / 2));

        SlopeCheckHorizontal(checkPos);
        SlopeCheckVertical(checkPos);
    }
    private void SlopeCheckHorizontal(Vector2 checkPos)
    {
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, transform.right, slopeCheckDistance, _groundMask);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -transform.right, slopeCheckDistance, _groundMask);

        if (slopeHitFront)
        {
            isOnSlope = true;

            slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);

        }
        else if (slopeHitBack)
        {
            isOnSlope = true;

            slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
        }
        else
        {
            slopeSideAngle = 0.0f;
            isOnSlope = false;
        }

    }

    private void SlopeCheckVertical(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckDistance, _groundMask);

        if (hit)
        {

            slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;

            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (slopeDownAngle != lastSlopeAngle)
            {
                isOnSlope = true;
            }

            lastSlopeAngle = slopeDownAngle;

            Debug.DrawRay(hit.point, slopeNormalPerp, Color.blue);
            Debug.DrawRay(hit.point, hit.normal, Color.green);
        }

        if (slopeDownAngle > maxSlopeAngle || slopeSideAngle > maxSlopeAngle)
        {
            canWalkOnSlope = false;
        }
        else
        {
            canWalkOnSlope = true;
        }

        if (isOnSlope && canWalkOnSlope && _directionOfMovement == 0.0f)
        {
            _playerCollider.sharedMaterial = _hightFriction;
        }
        else
        {
            _playerCollider.sharedMaterial = _lowFriction;
        }
    }

    private void ContolSettings()
    {
        _moveRight = _playerControlSettings.RightMove;
        _moveLeft = _playerControlSettings.LeftMove;
        _jump = _playerControlSettings.Jump;
        _attack = _playerControlSettings.Attack;
}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
