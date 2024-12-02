using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Boss : Enemy {    
    Collider bossCollider;

    public List<BossPhase> Stages;
    int currentStage = 0;

    public override void Awake()
    {
        bossCollider = GetComponent<Collider>();
        base.Awake();
    }

    void Start() {
        bossCollider.enabled = true;
            
        foreach (var system in Stages.SelectMany(stage => stage.enemySystems)) {
                system.OnEnemyDestroyed.AddListener(CheckStageComplete);
        }
            
        InitializeStage();
    }
       
    void CheckStageComplete() {
        if (Stages[currentStage].IsPhaseCompleted()) {
            AdvanceToNextStage();
        }
    }

    void AdvanceToNextStage() {
        currentStage++;
        bossCollider.enabled = true;
            
        if (currentStage < Stages.Count-1) {
            InitializeStage();
        }
    }

    void InitializeStage() {
        Stages[currentStage].InitializeStage();
        bossCollider.enabled = !Stages[currentStage].IsBossInvulnerable;
        Debug.Log("AAAAAA" + bossCollider.enabled);
    }

    protected override void Die()
    {
        base.Die();
        GameStateManager.Instance.EndLevel();
    }
}
