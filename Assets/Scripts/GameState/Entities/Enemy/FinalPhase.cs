using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FinalPhase : BossPhase {
    public List<GameObject> activateables;

    public override void Awake() {
        foreach (var ac in activateables) {
            ac.SetActive(false);
        }
    }

    public override void InitializeStage() {
        foreach (var ac in activateables)
        {
            ac.SetActive(true);
        }
    }
        
    public override bool IsPhaseCompleted() {
        return false;
    }
}
