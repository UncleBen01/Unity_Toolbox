using UnityEngine;

public class Interactable : MonoBehaviour, IInteraction
{


    public bool CanInteract(IInteraction.InteractionAction action)
    {
        return action == IInteraction.InteractionAction.Pickup;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void Interact(IInteraction.InteractionAction action, Transform transformInteract)
    {
        //Logique de l'interaction
        switch(action)
        {
            case IInteraction.InteractionAction.None:

                break;

            case IInteraction.InteractionAction.Pickup:

                break;

        }
    }
}
