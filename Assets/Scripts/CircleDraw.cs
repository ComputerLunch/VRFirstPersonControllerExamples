using UnityEngine;
using System.Collections; 

public class CircleDraw : MonoBehaviour {   
	public float theta_scale = 0.01f;        //Set lower to add more points
	int size; //Total number of points in circle

	LineRenderer lineRenderer;

	public float radius = 3f;
	public float lineStartWidth = 1.0f;
	public float lineEndWidth = 1.0f;
	public float alpha = 1.0f; // Seems to be full

	const float COMPLETE_CIRCLE_ARC = 0.64f;

	public Color color;

	int numSegments = 180;

	void Awake () {       
		   
		lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
		lineRenderer.material.color = color;
	}

	void BuildArcMesh(){

		float sizeValue = ( Mathf.PI) / theta_scale; 
		size = (int)sizeValue;
		size++;

		numSegments = (int)(size * alpha * COMPLETE_CIRCLE_ARC);

		lineRenderer.SetWidth(lineStartWidth,lineEndWidth); //thickness of line
		lineRenderer.SetVertexCount(numSegments); 
		lineRenderer.useWorldSpace = false;
	}

	void FixedUpdate () {   

		BuildArcMesh();

		Vector3 pos;
		float theta = 0f;
		for(int i = 0; i < numSegments ; i++){          
			theta += ( Mathf.PI * theta_scale);         
			float x = radius * Mathf.Cos(theta);
			float y = radius * Mathf.Sin(theta);          
			x += gameObject.transform.position.x;
			y += gameObject.transform.position.y;
			pos = new Vector3(x, y, 0);
			lineRenderer.SetPosition(i, pos);
		}
	}
}