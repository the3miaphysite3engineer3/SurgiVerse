using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandData : MonoBehaviour
{
    // Show if the model is left or right
    public enum HandModelType { Left, Right };

    public HandModelType type;
    public Transform root;
    public Animator animator;
    public Transform[] fingerBones;
}
