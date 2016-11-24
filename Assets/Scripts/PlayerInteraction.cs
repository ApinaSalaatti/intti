using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private Camera eyes;

    [SerializeField]
    private Image crosshair;

    [SerializeField]
    private Sprite defaultSprite;
    [SerializeField]
    private Sprite useSprite;
    [SerializeField]
    private Sprite eyeSprite;

    [SerializeField]
    private Text interactionDescriptionText;

    private Interaction currentInteraction;
    private float interactionDistance = 2f;

    private Interaction itemInHand;

    // Use this for initialization
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnEnable()
    {
        crosshair.enabled = true;
    }
    void OnDisable()
    {
        if (crosshair != null) // Is null when shutting the game down
            crosshair.enabled = false;
        if (interactionDescriptionText != null)
            interactionDescriptionText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("LOL");
        }

        CheckForInteraction();
    }

    private void CheckForInteraction()
    {
        currentInteraction = null;
        RaycastHit hit;
        Vector3 pos = eyes.transform.position;
        Vector3 dir = eyes.transform.TransformDirection(Vector3.forward);
        bool interactionFound = false;
        int mask = LayerMask.GetMask("Default");
        if (Physics.Raycast(pos, dir, out hit, interactionDistance, mask))
        {
            Interaction intr = hit.collider.gameObject.GetComponent<Interaction>();
            if (intr != null)
            {
                currentInteraction = intr;
                interactionFound = true;
            }
        }

        if (interactionFound)
        {
            //crosshair.color = Color.green;
            switch (currentInteraction.Type)
            {
                case InteractionType.LOOKAT:
                    crosshair.sprite = eyeSprite;
                    break;
                default:
                    crosshair.sprite = useSprite;
                    break;
            }

            interactionDescriptionText.text = currentInteraction.Description(itemInHand);
        }
        else
        {
            interactionDescriptionText.text = "";
            //crosshair.color = Color.white;
            crosshair.sprite = defaultSprite;
        }

        if (Input.GetButtonDown("Use") && currentInteraction != null)
        {
            currentInteraction.OnInteraction(itemInHand);
        }

        if (itemInHand != null)
        {
            Vector3 targetPos = Camera.main.transform.position + Camera.main.transform.TransformDirection(Vector3.forward);
            targetPos.y -= 0.5f;
            itemInHand.transform.position = Vector3.Lerp(itemInHand.transform.position, targetPos, Time.deltaTime * 10f);

            /*
            Vector3 objRot = itemInHand.transform.rotation.eulerAngles;
            objRot.y += xChange;

            float xInput = Input.GetAxisRaw("Horizontal");
            float yInput = Input.GetAxisRaw("Vertical");
            objRot.y += xInput;
            objRot.x += yInput;

            itemInHand.transform.rotation = Quaternion.Euler(objRot);*/

            if (Input.GetButtonDown("Pickup"))
            {
                ReleaseCarriedObject();
            }
        }
        else
        {
            if (Input.GetButtonDown("Pickup") && currentInteraction != null && currentInteraction.CanBePickedUp)
            {
                PickupObject(currentInteraction);
            }
        }
    }

    private void PickupObject(Interaction obj)
    {
        Debug.Log("Picking up " + obj.name);
        itemInHand = obj;
        obj.GetComponent<Rigidbody>().isKinematic = true;
        obj.GetComponent<Collider>().enabled = false;
        itemInHand.SendMessage("OnPickup", SendMessageOptions.DontRequireReceiver);
    }
    // This will sometimes be called from the carried object
    public void ReleaseCarriedObject()
    {
        Debug.Log("RELEASEING " + itemInHand.name);
        itemInHand.GetComponent<Rigidbody>().isKinematic = false;
        itemInHand.GetComponent<Collider>().enabled = true;
        itemInHand.SendMessage("OnRelease", SendMessageOptions.DontRequireReceiver);
        itemInHand = null;
    }
}
