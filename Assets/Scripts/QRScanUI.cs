using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HearXR;

public class QRScanUI : MonoBehaviour
{
    [SerializeField]
    private QRCodeDecodeController qrDecoder;
    [SerializeField]
    private ActivateSceneManager activateSceneManager;
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private Text uiText;
    //[SerializeField]
    private RTdatabase rtDatabase;
    //[SerializeField] 
    private HeadphoneMotionExample headphoneMotion;
    // Start is called before the first frame update
    void Start()
    {
        rtDatabase = GameObject.Find("RealtimeDB").GetComponent<RTdatabase>();
        headphoneMotion = GameObject.Find("HeadphoneMotionExample").GetComponent<HeadphoneMotionExample>();
        panel.SetActive(false);

        // uiText = GameObject.Find("Info").GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {

    }
 

    public void displayString(string text)
    {
        // uiText.text = text;
    }
    public void qrScanFinished(string dataText)
    {
        // uiText.text = dataText;
        if (rtDatabase != null)
        {
            rtDatabase.SetUniqueId(dataText);
        }
        // calibrate headphone rotation
        Debug.Log("ResetCalibration: ");
        qrDecoder.Reset();
        headphoneMotion.ResetCalibration();
        activateSceneManager.ActivateAllElements(true);
        panel.SetActive(true);
    }
}
