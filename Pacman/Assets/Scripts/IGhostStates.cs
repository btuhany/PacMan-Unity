
public interface IGhostStates
{
    GhostStateID StateID { get; }
    void Enter();
    void Update();
    void OnNode(Node node);
    void Exit();
}
