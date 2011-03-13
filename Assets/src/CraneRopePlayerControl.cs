using UnityEngine;
using System.Collections;

public class CraneRopePlayerControl : MonoBehaviour {
	public GameObject ropeSegmentsContainer;
	public float extensionRate = 1.0f;
	
	void FixedUpdate () {
		var input = Input.GetAxis("Vertical");
		ropeSegmentsContainer.audio.volume = Mathf.Abs(input);
		var scale = ropeSegmentsContainer.transform.localScale;
		var newScale = new Vector3(scale.x, scale.y + (input * extensionRate * Time.deltaTime), scale.z);
		ropeSegmentsContainer.transform.localScale = newScale;
//		foreach (Transform child in ropeSegmentsContainer.transform)
//		{
//			child.hingeJoint.anchor = new Vector3(0.0f, newScale.y, 0.0f);
//		}
	}
}
