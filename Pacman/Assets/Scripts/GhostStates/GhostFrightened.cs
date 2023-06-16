using System.Collections.Generic;
using UnityEngine;

public class GhostFrightened : GhostState, IGhostStates
{

    public GhostStateID StateID => GhostStateID.Frightened;

    float _timeCounter = 0;
    const float maxTime = 7;

    public GhostFrightened(Ghost ghost) : base(ghost)
    {
    }

    public void Enter()
    {
       // _ghost.Movement.CurrentDir = Vector2.zero;
        //_ghost.Movement.NextDir = Vector2.zero;
        _ghost.FrightenedState();
        _ghost.Movement.ChangeSpeedMultiplier(0.65f);
        if (!_ghost.IsInNode && !_ghost.NodeDirectionLock)
            _ghost.ChangeDirToOpposite();
    }

    public void Exit()
    {
        _ghost.IsInNode = false;
        _ghost.NodeDirectionLock = false;
        _timeCounter = 0;
        _ghost.Movement.ChangeSpeedMultiplier(1f);
    }

    public void Update()
    {
        _timeCounter+= Time.deltaTime;
        if(_timeCounter>=maxTime)
        {
            if(_ghost.IsInHome)
            {
                _ghost.TimeConsumed = _timeCounter;
                _ghost.StateMachine.ChangeState(GhostStateID.Home);
            }
            else
            {
                _ghost.StateMachine.ChangeState(GhostStatesManager.Instance.CurrentState);
            }
        }
        else if(_timeCounter >= maxTime / 2.6f)
        {
            _ghost.WhiteBlueLook();
        }
    }
    public void OnNode(Node node)
    {
        if (node.IsHomeEnterNode) return;
        if (_ghost.NodeDirectionLock) return;
        if(_ghost.IsInHome)
        {
            _ghost.Movement.SetNextDirection(_ghost.Movement.OppositeDir());
        }
        else
        {
            _ghost.Movement.SetNextDirection(_ghost.RandomDirection(node));
        }
    }
    
}
