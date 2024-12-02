using UnityEngine;

public class HealthItem : Item {
    protected override void ItemEffect(Collision collision)
    {
        var p = collision.gameObject.GetComponent<Player>();
        if (p is not null)
        {
            p.AddHealth(1);
        }
    }
}
