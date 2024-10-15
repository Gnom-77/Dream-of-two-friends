using UnityEngine;

[CreateAssetMenu]
public class SavedNextSpawnPoint : ScriptableObject
{
    [SerializeField] private int _doorIndex;

    public int DoorIndex
    {
        get
        {
            return _doorIndex;
        }
        set
        {
            _doorIndex = value;
        }
    }
}
