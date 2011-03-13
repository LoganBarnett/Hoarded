using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Grasper : MonoBehaviour {
	List<GameObject> hinges = new List<GameObject>();
	public AudioClip graspSound;
	
	public bool IsGrasping
	{
		get { return hinges.Count > 0; }
	}
	
	public void Grasp(GameObject contact) {
		Debug.Log(contact.name);
		if (contact.rigidbody == null) return;
		if (hinges.Exists(h => h == contact.gameObject)) return;
		
		// destroy hinges previously existing, or sadness occurs
		var oldHinge = contact.GetComponent<HingeJoint>();
		if (oldHinge != null) GameObject.Destroy(oldHinge);
		contact.AddComponent<HingeJoint>();
		contact.hingeJoint.connectedBody = rigidbody;
//		hingeClone.hingeJoint.connectedBody = contact.otherCollider.rigidbody;
		hinges.Add(contact.gameObject);
		
		AudioSource.PlayClipAtPoint(graspSound, transform.position);
	}

	public void Grasp(IEnumerable<GameObject> touchingObjects) {
		foreach (var contact in touchingObjects)
		{
			Grasp(contact);
		}
	}
	
	public void ReleaseGrasp() {
		foreach (var hinge in hinges)
		{
//			Debug.Log("Destroying hinge");
			GameObject.Destroy(hinge.GetComponent<HingeJoint>());
		}
		hinges.Clear();
	}
	
	void FixedUpdate() {
		var destroyedHinges = hinges.Where(h => h == null).ToList();
		foreach (var hinge in destroyedHinges)
		{
			hinges.Remove(hinge);
		}
		var brokenHinges = hinges.Where(h => h.hingeJoint.connectedBody == null || h.hingeJoint.connectedBody != gameObject.rigidbody).ToList();
		foreach (var hinge in brokenHinges)
		{
			GameObject.Destroy(hinge.hingeJoint);
			hinges.Remove(hinge);
		}
	}
}
