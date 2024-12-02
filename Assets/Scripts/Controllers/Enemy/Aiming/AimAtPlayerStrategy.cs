using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


[CreateAssetMenu(fileName = "AimAtPlayerStrategy", menuName = "Shmup/AimingStrategy/AtPlayer")]
public class AimAtPlayerStrategy : AimingStrategy
{
    private Transform player;

    public override void Init()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
    }

    public override Quaternion Aim(Transform firingPoint)
    {
        Vector3 delta = player.position - firingPoint.position;
        delta.Normalize();

        return Quaternion.FromToRotation(Vector3.forward, delta);
    }
}