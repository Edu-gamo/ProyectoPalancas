using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCCD : MonoBehaviour {

	public Transform[] joints;
	public Transform target;

    private Vector3[] jointsPositions;

    public float[] theta;
    
	[SerializeField]
	private float[] sin;
	[SerializeField]
	private float[] cos; 

	public bool done = false;
	private Vector3 tpos;
    
	[SerializeField]
	private int Mtries = 10;
	[SerializeField]
	private int tries = 0;
	
	private float epsilon = 0.1f;

    [Range(0.0f, 180.0f)]
    public float maxAngle;

    [Range(0.0f, 180.0f)]
    public float minAngle;

    void Start () {
		theta = new float[joints.Length];
		sin = new float[joints.Length];
		cos = new float[joints.Length];
		tpos = target.position;
        jointsPositions = new Vector3[joints.Length];

        for (int i = 0; i < joints.Length; i++) {
            jointsPositions[i] = new Vector3(joints[i].position.x, joints[i].position.y, joints[i].position.z);
        }
    }
	
	void Update () {

        if (target != null && target.position.y > 1.25f) {

            if (!done) {

                if (tries <= Mtries) {

                    for (int i = jointsPositions.Length - 2; i >= 0; i--) {

                        Vector3 r1 = jointsPositions[jointsPositions.Length - 1] - jointsPositions[i];
                        Vector3 r2 = tpos - jointsPositions[i];

                        cos[i] = Vector3.Dot(r1, r2) / (r1.magnitude * r2.magnitude);
                        sin[i] = Vector3.Cross(r1, r2).magnitude / (r1.magnitude * r2.magnitude);

                        Vector3 axis = Vector3.Cross(r1, r2).normalized;

                        theta[i] = Mathf.Acos(cos[i]);

                        if (sin[i] < 0) theta[i] = -theta[i];
                        theta[i] *= Mathf.Rad2Deg;

                        joints[i].rotation = Quaternion.AngleAxis(theta[i], axis) * joints[i].rotation;

                        //CONSTRAINTS
                        if (i > 0) {
                            Vector3 ToParent = (joints[i - 1].position - joints[i].position).normalized;
                            Vector3 ToChild = (joints[i + 1].position - joints[i].position).normalized;
                            axis = Vector3.Cross(ToParent, ToChild).normalized;

                            float angle = Mathf.Acos(Vector3.Dot(ToParent, ToChild) / (ToParent.magnitude * ToChild.magnitude)) * Mathf.Rad2Deg;

                            if (angle > maxAngle || angle < ((i == 1) ? minAngle * 4 : minAngle)) {
                                angle = Mathf.Clamp(angle, ((i == 1) ? minAngle * 4 : minAngle), maxAngle);

                                Quaternion qrot = Quaternion.AngleAxis(angle, axis);
                                joints[i].rotation = joints[i - 1].rotation;
                                joints[i].Rotate(axis, 180 + angle, Space.World);
                            }
                        } else if(i == 0) {
                            Vector3 ToParent = Vector3.down;
                            Vector3 ToChild = (joints[i + 1].position - joints[i].position).normalized;
                            axis = Vector3.Cross(ToParent, ToChild).normalized;

                            float angle = Mathf.Acos(Vector3.Dot(ToParent, ToChild) / (ToParent.magnitude * ToChild.magnitude)) * Mathf.Rad2Deg;

                            if (angle > maxAngle + maxAngle / 4 || angle < maxAngle - maxAngle / 4) {
                                angle = Mathf.Clamp(angle, maxAngle - maxAngle / 4, maxAngle + maxAngle / 4);

                                Quaternion qrot = Quaternion.AngleAxis(angle, axis);
                                //joints[i].rotation = joints[i - 1].rotation;
                                joints[i].rotation = Quaternion.identity;
                                joints[i].Rotate(axis, 180 + angle, Space.World);
                            }
                        }

                        for (int j = i; j < joints.Length; j++) {
                            jointsPositions[j] = new Vector3(joints[j].position.x, joints[j].position.y, joints[j].position.z);
                        }

                    }

                    tries++;
                }
            }

            float f = (tpos - jointsPositions[jointsPositions.Length - 1]).magnitude;

            if (f < epsilon) {
                done = true;
            } else {
                done = false;
            }

            if (target.position != tpos) {
                tries = 0;
                tpos = target.position;
            }
        }

    }
}
