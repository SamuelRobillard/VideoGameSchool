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
            for (int i = 0; i < 10; i++)
            {

                sr.color = Color.black;
                await Task.Delay(100);
                sr.color = Color.red;
                await Task.Delay(100);
                sr.color = Color.blue;
                await Task.Delay(100);
                sr.color = Color.green;
                await Task.Delay(100);
                if (i == 4)
                {
                    fading = true;
                }
            }
           
            
           
        }
    }
}