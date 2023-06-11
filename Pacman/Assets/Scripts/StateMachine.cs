public class StateMachine
{
    private IGhostStates[] _states = new IGhostStates[5];
    public GhostStateID CurrentState { get; private set; }
    public void RegisterState(IGhostStates state)
    {
        int index = (int)state.StateID;
        _states[index] = state;
    }
    public void ChangeState(GhostStateID newState)
    {
        GetState(CurrentState)?.Exit();
        CurrentState = newState;
        GetState(CurrentState)?.Enter();
    }
    public IGhostStates GetState(GhostStateID state)
    {
        int index = (int)state;
        return _states[index];
    }
    public void Update()
    {
        GetState(CurrentState)?.Update();
    }
    public void OnNodeCollider(Node node)
    {
        GetState(CurrentState)?.OnNode(node);
    }
}