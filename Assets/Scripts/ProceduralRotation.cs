using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralRotation : MonoBehaviour
{
    [SerializeField]
    GameObject rotationJoint = default;
    [SerializeField]
    GameObject headtrackObject = default;
        [SerializeField]
    Vector3 rotation = new Vector3(0,0,0);
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (headtrackObject != null && rotationJoint != null)
        {
            float rotx = ExtensionMethods.Remap(-headtrackObject.transform.eulerAngles.y, -60, 60f, 12, 24f);
            float roty = ExtensionMethods.Remap(headtrackObject.transform.rotation.z, -60, 60f, -15f, 15f);
            float rotz = ExtensionMethods.Remap(-headtrackObject.transform.eulerAngles.x, -60, 60f, -10f, 25f);
            rotation = new Vector3(-headtrackObject.transform.eulerAngles.y, roty, -headtrackObject.transform.eulerAngles.x+56);
            // rotationJoint.transform.rotation = Quaternion.Euler(rotation);
            rotationJoint.transform.localRotation = Quaternion.Euler(rotation);
        }
    }
}
