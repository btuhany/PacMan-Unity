using System.Collections.Generic;
using UnityEngine;

public class GhostFrightened : GhostState, IGhostStates
{

    public GhostStateID StateID => GhostStateID.Frightened;




    public GhostFrightened(Ghost ghost) : base(ghost)
    {
    }

    public void Enter()
    {
        _ghost.ChangeDirToOpposite();
        _ghost.FrightenedState();
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
        RandomPathFinding(node);
    }
    private void RandomPathFinding(Node node)
    {

        List<Vector2> currentAvailableDirections = new List<Vector2>();

        foreach (Vector2 dir in node.AvailableDirections)
        {
            currentAvailableDirections.Add(dir);
        }    
          
        for (int i = 0; i < currentAvailableDirections.Count; i++)
        {
            if (currentAvailableDirections[i] == _ghost.Movement.OppositeDir())
                currentAvailableDirections.Remove(currentAvailableDirections[i]);
        }
        _ghost.Movement.SetNextDirection(currentAvailableDirections[Random.Range(0, currentAvailableDirections.Count)]);
    }
}
