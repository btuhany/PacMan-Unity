using UnityEngine;

public class InkyGhost : Ghost
{
    [SerializeField] Transform _blinkyGhost;
    
    
    public override Vector3 ChaseTarget()
    {
        Vector3 blinkyRespectOriginPos;
        if (_targetPacman.Movement.CurrentDir == Vector2.up)
        {
            blinkyRespectOriginPos = new Vector3(_targetPacman.transform.position.x - 2, _targetPacman.transform.position.y + 2, _targetPacman.transform.position.z);
        }
        else
        {
            blinkyRespectOriginPos = new Vector3(_targetPacman.transform.position.x + _targetPacman.Movement.CurrentDir.x * 2, _targetPacman.transform.position.y + _targetPacman.Movement.CurrentDir.y * 2, _targetPacman.transform.position.z);
        }
        Vector3 reflectedBlinkPos = 2*blinkyRespectOriginPos - _blinkyGhost.transform.position;
        return reflectedBlinkPos;
        
    }
}
