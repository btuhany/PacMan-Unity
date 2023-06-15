using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb;

    [SerializeField] float _speed;
    [SerializeField] Vector2 _initialDir;
    [SerializeField] LayerMask _obstacleLayer;

    float _speedMultiplier = 1.0f;
    Vector3 _startPos;
    Vector2 _currentDir;
    Vector2 _nextDir;
    public bool IsActive = false;
    public bool IsStopeed => _rb.velocity == Vector2.zero;
    public Vector2 CurrentDir { get => _currentDir; }
    public Rigidbody2D Rb { get => _rb; set => _rb = value; }

    public event System.Action OnDirectionChanged;

    private void Awake()
    {
        _startPos = transform.position;
        ResetState();
    }
    public void ResetState()
    {
        _speedMultiplier = 1.0f;
        _currentDir = _initialDir;
        _nextDir = Vector2.zero;
        transform.position = _startPos;
        _rb.isKinematic= false;
    }
    private void FixedUpdate()
    {
        if (!IsActive) return;

        // _rb.MovePosition(_rb.position + _currentDir * _speed * _speedMultiplier * Time.fixedDeltaTime);
        _rb.velocity = _currentDir * _speed * _speedMultiplier;

    }
    public void SetDirection(Vector2 direction, bool forced = false)
    {
        if (direction == _nextDir) return;
        if(CheckIfOppositeDir(direction))
        {
            ChangeDirection(direction);
        }
        else if (forced || !IsThereObstacle(direction))
        {
            ChangeDirection(direction);
        }
        else
        {
            _nextDir= direction;
        }
    }
    public bool IsThereObstacle(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.7f, 0f, direction, 1.5f, _obstacleLayer);
        return hit.collider != null;
    }
    public void TryNextDirection()  //called ontriggerstays for nodes
    {
        if (_nextDir != Vector2.zero)
        {
            if(!IsThereObstacle(_nextDir))
                ChangeDirection(_nextDir);
        }
    }
    public bool CheckIfOppositeDir(Vector2 direction)
    {
        if (_currentDir == Vector2.left && direction == Vector2.right)
        {
            return true;
        }
        else if (_currentDir == Vector2.right && direction == Vector2.left)
        {
            return true;
        }
        else if (_currentDir == Vector2.up && direction == Vector2.down)
        {
            return true;
        }
        else if (_currentDir == Vector2.down && direction == Vector2.up)
        {
            return true;
        }
        return false;
    }
    public void ChangeDirection(Vector2 direction)
    {
        if(_currentDir == direction) { _nextDir = Vector2.zero; return; }
        _currentDir = direction;
        _nextDir = Vector2.zero;
        //if(_flip)
        //    _flip.RotateSprite(_currentDir);
        OnDirectionChanged?.Invoke();
    }
    public void SetNextDirection(Vector2 dir)
    {
        if(!IsThereObstacle(dir))  //&& !CheckIfOppositeDir(dir)
            ChangeDirection(dir);
    }
    public void StopMovement()
    {
        _rb.velocity = Vector2.zero;
       
    }
    public void ChangeToOppositeDir()
    {
        ChangeDirection(OppositeDir());
    }
    public Vector2 OppositeDir()
    {
        Vector2 oppositeDir;
        if (_currentDir == Vector2.right)
            oppositeDir = Vector2.left;
        else if (_currentDir == Vector2.left)
            oppositeDir = Vector2.right;
        else if (_currentDir == Vector2.up)
            oppositeDir = Vector2.down;
        else if (_currentDir == Vector2.down)
            oppositeDir = Vector2.up;
        else
            oppositeDir = Vector2.zero;
        return oppositeDir;
    }
    public void ChangeSpeedMultiplier(float multiplier)
    {
        _speedMultiplier = multiplier;
    }
}
