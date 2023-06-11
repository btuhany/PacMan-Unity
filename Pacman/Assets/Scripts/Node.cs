using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] LayerMask _obstacleLayer;
    public bool IsLeftAvailable = false;
    public bool IsRightAvailable = false;
    public bool IsUpAvailable = false;
    public bool IsDownAvailable = false;
    private bool IsThereNotObstacle(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.7f, 0f, direction, 1.5f, _obstacleLayer);
        return hit.collider == null;
    }
    private void CheckAvailableDirections()
    {
        if (IsThereNotObstacle(Vector2.right))
        {
            IsRightAvailable = true;
        }
        if (IsThereNotObstacle(Vector2.up))
        {
            IsUpAvailable = true;
        }
        if (IsThereNotObstacle(Vector2.left))
        {
            IsLeftAvailable = true;
        }
        if (IsThereNotObstacle(Vector2.down))
        {
            IsDownAvailable = true;
        }
    }
    private void Start()
    {
        CheckAvailableDirections();
    }
}
