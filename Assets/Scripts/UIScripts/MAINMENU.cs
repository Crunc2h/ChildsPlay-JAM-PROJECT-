using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MAINMENU : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenu() => SceneManager.LoadScene(0);
    public void Restart() => SceneManager.LoadScene(1);
    public void Exit() => Application.Quit();
    public void Credits() => SceneManager.LoadScene(4);

}
