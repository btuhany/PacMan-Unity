using UnityEngine;

public class GhostScatter : GhostState, IGhostStates
{
    public GhostStateID StateID => GhostStateID.Scatter;
    public GhostScatter(Ghost ghost) : base(ghost)
    {
        _target = ghost.ScatterTarget;
    }
    public void Enter()
    {
        _ghost.DefaultLook();
    }

    public void Exit()
    {
    }

    public void Update()
    {
        
    }
    public void OnNode(Node node)
    {
        if (node.IsHomeEnterNode) return;
        if (_ghost.NodeDirectionLock) return;
        _ghost.Movement.SetNextDirection(_ghost.MinDistanceDirection(node, _target));
    }
   
}
