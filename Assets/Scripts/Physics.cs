using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics : MonoBehaviour {

    public Ball[] balls;

    private bool quepalo = false;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update() {

        for (int i = 0; i < balls.Length; i++) {

            if (balls[i].position.y > 0.25) {
                //Calcular posicion
                balls[i].postPosition.x = balls[i].position.x + Time.deltaTime * balls[i].velocity.x;
                balls[i].postPosition.y = balls[i].position.y + Time.deltaTime * balls[i].velocity.y;
                balls[i].postPosition.z = balls[i].position.z + Time.deltaTime * balls[i].velocity.z;

                //Calcular velocidad
                balls[i].postVelocity.x = balls[i].velocity.x + Time.deltaTime * (0.0f / balls[i].mass);
                balls[i].postVelocity.y = balls[i].velocity.y + Time.deltaTime * (-9.81f / balls[i].mass);
                balls[i].postVelocity.z = balls[i].velocity.z + Time.deltaTime * (0.0f / balls[i].mass);


                balls[i].position.x = balls[i].postPosition.x;
                balls[i].position.y = balls[i].postPosition.y;
                balls[i].position.z = balls[i].postPosition.z;

                balls[i].velocity.x = balls[i].postVelocity.x;
                balls[i].velocity.y = balls[i].postVelocity.y;
                balls[i].velocity.z = balls[i].postVelocity.z;

                if (!quepalo) quepalo = true;

            } else if (quepalo) {
                balls[i].position.y = 0.1f;
                quepalo = false;
            }

        }

    }

}
