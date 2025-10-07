using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartTheGame : MonoBehaviour

    
{

    private string sceneToChange;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            try
            {
                sceneToChange = handleScene.lastScene;
                Debug.Log(handleScene.lastScene);
                if (sceneToChange != null || sceneToChange != "Start")
                {
                    Debug.Log(sceneToChange);
                    SceneManager.LoadScene(sceneToChange);
                }
                else
                {
                    SceneManager.LoadScene("scene1");
                }
            }
            catch
            {
                SceneManager.LoadScene("scene1");
            }
            
        }   
    }
}
