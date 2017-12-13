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

    public MyVector3 normalized() {
        return new MyVector3(x / magnitude(), y / magnitude(), z / magnitude());
    }

    public static float Distance(MyVector3 a, MyVector3 b) {
        return new MyVector3((b.x - a.x) , (b.y - a.y) , (b.z - a.z)).magnitude();
    }

    public float magnitude() {
        return Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2) + Mathf.Pow(z, 2));
    }

    public static MyVector3 operator +(MyVector3 v1, MyVector3 v2) {
        return new MyVector3(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
    }

    public static MyVector3 operator -(MyVector3 v1, MyVector3 v2) {
        return new MyVector3(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
    }

    public static MyVector3 operator *(float d, MyVector3 v) {
        return new MyVector3(v.x * d, v.y * d, v.z * d);
    }
    public static MyVector3 operator *(MyVector3 v, float d) {
        return new MyVector3(v.x * d, v.y * d, v.z * d);
    }

    public static MyVector3 Cross(MyVector3 v1, MyVector3 v2) {
        return new MyVector3((v1.y * v2.z - v1.z * v2.y), (v1.z * v2.x - v1.x * v2.z), (v1.x * v2.y - v1.y * v2.x));
    }

    public static float Dot(MyVector3 v1, MyVector3 v2) {
        return (v1.x * v2.x) + (v1.y * v2.y) + (v1.z * v2.z);
    }

    public override string ToString() {
        return "(" + x + ", " + y + ", " + z + ")";
    }

}
