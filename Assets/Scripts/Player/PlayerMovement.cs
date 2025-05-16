using UnityEngine;

public class PlayerMovement : Sounds
{
    [SerializeField] private PlayerNumber _playerNumber = new();
    [Header("Movement Settings")]
    [SerializeField] private Rigidbody2D _playerRb2D;
    [SerializeField] private CapsuleCollider2D _playerCollider;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Vector2 _groundCheckBoxSize = new Vector2(1f, 0.1f);
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _slopeCheckDistance;
    [SerializeField] private float _maxSlopeAngle;
    [SerializeField] private PhysicsMaterial2D _lowFriction;
    [SerializeField] private PhysicsMaterial2D _hightFriction;
    [Space(10)]
    [Header("Ground")]
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private LayerMask _groundMask;
    [Space(10)]
    [Header("Coyote Time Settings")]
    [SerializeField] private float _coyoteTime;
    [Space(10)]
    [Header("Jump Buffer Time Settings")]
    [SerializeField] private float _jumpBufferTime;
    [Space(10)]
    [Header("Player Animation")]
    [SerializeField] private Animator _animator;


    private Vector2 _targetVelocity;
    private bool _isFacingRight;
    private bool _isJump = false;
    private bool _isSmallJump = false;
    private bool _isGrounded;
    private int _directionOfMovement = 0;
    // Coyote Time
    private float _coyoteTimeCounter;
    // Jump Buffer Time Settings
    private float _jumpBufferCounter;
    // Slope
    private Vector2 _slopeNormalPerp;
    private bool _isOnSlope;
    private bool _canWalkOnSlope;
    private float _slopeDownAngle;
    private float _slopeSideAngle;
    private float _lastSlopeAngle;
    // Player Control Button
    private string _horizontalButtonName;
    private string _jumpButtonName;
    // Player Check Aiming
    private bool _isAiming;
    // Player Landing
    private bool _isLanding;

    private void Awake()
    {
        PlayerControlButton();
    }

    private void Start()
    {
        if (transform.rotation.y == 0)
            _isFacingRight = true;
        else
            _isFacingRight = false;
    }

    private void Update()
    {
        _animator.SetFloat("Run", Mathf.Abs(_directionOfMovement));

        if (_isAiming)
        {
            _directionOfMovement = 0;
            HorizontalDirection();
            return;
        }
        _directionOfMovement = HorizontalDirection();
        CoyoteTime();
        JumpBuffer();
        Jump();
        OnLanding();
    }


    private void FixedUpdate()
    {
        SlopeCheck();
        Movement();
    }

    public void SetAiming(bool isAiming)
    {
        _isAiming = isAiming;
    }

    private int HorizontalDirection()
    {
        float pressMoveButton = Input.GetAxisRaw(_horizontalButtonName);
        if (pressMoveButton > 0)
        {
            if (!_isFacingRight)
                Flip();
            return 1;
        }
        else if (pressMoveButton < 0)
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
        if (Input.GetButtonUp(_jumpButtonName) && _playerRb2D.linearVelocity.y > 0f)
        {
            _isSmallJump = true;

            _coyoteTimeCounter = 0f;
        }


    }

    private void CoyoteTime()
    {
        _isGrounded = CheckGrounding();
        if (_isGrounded)
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
        if (Input.GetButtonDown(_jumpButtonName))
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
        if (_isGrounded && !_isOnSlope && !_isJump)
        {
            _targetVelocity.Set(_horizontalSpeed * _directionOfMovement, 0.0f);
            _playerRb2D.linearVelocity = _targetVelocity;
        }
        else if (_isGrounded && _isOnSlope && _canWalkOnSlope && !_isJump)
        {
            _targetVelocity.Set(_horizontalSpeed * _slopeNormalPerp.x * -_directionOfMovement, _horizontalSpeed * _slopeNormalPerp.y * -_directionOfMovement);
            _playerRb2D.linearVelocity = _targetVelocity;
        }
        else if (!_isGrounded)
        {
            _targetVelocity.Set(_horizontalSpeed * _directionOfMovement, _playerRb2D.linearVelocity.y);
            _playerRb2D.linearVelocity = _targetVelocity;
        }

        if (_isJump)
        {
            _playerRb2D.linearVelocity = new Vector2(_playerRb2D.linearVelocity.x, _jumpForce);
            _isJump = false;

            _animator.SetBool("IsJumping", true);
            PlaySound(0);
        }
        if (_isSmallJump)
        {
            _playerRb2D.linearVelocity = new Vector2(_playerRb2D.linearVelocity.x, _playerRb2D.linearVelocity.y * 0.5f);
            _isSmallJump = false;

            //_animator.SetBool("IsJumping", true);
            //PlaySound(0);
        }
    }


    public bool CheckGrounding()
    {
        return Physics2D.OverlapBox(_groundCheck.position, _groundCheckBoxSize, 0f, _groundMask);
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    private void SlopeCheck()
    {
        Vector2 checkPos = transform.position - (Vector3)(new Vector2(0.0f, _playerCollider.size.y / 2));
        SlopeCheckHorizontal(checkPos);
        SlopeCheckVertical(checkPos);
    }
    private void SlopeCheckHorizontal(Vector2 checkPos)
    {
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, transform.right, _slopeCheckDistance, _groundMask);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -transform.right, _slopeCheckDistance, _groundMask);

        if (slopeHitFront)
        {
            _isOnSlope = true;

            _slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);

        }
        else if (slopeHitBack)
        {
            _isOnSlope = true;

            _slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
        }
        else
        {
            _slopeSideAngle = 0.0f;
            _isOnSlope = false;
        }

    }

    private void SlopeCheckVertical(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, _slopeCheckDistance, _groundMask);

        if (hit)
        {

            _slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;

            _slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (_slopeDownAngle != _lastSlopeAngle)
            {
                _isOnSlope = true;
            }

            _lastSlopeAngle = _slopeDownAngle;

            Debug.DrawRay(hit.point, _slopeNormalPerp, Color.blue);
            Debug.DrawRay(hit.point, hit.normal, Color.green);
        }

        if (_slopeDownAngle > _maxSlopeAngle || _slopeSideAngle > _maxSlopeAngle)
        {
            _canWalkOnSlope = false;
        }
        else
        {
            _canWalkOnSlope = true;
        }

        if (_isOnSlope && _canWalkOnSlope && _directionOfMovement == 0.0f)
        {
            _playerCollider.sharedMaterial = _hightFriction;
        }
        else
        {
            _playerCollider.sharedMaterial = _lowFriction;
        }
    }

    private void PlayerControlButton()
    {
        if (_playerNumber == PlayerNumber.PlayerOne)
        {
            _horizontalButtonName = "Horizontal One";
            _jumpButtonName = "Jump One";
        }
        else
        {
            _horizontalButtonName = "Horizontal Two";
            _jumpButtonName = "Jump Two";
        }
    }

    private void OnLanding()
    {
        if (_isLanding == false && CheckGrounding())
        {
            _isLanding = true;
            _animator.SetBool("IsJumping", false);
            PlaySound(1);
        }
        else if (_isLanding != false && !CheckGrounding())
        {
            _isLanding = false;
        }
    }
    private void PlayRunSound()
    {

        PlaySound(2);
        if (!CheckGrounding() || _directionOfMovement == 0)
            StopSound();
    }

    private void OnDrawGizmos()
    {
        if (_groundCheck)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(_groundCheck.position, _groundCheckBoxSize);
        }
    }

    private void OnDisable()
    {
        _directionOfMovement = 0;
        _playerRb2D.linearVelocity = Vector2.zero;
        _animator.SetFloat("Run", _directionOfMovement);
        _animator.SetBool("IsJumping", false);
    }
}
