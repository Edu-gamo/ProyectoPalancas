using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public Transform[] balls;
    public Transform[] targets;
    public Transform targetExtra; //Donde va el brazo cuando no tiene nada que hacer

    bool withBall;

    int currentBall;
    int currentTarget;

    bool changeTarget;

    //MyFABRIK myFab;
    MyCCD myCCD;

    float delay = 1.0f;
    float maxDelay = 1.0f;

    public Transform eje;
    public Transform pointRightR;
    public Transform pointLeftL;

    private float fg, fgy, bp, br, anguloBal, mass, sumFuerzas, inercia, mAngular, anguloRotacion;

    private float tiempoImpacto, fuerzaImpacto;

    float torque, accelAngular;

    float sumFuerzasRight, sumFuerzasLeft;

    public Transform palanca;

    // Use this for initialization
    void Start () {

        //anguloBal = Mathf.Acos(anguloBalancin()) * Mathf.Rad2Deg;

        //myFab = GetComponentInParent<MyFABRIK>();
        myCCD = GetComponentInParent<MyCCD>();
        withBall = false;
        currentBall = 0;
        currentTarget = 0;
        //myFab.target = balls[currentBall];
        myCCD.target = balls[currentBall];
        changeTarget = false;

        tiempoImpacto = 1.5f;
    }
	
	// Update is called once per frame
	void Update () {

        /*anguloBal = anguloBalancin();

        anguloRotacion = calculoImpacto();
        Debug.Log(anguloRotacion);*/

        if (withBall) balls[currentBall].position = myCCD.joints[myCCD.joints.Length - 1].position;
        //if (withBall) balls[currentBall].GetComponent<Ball>().position = myFab.joints[myFab.joints.Length - 1].position;
        //if (withBall) balls[currentBall].GetComponent<Ball>().position = myCCD.joints[myCCD.joints.Length - 1].position;
        //if (myFab.done) {
        if (myCCD.done) {

            //myFab.done = false;
            myCCD.done = false;
            changeTarget = true;

            if (withBall) {
                withBall = false;
                balls[currentBall].GetComponent<Rigidbody>().useGravity = true;
                balls[currentBall].GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                //balls[currentBall].GetComponent<Ball>().velocity = new Vector3(0, 0, 0);
                balls[currentBall].GetComponent<Rigidbody>().mass = 5;
                //balls[currentBall].GetComponent<Ball>().mass = 0.5f;
                for (int i = 0; i < balls.Length; i++) {
                    if (i != currentBall) balls[i].GetComponent<Rigidbody>().mass = 1;
                    //if (i != currentBall) balls[i].GetComponent<Ball>().mass = 2f;
                }
            } else {
                withBall = true;
                balls[currentBall].GetComponent<Rigidbody>().useGravity = false;
            }

        }

        delay -= Time.deltaTime;
        //Debug.Log(delay);
        if (delay <= 0.0f) {
            delay = maxDelay;

            if (changeTarget) {

                changeTarget = false;

                if (withBall) {
                    //myFab.target = targets[currentTarget];
                    myCCD.target = targets[currentTarget];
                } else {
                        currentBall++;
                        if (currentBall >= balls.Length) currentBall = 0;
                        currentTarget++;
                        if (currentTarget >= targets.Length) currentTarget = 0;
                        //myFab.target = balls[currentBall];
                        myCCD.target = balls[currentBall];
                    }

                }
        }

    }

    //float anguloBalancin() {
    //    Vector3 punto = new Vector3(eje.position.x, eje.position.y, pointRightR.position.z);
    //    Vector3 ejeRightR = new Vector3(pointRightR.position.x - eje.position.x, pointRightR.position.y - eje.position.y, pointRightR.position.z - eje.position.z);
    //    Vector3 ejePunto = new Vector3(punto.x - eje.position.x, punto.y - eje.position.y, punto.z - eje.position.z);
    //    /*Debug.DrawLine(eje.position, pointRightR.position, Color.red, 1);
    //    Debug.DrawLine(eje.position, new Vector3(punto.x, punto.y, punto.z), Color.blue, 1);
    //    Debug.DrawLine(eje.position, new Vector3(eje.position.x, eje.position.y + 1, eje.position.z), Color.green, 1);
    //    Debug.DrawLine(eje.position, new Vector3(eje.position.x, eje.position.y + 1, eje.position.z + 1), Color.yellow, 1);*/
    //    float angle = Mathf.Acos(Vector3.Dot(ejeRightR, ejePunto) / (ejeRightR.magnitude * ejePunto.magnitude)) * Mathf.Rad2Deg;
    //    //if (Vector3.Cross(ejeRightR, ejePunto).magnitude / (ejeRightR.magnitude * ejePunto.magnitude) < 0) angle = -angle;
    //    if (pointRightR.position.y < eje.position.y) angle = -angle;
    //    return angle;
    //}

    //float calculoImpacto() {
    //    for (int i = 0; i < balls.Length; i++) {
    //        balls[i].GetComponent<Ball>().fgy = balls[i].GetComponent<Ball>().mass * (-balls[i].GetComponent<Ball>().gravity * Mathf.Cos(anguloBal));
    //    }
    //    /*switch (currentBall) {
    //        case 0:
    //            bp = (pointLeftL.position - eje.position).magnitude;
    //            br = (pointRightR.position - eje.position).magnitude;
    //            sumFuerzas = ((balls[0].GetComponent<Ball>().fgy * br) - (balls[1].GetComponent<Ball>().fgy * bp)) * Mathf.Sin(anguloBal);
    //            break;
    //        case 1:
    //            bp = (pointRightR.position - eje.position).magnitude;
    //            br = (pointLeftL.position - eje.position).magnitude;

    //            sumFuerzasLeft = -balls[1].GetComponent<Ball>().fgy * br * Mathf.Sin(anguloBal);
    //            sumFuerzasRight = balls[0].GetComponent<Ball>().fgy * bp * Mathf.Sin(anguloBal);

    //            sumFuerzas = ((balls[1].GetComponent<Ball>().fgy * br) - (balls[0].GetComponent<Ball>().fgy * bp)) * Mathf.Sin(anguloBal);
    //            break;
    //    }*/

    //    bp = (pointRightR.position - eje.position).magnitude;
    //    br = (pointLeftL.position - eje.position).magnitude;

    //    fuerzaImpacto = balls[0].GetComponent<Ball>().mass * (balls[0].GetComponent<Ball>().velocity.y / tiempoImpacto);

    //    sumFuerzas = (fuerzaImpacto - balls[1].GetComponent<Ball>().fgy * br) * Mathf.Sin(anguloBal);
    //    //Debug.Log(fuerzaImpacto);

    //    torque = bp * sumFuerzas * Mathf.Sin(90 + anguloBal);
    //    //torque = bp * 27.8f * Mathf.Sin(90 + anguloBal);
    //    inercia = 0.5f * 0.5f * Mathf.Pow(bp, 2);
    //    accelAngular = torque / inercia;
    //    anguloRotacion = anguloBal + 0.5f * accelAngular * Mathf.Pow(tiempoImpacto, 2);
    //    return anguloRotacion;
    //}

}
