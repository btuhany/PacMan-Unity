using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] LayerMask _obstacleLayer;
    
    public bool IsLeftAvailable = false;
    public bool IsRightAvailable = false;
    public bool IsUpAvailable = false;
    public bool IsDownAvailable = false;

    public bool IsManual = false;
    public bool IsHomeEnterNode = false;
    public List<Vector2> AvailableDirections = new List<Vector2>();
    private void Start()
    {
        if (!IsManual)
            CheckAvailableDirections();
        else
            AddAvailableDirectionsToList();
    }
    void AddAvailableDirectionsToList()
    {
        if(IsLeftAvailable)
        {
            AvailableDirections.Add(Vector2.left);
        }
        if(IsRightAvailable)
        {
            AvailableDirections.Add(Vector2.right);
        }
        if(IsUpAvailable)
        {
            AvailableDirections.Add(Vector2.up);
        }
        if(IsDownAvailable)
        {
            AvailableDirections.Add(Vector2.down);
        }
    }
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
            AvailableDirections.Add(Vector2.right);
        }
        if (IsThereNotObstacle(Vector2.up))
        {
            IsUpAvailable = true;
            AvailableDirections.Add(Vector2.up);
        }
        if (IsThereNotObstacle(Vector2.left))
        {
            IsLeftAvailable = true;
            AvailableDirections.Add(Vector2.left);
        }
        if (IsThereNotObstacle(Vector2.down))
        {
            IsDownAvailable = true;
            AvailableDirections.Add(Vector2.down);
        }
    }
}
