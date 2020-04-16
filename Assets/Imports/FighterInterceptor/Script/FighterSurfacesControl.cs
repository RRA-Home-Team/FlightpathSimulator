using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterSurfacesControl : MonoBehaviour
{
    public Transform rightElevator, leftElevator;
    public Transform rightFlap, leftFlap;
    public Transform rightRudder, leftRudder;
    public float MRx, MRy, MRz;
    public bool turnFlap;
    public bool turnRudder;
    public float turnSpeed;
    private float angleX;

    private const float MAX_ELEVATOR_ANGLE = 25;
    private const float MAX_FLAP_ANGLE = 45;
    private const float MAX_RUDDER_ANGLE = 30;

    void Start () {
		
	}
	
	void Update () {

        MRx = Mathf.Clamp(MRx + Input.GetAxis("Pitch") * 0.05f, -1f, 1f);
        MRz = Mathf.Clamp(MRz - Input.GetAxis("Roll") * 0.05f, -1f, 1f);
        MRy = Mathf.Clamp(MRy + Input.GetAxis("Yaw") * 0.05f, -1f, 1f);

        if (Input.GetKeyDown("f")) {
            Toggle(turnFlap);
        }


        Toggle(turnRudder);

        rightElevator.transform.localRotation = Quaternion.Euler(new Vector3((MRz + MRx) * MAX_ELEVATOR_ANGLE, 0, 0));
        leftElevator.transform.localRotation = Quaternion.Euler(new Vector3((-MRz + MRx) * MAX_ELEVATOR_ANGLE, 0, 0));

        TurnRudder();
        TurnFlap();
    }

    private void Toggle(bool toggle) {
        if (!toggle) toggle = true; 
        else toggle = false;
    }

    private void TurnRudder() {
        int angleX = turnRudder ? 1 : 0;
        TranslateAngle(rightRudder, 16, angleX * MRy * MAX_RUDDER_ANGLE, 107);
        TranslateAngle(leftRudder, -16, angleX * MRy * MAX_RUDDER_ANGLE, 73);
    }

    private void TurnFlap() {
        angleX = Mathf.Lerp(angleX, turnFlap ? 1 : 0, turnSpeed * Time.deltaTime);
        TranslateAngle(rightFlap, 6, angleX * MAX_FLAP_ANGLE, 5);
        TranslateAngle(leftFlap, -6, angleX * MAX_FLAP_ANGLE, -5);
    }

    void TranslateAngle(Transform tr, float heading, float attitude, float bank) {

        float c1 = Mathf.Cos(heading * Mathf.Deg2Rad / 2);
        float s1 = Mathf.Sin(heading * Mathf.Deg2Rad / 2);
        float c2 = Mathf.Cos(attitude * Mathf.Deg2Rad / 2);
        float s2 = Mathf.Sin(attitude * Mathf.Deg2Rad / 2);
        float c3 = Mathf.Cos(bank * Mathf.Deg2Rad / 2);
        float s3 = Mathf.Sin(bank * Mathf.Deg2Rad / 2);

        float w = c1 * c2 * c3 - s1 * s2 * s3;
        float x = s2 * c1 * c3 + s1 * c2 * s3;
        float y = s1 * c2 * c3 + s2 * c1 * s3;
        float z = s3 * c1 * c2 - s1 * s2 * c3;

        tr.transform.localRotation = new Quaternion(x, y, z, w);
    }
}
