using UnityEngine;
using UnityEngine.SceneManagement;

public class WonScene : MonoBehaviour
{
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
