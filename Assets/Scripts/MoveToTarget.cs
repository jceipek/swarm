using UnityEngine;
using System.Collections;

public class MoveToTarget : MonoBehaviour
{

	public float m_movementSpeed = 10f;
	public float m_targetAttractionSpeed = 3f;
	public float m_clumpDistance = 5f;
	public float m_clumpinessDamping = 0.5f;
	public float m_avoidanceStrength = 0.01f;

	private Vector2 m_target;
	private Vector2 m_movementVector;
	private Vector2 m_velocityVector;
	private Vector2 m_avoidanceVector;
	private Collider2D[] m_surroundingAllies = new Collider2D[10];

	public void SetTarget (Vector2 target)
	{
		m_target = target;

	}

	private void Start ()
	{
		StartCoroutine (UpdateAllyAvoidance ());
	}

	private IEnumerator UpdateAllyAvoidance ()
	{
		float interval = 0.2f;
		float surroundingRadius = 1f;
		while (true) {
			int surroundingCount = Physics2D.OverlapCircleNonAlloc (transform.position, surroundingRadius, m_surroundingAllies);
			m_avoidanceVector = Vector2.zero;
			for (int i = 0; i < surroundingCount; i++) {
				Collider2D currentPotentialAlly = m_surroundingAllies [i];
				if (currentPotentialAlly.gameObject != gameObject) {
					GameObject currentAlly = currentPotentialAlly.gameObject; // TODO: Add a test to detect if actually an ally (currently just avoids everything)
					m_avoidanceVector += (Vector2)((transform.position - currentAlly.transform.position));
				}
			}
			yield return new WaitForSeconds (interval);
		}
	}

	private void Move ()
	{
		float distance = ((Vector3)m_target - transform.position).sqrMagnitude;
		Vector2 direction = (((Vector3)m_target - transform.position).normalized);
		m_velocityVector = direction * Mathf.Lerp (m_targetAttractionSpeed, 0.0f, 1.0f - Mathf.Exp (distance));
		m_movementVector += m_velocityVector * Time.fixedDeltaTime;
		m_movementVector += m_avoidanceVector * m_avoidanceStrength;
		transform.position += (Vector3)m_movementVector * m_movementSpeed;
		if (distance > m_clumpDistance) {
			m_movementVector *= m_clumpinessDamping;
		}
	}

	private void FixedUpdate ()
	{
		Move ();
	}

}