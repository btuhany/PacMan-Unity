using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GhostHome : GhostState, IGhostStates
{

    public GhostStateID StateID => GhostStateID.Home;

    float _timeCounter = 0;
    bool _isExit = false;
    public GhostHome(Ghost ghost) : base(ghost)
    {

    }

    public void Enter()
    {
        _ghost.DefaultLook();
        _ghost.Movement.ChangeSpeedMultiplier(0.7f);
        _ghost.DefaultLook();
        if(_ghost.TimeConsumed > 0)
        {
            _timeCounter += _ghost.TimeConsumed;
            _ghost.TimeConsumed = 0;
        }
        
    }

    public void Exit()
    {
        _ghost.Movement.ChangeSpeedMultiplier(1f);
        _timeCounter = 0;
        _isExit = false;
    }

    public void Update()
    {
        if(!_isExit)
        {
            _timeCounter += Time.deltaTime;
            if(_timeCounter >= _ghost.HomeExitTime)
            {
                _timeCounter = 0;
                _isExit = true;
                _ghost.IsInHome= false;
            }
        }
        else
        {
            if(Vector2.Distance(_ghost.CurrentPos, _ghost.EatenTarget)<0.8f)
            {
                _ghost.StateMachine.ChangeState(GhostStatesManager.Instance.CurrentState);
            }
        }
    }
    public void OnNode(Node node)
    {
        if (_ghost.NodeDirectionLock) return;

        if(_isExit) { ExitFromHome(node); }
        else UpAndDownMove(node);
    }
    private void UpAndDownMove(Node node)
    {
        _ghost.Movement.SetNextDirection(_ghost.Movement.OppositeDir());
    }
    private void ExitFromHome(Node node)
    {
        _ghost.Movement.SetNextDirection(_ghost.MinDistanceDirection(node, _ghost.EatenTarget));
    }

}
