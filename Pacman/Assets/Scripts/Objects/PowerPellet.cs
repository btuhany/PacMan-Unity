
using UnityEngine;

public class PowerPellet : Pellet
{

    protected override void GetEaten()
    {
        base.GetEaten();
        GameManager.Instance.PowerPelletEaten();
    }
}
