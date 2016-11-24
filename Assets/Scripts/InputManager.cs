using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
    private static InputManager instance;
    public static InputManager Instance { get { return instance; } }

    private PlayerMover mover;
    private MouseLook look;
    private PlayerInteraction interaction;

    [SerializeField]
    private GameObject player;

    private GameObject inputGrabber;

    void Awake ()
    {
        instance = this;
    }

    void Start ()
    {
        mover = player.GetComponent<PlayerMover>();
        look = player.GetComponent<MouseLook>();
        interaction = player.GetComponent<PlayerInteraction>();
    }

    public bool GrabInput(GameObject grabber)
    {
        if(inputGrabber == null)
        {
            inputGrabber = grabber;
            mover.enabled = false;
            look.enabled = false;
            interaction.enabled = false;
            return true;
        }
        return false;
    }
    public bool ReleaseInput(GameObject grabber)
    {
        if(grabber == inputGrabber)
        {
            inputGrabber = null;
            mover.enabled = true;
            look.enabled = true;
            interaction.enabled = true;
            return true;
        }
        return false;
    }
}
