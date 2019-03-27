using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleCrateSettingsController : MonoBehaviour {

    // this var will be set in the GUI
    public float          BreakForce     = Mathf.Infinity;
    public float          BreakTorque    = Mathf.Infinity;
    public bool           isKinematic    = false;
    public PhysicMaterial physicMaterial = null;

	// Use this for initialization
	void Start () {
        setForce(BreakForce, BreakTorque);
        setIsKinematic(isKinematic);
        setPhysicMaterial(physicMaterial);
	}

    void setForce(float BreakForce, float BreakTorque) {
        FixedJoint[] allChildrenJoints = GetComponentsInChildren<FixedJoint>();
        foreach (FixedJoint fixedJoint in allChildrenJoints) {
            fixedJoint.breakForce  = BreakForce;
            fixedJoint.breakTorque = BreakForce;
        }
    }

    void setIsKinematic(bool isKinematic) {
        Rigidbody[] allChildrenRigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rigidBody in allChildrenRigidBodies) {
            rigidBody.isKinematic = isKinematic;
        }
    }

    void setPhysicMaterial(PhysicMaterial physicMaterial) {
        BoxCollider[] allChildrenBoxColliders = GetComponentsInChildren<BoxCollider>();
        foreach (BoxCollider boxCollider in allChildrenBoxColliders) {
            boxCollider.material = physicMaterial;
        }
    }
}
