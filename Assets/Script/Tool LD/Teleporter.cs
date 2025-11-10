using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    [Header("Portail de sortie")]
    public Transform portailSortie;

    [Header("Tag du joueur")]
    public string playerTag = "Player";

    [Header("Options")]
    public bool conserverOrientation = true;
    public bool activerEffet = false;

    private bool peutTeleporter = true;
    public float delaiTeleport = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (peutTeleporter && other.CompareTag(playerTag) && portailSortie != null)
        {
            StartCoroutine(Teleporter(other));
        }
    }

    private System.Collections.IEnumerator Teleporter(Collider joueur)
    {
        peutTeleporter = false;

        // Déplace le joueur
        joueur.transform.position = portailSortie.position;

        // Optionnel : ajuste la rotation pour correspondre à celle du portail de sortie
        if (conserverOrientation)
            joueur.transform.rotation = portailSortie.rotation;

        // Effet visuel ou son (si activé)
        if (activerEffet)
        {
            // Exemple : tu peux ajouter ici un effet de particule ou un son
        }

        // Petit délai pour éviter les boucles de téléport
        yield return new WaitForSeconds(delaiTeleport);
        peutTeleporter = true;
    }

    private void OnDrawGizmos()
    {
        if (portailSortie != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, portailSortie.position);
            Gizmos.DrawSphere(portailSortie.position, 0.2f);
        }
    }
}