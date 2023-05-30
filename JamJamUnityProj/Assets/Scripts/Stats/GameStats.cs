using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameStats : ScriptableObject
{
    [Header("Melee")]
    public float MeleeDamage = 1f;

    [Header("Boomerang")]
    public float boomerangDamage = 1f;
    public float boomerangSpeed = 1f;
    public float boomerangRange = 1f;

    [Header("Player")]
    public float Health = 1f;
    public float DamageResistance = 1f;
    public float WalkSpeed = 1f;
    public float LootMultiplier = 1f;
    public float Luck = 1f;
    public float Corruption = 1f;
}
