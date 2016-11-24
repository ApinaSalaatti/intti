using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public enum InteractionType { USE, LOOKAT }

public abstract class Interaction : MonoBehaviour
{
    [SerializeField]
    private InteractionType type;
    public InteractionType Type { get { return type; } }

    [SerializeField]
    private bool canBePickedUp;
    public bool CanBePickedUp { get { return canBePickedUp; } }

    public abstract string Description(Interaction itemInHand);

    public abstract void OnInteraction(Interaction itemInHand);
}
