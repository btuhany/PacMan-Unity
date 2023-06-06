using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Flip))]
public class Movement : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] Flip _flip;

    [SerializeField] float _speed;
    [SerializeField] Vector2 _initialDir;
    [SerializeField] LayerMask _obstacleLayer;

    float _speedMultiplier = 1.0f;
    Vector3 _startPos;
    Vector2 _currentDir;
    Vector2 _nextDir;

    public Vector2 CurrentDir { get => _currentDir; }

    private void Awake()
    {
        _startPos = transform.position;
        ResetState();
    }
    void ResetState()
    {
        _speedMultiplier = 1.0f;
        _currentDir = _initialDir;
        _nextDir = Vector2.zero;
        transform.position = _startPos;
        _rb.isKinematic= false;
    }
    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _currentDir * _speed * _speedMultiplier * Time.fixedDeltaTime);
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
    private bool CheckIfOppositeDir(Vector2 direction)
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
    private void ChangeDirection(Vector2 direction)
    {
        _currentDir = direction;
        _flip.RotateSprite(_currentDir);
        _nextDir = Vector2.zero;
    }
}
