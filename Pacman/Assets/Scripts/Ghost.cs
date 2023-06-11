using System;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Movement Movement;
    [SerializeField] Transform _targetPacman;
    [SerializeField] GhostStateID _initialState;
    public int Point = 200;
    public Vector3 ChaseTarget => _targetPacman.position;


    public bool NodeDirectionLock = false;

    StateMachine _stateMachine;
    Vector3 _initalPos;
    public Vector2 CurrentPos => new Vector2(transform.position.x, transform.position.y);

 

    private void Awake()
    {
        _initalPos = transform.position;
        _stateMachine = new StateMachine();
        _stateMachine.RegisterState(new GhostChase(this));
        //_stateMachine.RegisterState(new GhostEaten());
        //_stateMachine.RegisterState(new GhostFrightened());
        //_stateMachine.RegisterState(new GhostHome());
        //_stateMachine.RegisterState(new GhostScatter());
    }
    private void OnEnable()
    {
        Movement.OnDirectionChanged += HandleOnDirectionChanged;
    }

    private void Start()
    {
        _stateMachine.ChangeState(_initialState);
    }
    public void ResetState()
    {
        transform.position = _initalPos;
        Movement.ResetState();
        this.gameObject.SetActive(true);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Pacman"))
        {
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Node"))
        {
            _stateMachine.OnNodeCollider(collision.GetComponent<Node>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Node"))
        {
            NodeDirectionLock = false;
        }
    }
    private void HandleOnDirectionChanged()
    {
        NodeDirectionLock = true;
    }
}
