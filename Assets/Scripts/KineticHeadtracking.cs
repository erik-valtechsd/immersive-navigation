using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KineticHeadtracking : MonoBehaviour
{

    [SerializeField] private GameObject headtrackObject = default;
    [SerializeField] private GameObject sphere = default;
    [SerializeField] private bool reverse = false;
    [SerializeField] private float scale = 0.5f;
    private List<GameObject> spheres = new List<GameObject>();
    int numSpheres = 100;
    float direction;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numSpheres; i++)
        {
            spheres.Add(GameObject.Instantiate(sphere));
            float incScale = 1f - (((float)i) / ((float)numSpheres));
            spheres[i].transform.localScale = new Vector3(incScale, incScale, incScale);
            spheres[i].transform.parent = sphere.transform.parent;
        }

        if (reverse)
        {
            direction = -1f;
        }
        else
        {
            direction = 1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHelix();
    }

    void UpdateHelix()
    {
        float rotY = ExtensionMethods.Remap(headtrackObject.transform.rotation.y, -60f, 60f, -100f, 100f);
        float rotYHeight = ExtensionMethods.Remap(headtrackObject.transform.rotation.y, -60f, 60f, 0f, 10f);
        float rotX = ExtensionMethods.Remap(headtrackObject.transform.rotation.x, 0, 60f, 0f, 1f);
        for (int i = 0; i < numSpheres; i++)
        {
            float x = Mathf.Sin(i * rotY) * scale * direction;
            float y = i * rotYHeight * 0.005f;
            float z = Mathf.Cos(i * rotY) * scale * direction;
            spheres[i].transform.position = new Vector3(x, y, sphere.transform.position.z + z);
        }

    }

}

public static class ExtensionMethods
{

    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

}
