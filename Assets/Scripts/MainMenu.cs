using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject heroCardPrefab;
    [SerializeField] Transform heroCardParent;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;
    private void Start()
    {
        // Create hero cards
        foreach (Transform child in heroCardParent)
        {
            Destroy(child.gameObject);
        }
        foreach (HeroStats.HeroType hero in Enum.GetValues(typeof(HeroStats.HeroType)))
        {
            GameObject card = Instantiate(heroCardPrefab, heroCardParent);
            card.GetComponent<HeroCardUI>().SetHero(hero);
        }

        // Set the resolution dropdown to the current resolution
        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width &&
                               resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // Set fullscreen toggle to current fullscreen state
        Screen.fullScreen = true;
    }
    public void PlayGame(HeroStats.HeroType hero)
    {
        Player.selectedHero = hero;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void SetVolume(float value)
    {
        audioMixer.SetFloat("Volume", value);
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
