using UnityEngine;
using System.Collections;

[RequireComponent (typeof(MoveToTarget))]
public class TargetController : MonoBehaviour
{

	private MoveToTarget m_moveToTarget;
	private float m_cameraOffset;

	void OnEnable ()
	{
		m_moveToTarget = GetComponent<MoveToTarget> ();
		m_cameraOffset = transform.position.z - Camera.main.transform.position.z;
	}

	
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButtonDown ("Click")) {


			var pos = Input.mousePosition;
			pos.z = m_cameraOffset;
			/*transform.position = Camera.main.ScreenToWorldPoint (pos);*/
			m_moveToTarget.SetTarget (Camera.main.ScreenToWorldPoint (pos));

		}
	}
}
