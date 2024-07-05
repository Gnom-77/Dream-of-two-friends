using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlSettings : MonoBehaviour
{
    [SerializeField] private string _rightMove;
    [SerializeField] private string _leftMove;
    [SerializeField] private string _jump;
    [SerializeField] private string _attack;
    
    public string RightMove => _rightMove;
    public string LeftMove => _leftMove;
    public string Jump => _jump;
    public string Attack => _attack;


}
