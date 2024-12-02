using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EnemyWaveList", menuName = "Schmup/EnemyWaveList", order = 1)]
public class EnemyWaveList : ScriptableObject
{
    [SerializeField] public List<EnemyWave> waves = new List<EnemyWave>();
}
