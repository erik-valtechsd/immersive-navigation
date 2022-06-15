using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HearXR;

public class ActivateSceneManager : MonoBehaviour
{

    [SerializeField]
    GameObject sceneElement1 = default;
    [SerializeField]
    GameObject sceneElement2 = default;
    [SerializeField]
    GameObject sceneElement3 = default;
    [SerializeField]
    GameObject sceneElement4 = default;
    [SerializeField]
    GameObject sceneElement5 = default;
    [SerializeField]
    GameObject sceneElement6 = default;
    [SerializeField]
    GameObject sceneElement7 = default;
    [SerializeField]
    GameObject sceneElement8 = default;
    [SerializeField]
    public bool deactivateOnStart = false;
    // Start is called before the first frame update
    void Start()
    {
        if (deactivateOnStart)
        {
            ActivateAllElements(false);
        }


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivateAllElements(bool activate = true)
    {
        Debug.Log("ActivateAllElements");
        ActivateElement(sceneElement1, activate);
        ActivateElement(sceneElement2, activate);
        ActivateElement(sceneElement3, activate);
        ActivateElement(sceneElement4, activate);
        ActivateElement(sceneElement5, activate);
        ActivateElement(sceneElement6, activate);
        ActivateElement(sceneElement7, activate);
        ActivateElement(sceneElement8, activate);
    }

    public void ActivateElement(GameObject element, bool activate = true)
    {
        if (element != null)
        {
            element.SetActive(activate);
        }
    }
}
