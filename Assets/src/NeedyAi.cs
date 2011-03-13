using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Grasper))]
public class NeedyAi : MonoBehaviour {
	public float advanceSpeed = 3.0f;
	public float retreatSpeed = 2.0f;
	public float deathOnForce = 10.0f;
	public float gripLossDuration = 3.0f;
	public Material deathMaterial;
	public AudioClip deathSound;
	public AudioClip hitSound;
	
	bool isDead = false;
	bool isHoldingFood = false;
	bool isLooseGrip = false;
	bool isGrounded = false;
	float originalDrag;
	Grasper grasper;
	
	void Start() {
		grasper = GetComponent<Grasper>();
		originalDrag = rigidbody.drag;
	}
	
	void FixedUpdate() {
		if (isDead) return;
		if (isLooseGrip) return;
		
		if (isGrounded) {
			if (grasper.IsGrasping) {
				rigidbody.AddForce(-retreatSpeed * Time.fixedDeltaTime, 0.0f, 0.0f,ForceMode.Impulse);
			}
			else {
				rigidbody.AddForce(advanceSpeed * Time.fixedDeltaTime, 0.0f, 0.0f, ForceMode.Impulse);
			}
		}
		
		rigidbody.drag = isGrounded ? originalDrag : 0.0f;
		
		isGrounded = false;
		
		
		if (!grasper.IsGrasping && isHoldingFood) {
			LoseGrip();
		}
	}
	
	void LoseGrip() {
		isHoldingFood = false;
		StartCoroutine(LosenGrip());
	}
	
	IEnumerator LosenGrip() {
		isLooseGrip = true;
		yield return new WaitForSeconds(gripLossDuration);
		isLooseGrip = false;
	}
	
	void OnCollisionStay(Collision collision) {
		if (collision.gameObject.CompareTag("ground"))
		{
			isGrounded = true;
		}
	}
	
	void OnCollisionEnter(Collision collision) {
		if (isDead) return;
		
		if (collision.impactForceSum.magnitude > deathOnForce) {
			Die();
		}
		else AudioSource.PlayClipAtPoint(hitSound, transform.position);
		
		if (collision.gameObject.tag == "food" && !grasper.IsGrasping && !isLooseGrip) {
			grasper.Grasp(collision.gameObject);
			isHoldingFood = true;
		}
	}
	
	public void Die() {
		isDead = true;
		renderer.material = deathMaterial;
		AudioSource.PlayClipAtPoint(deathSound, transform.position);
		GreedManager.PersonKilled();
	}
}
