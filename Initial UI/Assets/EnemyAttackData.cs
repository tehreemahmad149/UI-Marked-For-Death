using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackData
{
    public Collider attackHitbox;
    public float duration;
    public int startupFrames;
    public int attackFrames;
    public int recoveryFrames;
    public float stunDuration;
    public float stamina;
    public float dash;
    public float damage;
}

public class EnemyAttackData : MonoBehaviour
{
    public AttackData quickSlashData;
    public AttackData strongSlashData;
    public AttackData lowSweepData;
}
