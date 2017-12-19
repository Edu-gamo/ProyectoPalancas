using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public float mass;

    public Vector3 position;
    public Vector3 postPosition;
    public Vector3 positionInit;

    public Vector3 velocity;
    public Vector3 postVelocity;
    public Vector3 velocityInit;

    /*private float t;
    private bool quepalo = false;
    private float gravity = -9.81f;*/

    // Use this for initialization
    void Start () {

        positionInit = this.transform.position;
        position = this.transform.position;
        velocityInit = new Vector3(0, 0, 0);
        velocity = new Vector3(0, 0, 0);
        //if (position.y == 0.1) velocity.y = -10;

    }
	
	// Update is called once per frame
	void FixedUpdate() {

        /*if (position.y > this.GetComponent<SphereCollider>().radius/2) {
            t += Time.deltaTime;
            velocity.y = velocityInit.y + gravity * t;
            position.y = positionInit.y + velocityInit.y * t + (gravity / 2) * Mathf.Pow(t, 2);
            if (!quepalo) quepalo = true;
        } else if(quepalo) {
            position.y = 0.1f;
            quepalo = false;
        }*/
        this.transform.position = position;

    }
}
