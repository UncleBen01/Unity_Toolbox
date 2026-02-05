using UnityEngine;

public interface IInteraction
{
    public enum InteractionAction
    {
        None,
        Pickup
    }

    /// <summary>
    /// D�termine si une action est possible avec un certain objet. Renvoie un bool si l'action est r�alisable ou non. *V�RIFICATION*
    /// </summary>
    /// <param name="action">Action � faire</param>
    /// <returns>Retourne true si l'action est r�alisable. Retourne false sinon</returns>
    public bool CanInteract(InteractionAction action);

    /// <summary>
    /// Action lorsque l'objet est interagi. En fonction de l'action passée, elle exécute les comportements associés
    /// </summary>
    /// <param name="action">Action � faire</param>
    /// <param name="transformInteract">Emplacement de l'interagisseur qui fait l'interaction</param>
    public void Interact(InteractionAction action, Transform transformInteract);

    /// <summary>
    /// Renvoie le transform de l'objet
    /// </summary>
    /// <returns>Retourne le transform</returns>
    public Transform GetTransform();
}
