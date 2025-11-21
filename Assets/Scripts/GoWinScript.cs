using UnityEngine;
using UnityEngine.SceneManagement;

public class GoWinScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        SceneManager.LoadScene("WinScene"); 
    }
}
