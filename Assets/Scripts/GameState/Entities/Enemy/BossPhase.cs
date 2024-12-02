using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossPhase : MonoBehaviour {
    public List<Enemy> enemySystems;
    public bool IsBossInvulnerable = true;

    public virtual void Awake() {
        foreach (var system in enemySystems) {
            system.gameObject.SetActive(false);
        }
    }
    public virtual void InitializeStage() {
        foreach (var system in enemySystems) {
            system.gameObject.SetActive(true);
        }
    }

    public virtual bool IsPhaseCompleted() {
        return enemySystems.All(system => system == null || (system.GetHealth() <= 0));
    }
}
