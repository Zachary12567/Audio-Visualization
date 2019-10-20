using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {

    public float rotationSpeed;

    public bool isBigLight;
    public bool isLowerLights;
    public bool isRotating;
    private Transform to;

    public float angle;
    public float angleX;
    public float angleY;
    public float startingXAngle;
    public float startingYAngle;
    public float desiredYAngle;
    public float currentXAngle;
    public float currentYAngle;

    private Light bigLight;

	// Use this for initialization
	void Start () {
        if (this.tag == "Big Light") {
            isBigLight = true;
            bigLight = GetComponent<Light>();
        } else if(this.tag == "Lower Light") {
            isLowerLights = true;
        }
	}

    // Update is called once per frame
    void Update() {

        if (isBigLight) {

            if (this.name == "Big Light (1)") {
                bigLight.intensity = AudioVisualizer.samples[10] * 500;
            } else if (this.name == "Big Light (2)") {
                bigLight.intensity = AudioVisualizer.samples[5] * 500;
            } else {
                bigLight.intensity = AudioVisualizer.samples[0] * 1000;
            }

            //bigLight.intensity = Random.Range(0,250);
        } else if (isLowerLights) {
            currentYAngle = (int)transform.eulerAngles.y;
            if (currentYAngle > 180) {
                currentYAngle -= 360;
            }
            if ((int)transform.eulerAngles.y >= 360 || (int)transform.eulerAngles.y <= -360) {
                transform.Rotate(0, 0, 0);
            }

            if (!isRotating) {

                startingYAngle = currentYAngle;
                angle = (int)Random.Range(-35, 35);
                isRotating = true;
            } else {
                if (angle > currentYAngle) {
                    transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
                    //transform.Rotate(0, currentYAngle * .01f, 0);
                    if (currentYAngle + .001f >= angle) {
                        isRotating = false;
                    }
                } else {
                    transform.Rotate(Vector3.up * -1 * rotationSpeed * Time.deltaTime);
                    //transform.Rotate(0, currentYAngle * .01f, 0);
                    if (currentYAngle - .001f <= angle) {
                        isRotating = false;
                    }
                }
                if (AudioVisualizer.samples[0] * 1000 + 2 > 30) {
                    isRotating = false;
                } else {
                    isRotating = true;
                }
            }
            //transform.eulerAngles = new Vector3(0, angle, 0);
        } else {
            currentXAngle = transform.eulerAngles.x;
            currentYAngle = (int)transform.eulerAngles.y;
            if (currentYAngle > 180) {
                currentYAngle -= 360;
            }
            if ((int)transform.eulerAngles.y >= 360 || (int)transform.eulerAngles.y <= -360) {
                transform.Rotate(0, 0, 0);
            }

            if (!isRotating) {

                startingYAngle = currentYAngle;
                startingXAngle = currentXAngle;
                angleY = (int)Random.Range(-90, 90);
                angleX = Random.Range(-60, 5);
                isRotating = true;
            } else {
                if (angleY > currentYAngle) {
                    transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
                    //transform.Rotate(0, currentYAngle * .01f, 0);
                    if (currentYAngle + .001f >= angleY) {
                        isRotating = false;
                    }
                } else {
                    transform.Rotate(Vector3.up * -1 * rotationSpeed * Time.deltaTime);
                    //transform.Rotate(0, currentYAngle * .01f, 0);
                    if (currentYAngle - .001f <= angleY) {
                        isRotating = false;
                    }
                }
                if (angleX > currentXAngle) {
                    transform.Rotate(Vector3.right * -1 * rotationSpeed * Time.deltaTime);
                    //transform.Rotate(0, currentYAngle * .01f, 0);
                    if (currentXAngle + .001f >= angleX) {
                        isRotating = false;
                    }
                } else {
                    transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
                    //transform.Rotate(0, currentYAngle * .01f, 0);
                    if (currentXAngle - .001f <= angleX) {
                        isRotating = false;
                    }
                }

            }
        }
    }

    void ChooseRandomAngle() {
        to.rotation = new Quaternion(0, Random.Range(-20, 20), 0, 0);
        Debug.Log(to.rotation.ToString());
    }
    void RotateToAngle(Transform toRotation) {
        transform.rotation = Quaternion.Lerp(transform.rotation, to.rotation, Time.time * rotationSpeed);
    }
}
