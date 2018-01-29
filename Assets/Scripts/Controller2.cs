using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller2 : MonoBehaviour
{

    public Transform[] balls;
    public Transform[] targets;

    bool withBall;

    int currentBall;
    int currentTarget;

    bool changeTarget;

    MyFABRIK myFab;
    //MyCCD myCCD;

    float delay = 1.0f;
    float maxDelay = 1.0f;

    // Use this for initialization
    void Start() {
        myFab = GetComponentInParent<MyFABRIK>();
        //myCCD = GetComponentInParent<MyCCD>();
        withBall = false;
        currentBall = 0;
        currentTarget = 0;
        myFab.target = balls[currentBall];
        //myCCD.target = balls[currentBall];
        changeTarget = false;
    }

    // Update is called once per frame
    void Update() {

        if (withBall) balls[currentBall].position = myFab.joints[myFab.joints.Length - 1].position;
        //if (withBall) balls[currentBall].position = myCCD.joints[myCCD.joints.Length - 1].position;

        if (myFab.done) {
        //if (myCCD.done) {

            myFab.done = false;
            //myCCD.done = false;
            changeTarget = true;

            if (withBall) {
                withBall = false;
                balls[currentBall].GetComponent<Rigidbody>().useGravity = true;
                balls[currentBall].GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                balls[currentBall].GetComponent<Rigidbody>().mass = 5;
                for (int i = 0; i < balls.Length; i++) {
                    if (i != currentBall) balls[i].GetComponent<Rigidbody>().mass = 1;
                }
            } else {
                withBall = true;
                balls[currentBall].GetComponent<Rigidbody>().useGravity = false;
            }

        }

        //delay -= Time.deltaTime;

        //if (delay <= 0.0f) {
            //delay = maxDelay;

            if (changeTarget) {

                changeTarget = false;

                if (withBall) {
                    myFab.target = targets[currentTarget];
                    //myCCD.target = targets[currentTarget];
            } else {
                    currentBall++;
                    if (currentBall >= balls.Length) currentBall = 0;
                    currentTarget++;
                    if (currentTarget >= targets.Length) currentTarget = 0;
                    myFab.target = balls[currentBall];
                    //myCCD.target = balls[currentBall];
            }

            }

        //}

    }

}
