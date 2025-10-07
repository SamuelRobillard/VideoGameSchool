using System;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class QuitTheGame : MonoBehaviour
{
    [SerializeField] Button buttonQuit;
    [SerializeField] Button buttonMenu;


    private GameObject canvas;
    private bool isOpen = false;
    private float cooldown = 0.2f;
    private float lastOpened = -9999f;
    void Start()
    {

        buttonQuit.onClick.AddListener(TaskQuit);
        buttonMenu.onClick.AddListener(TaskLoadSceneStart);

        canvas = GameObject.Find("canvaMenu");
        if (SceneManager.GetActiveScene().name == "Start" || SceneManager.GetActiveScene().name == "defeat" || SceneManager.GetActiveScene().name == "Victory")
        {
            canvas.SetActive(true);
        }
        else
        {
            canvas.SetActive(false);
        }

    }

    void Update()
    {

        if (Input.GetKey(KeyCode.Escape) && Time.time > lastOpened + cooldown && (SceneManager.GetActiveScene().name != "Start" &&
        SceneManager.GetActiveScene().name != "defeat" && SceneManager.GetActiveScene().name != "Victory"
        ))
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
        try
        {
            // stock le nom de la derniere scene pour pouvoir reload la bonne
            // ne prend pas en compte les scene Start, defeat et Victory
            if (SceneManager.GetActiveScene().name != "Start" && SceneManager.GetActiveScene().name != "defeat" && SceneManager.GetActiveScene().name != "Victory")
                handleScene.lastScene = SceneManager.GetActiveScene().name;

        }
        catch
        {
            Debug.Log("no scene");
        }
        if (SceneManager.GetActiveScene().name != "Start" && SceneManager.GetActiveScene().name != "defeat" && SceneManager.GetActiveScene().name != "Victory")
        {
            SceneManager.LoadScene("Start");
        }
        else
        {
            if (handleScene.lastScene != null && handleScene.lastScene != "Start")
            {
                SceneManager.LoadScene(handleScene.lastScene);
            }
            else
            {
                SceneManager.LoadScene("scene1");
            }
        }
    }
    public void CloseMenu()
    {
        canvas = GameObject.Find("canvaMenu");
        canvas.SetActive(false);
    }
    public void LoadNextScene()
    {
        
        SceneManager.LoadScene("Scene2"); 
    }
}
