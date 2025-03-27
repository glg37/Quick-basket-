using UnityEngine;
using UnityEngine.SceneManagement;  

public class MenuController : MonoBehaviour
{
    
    public void Jogar()
    {
        
        SceneManager.LoadScene("SampleScene");
    }

    
    public void Sair()
    {
        
        Application.Quit();

     
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
