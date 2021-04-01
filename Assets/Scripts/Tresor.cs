using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe a ajouter sur les tresors.
/// Permet l'interaction (quand on clique sur un tresor).
/// </summary>
public class Tresor : MonoBehaviour
{
    //DEBUT DES CHAMPS SERIALISES
    [SerializeField] int _valeur = 10; //int, la valeur de l'objet (en points pour le joueur)
    //FIN DES CHAMPS SERIALISES

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
        GestNiveau.instance.RecupererTresor(_valeur); //declenche la methode RecupererTresor avec le Singleton de GestNiveau
        Destroy(gameObject); //detruit le tresor
    }
}