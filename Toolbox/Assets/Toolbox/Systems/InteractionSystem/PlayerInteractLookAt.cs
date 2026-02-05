using UnityEngine;

public class PlayerInteractLookAt : MonoBehaviour
{
    private const float RAYON_CAST_SPHERE = .1f;
    private const float DISTANCE_INTERACTION = 3f;

    private Transform cameraMainTransform;

    private void Start()
    {
        cameraMainTransform = Camera.main.transform;
    }

    public IInteraction GetInteractableObject()
    {
        RaycastHit[] hits = Physics.SphereCastAll(
            cameraMainTransform.position,
            RAYON_CAST_SPHERE,
            cameraMainTransform.forward,
            DISTANCE_INTERACTION
        );

        // Trier par distance
        System.Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

        foreach (RaycastHit hit in hits)
        {
            IInteraction interaction = hit.transform.GetComponentInParent<IInteraction>();
            if (interaction != null)
                return interaction;
        }

        return null;
    }

}
