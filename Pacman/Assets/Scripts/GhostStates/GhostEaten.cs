using UnityEngine;

public class GhostEaten : GhostState, IGhostStates
{
    public GhostStateID StateID => GhostStateID.Eaten;
    public GhostEaten(Ghost ghost) : base(ghost)
    {
        _target = _ghost.EatenTarget;   
    }
    public void Enter()
    {
        _ghost.EatenStateEnter();
        _ghost.Movement.ChangeSpeedMultiplier(1.7f);
    }
    public void Exit()
    {
        _ghost.Movement.StopMovement();
        _ghost.Movement.ChangeSpeedMultiplier(1f);
    }
    public void Update()
    {
        if (Vector3.Distance(_ghost.EatenTarget, _ghost.transform.position) < 1.3f)
        {
            _ghost.StateMachine.ChangeState(GhostStateID.Chase);
            _ghost.StartBugCheck();
        }
    }
    public void OnNode(Node node)
    {
        if (_ghost.NodeDirectionLock) return;

        _ghost.Movement.SetNextDirection(_ghost.MinDistanceDirection(node, _target));
    }
}
