using Assets.Scripts.Weapons;
using UnityEngine;
using Utilities;

public class RotateTowardsPlayer : MonoBehaviour
{
    private Transform player;
    private GameStateManager gameStateManager;

    public void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        gameStateManager = GameStateManager.Instance;
    }

    public void Update() {
        Vector3 delta = player.position - transform.position;
        delta = gameStateManager.isTopView() ? delta.With(null, 0f) : delta.With(0f);
        delta.Normalize();
       
        transform.rotation = Quaternion.FromToRotation(gameStateManager.isTopView() ? Vector3.forward : Vector3.back, delta);
    }
}