using System.Collections.Generic;
using UnityEngine;

public class GhostFrightened : GhostState, IGhostStates
{

    public GhostStateID StateID => GhostStateID.Frightened;

    float timeCounter = 0;
    const float maxTime = 6;

    public GhostFrightened(Ghost ghost) : base(ghost)
    {
    }

    public void Enter()
    {
        if(!_ghost.NodeDirectionLock)
            _ghost.ChangeDirToOpposite();
        _ghost.FrightenedState();
        _ghost.Movement.ChangeSpeedMultiplier(0.5f);
    }

    public void Exit()
    {
        _ghost.Movement.ChangeSpeedMultiplier(1f);
    }

    public void Update()
    {
        timeCounter+= Time.deltaTime;
        if(timeCounter>=maxTime)
        {
            timeCounter=0;
            _ghost.StateMachine.ChangeState(GhostStatesManager.Instance.CurrentState);
        }
    }
    public void OnNode(Node node)
    {
        if (node.IsHomeEnterNode) return;
        if (_ghost.NodeDirectionLock) return;
        _ghost.Movement.SetNextDirection(_ghost.RandomDirection(node));
    }
    
}
