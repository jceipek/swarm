using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwarmInputManager : MonoBehaviour
{
	public static SwarmInputManager g;
	public float m_selectionRadius = 5.0f;
	private HashSet<MoveToTarget> allSwarmComponents = new HashSet<MoveToTarget> ();
	private HashSet<MoveToTarget> selectedSwarmComponents = new HashSet<MoveToTarget> ();
	private float m_cameraOffset;

	private void Awake ()
	{
		g = this;
	}

	private void OnEnable ()
	{
		m_cameraOffset = transform.position.z - Camera.main.transform.position.z;
	}

	private void Start ()
	{
		MoveToTarget[] swarmComponents = FindObjectsOfType<MoveToTarget> ();
		foreach (var swarmComponent in swarmComponents) {
			allSwarmComponents.Add (swarmComponent);
		}
	}

	private void Update ()
	{
		if (Input.GetButtonDown ("SetDest")) {
			var pos = Input.mousePosition;
			pos.z = m_cameraOffset;
			foreach (var swarmComponent in selectedSwarmComponents) {
				swarmComponent.SetTarget (Camera.main.ScreenToWorldPoint (pos));
			}
		}

		if (Input.GetButtonDown ("Select")) {
			DeselectAllSwarmComponents ();
		}

		if (Input.GetButton ("Select")) {
			var pos = Input.mousePosition;
			pos.z = m_cameraOffset;
			SelectAtPos (Camera.main.ScreenToWorldPoint (pos));
		}
	}

	private void SelectSwarmComponent (MoveToTarget swarmComponent)
	{
		selectedSwarmComponents.Add (swarmComponent);
		swarmComponent.EnableControl ();
	}

	private void DeselectSwarmComponent (MoveToTarget swarmComponent)
	{
		selectedSwarmComponents.Remove (swarmComponent);
		swarmComponent.DisableControl ();
	}

	private void DeselectAllSwarmComponents ()
	{
		foreach (var swarmComponent in allSwarmComponents) {
			DeselectSwarmComponent (swarmComponent);
		}
	}

	private void SelectAtPos (Vector2 pos)
	{
		foreach (var swarmComponent in allSwarmComponents) {
			if ((((Vector2)swarmComponent.transform.position) - pos).magnitude < m_selectionRadius) {
				SelectSwarmComponent (swarmComponent);
			}
		}
	}
}