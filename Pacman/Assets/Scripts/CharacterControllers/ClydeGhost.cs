using UnityEngine;

public class ClydeGhost : Ghost
{
    public override Vector3 ChaseTarget()
    {
        if(Vector3.Distance(_targetPacman.transform.position, transform.position) > 8)
        {
            return _targetPacman.transform.position;
        }
        else
        {
            return ScatterTarget;
        }
    }
}
