using UnityEngine;

public class PlatformConstrained : MonoBehaviour {
	void Update() {
		transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
	}
}
