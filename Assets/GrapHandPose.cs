using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// Trigger the pose when grabbing this object
public class GrapHandPose : MonoBehaviour
{

    public HandData rightHandPose;
    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(SetupPose);

        //Disactivate this grab and pose at the start of the game
        rightHandPose.gameObject.SetActive(false);
    }

    public void SetupPose(BaseInteractionEventArgs arg)
    {
        if (arg.interactorObject is XRDirectInteractor)
        {
            HandData handData = arg.interactorObject.transform.GetComponentInChildren<HandData>();
            // Disable the animator of the right ( Freeze right model when it's grabbing )
            handData.animator.enabled = false;
        }
    }

}
