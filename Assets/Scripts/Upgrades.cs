using System.Collections.Generic;
using UnityEngine;

public class Upgrade
{
    public delegate void ApplyUpgrade(Player player);
    public string name, description;
    public HashSet<UpgradeType> requiredUpgrades;
    public ApplyUpgrade applyUpgrade;

    public enum UpgradeType
    {
        AddGun,
        AddMaxHp1,
        AddMaxHp2,
        MoreXp1,
        MoreXp2,
        AddDamage1,
        AddDamage2,
        AddMoveSpeed1,
        AddMoveSpeed2,
        AddFireRate1,
        AddFireRate2,
        AddPenetration,
        AddBulletSpeed1,
        AddBulletSpeed2,
        AddBulletLife,
        AddMoreOptions,
    }

    public static Upgrade[] GetRandomUpgrades(HeroStats hero)
    {
        List<Upgrade> possibleUpgrades = new List<Upgrade>();
        foreach (Upgrade upgrade in upgrades.Values)
        {
            if (upgrade.requiredUpgrades.IsSubsetOf(hero.upgrades))
            {
                possibleUpgrades.Add(upgrade);
            }
        }
        Upgrade[] randomUpgrades;

        randomUpgrades = new Upgrade[possibleUpgrades.Count < hero.upgradeOptions ? possibleUpgrades.Count : hero.upgradeOptions];
        for (int i = 0; i < hero.upgradeOptions && possibleUpgrades.Count != 0; i++)
        {
            int randomIndex = Random.Range(0, possibleUpgrades.Count);
            randomUpgrades[i] = possibleUpgrades[randomIndex];
            possibleUpgrades.RemoveAt(randomIndex);
        }
        return randomUpgrades;
    }

    public static Dictionary<UpgradeType, Upgrade> upgrades = new Dictionary<UpgradeType, Upgrade>
    {
        {UpgradeType.AddGun, new Upgrade
        {
            name = "Add Gun 1",
            description = "Add a gun to the player",
            requiredUpgrades = new HashSet<UpgradeType>()
            {
                UpgradeType.AddBulletSpeed1,
                UpgradeType.AddBulletLife,
                UpgradeType.AddDamage2,
                UpgradeType.AddFireRate2,
            },
            applyUpgrade = (player) =>
            {
                player.AddGun();
                player.hero.upgrades.Add(UpgradeType.AddGun);
            }
        }},
        {UpgradeType.AddMaxHp1, new Upgrade
        {
            name = "Add Max Hp 1",
            description = "Add 10 to the player's max hp",
            requiredUpgrades = new HashSet<UpgradeType>(),
            applyUpgrade = (player) =>
            {
                player.AddMaxHp(10);
                player.hero.upgrades.Add(UpgradeType.AddMaxHp1);
            }
        }},
        {UpgradeType.AddMaxHp2, new Upgrade
        {
            name = "Add Max Hp 2",
            description = "Add 50 to the player's max hp",
            requiredUpgrades = new HashSet<UpgradeType>()
            {
                UpgradeType.AddMaxHp1
            },
            applyUpgrade = (player) =>
            {
                player.AddMaxHp(50);
                player.hero.upgrades.Add(UpgradeType.AddMaxHp2);
            }
        }},
        {UpgradeType.MoreXp1, new Upgrade
        {
            name = "More Xp 1",
            description = "Add 20% to the player's xp drop multiplier",
            requiredUpgrades = new HashSet<UpgradeType>(),
            applyUpgrade = (player) =>
            {
                player.hero.xpDropMultiplier *= 1.2f;
                player.hero.upgrades.Add(UpgradeType.MoreXp1);
            }
        }},
        {UpgradeType.MoreXp2, new Upgrade
        {
            name = "More Xp 2",
            description = "Add 50% to the player's xp drop multiplier",
            requiredUpgrades = new HashSet<UpgradeType>()
            {
                UpgradeType.MoreXp1
            },
            applyUpgrade = (player) =>
            {
                player.hero.xpDropMultiplier *= 1.5f;
                player.hero.upgrades.Add(UpgradeType.MoreXp2);
            }
        }},
        {UpgradeType.AddDamage1, new Upgrade
        {
            name = "Add Damage 1",
            description = "Add 1 to the player's damage",
            requiredUpgrades = new HashSet<UpgradeType>(),
            applyUpgrade = (player) =>
            {
                player.hero.bulletStats.damage += 2;
                player.hero.upgrades.Add(UpgradeType.AddDamage1);
            }
        }},
        {UpgradeType.AddDamage2, new Upgrade
        {
            name = "Add Damage 2",
            description = "Add 5% damage buff",
            requiredUpgrades = new HashSet<UpgradeType>()
            {
                UpgradeType.AddDamage1
            },
            applyUpgrade = (player) =>
            {
                player.hero.bulletStats.damage *= 1.05f;
                player.hero.upgrades.Add(UpgradeType.AddDamage2);
            }
        }},
        {UpgradeType.AddMoveSpeed1, new Upgrade
        {
            name = "Add Move Speed 1",
            description = "Add 1 to the player's move speed",
            requiredUpgrades = new HashSet<UpgradeType>(),
            applyUpgrade = (player) =>
            {
                player.hero.moveSpeed += 0.2f;
                player.hero.upgrades.Add(UpgradeType.AddMoveSpeed1);
            }
        }},
        {UpgradeType.AddMoveSpeed2, new Upgrade
        {
            name = "Add Move Speed 2",
            description = "Add 5% to the player's move speed",
            requiredUpgrades = new HashSet<UpgradeType>()
            {
                UpgradeType.AddMoveSpeed1
            },
            applyUpgrade = (player) =>
            {
                player.hero.moveSpeed *= 1.05f;
                player.hero.upgrades.Add(UpgradeType.AddMoveSpeed2);
            }
        }},
        {UpgradeType.AddFireRate1, new Upgrade
        {
            name = "Add Fire Rate 1",
            description = "Lowers fire rate by 5%",
            requiredUpgrades = new HashSet<UpgradeType>(),
            applyUpgrade = (player) =>
            {
                player.hero.fireRate *= 0.95f;
                player.hero.upgrades.Add(UpgradeType.AddFireRate1);
            }
        }},
        {UpgradeType.AddFireRate2, new Upgrade
        {
            name = "Add Fire Rate 2",
            description = "Lowers fire rate by 10%",
            requiredUpgrades = new HashSet<UpgradeType>()
            {
                UpgradeType.AddFireRate1
            },
            applyUpgrade = (player) =>
            {
                player.hero.fireRate *= 0.9f;
                player.hero.upgrades.Add(UpgradeType.AddFireRate2);
            }
        }},
        {UpgradeType.AddPenetration, new Upgrade
        {
            name = "Add Penetration",
            description = "Add 1 to the player's penetration",
            requiredUpgrades = new HashSet<UpgradeType>(),
            applyUpgrade = (player) =>
            {
                player.hero.bulletStats.penetration += 1;
                player.hero.upgrades.Add(UpgradeType.AddPenetration);
            }
        }},
        {UpgradeType.AddBulletSpeed1, new Upgrade
        {
            name = "Add Bullet Speed 1",
            description = "Add 1 to the player's bullet speed",
            requiredUpgrades = new HashSet<UpgradeType>(),
            applyUpgrade = (player) =>
            {
                player.hero.bulletStats.speed += 1;
                player.hero.upgrades.Add(UpgradeType.AddBulletSpeed1);
            }
        }},
        {UpgradeType.AddBulletSpeed2, new Upgrade
        {
            name = "Add Bullet Speed 2",
            description = "Add 5% to the player's bullet speed",
            requiredUpgrades = new HashSet<UpgradeType>()
            {
                UpgradeType.AddBulletSpeed1
            },
            applyUpgrade = (player) =>
            {
                player.hero.bulletStats.speed *= 1.05f;
                player.hero.upgrades.Add(UpgradeType.AddBulletSpeed2);
            }
        }},
        {UpgradeType.AddBulletLife, new Upgrade
        {
            name = "Add Bullet Life",
            description = "Add 1 to the player's bullet life",
            requiredUpgrades = new HashSet<UpgradeType>(),
            applyUpgrade = (player) =>
            {
                player.hero.bulletStats.life += 0.5f;
                player.hero.upgrades.Add(UpgradeType.AddBulletLife);
            }
        }},
        {UpgradeType.AddMoreOptions, new Upgrade
        {
            name = "Add More Options",
            description = "Add 1 to the player's upgrade options",
            requiredUpgrades = new HashSet<UpgradeType>(),
            applyUpgrade = (player) =>
            {
                player.hero.upgradeOptions += 1;
                player.hero.upgrades.Add(UpgradeType.AddMoreOptions);
            }
        }},

    };
}

