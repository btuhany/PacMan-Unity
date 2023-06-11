using UnityEngine;

public class GhostEaten : IGhostStates
{
    public GhostStateID StateID => GhostStateID.Eaten;

    public void Enter()
    {
    }

    public void Exit()
    {
    }

    public void Update()
    {
    }
    public void OnNode(Node node)
    {

    }
}
