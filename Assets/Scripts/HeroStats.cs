using System.Collections.Generic;
using UnityEngine;

public class HeroStats
{
    public string heroName;
    public Sprite sprite;
    public int lvl = 1, upgradeOptions;
    public float startHp, moveSpeed, fireRate, xpDropMultiplier, bulletSpread;
    public HashSet<Upgrade.UpgradeType> upgrades = new HashSet<Upgrade.UpgradeType>();
    public BulletStats bulletStats;
    public GameObject bulletPrefab;

    public enum HeroType
    {
        Knight,
        Rogue,
        Archer,
    }
    static string imageName = "Sprites/rogues";

    static Sprite LoadFromPath(string spriteName)
    {
        Sprite[] all = Resources.LoadAll<Sprite>(imageName);

        foreach (var s in all)
        {
            if (s.name == spriteName)
            {
                return s;
            }
        }
        return null;
    }

    static GameObject LoadPrefabFromPath(string path)
    {
        return Resources.Load<GameObject>(path);
    }

    public static Dictionary<HeroType, HeroStats> Heros = new Dictionary<HeroType, HeroStats>
    {

        {HeroType.Knight, new HeroStats
        {
            heroName = "Knight",
            sprite = LoadFromPath("rogues_5"),
            lvl = 1,
            upgradeOptions = 3,
            startHp = 100,
            moveSpeed = 4,
            fireRate = 1f,
            xpDropMultiplier = 1,
            bulletSpread = 20,
            upgrades = new HashSet<Upgrade.UpgradeType>(),
            bulletStats = new BulletStats
            {
                players = true,
                life = 0.5f,
                speed = 6,
                size = 1,
                damage = 10,
                penetration = 2,
                knockback = 1f,
                rotationSpeed = 360,
            },
            bulletPrefab = LoadPrefabFromPath("BulletPrefabs/Sword"),
        }},
        {HeroType.Rogue, new HeroStats
        {
            heroName = "Rogue",
            sprite = LoadFromPath("rogues_3"),
            lvl = 1,
            upgradeOptions = 3,
            startHp = 70,
            moveSpeed = 6,
            fireRate = 0.3f,
            xpDropMultiplier = 1,
            bulletSpread = 10,
            upgrades = new HashSet<Upgrade.UpgradeType>(),
            bulletStats = new BulletStats
            {
                players = true,
                life = 1,
                speed = 15,
                size = 1,
                damage = 3,
                penetration = 1,
                knockback = 0.4f,
                rotationSpeed = 720,
            },
            bulletPrefab = LoadPrefabFromPath("BulletPrefabs/Dagger"),
        }},
        {HeroType.Archer, new HeroStats
        {
            heroName = "Archer",
            sprite = LoadFromPath("rogues_2"),
            lvl = 1,
            upgradeOptions = 3,
            startHp = 60,
            moveSpeed = 5,
            fireRate = 0.7f,
            xpDropMultiplier = 1,
            bulletSpread = 5,
            upgrades = new HashSet<Upgrade.UpgradeType>(),
            bulletStats = new BulletStats
            {
                players = true,
                life = 2,
                speed = 20,
                size = 1,
                damage = 6,
                penetration = 2,
                knockback = 0.2f,
                rotationSpeed = 0,
            },
            bulletPrefab = LoadPrefabFromPath("BulletPrefabs/Arrow"),
        }},
    };
}


