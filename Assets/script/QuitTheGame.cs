using System;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class QuitTheGame : MonoBehaviour
{
    [SerializeField] Button buttonQuit;
    [SerializeField] Button buttonMenu;
    
    [SerializeField] string sceneToLoad;
    private GameObject canvas;
    private bool isOpen = false;
    private  float cooldown = 0.2f;
    private float lastOpened = -9999f;
    void Start()
    {

        buttonQuit.onClick.AddListener(TaskQuit);
        buttonMenu.onClick.AddListener(TaskLoadSceneStart);

        canvas = GameObject.Find("canvaMenu");
        if (sceneToLoad == "Start")
        {
            canvas.SetActive(false);
        }
        else
        {
            canvas.SetActive(true);
        }
       
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && Time.time > lastOpened + cooldown && sceneToLoad =="Start")
        {
            lastOpened = Time.time;
            if (isOpen)
            {
                canvas.SetActive(false);
                isOpen = false;
            }
            else
            {
                canvas.SetActive(true);
                isOpen = true;
            }
        }
    }
    private void TaskQuit()
    {
        Debug.Log("Quitter le jeu !");
        Application.Quit();

        // En mode Éditeur, Unity ne ferme pas l'éditeur.
        // Pour tester dans l’éditeur, tu peux ajouter :
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    private void TaskLoadSceneStart()
    {
        Debug.Log("scene start");
        SceneManager.LoadScene(sceneToLoad);
    }
    public void CloseMenu()
    {
        canvas.SetActive(false);
    }
}
