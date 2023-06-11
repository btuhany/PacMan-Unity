using UnityEngine;

public abstract class GhostState 
{
	protected Ghost _ghost;
	protected Vector3 _target;
	public GhostState(Ghost ghost)
	{
		_ghost = ghost;
	}
}
