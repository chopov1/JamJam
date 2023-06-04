using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MagicItem : ScriptableObject
{
    public Image icon;
    public string itemName;
    public GameStats stats;
    public GameStats.Stat buffedStat;
    public GameStats.Stat debuffedStat;

    static List<string> adjectives;
    static List<string> nouns;

    static MagicItem()
    {
        adjectives = new List<string>()
        {
            "Cursed",
            "Lucky",
            "Blessed",
            "Enchanted",
            "Banished",
            "Hellish"
        };

        nouns = new List<string>()
        {
            "Elixir",
            "Potion",
            "Ale",
            "Drink",
            "Concoction",
            "Tincture",
            "Brew",
            "Tonic"
        };
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

        item.stats.SetStat(item.buffedStat, 1.4f); //Random.Range(1.1f, 1.5f));
        item.stats.SetStat(item.debuffedStat, 0.8f); //Random.Range(0.7f, 0.9f));

        item.itemName = $"{LaneLibrary.RandomMethods.Choose(adjectives.ToArray())} {LaneLibrary.RandomMethods.Choose(nouns.ToArray())} of {item.buffedStat.ToString()}";

        return item;
    }
}
