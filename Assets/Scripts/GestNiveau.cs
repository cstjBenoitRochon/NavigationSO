using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //importe TextMeshPro

/// <summary>
/// Classe du gestionnaire de niveau.
/// Doit etre appliquee sur un gameObject vide qui contiendra les elements du jeu.
/// </summary>
public class GestNiveau : MonoBehaviour
{
    //DEBUT DES CHAMPS SERIALISES
    [Header("Données et interface")] //Entete de section pour les données et l'interface
    [SerializeField] private InfosJoueur _iJoueur; //infos du joueur
    [SerializeField] private InfosNavig _iNavig; //infos de la navigation
    [SerializeField] private TextMeshProUGUI _champNiveau; //champ texte TMP du niveau
    [SerializeField] private TextMeshProUGUI _champScore; //champ texte TMP du score
    [SerializeField] private TextMeshProUGUI _champVie; //champ texte TMP du niveau de vie

    [Header("Caractéristiques du niveau")] //Entete de section pour les caractéristiques du niveau
    [SerializeField] private int _nbTresorsIni = 3; //int, nombre de tresors a placer
    [SerializeField] private GameObject _prefabTresor; //prefab du tresor
    [SerializeField] private int _nbEnnemis = 2; //int, nombre d'ennemis a placer
    [SerializeField] private GameObject _prefabEnnemi; //prefab de l'ennemi
    [SerializeField] private Vector2 _posMin; //v2, pos min pour placer objets+ennemis
    [SerializeField] private Vector2 _posMax; //v2, pos max pour placer objets+ennemis
    //FIN DES CHAMPS SERIALISES

    private int _nbTresorsRecup = 0; //int, nombre de tresors recuperes par le joueur (0 au début)

    private static GestNiveau _instance; //propriete privee requise pour le Singleton
    public static GestNiveau instance => _instance; //accesseur pour le Singleton

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        //DEBUT DE LA SECTION SINGLETON
        if(_instance == null) _instance = this; //condition habituelle du Singleton
        else
        {
            Destroy(gameObject); //detruire
            return; //mettre fin a l'execution
        }
        //FIN DE LA SECTION SINGLETON

        AfficherNiveau(); //afficher le niveau actuel dans le champ du niveau
        AfficherScore(); //afficher le score actuel dans le champ du score
        AfficherVie(); //afficher le nombre de vie actuel dans le champ de la vie

        Ajouter(_nbTresorsIni, _prefabTresor); //ajouter les tresors requis
        Ajouter(_nbEnnemis, _prefabEnnemi); //ajouter les ennemis requis
    }

    /// <summary>
    /// Permet d'ajouter un type d'objet dans la scene, selon une quantite determinee
    /// </summary>
    /// <param name="nb">Le nombre</param>
    /// <param name="prefab">Le prefab de l'objet</param>
    private void Ajouter(int nb, GameObject prefab) //prevoir les 2 parametres requis
    {
        for(int i = 0; i < nb; i++) //boucle for
        {
            GameObject go = Instantiate(prefab, transform); //instanciation, comme enfant du niveau (garder ref de l'instance)
            float x =  Random.Range(_posMin.x, _posMax.x); //etablir x aleatoire, selon proprietes min et max
            float y = Random.Range(_posMin.y, _posMax.y); //etablir y aleatoire, selon proprietes min et max
            go.transform.position = new Vector2(x, y); //appliquer la position en x et y
        }
    }

    /// <summary>
    /// Affiche le texte requis dans le champ du niveau
    /// </summary>
    private void AfficherNiveau()
    {
        _champNiveau.text = "Niveau: " + _iNavig.niveau; //affichage de la concatenation du mot et de l'info
    }

    /// <summary>
    /// Affiche le texte requis dans le champ de la vie
    /// </summary>
    private void AfficherVie()
    {
        string pluriel = (_iJoueur.vies>1)? "s" : ""; //preparation d'une variable pour le pluriel
        _champVie.text = $"Vie{pluriel} {_iJoueur.vies}"; //affichage de la concatenation du mot, du pluriel et de l'info
    }

    /// <summary>
    /// Affiche le texte requis dans le champ du score
    /// </summary>
    private void AfficherScore()
    {
        _champScore.text = "Score: " + _iJoueur.score; //affichage de la concatenation du mot et de l'info
    }

    /// <summary>
    /// Methode publique appelee par un tresor quand le joueur le recupere.
    /// Donne les points et passe au niveau suivant quand il n'y a plus de tresors.
    /// </summary>
    /// <param name="points">Les points associes a ce tresor</param>
    public void RecupererTresor(int points) //ne pas oublier d'ajouter le parametre
    {
        _iJoueur.score += points; //ajout des points dans les infos du joueur
        AfficherScore(); //mise a jour de l'affichage du score
        _nbTresorsRecup++; //incrementation du nombre de tresors du actuels
        if(_nbTresorsRecup>=_nbTresorsIni) //si tous les trésors ont ete ramasses
            _iNavig.AllerNiveauSuivant(); //changer de scene avec la methode des infos de navigation
    }

    /// <summary>
    /// Methode publique appelee par un ennemi quand le joueur perd une vie.
    /// Enleve une vie et passe a l'ecran de fin si la reserve est epuisee.
    /// </summary>
    public void PerdreVie()
    {
        _iJoueur.vies--; //decrementation de la vie dans les infos du joueur
        AfficherVie(); //mise a jour de l'affichage de la vie
        if(_iJoueur.vies <= 0) _iNavig.AllerFin(); //si mort pour vrai, aller a la scene de fin
    }
}