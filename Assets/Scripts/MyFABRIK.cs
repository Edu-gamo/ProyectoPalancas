using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MyFABRIK : MonoBehaviour {

    public Transform[] joints;
    public Transform target;

    private MyVector3[] jointsPositions;
    private MyVector3 targetPosition;

    private MyVector3[] copy;
    private float[] distances;
    public bool done;
    public int maxIter = 1;
    private int iter = 0;

    float threshold_distance = 0.1f;

    void Start() {
        distances = new float[joints.Length - 1];
        copy = new MyVector3[joints.Length];
        jointsPositions = new MyVector3[joints.Length];
    }

    void Update()
    {

        if (target.position.y > 1.5f)
        {

            for (int i = 0; i < joints.Length; i++)
            {
                jointsPositions[i] = new MyVector3(joints[i].position.x, joints[i].position.y, joints[i].position.z);
                //Debug.Log("INICIO joints[" + i + "]: " + joints[i].position + "\njointsPositions[" + i + "]: " + jointsPositions[i]);
            }
            targetPosition = new MyVector3(target.position.x, target.position.y, target.position.z);

            // Copy the joints positions to work with
            // and calculate all the distances
            for (int i = 0; i < copy.Length; i++)
            {
                copy[i] = jointsPositions[i];
                if (i < distances.Length) distances[i] = (jointsPositions[i + 1] - jointsPositions[i]).magnitude();
            }
            
            done = (targetPosition - jointsPositions[jointsPositions.Length - 1]).magnitude() < threshold_distance;
            if (!done)
            {
                float targetRootDist = MyVector3.Distance(copy[0], targetPosition);

                // Update joint positions
                if (targetRootDist > distances.Sum())
                {
                    // The target is unreachable
                    for (int i = 0; i < copy.Length - 1; i++)
                    {
                        float dist = (targetPosition - copy[i]).magnitude();
                        float lam = distances[i] / dist;
                        copy[i + 1] = (1 - lam) * copy[i] + lam * targetPosition;
                    }
                }
                else
                {
                    // The target is reachable
                    iter = 0;
                    while (!done /*|| iter < maxIter*/)
                    {
                        // STAGE 1: FORWARD REACHING
                        copy[copy.Length - 1] = targetPosition;
                        for (int i = copy.Length - 1; i > 0; i--)
                        {
                            MyVector3 temp = (copy[i - 1] - copy[i]).normalized();
                            temp = temp * distances[i - 1];
                            copy[i - 1] = temp + copy[i];
                        }

                        // STAGE 2: BACKWARD REACHING
                        copy[0] = jointsPositions[0];
                        for (int i = 0; i < copy.Length - 2; i++)
                        {
                            MyVector3 temp = (copy[i + 1] - copy[i]).normalized();
                            temp = temp * distances[i];
                            copy[i + 1] = temp + copy[i];
                        }

                        done = (targetPosition - copy[copy.Length - 1]).magnitude() < threshold_distance;
                        iter++;

                    }
                    //Debug.Log(iter);
                }

                // Update original joint rotations
                for (int i = 0; i <= joints.Length - 2; i++) {

                    MyVector3 a = jointsPositions[i + 1] - jointsPositions[i];

                    //Debug.Log("PREFINAL joints[" + i + "]: " + joints[i].position + "\njointsPositions[" + i + "]: " + jointsPositions[i]);

                    MyVector3 b = copy[i + 1] - copy[i];
                    MyVector3 axis = MyVector3.Cross(a, b).normalized();

                    float cosa = MyVector3.Dot(a, b) / (a.magnitude() * b.magnitude());
                    float sina = MyVector3.Cross(a.normalized(), b.normalized()).magnitude();

                    float angle = Mathf.Atan2(sina, cosa);

                    //Quaternion q = new Quaternion(axis.x * Mathf.Sin(angle / 2), axis.y * Mathf.Sin(angle / 2), axis.z * Mathf.Sin(angle / 2), Mathf.Cos(angle / 2));
                    MyQuaternion q = new MyQuaternion(Mathf.Cos(angle / 2), axis.x * Mathf.Sin(angle / 2), axis.y * Mathf.Sin(angle / 2), axis.z * Mathf.Sin(angle / 2));
                    //Debug.Log("MY_FABRIK: " + q);
                    //joints[i].rotation = q * joints[i].rotation;
                    MyQuaternion actualRot = new MyQuaternion(joints[i].rotation.w, joints[i].rotation.x, joints[i].rotation.y, joints[i].rotation.z);
                    MyQuaternion newRot = MyQuaternion.multiply(q, actualRot);

                    
                    jointsPositions[i] = copy[i];
                    joints[i].position = new Vector3(jointsPositions[i].x, jointsPositions[i].y, jointsPositions[i].z);
                    joints[i].rotation = new Quaternion(newRot.x, newRot.y, newRot.z, newRot.w);

                    //Debug.Log("FINAL joints[" + i + "]: " + joints[i].position + "\njointsPositions[" + i + "]: " + jointsPositions[i]);

                    for (int j = i; j < joints.Length; j++) { //Actualizar posiciones de los hijos despues de rotar
                        jointsPositions[j] = new MyVector3(joints[j].position.x, joints[j].position.y, joints[j].position.z);
                    }

                }
            }
        }
    }

}
