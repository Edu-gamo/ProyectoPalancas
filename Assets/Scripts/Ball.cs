using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public float mass;
    public float radius;            //radio de la pelota

    public Vector3 position;
    public Vector3 postPosition;

    public Vector3 velocity;
    public Vector3 postVelocity;

    private float t;
    public float gravity = -9.81f;
    public float elasticity = 0.2f;

    //PUNTOS,VECTORES, NORMAL DEL PLANO
    public Transform plano;                     //objeto del plano
    public Vector3 puntoPlano1;                 //vector con las coordenadas del primer punto
    public Vector3 puntoPlano2;                 //vector con las coordenadas del segundo punto
    public Vector3 puntoPlano3;                 //vector con las coordenadas del tercer punto
    private Vector3 vectorPlano1_3;                  //vector  formado por el primer y tercer punto
    private Vector3 vectorPlano2_3;                  //vector  formado por el segundo y tercer punto
    private Vector3 normalPlano;                //la normal del plano
    private float dotPlano;
    ///////////////////////////

    //PUNTOS, VECTORES, NORMAL DE LA TABLA DEL BALANCIN
    public Transform pointLeftL;
    public Transform pointRightR;
    private Vector3 pointLeftLVector;
    private Vector3 pointRightRVector;
    private Vector3 pointLeftLF;
    private Vector3 vectorLL_RR;
    private Vector3 vectorLL_LF;
    private Vector3 normalDown;
    private float dDown;
    ///////////////////////////

    //PUNTOS, VECTORES, NORMAL DE LA CAJA1
    public Transform pointRightRU;
    private Vector3 pointRightRUVector;
    private Vector3 pointRightCUVector;
    public Transform pointRightCU;
    private Vector3 pointRightCVector;
    public Transform pointRightC;
    private Vector3 pointRightCU2Vector;
    private Vector3 vectorRC_RCF;
    private Vector3 vectorRC_RCU;
    private Vector3 normalRC;
    private float dRightC;
    private Vector3 pointRightRF;
    private Vector3 pointLeftCF;
    private Vector3 pointRightCF;
    public Transform pointLeftLU;
    private Vector3 pointLeftLUVector;
    private Vector3 pointLeftLU2Vector;
    private Vector3 vectorLL_LLU;
    private Vector3 vectorLL_LLU2;
    private Vector3 normalL;
    private float dLeft;
    public Transform pointLeftC;
    public Transform pointLeftCU;
    ///////////////////////////

    public Transform rightBox;
    public Transform leftBox;

    public float fgy; //Fuerza gravitacional en el eje Y

    // Use this for initialization
    void Start () {

        position = this.transform.position;
        velocity = new Vector3(0, 0, 0);

        radius = this.GetComponent<Renderer>().bounds.size.x / 2;

        //PLANO
        //Definimos donde van a estar colocados cada uno de los tres puntos del plano
        puntoPlano1 = new Vector3(plano.position.x + plano.GetComponent<Renderer>().bounds.size.x / 2, plano.position.y + radius, plano.position.z + plano.GetComponent<Renderer>().bounds.size.z / 2);
        puntoPlano2 = new Vector3(plano.position.x - plano.GetComponent<Renderer>().bounds.size.x / 2, plano.position.y + radius, plano.position.z - plano.GetComponent<Renderer>().bounds.size.z / 2);
        puntoPlano3 = new Vector3(plano.position.x - plano.GetComponent<Renderer>().bounds.size.x / 2, plano.position.y + radius, plano.position.z + plano.GetComponent<Renderer>().bounds.size.z / 2);
        //realizamos dos vectores con los 3 puntos
        vectorPlano1_3 = puntoPlano1 - puntoPlano3;
        vectorPlano2_3 = puntoPlano2 - puntoPlano3;
        //calculamos el normal y el dot del plano
        normalPlano = Vector3.Cross(vectorPlano1_3, vectorPlano2_3).normalized;
        dotPlano = -(normalPlano.x * puntoPlano1.x) - (normalPlano.y * puntoPlano1.y) - (normalPlano.z * puntoPlano1.z);
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //Tabla balancin
        //Definimos donde van a estar colocados cada uno de los tres puntos del balancin
        pointLeftLVector = new Vector3(pointLeftL.position.x, (pointLeftL.position.y + radius /** Mathf.Sin(anguloBal)*/), (pointLeftL.position.z - radius /** Mathf.Cos(anguloBal)*/));
        pointRightRVector = new Vector3(pointRightR.position.x, (pointRightR.position.y + radius /** Mathf.Sin(anguloBal)*/), (pointRightR.position.z - radius /** Mathf.Cos(anguloBal)*/));
        pointLeftLF = new Vector3(pointLeftL.position.x + 1.0f, (pointLeftL.position.y + radius /** Mathf.Sin(anguloBal)*/), (pointLeftL.position.z - radius /** Mathf.Cos(anguloBal)*/));
        //realizamos dos vectores con los 3 puntos
        vectorLL_RR = pointRightRVector - pointLeftLVector;
        vectorLL_LF = pointLeftLF - pointLeftLVector;
        //calculamos el normal y el dot del balancin
        normalDown = Vector3.Cross(vectorLL_RR, vectorLL_LF).normalized;
        dDown = -(normalDown.x * pointRightRVector.x) - (normalDown.y * pointRightRVector.y) - (normalDown.z * pointRightRVector.z);
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //caja1
        //pointRightCUVector = new Vector3(pointRightCU.position.x, pointRightCU.position.y, pointRightCU.position.z);
        //pointRightCVector = new Vector3(pointRightC.position.x, pointRightC.position.y, pointRightC.position.z);
        //pointRightCU2Vector = new Vector3(pointRightCU.position.x + 1f, pointRightCU.position.y, pointRightCU.position.z);
        //Debug.Log(pointRightCUVector.x + " " + pointRightCUVector.y + " " + pointRightCUVector.z + "\n" +
        //    pointRightCVector.x + " " + pointRightCVector.y + " " + pointRightCVector.z + "\n" +
        //    pointRightCU2Vector.x + " " + pointRightCU2Vector.y + " " + pointRightCU2Vector.z + "\n\n");
        ////realizamos dos vectores con los 3 puntos
        //vectorRC_RCF = pointRightCVector - pointRightCUVector;
        //vectorRC_RCU = pointRightCU2Vector - pointRightCUVector;
        ////calculamos el normal y el dot del balancin
        //normalRC = Vector3.Cross(vectorRC_RCF, vectorRC_RCU).normalized;
        //dRightC = -(normalRC.x * pointRightCVector.x) - (normalRC.y * pointRightCVector.y) - (normalRC.z * pointRightCVector.z);

        //pointLeftLUVector = new Vector3(pointLeftLU.position.x, pointLeftLU.position.y, pointLeftLU.position.z);
        //pointLeftLU2Vector = new Vector3(pointLeftLU.position.x + 0.25f, pointLeftLU.position.y, pointLeftLU.position.z);
        ////realizamos dos vectores con los 3 puntos
        //vectorLL_LLU = pointLeftLVector - pointLeftLUVector;
        //vectorLL_LLU2 = pointLeftLVector - pointLeftLU2Vector;
        ////calculamos el normal y el dot del balancin
        //normalL = Vector3.Cross(vectorLL_LLU, vectorLL_LLU2).normalized;
        //dLeft = -(normalL.x * pointLeftLVector.x) - (normalL.y * pointLeftLVector.y) - (normalL.z * pointLeftLVector.z);
        //Debug.Log(pointLeftLUVector.x + " " + pointLeftLUVector.y + " " + pointLeftLUVector.z + "\n" +
        //    pointLeftLU2Vector.x + " " + pointLeftLU2Vector.y + " " + pointLeftLU2Vector.z + "\n" +
        //    pointLeftLVector.x + " " + pointLeftLVector.y + " " + pointLeftLVector.z + "\n\n");
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    }

    // Update is called once per frame
    void FixedUpdate() {
        t += Time.deltaTime;

        if (Vector3.Distance(position, rightBox.position) < radius) {
            position = rightBox.position;
        } else if (Vector3.Distance(position, leftBox.position) < radius) {
            position = leftBox.position;
        } else {
            //EULER
            postPosition = position + (t / 100) * velocity;
            postVelocity.y = velocity.y + (t / 100) * (gravity / mass);

            //detectColision(normalPlano, dotPlano, false);
            //detectColision(normalDown, dDown, true);
            //detectColision(normalRC, dRightC, false);

            position = postPosition;
            velocity = postVelocity;
        }

        this.transform.position = position;
    }

    void detectColision(Vector3 normalPlano, float dotPlano, bool esBalancin) {
        //Colision con el plano
        float dotProductDownPlano = Vector3.Dot(normalPlano, position);
        float dotProductPostDownPlano = Vector3.Dot(normalPlano, postPosition);
        //si detecta colision
        if ((dotProductDownPlano + dotPlano) * (dotProductPostDownPlano + dotPlano) <= 0.075f) {
            float dotProductPostVelDown = Vector3.Dot(normalPlano, postVelocity);
            float dotProductVelDown = Vector3.Dot(normalPlano, velocity);
            postPosition = postPosition - (1 + elasticity) * (dotProductPostDownPlano + dotPlano) * normalPlano;
            postVelocity = postVelocity - (1 + elasticity) * (dotProductPostVelDown) * normalPlano;
            //reseteamos el tiempo para el calculo de la nueva velocidad
            t = 0.0f;
        }
    }
}
