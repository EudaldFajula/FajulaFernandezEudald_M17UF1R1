using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScene : MonoBehaviour
{
    public void OnMainMenuClick()
    {
        SceneManager.LoadScene("StartMenu");
    }
    private void Start()
    {
        
    }
}
