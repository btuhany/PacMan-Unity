using System.Collections.Generic;
using UnityEngine;

public class GhostHome : GhostState, IGhostStates
{

    public GhostStateID StateID => GhostStateID.Home;

    float _timeCounter = 0;
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
        _timeCounter += Time.deltaTime;
        if(_timeCounter >= _ghost.HomeExitTime)
        {
            _ghost.IsInHome= false;
            _ghost.StateMachine.ChangeState(GhostStatesManager.Instance.CurrentState);
        }
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
