using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterSurfacesControl : MonoBehaviour
{
    public Transform rightElevator, leftElevator;
    public Transform rightFlap, leftFlap;
    public Transform rightRudder, leftRudder;
    public float turnSpeed;

    private const float MAX_ELEVATOR_ANGLE = 25;
    private const float MAX_FLAP_ANGLE = 45;
    private const float MAX_RUDDER_ANGLE = 30;

    void Start () {
		
	}
	
	void Update () {
        TurnElevators(Input.GetAxis("Pitch"), Input.GetAxis("Roll"));
        TurnRudder(Input.GetAxis("Yaw"));
        TurnFlap(Input.GetAxis("Pitch"));
    }

    float ElevatorAngleLeft;
    float ElevatorAngleRight;

    private void TurnElevators(float pitch, float roll) {
        ElevatorAngleLeft = Mathf.Lerp(ElevatorAngleLeft, pitch - roll, turnSpeed * Time.deltaTime);
        ElevatorAngleRight = Mathf.Lerp(ElevatorAngleRight, pitch + roll, turnSpeed * Time.deltaTime);

        TranslateAngle(rightElevator, 0, ElevatorAngleLeft * MAX_ELEVATOR_ANGLE, 0);
        TranslateAngle(leftElevator, -0, ElevatorAngleRight * MAX_ELEVATOR_ANGLE, 0);
    }

    float RudderAngle;

    private void TurnRudder(float yaw) {
        RudderAngle = Mathf.Lerp(RudderAngle, yaw, turnSpeed * Time.deltaTime);

        TranslateAngle(rightRudder, 16, RudderAngle * MAX_RUDDER_ANGLE, 107);
        TranslateAngle(leftRudder, -16, RudderAngle * MAX_RUDDER_ANGLE, 73);
    }

    float FlapAngle;

    private void TurnFlap(float pitch) {
        FlapAngle = Mathf.Lerp(FlapAngle, pitch, turnSpeed * Time.deltaTime);

        TranslateAngle(rightFlap, 6, FlapAngle * MAX_FLAP_ANGLE, 5);
        TranslateAngle(leftFlap, -6, FlapAngle * MAX_FLAP_ANGLE, -5);
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
