using UnityEngine;

// une classe statique qui permet de stocker la dernier scene pour 
// pouvoir la load dans les scenes defeat ou start (si le joueur va au menu principal a partir du niveau 2
// on veut que le niveau 2 se charge s'il clique sur play et non le niveau 1)
public static class handleScene
{
    public static string lastScene;
}
