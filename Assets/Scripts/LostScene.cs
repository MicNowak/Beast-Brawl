using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LostScene : MonoBehaviour
{
    [SerializeField] Image deadHeroImage;
    void Start()
    {
        deadHeroImage.sprite = HeroStats.Heros[Player.selectedHero].sprite;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
