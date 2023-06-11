using System;
using UnityEngine;

public abstract class Ghost : MonoBehaviour
{
    [SerializeField] Transform _targetPacman;
    [SerializeField] GhostStateID _initialState;
    [SerializeField] CircleCollider2D _collider;

    [SerializeField] GameObject _body;
    [SerializeField] GameObject _eyes;
    [SerializeField] GameObject _blueBody;
    [SerializeField] GameObject _whiteBody;

    public Movement Movement;
    public int Point = 200;
    public virtual Vector3 ChaseTarget()
    {
        return  _targetPacman.position;
    }

    public Vector3 ScatterTarget = new Vector3(11.5f, 18.5f, 0); //blinky
    [HideInInspector] public Vector3 EatenTarget = new Vector3(0, 0.7f);

    public bool NodeDirectionLock = false;
    StateMachine _stateMachine;
    Vector3 _initalPos;
    public Vector2 CurrentPos => new Vector2(transform.position.x, transform.position.y);

    public StateMachine StateMachine { get => _stateMachine; }

    private void Awake()
    {
        _initalPos = transform.position;
        _stateMachine = new StateMachine();
        _stateMachine.RegisterState(new GhostChase(this));
        _stateMachine.RegisterState(new GhostEaten(this));
        _stateMachine.RegisterState(new GhostFrightened(this));
        _stateMachine.RegisterState(new GhostHome(this));
        _stateMachine.RegisterState(new GhostScatter(this));
    }
    private void OnEnable()
    {
        Movement.OnDirectionChanged += HandleOnDirectionChanged;
        DefaultLook();
    }

    private void Start()
    {
        _stateMachine.ChangeState(_initialState);
    }
    private void Update()
    {
        _stateMachine.Update();
    }
    public void StopMovement()
    {
        Movement.IsActive = false;
    }
    public void ChangeDirToOpposite()
    {
        Movement.ChangeToOppositeDir();
    }
    public void ResetState()
    {
        Movement.IsActive = true;
        transform.position = _initalPos;
        Movement.ResetState();
        this.gameObject.SetActive(true);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Pacman"))
        {
            switch (_stateMachine.CurrentState)
            {
                case GhostStateID.Chase:
                    GameManager.Instance.PacmanEaten();
                    break;
                case GhostStateID.Scatter:
                    GameManager.Instance.PacmanEaten();
                    break;
                case GhostStateID.Frightened:
                    GameManager.Instance.GhostEaten(this);
                    _stateMachine.ChangeState(GhostStateID.Eaten);
                    break;
                default:
                    break;
            }
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
    public void FrightenedState()
    {
        _body.gameObject.SetActive(false);
        _eyes.gameObject.SetActive(false);
        _blueBody.gameObject.SetActive(true);
        _whiteBody.gameObject.SetActive(false);

        _collider.isTrigger = false;
    }
    public void WhiteBlueLook()
    {
        _body.gameObject.SetActive(false);
        _eyes.gameObject.SetActive(false);
        _blueBody.gameObject.SetActive(false);
        _whiteBody.gameObject.SetActive(true);

        _collider.isTrigger = false;
    }
    public void DefaultLook()
    {
        _body.gameObject.SetActive(true);
        _eyes.gameObject.SetActive(true);
        _blueBody.gameObject.SetActive(false);
        _whiteBody.gameObject.SetActive(false);

        _collider.isTrigger = false;
        NodeDirectionLock = false;
    }
    public void EatenStateEnter()
    {
        _body.gameObject.SetActive(false);
        _eyes.gameObject.SetActive(true);
        _blueBody.gameObject.SetActive(false);
        _whiteBody.gameObject.SetActive(false);

        _collider.isTrigger = true;
    }

}
