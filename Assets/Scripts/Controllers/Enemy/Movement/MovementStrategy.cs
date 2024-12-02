

using UnityEngine;

public abstract class MovementStrategy : ScriptableObject
{
    public abstract void Move(Transform transform);
}