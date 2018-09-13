using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerCharactor
{
    Kohaku_mode,
    Azaha_mode
};

public enum PlayerState
{
    Player_Idle,
    Player_Run,
    Player_Jump,
    Player_DoubleJump,
    Player_Land,
    Player_Attack,
    Player_SlideStep,
    Player_SpecialAttack,
    Player_Swing,
    Player_Fall,
    Player_Fly,
    Player_Ride,
    Player_Climing,
    Player_Hang,
    Player_JumpAttack,
    Player_Hurt,
    Player_Death
};

public enum EnemyStatus
{
    enemy_Idle,
    enemy_Attack,
    enemy_Run,
    enemy_Death,
    // 
    enemy_BattleRun,
    enemy_BackMove,
    enemy_RunReload,
    enemy_Reload,
    //
    enemy_Breath
}

[System.Serializable]
public class CharactorStatistics {

    [Header("-Charactor Status Value")]
    public int HP;
    public int hpMax;
    public int Damage;
    public int Def;
    public int Exp;
    public int LV;
    public int[] nextLV;

    public void AddCharactorHealthPoint(int enemyDamage)
    {
        int curDamage = enemyDamage + Def;
        if (curDamage > 0) curDamage = 0;
        HP += curDamage;
    }

    public void DrinkPotion(int potionValue)
    {
        HP += potionValue;
        if (HP >= hpMax) HP = hpMax;
    }

    public void AddExperiencePoint(int experiencePoint)
    {
        Exp += experiencePoint;
        if (Exp >= nextLV[LV])
        {
            HP = hpMax;
            Exp -= nextLV[LV];
            LV++;
        }
    }
}
