using UnityEngine;

public class UpgradesUI : MonoBehaviour
{
    [SerializeField] GameObject upgradeUIPrefab;
    [SerializeField] Transform upgradesParent;

    public void ShowUpgradeList(Upgrade[] upgradeArr)
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
        foreach (Transform child in upgradesParent)
        {
            Destroy(child.gameObject);
        }
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        foreach (Upgrade upgrade in upgradeArr)
        {
            GameObject upgradeUIObj = Instantiate(upgradeUIPrefab, upgradesParent);
            UpgradeUI upgradeUI = upgradeUIObj.GetComponent<UpgradeUI>();
            upgradeUI.SetUpgrade(upgrade, player);
            upgradeUI.upgradeButton.onClick.AddListener(() =>
            {
                Time.timeScale = 1;
                gameObject.SetActive(false);
            });
        }
    }
}
