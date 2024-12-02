using UnityEngine;

public class HandRotation : MonoBehaviour
{
    #region Rotating hand v1
    [SerializeField] private GameObject _handPivot;
    [SerializeField] private float _rotationSpeed;

    private float _rotateDirection = 0;

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        RoteteHand();
    }

    private void RoteteHand()
    {
        _handPivot.transform.Rotate(0.0f, 0.0f, _rotationSpeed * _rotateDirection, Space.World);
        Debug.Log(_handPivot.transform.rotation.eulerAngles.z);
    }

    private void PlayerInput()
    {
        _rotateDirection = Input.GetAxisRaw("Vertical Two");
    }
    #endregion

    //#region Rotating hand v2
    //[SerializeField] private GameObject _handPivot;
    //[SerializeField] private float _rotationSpeed;

    //private float _targetAngle = 0;
    //private bool _isRotating = false;

    //private void Update()
    //{
    //    PlayerInput();
    //}

    //private void FixedUpdate()
    //{
    //    if (_isRotating)
    //    {
    //        RotateHand();
    //    }
    //}

    //private void RotateHand()
    //{
    //    float currentAngle = _handPivot.transform.rotation.eulerAngles.z;
    //    float angleDifference = Mathf.DeltaAngle(currentAngle, _targetAngle);

    //    if (Mathf.Abs(angleDifference) > 0.1f)
    //    {
    //        float rotationStep = _rotationSpeed;
    //        float newAngle = Mathf.MoveTowardsAngle(currentAngle, _targetAngle, rotationStep);
    //        _handPivot.transform.rotation = Quaternion.Euler(0.0f, 0.0f, newAngle);
    //    }
    //    else
    //    {
    //        _isRotating = false;
    //    }
    //}

    //private void PlayerInput()
    //{
    //    if (Input.GetAxisRaw("Horizontal Two") > 0)
    //    {
    //        _targetAngle = 0;
    //        _isRotating = true;
    //    }
    //    else if (Input.GetAxisRaw("Vertical Two") > 0)
    //    {
    //        _targetAngle = 89;
    //        _isRotating = true;
    //    }
    //    else if (Input.GetAxisRaw("Horizontal Two") < 0)
    //    {
    //        _targetAngle = 179;
    //        _isRotating = true;
    //    }
    //    else if (Input.GetAxisRaw("Vertical Two") < 0)
    //    {
    //        _targetAngle = 271;
    //        _isRotating = true;
    //    }
    //    else
    //    {
    //        _isRotating = false;
    //    }
    //}
    //#endregion

}
