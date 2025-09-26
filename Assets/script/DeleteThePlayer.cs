using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class DeleteThePlayer : MonoBehaviour
{

    public string sceneToChange;
    public GameObject player;
    public float fadeDuration = 1f;     // Durée du fondu (en secondes) avant disparition
    public bool destroyAfter = false;    // Si vrai : détruire l’objet à la fin. Sinon : juste le désactiver

    private SpriteRenderer sr;          // Référence au SpriteRenderer de l’objet
    private bool fading = false;        // Indique si le fade a commencé
    private float t = 0f;               // Compteur de temps pour suivre la progression du fade

    void Start()
    {
        // Récupère automatiquement le SpriteRenderer attaché à cet objet
        sr = player.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Si le fade n’a pas encore commencé, on sort de la fonction
        if (!fading) return;

        // On incrémente le temps écoulé depuis le début du fade
        t += Time.deltaTime;

        // Calcul d’un alpha (opacité) qui diminue linéairement de 1 → 0
        float alpha = Mathf.Clamp01(1f - (t / fadeDuration));

        // On applique le nouvel alpha au sprite (couleur inchangée, juste la transparence)
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);

        // Quand le temps écoulé dépasse la durée du fade
        if (t >= fadeDuration)
        {


            SceneManager.LoadScene(sceneToChange);
            // SceneManager.LoadScene(foundScene);
        }
    }

    async void OnTriggerEnter2D(Collider2D other)
    {
        // Si l’objet qui entre en collision a le tag "Hazard" (piège/zone de danger)
        if (other.CompareTag("Player"))
        {
            // On active le fade
            GameObject fire1 = GameObject.Find("burning_start_1_0 (1)");
            GameObject fire2 = GameObject.Find("burning_start_1_0 (2)");
            GameObject fire3 = GameObject.Find("burning_start_1_0 (3)");
            GameObject fire4 = GameObject.Find("burning_start_1_0 (4)");
            fire1.GetComponent<SpriteRenderer>().enabled = true;
            fire2.GetComponent<SpriteRenderer>().enabled = true;
            fire3.GetComponent<SpriteRenderer>().enabled = true;
            fire4.GetComponent<SpriteRenderer>().enabled = true;
            await Task.Delay(2000);
            fading = true;
                
        
           
            
           
        }
    }
}