using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycaster : MonoBehaviour {

	public Layer[] layerPriorities = {
		Layer.Enemy,
		Layer.Walkable,
	};

	[SerializeField] private float distanceToBackGround = 100f;
	Camera ViewCamera;

	private RaycastHit m_hit;
	public RaycastHit Hit {
		get {return m_hit;}
	}

	private Layer m_layerHit;
	public Layer layerHit{
		get{return m_layerHit;}
	}

	// Use this for initialization
	void Start () {
		ViewCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update ()
	{
		foreach (Layer layer in layerPriorities) {
			var hit = RayCastForLayer (layer);
			if (hit.HasValue) {
				m_hit = hit.Value;
				m_layerHit = layer;
				return;
			}
		}

		m_hit.distance = distanceToBackGround;
		m_layerHit = Layer.RayEndStop;
	}

	RaycastHit? RayCastForLayer (Layer layer)
	{
		int layermask = 1 << (int)layer;
		Ray ray = ViewCamera.ScreenPointToRay (Input.mousePosition);

		RaycastHit hit;
		bool hasHit = Physics.Raycast (ray, out hit, distanceToBackGround, layermask);

		if (hasHit) {
			return hit;
		}
		return null;
	}
}
