using UnityEngine;

public class BroomMovement : MonoBehaviour
{
    [Header("Broom movement settings")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private Rigidbody2D _broomRb2D;

    private int _directionOfMovementHorizontal = 0;
    private int _directionOfMovementVertical = 0;
    private Vector2 _targetVelocity;


    private void Update()
    {
        _directionOfMovementHorizontal = HorizontalDirection();
        _directionOfMovementVertical = VerticalDirection();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        _targetVelocity.Set(_directionOfMovementHorizontal, _directionOfMovementVertical);
        _broomRb2D.linearVelocity = _targetVelocity.normalized * _speed;
    }

    private int HorizontalDirection()
    {
        float pressMoveButton = Input.GetAxisRaw("Horizontal One");
        if (pressMoveButton > 0)
        {
            return 1;
        }
        else if (pressMoveButton < 0)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    private int VerticalDirection()
    {
        float pressMoveButton = Input.GetAxisRaw("Vertical One");
        if (pressMoveButton > 0)
        {
            return 1;
        }
        else if (pressMoveButton < 0)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }
}
