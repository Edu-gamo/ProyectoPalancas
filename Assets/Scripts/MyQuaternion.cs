using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AxisAngle {
    public MyVector3 axis;
    public float angle;
}

public class MyQuaternion {

    public float w;
    public float x;
    public float y;
    public float z;

    public MyQuaternion() {
        w = 0;
        x = 0;
        y = 0;
        z = 0;
    }

    public MyQuaternion(float _w, float _x, float _y, float _z) {
        w = _w;
        x = _x;
        y = _y;
        z = _z;
    }

    public static MyQuaternion invert(MyQuaternion q1)
    {
        return new MyQuaternion(q1.w, -q1.x, -q1.y, -q1.z);
    }

    public static MyQuaternion fromAxis(MyVector3 axis, float angle)
    {
        return new MyQuaternion(Mathf.Cos(angle / 2),
                                axis.x * Mathf.Sin(angle / 2),
                                axis.y * Mathf.Sin(angle / 2),
                                axis.z * Mathf.Sin(angle / 2));
    }

    public MyQuaternion multiply(MyQuaternion q1) {
        MyQuaternion qRes = new MyQuaternion();
        qRes.w = q1.w * this.w - q1.x * this.x - q1.y * this.y - q1.z * this.z;
        qRes.x = q1.w * this.x + q1.x * this.w - q1.y * this.z + q1.z * this.y;
        qRes.y = q1.w * this.y + q1.x * this.z + q1.y * this.w - q1.z * this.x;
        qRes.z = q1.w * this.z - q1.x * this.y + q1.y * this.x + q1.z * this.w;
        return qRes;
    }

    public static MyQuaternion multiply(MyQuaternion q1, MyQuaternion q2) {
        MyQuaternion qRes = new MyQuaternion();
        qRes.w = q2.w * q1.w - q2.x * q1.x - q2.y * q1.y - q2.z * q1.z;
        qRes.x = q2.w * q1.x + q2.x * q1.w - q2.y * q1.z + q2.z * q1.y;
        qRes.y = q2.w * q1.y + q2.x * q1.z + q2.y * q1.w - q2.z * q1.x;
        qRes.z = q2.w * q1.z - q2.x * q1.y + q2.y * q1.x + q2.z * q1.w;
        return qRes;
    }

    public static MyQuaternion operator *(MyQuaternion q1, MyQuaternion q2) {
        MyQuaternion qRes = new MyQuaternion();
        qRes.w = q2.w * q1.w - q2.x * q1.x - q2.y * q1.y - q2.z * q1.z;
        qRes.x = q2.w * q1.x + q2.x * q1.w - q2.y * q1.z + q2.z * q1.y;
        qRes.y = q2.w * q1.y + q2.x * q1.z + q2.y * q1.w - q2.z * q1.x;
        qRes.z = q2.w * q1.z - q2.x * q1.y + q2.y * q1.x + q2.z * q1.w;
        return qRes;
    }

    public void invert() {
        this.x = -this.x;
        this.y = -this.y;
        this.z = -this.z;
    }

    public static AxisAngle toAxis(MyQuaternion q1, float angle) {
        AxisAngle temp;
        temp.angle = Mathf.Sin(q1.w / 2);
        temp.axis = new MyVector3(q1.x / Mathf.Sqrt(1 - q1.w * q1.w),
                                q1.y / Mathf.Sqrt(1 - q1.w * q1.w),
                                q1.z / Mathf.Sqrt(1 - q1.w * q1.w));
        return temp;
    }


}