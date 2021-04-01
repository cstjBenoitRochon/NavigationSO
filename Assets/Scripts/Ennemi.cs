using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe a ajouter sur les ennemis.
/// Permet l'interaction (quand on clique sur un ennemi).
/// </summary>
public class Ennemi : MonoBehaviour
{
    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        //detecter si c'est une collision avec le perso //POUR UNE PROCHAINE VERSION
    }

    /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button while
    /// over the GUIElement or Collider.
    /// </summary>
    void OnMouseDown()
    {
        GestNiveau.instance.PerdreVie(); //declenche la methode PerdreVie avec le Singleton de GestNiveau
    }
}