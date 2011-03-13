using UnityEngine;

public class FoodCapture : MonoBehaviour {
	
	void OnTriggerEnter(Collider other) {
		if (other.tag == "food")
		{
			GreedManager.TakeFood();
			if (other.hingeJoint != null)
			{
				var needyPerson = other.hingeJoint.connectedBody.gameObject;
				Debug.Log(needyPerson.tag);
				if (needyPerson.tag != "crane") GameObject.Destroy(needyPerson);
				
			}
			GameObject.Destroy(other.gameObject);
			
		}
	}
}
