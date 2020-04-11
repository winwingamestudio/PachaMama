using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGLooper : MonoBehaviour {

	// Scroll speed
	public float scrollSpeed = 0.1f;
	public float TimeForScroll = 0;
	public bool scrollingEnable;


	// Offset of Texture
	private Vector2 offset = Vector2.zero;

	// We will need the material to get offset
	private Material material;


	// Use this for initialization
	void Start () {

		material = GetComponent<Renderer>().material;

		// get starting texture offset value;
		offset = material.GetTextureOffset("_MainTex");
		scrollingEnable = true;

	}

	// Update is called once per frame
	void Update () {
		if (scrollingEnable)
	{
	TimeForScroll += Time.deltaTime;
	}
	offset = new Vector2(TimeForScroll  * scrollSpeed, 0);
  GetComponent<Renderer>().material.mainTextureOffset = offset;
		// increment new offset value;
	//	offset.x += scrollSpeed * Time.deltaTime;

		// set offset
	//	material.SetTextureOffset("_MainTex", offset);

	}
}
