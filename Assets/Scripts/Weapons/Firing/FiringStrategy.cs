using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class FiringStrategy : ScriptableObject
{
    public virtual void Init() { }
    public abstract void Fire(Transform firingPos, Quaternion aim, GameObject projectile, int bulletDamage);
}


