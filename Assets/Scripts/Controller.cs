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

    MyFABRIK myFab;

    // Use this for initialization
    void Start () {

        myFab = GetComponentInParent<MyFABRIK>();
        withBall = false;
        currentBall = 0;
        currentTarget = 0;
        myFab.target = balls[currentBall];
        changeTarget = false;

    }
	
	// Update is called once per frame
	void Update () {

        if (myFab.done) {

            changeTarget = true;

            if (withBall) {
                withBall = false;
                balls[currentBall].GetComponent<Rigidbody>().useGravity = true;
            } else {
                withBall = true;
                balls[currentBall].GetComponent<Rigidbody>().useGravity = false;
            }

        }

        if (withBall) balls[currentBall].position = myFab.joints[myFab.joints.Length - 1].position;

        if (changeTarget) {

            if (withBall) {
                myFab.target = targets[currentTarget];
            } else {
                currentBall++;
                if (currentBall >= balls.Length) currentBall = 0;
                currentTarget++;
                if (currentTarget >= targets.Length) currentTarget = 0;
                myFab.target = balls[currentBall];
            }

        }























        /*if (myFab.done) {
            changeTarget = true;
            if (withBall) {
                withBall = false;
                currentBall++;
                if (currentBall >= balls.Length) currentBall = 0;
                balls[currentBall].GetComponent<Rigidbody>().useGravity = true;
                currentTarget++;
                if (currentTarget >= targets.Length) currentTarget = 0;
            } else {
                withBall = true;
                balls[currentBall].GetComponent<Rigidbody>().useGravity = false;
                balls[currentBall].GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                
                //currentBall++;
                //if (currentBall >= balls.Length) currentBall = 0;
            }
        }

        if (changeTarget) {
            changeTarget = false;
            myFab.done = false;
            if (withBall) { //Go to target
                myFab.target = targets[currentTarget];
            } else { //Go to ball
                myFab.target = balls[currentBall];
            }
        }*/

    }
}
