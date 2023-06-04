using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameStats : ScriptableObject
{
    public enum Stat
    {
        MeleeDamage = 0,
        BoomerangeDamage,
        BoomerangSpeed,
        BoomerangRange,
        Health,
        DamageResistance,
        WalkSpeed,
        LootMultiplier,
        Luck,
        Corruption
    }

    public Dictionary<Stat, float> statistics;

    public GameStats()
    {
        InitializeDictionary();
    }

    public void InitializeDictionary()
    {
        statistics = new Dictionary<Stat, float>();
        foreach (Stat stat in Enum.GetValues(typeof(Stat)))
        {
            statistics[stat] = 1f;
        }
    }

    public float GetStat(Stat stat)
    {
        return statistics[stat];
    }

    public void SetStat(Stat stat, float value)
    {
        statistics[stat] = value;
    }

    public static Stat GetRandomStat()
    {
        return (GameStats.Stat)System.Enum.Parse(typeof(GameStats.Stat), LaneLibrary.RandomMethods.Choose<string>(System.Enum.GetNames(typeof(GameStats.Stat))));
    }
}
