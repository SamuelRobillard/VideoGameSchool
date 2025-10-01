using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToggleUISlide : MonoBehaviour
{
    public RectTransform UI;  // assigner dans l’Inspector
    private Button button;
    private bool isVisible = false;  // false par défaut (caché)
    private float slideDistance = 700f; // distance vers gauche/droite
    public float speed = 1f;

    void Start()
    {
        button = GetComponent<Button>();
        if (button != null)
            button.onClick.AddListener(ToggleUIVisibility);

        // Met l'UI caché au démarrage
        UI.anchoredPosition += new Vector2(-slideDistance, 0);
    }

    void ToggleUIVisibility()
    {
        StopAllCoroutines();
        if (isVisible)
        {
            // Ferme le menu → reprend le jeu
            StartCoroutine(Slide(UI.anchoredPosition, UI.anchoredPosition + new Vector2(-slideDistance, 0)));
            Time.timeScale = 1f;
        }
        else
        {
            // Ouvre le menu → met en pause
            StartCoroutine(Slide(UI.anchoredPosition, UI.anchoredPosition + new Vector2(slideDistance, 0)));
            Time.timeScale = 0f;
        }

        isVisible = !isVisible;
    }

    IEnumerator Slide(Vector2 start, Vector2 end)
    {
        float t = 0;
        while (t < 1)
        {
            // ⚡ Utilise unscaledDeltaTime pour que l’UI continue à bouger même en pause
            t += Time.unscaledDeltaTime * speed;
            UI.anchoredPosition = Vector2.Lerp(start, end, t);
            yield return null;
        }
        UI.anchoredPosition = end;
    }
}
