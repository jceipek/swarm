using UnityEngine;
using System.Collections;

public class MoveToTarget : MonoBehaviour
{

	public float m_movementSpeed;
	private Vector2 m_target;

	// Use this for initialization
	void Start ()
	{
	
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		transform.position += ((Vector3)m_target - transform.position) * m_movementSpeed * Time.fixedDeltaTime;
	}
	
	public void SetTarget (Vector2 target)
	{
		m_target = target;
	}
}
