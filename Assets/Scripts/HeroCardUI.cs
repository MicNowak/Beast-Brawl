using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroCardUI : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI heroName;
    [SerializeField] Image image;
    [SerializeField] Button selectButton;
    public void SetHero(HeroStats.HeroType hero)
    {
        heroName.text = hero.ToString();
        image.sprite = HeroStats.Heros[hero].sprite;
        selectButton.onClick.AddListener(() => FindObjectOfType<MainMenu>().PlayGame(hero));
    }
}
