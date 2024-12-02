

using UnityEngine;

public abstract class AimingStrategy : ScriptableObject
{
    public virtual void Init() { }
    public abstract Quaternion Aim(Transform firingPoint);
}