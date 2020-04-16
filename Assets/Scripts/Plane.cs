using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public bool PlayerControlsEnabled = true;
    public float Speed = 100f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update() {
        GetComponent<Rigidbody>().velocity = transform.forward * Speed;
        
        if (PlayerControlsEnabled) {
            AdjustPitch(Input.GetAxis("Pitch"));
            AdjustRoll(Input.GetAxis("Roll"));
            AdjustYaw(Input.GetAxis("Yaw"));
        }
    }

    public void AdjustPitch(float amount) {
        transform.Rotate(new Vector3(amount, 0, 0) * Time.deltaTime * Speed, Space.Self);
    }

    public void AdjustRoll(float amount) {
        transform.Rotate(new Vector3(0, 0, amount) * Time.deltaTime * Speed, Space.Self);
    }

    public void AdjustYaw(float amount) {
        transform.Rotate(new Vector3(0, amount, 0) * Time.deltaTime * Speed * 0.3f, Space.Self);
    }
}
