using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(Grasper))]
public class CraneGraspPlayerControl : MonoBehaviour {
	public GameObject hingePrefab;
	
	bool isReleasing;
	Grasper grasper;
	
	void Start() {
		grasper = GetComponent<Grasper>();
	}
	
	void FixedUpdate() {
		if (Input.GetButtonDown("Grasp") && grasper.IsGrasping)
		{
//			Debug.Log("Cleaning up hinges");
			grasper.ReleaseGrasp();
			isReleasing = true;
		}
	}
	
	void OnCollisionEnter(Collision collision) {
		Grasp(collision);
	}
	
	void OnCollisionStay(Collision collision) {
		Grasp(collision);
	}
	
	void Grasp(Collision collision) {
		if (isReleasing)
		{
			isReleasing = false;
			return;
		}
			
		if (Input.GetButtonDown("Grasp") && !grasper.IsGrasping) {
			var validContacts = collision.contacts.Where(c => 
            	c.otherCollider.tag != "crane"
			    && c.otherCollider.rigidbody != null
			    && !c.otherCollider.rigidbody.isKinematic).Select(c => c.otherCollider.gameObject);
			
//			Debug.Log(string.Format("contacts: {0} validcontacts: {1}", collision.contacts.Count(), validContacts.Count()));
			grasper.Grasp(validContacts);
		}
	}
}
