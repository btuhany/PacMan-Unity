using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GhostChase : GhostState, IGhostStates
{

    public GhostStateID StateID => GhostStateID.Chase;
    public GhostChase(Ghost ghost) : base(ghost)
    {
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
        _ghost.Movement.SetNextDirection(_ghost.MinDistanceDirection(node,_ghost.ChaseTarget()));
    }
}
