using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour {

    Quaternion start, end;

    public float angle = 90.0f;
    public float speed = 2.0f;
    public float startTime = 0.0f;

	// Use this for initialization
	void Start () {
        start = PendulumRotation(angle);
        end = PendulumRotation(-angle);
	}
	
	void FixedUpdate () {
        startTime += Time.deltaTime;
        transform.rotation = Quaternion.Lerp(start, end, (Mathf.Sin(startTime * speed + Mathf.PI / 2) + 1.0f ) / 2.0f);
	}

    void ResetTimer(){
        startTime = 0.0f;
    }

    private Quaternion PendulumRotation(float angle) {
        var pendulumRotation = transform.rotation;
        var angleZ = pendulumRotation.eulerAngles.x + angle;

        if (angle > 180) angle -= 360;
        else if (angle < -180) angle += 360;

        pendulumRotation.eulerAngles = new Vector3(angleZ, pendulumRotation.eulerAngles.y, pendulumRotation.eulerAngles.z);
        return pendulumRotation;
    }
}
