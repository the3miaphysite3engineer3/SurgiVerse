using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using Unity.VisualScripting;

public class SliceObject : MonoBehaviour
{

    public Transform startSlicePoint;
    public Transform endSlicePoint;
    public VelocityEstimator velocityEstimator;
    public LayerMask sliceableLayer;
    public float cutForce;
    public bool onClick;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit hit, sliceableLayer);
        if (hasHit)
        {
            GameObject target = hit.transform.gameObject;
            print(target);
            if (onClick)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Slice(target);
                }
            }
            else
            {
                Slice(target);
            }
        }
    }

    public void Slice(GameObject target)
    {
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
        planeNormal.Normalize(); 

        SlicedHull hull = target.Slice(endSlicePoint.position, planeNormal);

        if (hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(target);
            SetupSlicedComponent(upperHull);
            GameObject lowerHull = hull.CreateLowerHull(target);
            SetupSlicedComponent(lowerHull);
            Destroy(target);
        }

    }

    public void SetupSlicedComponent(GameObject slicedObject)
    {
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        collider.convex = true;
        rb.AddExplosionForce(cutForce, slicedObject.transform.position, 1);
    }
}
