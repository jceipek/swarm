using UnityEngine;
using System.Collections;

public class DieOnBurn : MonoBehaviour
{

	public float m_deathChance = 0.3f;

	Rigidbody2D m_rigidbody2D;

	void OnEnable ()
	{
		m_rigidbody2D = GetComponent<Rigidbody2D> ();
	}

	// XXX: There is a bug in Unity that disables triggers when the Rigidbody2D isKinematic.
	// When this is fixed, turn on isKinematic
	void OnTriggerStay2D (Collider2D other)
	{
		Debug.Log ("Burn");
		if (Random.value < m_deathChance) {
			TargetController targetController = GetComponent<TargetController> ();
			MoveToTarget moveToTarget = GetComponent<MoveToTarget> ();
			targetController.enabled = false;
			moveToTarget.enabled = false;
			m_rigidbody2D.gravityScale = 1f;
		}
	}	
}