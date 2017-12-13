using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IK_FABRIK : MonoBehaviour
{
    public Transform[] joints;
    public Transform target;

    private Vector3[] copy;
    private float[] distances;
    private bool done;
    public int maxIter = 1;
    private int iter = 0;

    float threshold_distance = 0.1f;

    void Start() {
        distances = new float[joints.Length - 1];
        copy = new Vector3[joints.Length];
    }

    void Update()
    {
        // Copy the joints positions to work with
        // and calculate all the distances
        //TODO1
        for(int i = 0; i < joints.Length; i++) {
            copy[i] = joints[i].position;
            if(i < joints.Length - 1) distances[i] = (joints[i+1].position - joints[i].position).magnitude;
        }

        //done = TODO2
        done = (target.position - joints[joints.Length - 1].position).magnitude < threshold_distance;
        if (!done)
        {
            float targetRootDist = Vector3.Distance(copy[0], target.position);

            // Update joint positions
            if (targetRootDist > distances.Sum())
            {
                // The target is unreachable
                //TODO3
                for(int i = 0; i < copy.Length - 1; i++) {
                    float dist = (target.position - copy[i]).magnitude;
                    float lam = distances[i] / dist;
                    copy[i + 1] = (1 - lam) * copy[i] + lam * target.position;
                }
            }
            else
            {
                // The target is reachable
                //while (TODO4)
                iter = 0;
                while (!done /*|| iter < maxIter*/)
                {
                    // STAGE 1: FORWARD REACHING
                    //TODO5
                    copy[copy.Length - 1] = target.position;
                    for (int i = copy.Length - 1; i > 0; i--) {
                        Vector3 temp = (copy[i - 1] - copy[i]).normalized;
                        temp = temp * distances[i - 1];
                        copy[i - 1] = temp + copy[i];
                    }

                    // STAGE 2: BACKWARD REACHING
                    //TODO6
                    copy[0] = joints[0].position;
                    for (int i = 0; i < copy.Length - 2; i++) {
                        Vector3 temp = (copy[i + 1] - copy[i]).normalized;
                        temp = temp * distances[i];
                        copy[i + 1] = temp + copy[i];
                    }

                    done = (target.position - copy[copy.Length - 1]).magnitude < threshold_distance;
                    iter++;

                }
                Debug.Log(iter);
            }

            // Update original joint rotations
            for (int i = 0; i <= joints.Length - 2; i++)
            {
                //TODO7
                Vector3 a = joints[i + 1].position - joints[i].position;
                Vector3 b = copy[i + 1] - copy[i];
                Vector3 axis = Vector3.Cross(a, b).normalized;
                //  float angle = Mathf.Acos(Vector3.Dot(A, B) / (A.magnitude * B.magnitude)) * Mathf.Rad2Deg;
                //joints[i].Rotate(eix, angle, Space.World);


                float cosa = Vector3.Dot(a, b) / (a.magnitude * b.magnitude);
                float sina = Vector3.Cross(a.normalized, b.normalized).magnitude;

                float angle = Mathf.Atan2(sina, cosa);

                /*joints[i].rotation = Quaternion.AngleAxis(angle, eix) * joints[i].rotation;

                joints[i].position = copy[i];*/
                Quaternion q = new Quaternion(axis.x * Mathf.Sin(angle / 2), axis.y * Mathf.Sin(angle / 2), axis.z * Mathf.Sin(angle / 2), Mathf.Cos(angle / 2));
                joints[i].position = copy[i];
                joints[i].rotation = q * joints[i].rotation;
            }          
        }
    }

}
