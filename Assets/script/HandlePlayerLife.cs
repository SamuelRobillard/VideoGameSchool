using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
public class HandlePlayerLife : MonoBehaviour
{

    [SerializeField] PlayerMovement playerMovement;
    private int viesRestante = 3;
    private bool lostAllLife = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void winALife()
    {
        if (viesRestante < 3 && !lostAllLife)
        {
            viesRestante += 1;
            GameObject lifes = GameObject.Find("lifes" + " " + "(" + viesRestante + ")");
            lifes.GetComponent<SpriteRenderer>().enabled = true;
        }


    }

    public async void loseALifeVersionRenderer()
    {
        if (!lostAllLife)
        {
               playerMovement.animator.SetBool("isHit", true);
        GameObject lifes = GameObject.Find("lifes" + " " + "(" + viesRestante + ")");
        lifes.GetComponent<SpriteRenderer>().enabled = false;
        viesRestante -= 1;
        if (viesRestante == 0)
        {
            lostAllLife = true;
            await Task.Delay(2000);
            
            endingGame();
        }
        }
     
    }
    private void endingGame()
    {
        
        winALife();
        winALife();
        winALife();
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);

    }
}
