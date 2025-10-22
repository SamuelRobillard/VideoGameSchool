using UnityEngine;

public class MouvementFleche : MonoBehaviour
{
    public float vitesse = 5.0f; // Vitesse de déplacement

    void Update()
    {
        // Récupère les entrées horizontales (flèches gauche/droite ou A/D)
        float inputHorizontal = Input.GetAxis("Horizontal");
        // Récupère les entrées verticales (flèches haut/bas ou Z/S)
        float inputVertical = Input.GetAxis("Vertical");

        // Crée un vecteur de mouvement
        Vector3 mouvement = new Vector3(inputHorizontal, 0.0f, inputVertical);

        // Applique le mouvement à l'objet
        transform.Translate(mouvement * vitesse * Time.deltaTime);
    }
}