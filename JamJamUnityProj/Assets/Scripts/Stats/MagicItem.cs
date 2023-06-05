using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MagicItem : ScriptableObject
{
    public Sprite icon;
    public string itemName;
    public GameStats stats;
    public GameStats.Stat buffedStat;
    public GameStats.Stat debuffedStat;
    public int price;

    #region Ricky's Stuff
    public Dictionary<string, string> itemAdjectives;
    public Dictionary<string, string> itemNouns;
    static List<string> positiveAdjectives;
    static List<string> negativeAdjectives;
    static List<string> positiveNouns;
    static List<string> negativeNouns;
    static Dictionary<GameStats.Stat, string> StatAndPositiveAdjective;
    static Dictionary<GameStats.Stat, string> StatAndNegativeAdjective;
    static Dictionary<GameStats.Stat, string> StatAndPositiveNoun;
    static Dictionary<GameStats.Stat, string> StatAndNegativeNoun;

    #endregion


    static List<string> adjectives;
    static List<string> nouns;

    static ItemIcons icons;
    static MagicItem()
    {
        // adjectives = new List<string>()
        // {
        //     "Cursed",
        //     "Lucky",
        //     "Blessed",
        //     "Enchanted",
        //     "Banished",
        //     "Hellish"
        // };

        // nouns = new List<string>()
        // {
        //     "Elixir",
        //     "Potion",
        //     "Ale",
        //     "Drink",
        //     "Concoction",
        //     "Tincture",
        //     "Brew",
        //     "Tonic"
        // };

        // Adjectives and Nouns are in the order that their respective stat appears in GameStats.Stat 
        positiveAdjectives = new List<string>()
        {
            "Empowering", // scythe damage up
            "Skillful", // scythe speed up
            "Sharp-Shooting", // scythe range up
            "Tough", // health up
            "Speedy", // walk speed up
            "Lucky", // loot multiplier up
            "Corrupted" // corruption up
        };
        negativeAdjectives = new List<string>()
        {
            "Weakening", // scythe damage down
            "Clumsy", // scythe speed down
            "Near-Sighted", // scythe range down
            "Frail", // health down
            "Sluggish", // walk speed down
            "Unlucky", // loot multiplier down
            "Forgiven" // corruption down
        };
        positiveNouns = new List<string>()
        {
            "Elixir of Power", // scythe damage up
            "Amulet of Control", // scythe speed up
            "Tonic of Far Reach", // scythe range up
            "Enchanted Armor", // health up
            "Boots of Haste", // walk speed up
            "Rabbit's Foot", // loot multiplier up
            "Grimmoire" // corruption up
        };
        negativeNouns = new List<string>()
        {
            "Potion of Weakness", // scythe damage down
            "Slippery Handle", // scythe speed down
            "Burdened Blade", // scythe range down
            "Poisoned Chalice", // health down
            "Leaden Boots", // walk speed down
            "Cracked Mirror", // loot multiplier down
            "Holy Relic" // corruption down
        };
        StatAndPositiveAdjective = new Dictionary<GameStats.Stat, string>();
        StatAndNegativeAdjective = new Dictionary<GameStats.Stat, string>();
        StatAndPositiveNoun = new Dictionary<GameStats.Stat, string>();
        StatAndNegativeNoun = new Dictionary<GameStats.Stat, string>();
        for(int i = 0; i < Enum.GetValues(typeof(GameStats.Stat)).Length; i++)
        {
            // I know this is a horrendous way of doing this don't come for me 
            StatAndPositiveAdjective.Add((GameStats.Stat)i, positiveAdjectives[i]);
            StatAndNegativeAdjective.Add((GameStats.Stat)i, negativeAdjectives[i]);
            StatAndPositiveNoun.Add((GameStats.Stat)i, positiveNouns[i]);
            StatAndNegativeNoun.Add((GameStats.Stat)i, negativeNouns[i]);
        }

        icons = Resources.Load<ItemIcons>("MagicItemIcons");
    }
    
    public static MagicItem GenerateMagicItem()
    {
        MagicItem item = ScriptableObject.CreateInstance<MagicItem>();

        item.stats = ScriptableObject.CreateInstance<GameStats>();
        // buff stat
        item.buffedStat = GameStats.GetRandomStat();
        item.debuffedStat = GameStats.GetRandomStat();

        while (item.debuffedStat == item.buffedStat)
        {
            item.debuffedStat = GameStats.GetRandomStat();
        }

        item.stats.SetStat(item.buffedStat, UnityEngine.Random.Range(1.1f, 1.5f));
        item.stats.SetStat(item.debuffedStat, UnityEngine.Random.Range(0.7f, 0.9f));

        item.price = Mathf.RoundToInt(7 * item.stats.GetStat(item.buffedStat));
        
        if(LaneLibrary.RandomMethods.RANDOM.Next(0,2) == 0) // Positive Adjective + Negative Noun
        {
            StatAndPositiveAdjective.TryGetValue(item.buffedStat, out string positiveAdjective);
            StatAndNegativeNoun.TryGetValue(item.debuffedStat, out string negativeNoun);
            item.itemName = $"{positiveAdjective} {negativeNoun}";
            item.icon = icons.negativeNouns[(int)item.debuffedStat];
            //Debug.Log(item.itemName);
        }
        else // Negative Adjective + Positive Noun
        {
            StatAndNegativeAdjective.TryGetValue(item.debuffedStat, out string negativeAdjective);
            StatAndPositiveNoun.TryGetValue(item.buffedStat, out string positiveNoun);
            item.itemName = $"{negativeAdjective} {positiveNoun}";
            item.icon = icons.positiveNouns[(int)item.buffedStat];
            //Debug.Log(item.itemName);
        }
        return item;
    }
}
