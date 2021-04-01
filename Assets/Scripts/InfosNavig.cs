using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //importe le gestionnaire de scene

/// <summary>
/// Classe ScriptableObject pour gerer les infos de la navigation.
/// Memorise le numero du niveau en cours.
/// Gere le changement de scene.
/// Contient une mecanique de reinitialisation.
/// </summary>
[CreateAssetMenu(menuName="Infos de navigation",fileName="InfosDeNavigation")] //consigne de creation du menu de creation de l'asset
public class InfosNavig : ScriptableObject //preciser que c'est un S.O.
{
    //DEBUT DES CHAMPS SERIALISES
    [Header("Valeurs initiales")] //Entete de section pour les Valeurs initiales (debut de partie)
    [SerializeField] private int _niveauIni = 0; //int, le numero du niveau initial (0 par défaut)

    [Header("Valeurs en cours")] //Entete de section pour les Valeurs en cours (pendant la partie)
    [SerializeField] private int _niveau; //int, le numero du niveau en cours

    [Header("Valeurs permanentes")] //Entete de section pour les Valeurs «permanentes»
    [SerializeField] private int _buildIndexMenu = 0;  //int, le buildIndex du menu
    [SerializeField] private int _buildIndexN1 = 1;  //int, le buildIndex du niveau 1
    [SerializeField] private int _nbScenesDuBuild = 2; //int, le nombre de scenes de type niveau dans le build
    [SerializeField] private int _buildIndexFin = 3;  //int, le buildIndex de la scene de fin
    //FIN DES CHAMPS SERIALISES

    public int niveau { get => _niveau; set => _niveau = value; } //accesseur et mutateur du niveau en cours

    /// <summary>
    /// Methode qui remplace les valeurs en cours par les valeurs initiales
    /// </summary>
    public void Initialiser()
    {
        _niveau = _niveauIni;  //change la valeur du niveau par la valeur par defaut (ini)
    }

    /// <summary>
    /// Methode publique qui permet d'acceder au niveau suivant
    /// </summary>
    public void AllerNiveauSuivant()
    {
        _niveau++;  //incremente le numero du niveau
        int indexSceneSuiv = _buildIndexN1+((_niveau-1) % _nbScenesDuBuild); //calcule le buildIndex de la scene suivante
        SceneManager.LoadScene(indexSceneSuiv);  //va a la scene suivante
    }

    /// <summary>
    /// Methode publique qui permet d'acceder au menu
    /// </summary>
    public void AllerMenu()
    {
        SceneManager.LoadScene(_buildIndexMenu);  //va a la scene du menu
    }

    /// <summary>
    /// Methode publique qui permet d'acceder a l'ecran de fin
    /// </summary>
    public void AllerFin()
    {
        SceneManager.LoadScene(_buildIndexFin);  //va a la scene de la fin
    }
}