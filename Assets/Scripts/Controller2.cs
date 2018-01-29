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

    List<Vector3> path = new List<Vector3>();

    // Use this for initialization
    void Start() {
        myFab = GetComponentInParent<MyFABRIK>();
        //myCCD = GetComponentInParent<MyCCD>();
        withBall = false;
        currentBall = 0;
        currentTarget = 0;
        myFab.target = balls[currentBall];
        /*myCCD.target = balls[currentBall];
        myCCD.tpos = balls[currentBall].position;*/
        changeTarget = false;

        /*path = makePath(balls[currentBall].position);
        myCCD.target = path[0];
        myCCD.tpos = path[0];*/
    }

    // Update is called once per frame
    void Update() {

        if (withBall) balls[currentBall].position = myFab.joints[myFab.joints.Length - 1].position;
        //if (withBall) balls[currentBall].position = myCCD.joints[myCCD.joints.Length - 1].position;

        if (myFab.done) {
        //if (myCCD.done /*&& path.Count == 0*/) {

            //path.Clear();

            myFab.done = false;
            //myCCD.done = false;
            changeTarget = true;

            if (withBall) {
                withBall = false;
                balls[currentBall].GetComponent<Rigidbody>().useGravity = true;
                balls[currentBall].GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                balls[currentBall].GetComponent<Rigidbody>().mass = 5;
                for (int i = 0; i < balls.Length; i++) {
                    if (i != currentBall) balls[i].GetComponent<Rigidbody>().mass = 0.5f;
                }
            } else {
                withBall = true;
                balls[currentBall].GetComponent<Rigidbody>().useGravity = false;
            }

        } /*else if(myCCD.done) {
            path.RemoveAt(0);
            myCCD.done = false;
            myCCD.target = path[0];
            myCCD.tpos = path[0];
        }*/

        //delay -= Time.deltaTime;

        //if (delay <= 0.0f) {
            //delay = maxDelay;

        if (changeTarget) {

            changeTarget = false;

            if (withBall) {
                myFab.target = targets[currentTarget];
                /*myCCD.target = targets[currentTarget];
                myCCD.tpos = targets[currentTarget].position;*/
                /*path = makePath(targets[currentTarget].position);
                myCCD.target = path[0];
                myCCD.tpos = path[0];*/
            } else {
                currentBall++;
                if (currentBall >= balls.Length) currentBall = 0;
                currentTarget++;
                if (currentTarget >= targets.Length) currentTarget = 0;
                myFab.target = balls[currentBall];
                /*myCCD.target = balls[currentBall];
                myCCD.tpos = balls[currentBall].position;*/
                /*path = makePath(balls[currentBall].position);
                myCCD.target = path[0];
                myCCD.tpos = path[0];*/
            }

        }

        //}

    }

    /*List<Vector3> makePath(Vector3 target) {

        List<Vector3> p = new List<Vector3>();

        Vector3 toTarget = target - myCCD.joints[myCCD.joints.Length - 1].position;

        for (int i = 1; i < toTarget.magnitude; i++) {
            p.Add(myCCD.joints[myCCD.joints.Length - 1].position + (toTarget.normalized * i));
        }

        p.Add(target);

        return p;
    }*/

}
