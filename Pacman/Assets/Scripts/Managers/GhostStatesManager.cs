using UnityEngine;

public class GhostStatesManager : MonoBehaviour
{
    [SerializeField] Ghost[] _ghostsArray;
    [SerializeField] float _maxScatterTime;
    [SerializeField] float _minScatterTime;
    [SerializeField] float _maxChaseTime;
    [SerializeField] float _minChaseTime;
    float _scatterTime;
    float _chaseTime;

    GhostStateID _initialState = GhostStateID.Scatter;
    public GhostStateID CurrentState;
    float _timeCounter;

    public static GhostStatesManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        _scatterTime = GetRandomScatterTime();
        foreach (Ghost ghost in _ghostsArray)
        {
            ghost.StateMachine.ChangeState(_initialState);
        }
    }
    private void Update()
    {
        _timeCounter+=Time.deltaTime;
        if(CurrentState == GhostStateID.Scatter)
        {
            if(_timeCounter >= _scatterTime)
            {
                ChangeGhostStates(GhostStateID.Chase);
                CurrentState = GhostStateID.Chase;
                _chaseTime = GetRandomChaseTime();
                _timeCounter = 0;
            }
        }
        else if(CurrentState == GhostStateID.Chase)
        {
            if (_timeCounter >= _chaseTime)
            {
                ChangeGhostStates(GhostStateID.Scatter);
                CurrentState = GhostStateID.Scatter;
                _scatterTime = GetRandomScatterTime();
                _timeCounter = 0;
            }
        }

    }
    float GetRandomScatterTime()
    {
        return Random.Range(_minScatterTime, _maxScatterTime);
    }
    float GetRandomChaseTime()
    {
        return Random.Range(_minChaseTime, _maxChaseTime);
    }
    void ChangeGhostStates(GhostStateID ghostStateID)
    {
        foreach (Ghost ghost in _ghostsArray)
        {
            if(ghost.IsInHome) { return; }
            if (ghost.StateMachine.CurrentState == GhostStateID.Frightened) return;
            Debug.Log(ghost.gameObject.name);
            ghost.StateMachine.ChangeState(ghostStateID);
        }
    }
    
}
