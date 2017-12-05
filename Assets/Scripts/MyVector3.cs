using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyVector3 {

    public float x;
    public float y;
    public float z;

    public MyVector3() {
        x = 0;
        y = 0;
        z = 0;
    }

    public MyVector3(float _x, float _y, float _z) {
        x = _x;
        y = _y;
        z = _z;
    }

    public static float Distance(MyVector3 a, MyVector3 b) {//ESTA MAL
        return (b.x - a.x) + (b.y - a.y) + (b.z - a.z);
    }

    public float magnitude() {
        return Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2) + Mathf.Pow(z, 2));
    }

    public static MyVector3 operator +(MyVector3 v1, MyVector3 v2) {
        return new MyVector3(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
    }
    public static MyVector3 operator -(MyVector3 v1, MyVector3 v2)
    {
        return new MyVector3(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
    }
    public override string ToString() {
        return "(" + x + ", " + y + ", " + z + ")";
    }

}
