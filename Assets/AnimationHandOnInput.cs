using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationHandOnInput : MonoBehaviour
{

    // Using InputActionProperty with another name ( Added Variable in unity )
    public InputActionProperty pinchAnimationAction;
    public InputActionProperty gripAnimationAction;
    // Using Animator with another name ( Added Variable in unity )
    public Animator handAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Tell me how many time the button pressed | Make the two hand touch
        float triggerValue = pinchAnimationAction.action.ReadValue<float>();
        // use the value to change the parameters on the animator
        handAnimator.SetFloat("Trigger", triggerValue);

        // Make the Grip Hand
        float gripValue = gripAnimationAction.action.ReadValue<float>();
        // use the value to change the parameters on the animator
        handAnimator.SetFloat("Grip", gripValue);
    }
}
