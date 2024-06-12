using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    public Button upgradeButton;
    [SerializeField] TMP_Text upgradeText;

    public void SetUpgrade(Upgrade upgrade, Player player)
    {
        upgradeButton.onClick.AddListener(() => upgrade.applyUpgrade(player));
        upgradeText.text = upgrade.name;
    }
}
