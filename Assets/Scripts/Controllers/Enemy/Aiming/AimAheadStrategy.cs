using UnityEngine;

[CreateAssetMenu(fileName = "AimAheadStrategy", menuName = "Shmup/AimingStrategy/Ahead")]
public class AimAheadStrategy : AimingStrategy
{    
    public override Quaternion Aim(Transform firingPoint)
    {
        return firingPoint.rotation;
    }
}