using System.Collections.Generic;
using UnityEngine;

public class GhostHome : GhostState, IGhostStates
{

    public GhostStateID StateID => GhostStateID.Home;
    public GhostHome(Ghost ghost) : base(ghost)
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
        if (_ghost.NodeDirectionLock) return;
        UpAndDownMove(node);
    }
    private void UpAndDownMove(Node node)
    {
        _ghost.Movement.SetNextDirection(_ghost.Movement.OppositeDir());
    }

}
