using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void ShowPauseMenu()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
    }
    public void HidePauseMenu()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
