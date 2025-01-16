using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject pausePanel;
    private bool isPaused = false;

    public void PlaySound()
    {
        //AudioManager.PlaySound(SoundType.CLICK);
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
        Debug.Log(sceneIndex);
    }
    
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) Continue();
            else Pause();
            
        }
    }
    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }
    public void Continue()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }
}
