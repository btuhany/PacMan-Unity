using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GhostChase : GhostState, IGhostStates
{

    public GhostStateID StateID => GhostStateID.Chase;
    public GhostChase(Ghost ghost) : base(ghost)
    {
    }

    public void Enter()
    {
        _ghost.DefaultLook();
    }

    public void Exit()
    {
    }

    public void Update()
    {
    

    }

    public void OnNode(Node node)
    {
        if (node.IsHomeEnterNode) return;
        if (_ghost.NodeDirectionLock) return;
        PathFinding(node, _ghost.ChaseTarget());
    }
    private void PathFindingNew(Node node, Vector3 target)
    {
        Dictionary<Vector2, float> directionsDistances = new Dictionary<Vector2, float>();
        foreach (Vector2 dir in node.AvailableDirections)
        {
            if (_ghost.Movement.OppositeDir() != dir)
            {
                float distance = Vector3.Distance(dir * 2, target);
                directionsDistances.Add(dir, distance);
            }
        }
       
    }
    private void PathFinding(Node node, Vector3 target)
    {
        Vector2 tempPosLeftRight;
        Vector2 tempPosUpDown;

        Vector2 dirLeftRight;
        Vector2 dirUpDown;

        bool forcedUpDown = false;
        bool forcedLeftRight = false;

        if (target.x >= _ghost.CurrentPos.x && node.IsRightAvailable && _ghost.Movement.CurrentDir != Vector2.left)
        {
            tempPosLeftRight = new Vector2(_ghost.CurrentPos.x + 1, _ghost.CurrentPos.y);
            dirLeftRight = Vector2.right;

        }
        else if (target.x <= _ghost.CurrentPos.x && node.IsLeftAvailable && _ghost.Movement.CurrentDir != Vector2.right)
        {
            tempPosLeftRight = new Vector2(_ghost.CurrentPos.x - 1, _ghost.CurrentPos.y);
            dirLeftRight = Vector2.left;

        }
        else
        {
            forcedUpDown = true;
            tempPosLeftRight = _ghost.CurrentPos;
            dirLeftRight = _ghost.Movement.CurrentDir;
        }

        if (target.y >= _ghost.CurrentPos.y && node.IsUpAvailable && _ghost.Movement.CurrentDir != Vector2.down)
        {
            tempPosUpDown = new Vector2(_ghost.CurrentPos.x, _ghost.CurrentPos.y + 1);
            dirUpDown = Vector2.up;

        }
        else if (target.y <= _ghost.CurrentPos.y && node.IsDownAvailable && _ghost.Movement.CurrentDir != Vector2.up)
        {
            tempPosUpDown = new Vector2(_ghost.CurrentPos.x, _ghost.CurrentPos.y - 1);
            dirUpDown = Vector2.down;

        }
        else
        {
            forcedLeftRight = true;
            tempPosUpDown = _ghost.CurrentPos;
            dirUpDown = _ghost.Movement.CurrentDir;
        }

        float distanceLeftRight = Vector2.Distance(tempPosLeftRight, target);
        float distanceUpDown = Vector2.Distance(tempPosUpDown, target);

        if (forcedLeftRight && !forcedUpDown)
        {
            _ghost.Movement.SetNextDirection(dirLeftRight);
            return;
        }
        else if (forcedUpDown && !forcedLeftRight)
        {
            _ghost.Movement.SetNextDirection(dirUpDown);
            return;
        }
        else if (forcedUpDown && forcedLeftRight)
        {
            if (node.IsUpAvailable && _ghost.Movement.CurrentDir != Vector2.down)
            {
                _ghost.Movement.SetNextDirection(Vector2.up);

            }
            else if (node.IsLeftAvailable && _ghost.Movement.CurrentDir != Vector2.right)
            {
                _ghost.Movement.SetNextDirection(Vector2.left);

            }
            else if (node.IsDownAvailable && _ghost.Movement.CurrentDir != Vector2.up)
            {
                _ghost.Movement.SetNextDirection(Vector2.down);

            }
            else if (node.IsRightAvailable && _ghost.Movement.CurrentDir != Vector2.left)
            {
                _ghost.Movement.SetNextDirection(Vector2.right);
            }
            return;
        }

        if (distanceLeftRight > distanceUpDown)
        {
            _ghost.Movement.SetNextDirection(dirUpDown);

        }
        else if (distanceLeftRight < distanceUpDown)
        {
            _ghost.Movement.SetNextDirection(dirLeftRight);

        }
        else
        {
            if (node.IsUpAvailable && dirUpDown == Vector2.up)
            {
                _ghost.Movement.SetNextDirection(dirUpDown);

            }
            else if (node.IsLeftAvailable && dirLeftRight == Vector2.left)
            {
                _ghost.Movement.SetNextDirection(dirLeftRight);

            }
            else if (node.IsDownAvailable && dirUpDown == Vector2.down)
            {
                _ghost.Movement.SetNextDirection(dirUpDown);

            }
            else if (node.IsRightAvailable && dirLeftRight == Vector2.right)
            {
                _ghost.Movement.SetNextDirection(dirLeftRight);
            }
        }
    }
}
