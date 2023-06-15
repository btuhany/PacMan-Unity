
using UnityEngine;

public class PinkyGhost : Ghost
{
    public override Vector3 ChaseTarget()
    {
        if(_targetPacman.Movement.CurrentDir == Vector2.up)
        {
            return new Vector3(_targetPacman.transform.position.x - 4 , _targetPacman.transform.position.y + 4, _targetPacman.transform.position.z);
        }
        else
        {
            return new Vector3(_targetPacman.transform.position.x + _targetPacman.Movement.CurrentDir.x * 4, _targetPacman.transform.position.y + _targetPacman.Movement.CurrentDir.y * 4, _targetPacman.transform.position.z);
        }
    }
}
