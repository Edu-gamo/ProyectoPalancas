using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public float mass;
    public float radius = 0.15f;

    public Vector3 position;
    public Vector3 postPosition;
    //public Vector3 positionInit;

    public Vector3 velocity;
    public Vector3 postVelocity;
    //public Vector3 velocityInit;

    private float t;
    private bool quepalo = false;
    private float gravity = -9.81f;

    public float elasticity = 0.2f;

    public Transform pointLeftL;
    public Transform pointRightR;
    public Transform pointLeftLU;
    public Transform pointRightRU;
    public Transform pointLeftC;
    public Transform pointRightC;
    public Transform pointLeftCU;
    public Transform pointRightCU;

    private Vector3 pointLeftLF;
    private Vector3 pointRightRF;
    private Vector3 pointLeftCF;
    private Vector3 pointRightCF;

    private Vector3 vectorLL_RR;
    private Vector3 vectorLL_LF;

    private Vector3 vectorRC_RCF;
    private Vector3 vectorRC_RCU;

    private Vector3 normalDown;
    private Vector3 normalRC;

    private float dDown;
    private float dRightC;

    // Use this for initialization
    void Start () {

        this.transform.localScale.Set(radius, radius, radius);
        //positionInit = this.transform.position;
        position = this.transform.position;
        //velocityInit = new Vector3(0, 0, 0);
        velocity = new Vector3(0, 0, 0);
        //if (position.y == 0.1) velocity.y = -10;

    }
	
	// Update is called once per frame
	void FixedUpdate() {

        /*t += Time.deltaTime;

        //EULER
        //Calcular posicion
        postPosition.x = position.x + (t / 1000) * velocity.x;
        postPosition.y = position.y + (t / 1000) * velocity.y;
        postPosition.z = position.z + (t / 1000) * velocity.z;

        //Calcular velocidad
        postVelocity.x = velocity.x + (t / 1000) * (0.0f / mass);
        postVelocity.y = velocity.y + (t / 1000) * (gravity / mass);
        postVelocity.z = velocity.z + (t / 1000) * (0.0f / mass);



        position = postPosition;
        velocity = postVelocity;*/

        //Calcular la normal del plano de la palanca
        pointLeftLF = new Vector3(pointLeftL.position.x + 1, pointLeftL.position.y, pointLeftL.position.z);
        pointRightRF = new Vector3(pointRightR.position.x + 1, pointRightR.position.y, pointRightR.position.z);
        pointLeftCF = new Vector3(pointLeftC.position.x + 1, pointLeftC.position.y, pointLeftC.position.z);
        pointRightCF = new Vector3(pointRightC.position.x + 1, pointRightC.position.y, pointRightC.position.z);

        vectorLL_RR = pointRightR.position - pointLeftL.position;
        vectorLL_LF = pointLeftLF - pointLeftL.position;
        normalDown = Vector3.Cross(vectorLL_RR, vectorLL_LF).normalized;
        dDown = -(normalDown.x * pointRightR.position.x) - (normalDown.y * pointRightR.position.y) - (normalDown.z * pointRightR.position.z);

        vectorRC_RCF = pointRightCF - pointRightC.position;
        vectorRC_RCU = pointRightCU.position - pointRightC.position;
        normalRC = Vector3.Cross(vectorRC_RCF, vectorRC_RCU).normalized;
        dRightC = -(normalRC.x * pointRightCF.x) - (normalRC.y * pointRightCF.y) - (normalRC.z * pointRightCF.z);

        //if (position.y > this.GetComponent<SphereCollider>().radius * this.transform.localScale.y) {
        t += Time.deltaTime;

        //EULER
        //Calcular posicion
        postPosition = position + (t / 100) * velocity;
            /*postPosition.x = position.x + (t / 100) * velocity.x;
            postPosition.y = position.y + (t / 100) * velocity.y;
            postPosition.z = position.z + (t / 100) * velocity.z;*/

            //Calcular velocidad
            //postVelocity.x = velocity.x + (t / 100) * (0.0f / mass);
            postVelocity.y = velocity.y + (t / 100) * (gravity / mass);
            //postVelocity.z = velocity.z + (t / 100) * (0.0f / mass);
            //if (!quepalo) quepalo = true;
        /*} else if (quepalo) {
            //position.y = this.GetComponent<SphereCollider>().radius * this.transform.localScale.y;
            velocity = new Vector3(0.0f, 0.0f, 0.0f);
            quepalo = false;
        }*/

        //Colision con la palanca
        /*float dotProductDown = Vector3.Dot(normalDown, position);
        float dotProductPostDown = Vector3.Dot(normalDown, postPosition);

        if ((dotProductDown + dDown) * (dotProductPostDown + dDown) <= 0.15f) {

            float dotProductPostVelDown = Vector3.Dot(normalDown, postVelocity);
            float dotProductVelDown = Vector3.Dot(normalDown, velocity);
            Vector3 normalVel = dotProductVelDown * normalDown;

            postPosition = postPosition - (1 + elasticity) * (dotProductPostDown + dDown) * normalDown;
            //postVelocity = postVelocity - (1 + elasticity) * (dotProductPostVelDown) * normalDown;
            postVelocity = new Vector3(0.0f, 0.0f, 0.0f);

        }

        float dotProductRC = Vector3.Dot(normalRC, position);
        float dotProductPostRC = Vector3.Dot(normalRC, postPosition);

        if ((dotProductRC + dRightC) * (dotProductPostRC + dRightC) <= 0.15f)
        {

            float dotProductPostVelRC = Vector3.Dot(normalRC, postVelocity);
            float dotProductVelRC = Vector3.Dot(normalRC, velocity);
            Vector3 normalVel = dotProductVelRC * normalRC;

            postPosition = postPosition - (1 + elasticity) * (dotProductPostRC + dRightC) * normalRC;
            //postVelocity = postVelocity - (1 + elasticity) * (dotProductVelRC) * normalRC;
            postVelocity = new Vector3(0.0f, 0.0f, 0.0f);

        }*/

        position = postPosition;
        velocity = postVelocity;

        /*if (position.y > this.GetComponent<SphereCollider>().radius) {
            t += Time.deltaTime;
            velocity.y = velocityInit.y + gravity * t;
            position.y = positionInit.y + velocityInit.y * t + (gravity / 2) * Mathf.Pow(t, 2);
            if (!quepalo) quepalo = true;
        } else if(quepalo) {
            position.y = this.GetComponent<SphereCollider>().radius;
            quepalo = false;
        }*/

        this.transform.position = position;

    }
}
