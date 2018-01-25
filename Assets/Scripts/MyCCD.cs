using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCCD : MonoBehaviour {

	public Transform[] joints;
	public Transform targ;

    private MyVector3[] jointsPositions;
    private MyVector3 targPosition;

    public float[] theta;
    
	[SerializeField]
	private float[] sin;
	[SerializeField]
	private float[] cos; 
    
	public bool done = false;
	private MyVector3 tpos;
    
	[SerializeField]
	private int Mtries = 10;
	[SerializeField]
	private int tries = 0;
	
	private float epsilon = 0.1f;


	// Initializing the variables
	void Start () {
		theta = new float[joints.Length];
		sin = new float[joints.Length];
		cos = new float[joints.Length];
		tpos = new MyVector3(targ.transform.position.x, targ.transform.position.y, targ.transform.position.z);
        MyVector3[] jointsPositions = new MyVector3[joints.Length];
}
	
	// Running the solver - all the joints are iterated through once every frame
	void Update () {

        targPosition = new MyVector3(targ.transform.position.x, targ.transform.position.y, targ.transform.position.z);

        if (!done) {
			if (tries <= Mtries) {
				for (int i = jointsPositions.Length - 2; i >= 0; i--) {
                    MyVector3 r1 = jointsPositions[jointsPositions.Length - 1] - jointsPositions[i];
                    MyVector3 r2 = tpos - jointsPositions[i];
                    
                    if (r1.magnitude() * r2.magnitude() <= 0.001f) {
						
					} else {
                        cos[i] = MyVector3.Dot(r1, r2) / (r1.magnitude() * r2.magnitude());
                        sin[i] = MyVector3.Cross(r1, r2).magnitude() / (r1.magnitude() * r2.magnitude());
                    }
                    
                    MyVector3 axis = MyVector3.Cross(r1, r2).normalized();
                    
                    theta[i] = Mathf.Acos(cos[i]);
                    
                    if (sin[i] < 0) theta[i] = -theta[i];
                    
                    theta[i] *= Mathf.Rad2Deg;

                    MyQuaternion quat = MyQuaternion.fromAxis(axis, theta[i]) * new MyQuaternion(joints[i].transform.rotation.w, joints[i].transform.rotation.x, joints[i].transform.rotation.y, joints[i].transform.rotation.z);
                    joints[i].transform.rotation = new Quaternion(quat.x, quat.y, quat.z, quat.w);
                    //joints[i].transform.rotation = Quaternion.AngleAxis(theta[i], axis) * joints[i].transform.rotation;


                }
				
				tries++;
			}
		}

        float f = (tpos - jointsPositions[jointsPositions.Length - 1]).magnitude();
        
		if (f < epsilon) {
			done = true;
		} else {
			done = false;
		}
		
		if(targPosition != tpos) {
			tries = 0;
			tpos = targPosition;
		}

	}

}
