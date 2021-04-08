using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Camera-Control/MouseLook")]
public class MouseLook : MonoBehaviour
{   
    [SerializeField]
    private enum RotationAxes {MouseXandY=0, MouseX = 1, MouseY = 2};
    [SerializeField]
    private RotationAxes axes = RotationAxes.MouseXandY;

    [SerializeField]
    private float sensivityX = 2f;
    [SerializeField]
    private float sensivityY = 2f;

    [SerializeField]
    private float minimumX = -360f;
    [SerializeField]
    private float maximumX = 360f;
    [SerializeField]
    private float minimumY = -360f;
    [SerializeField]
    private float maximumY = 360f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    private Quaternion originalRotation; 
    
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
        originalRotation = transform.localRotation; 
    }
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f)
            angle += 360f;
        if (angle > 360f)
            angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponentInParent<CharacterContol>().panelact)
        {
            if (axes == RotationAxes.MouseXandY)
            {
                rotationX += Input.GetAxis("MouseX") * sensivityX;
                rotationY += Input.GetAxis("MouseY") * sensivityY;
                rotationX = ClampAngle(rotationX, minimumX, maximumX);
                rotationY = ClampAngle(rotationY, minimumY, maximumY);
                Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
                Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
                transform.localRotation = originalRotation * xQuaternion * yQuaternion;
            }
            else if (axes == RotationAxes.MouseX)
            {
                rotationX += Input.GetAxis("Mouse X") * sensivityX;
                rotationX = ClampAngle(rotationX, minimumX, maximumX);
                Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
                transform.localRotation = originalRotation * xQuaternion;
            }
            else if (axes == RotationAxes.MouseY)
            {
                rotationY += Input.GetAxis("Mouse Y") * sensivityY;
                rotationY = ClampAngle(rotationY, minimumY, maximumY);
                Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
                transform.localRotation = originalRotation * yQuaternion;
            }
        }
    }
}
