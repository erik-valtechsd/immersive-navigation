/// <summary>
/// write by 52cwalk,if you have some question ,please contract lycwalk@gmail.com
/// </summary>
/// 
/// 

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QREncodeTest : MonoBehaviour
{
    public QRCodeEncodeController e_qrController;
    public RawImage qrCodeImage;
    public Text infoText;

    public string randomId;

    public Texture2D codeTex;
    private Texture2D generatedTex;
    // Use this for initialization
    void Start()
    {
        Encode();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void qrEncodeFinished(Texture2D tex)
    {
        if (tex != null && tex != null)
        {
            int width = tex.width;
            int height = tex.height;
            float aspect = width * 1.0f / height;
            qrCodeImage.GetComponent<RectTransform>().sizeDelta = new Vector2(170, 170.0f / aspect);
            qrCodeImage.texture = tex;
            codeTex = tex;
        }
        else
        {
        }
    }

    public void setCodeType(int typeId)
    {
        e_qrController.eCodeFormat = (QRCodeEncodeController.CodeMode)(typeId);
        Debug.Log("clicked typeid is " + e_qrController.eCodeFormat);
    }


    public void Encode()
    {
        if (e_qrController != null)
        {
            randomId = GenerateRandomNo().ToString();
            Debug.Log("GENERATED ID: " + randomId);
            int errorlog = e_qrController.Encode(randomId);
            infoText.color = Color.red;
            if (errorlog == -13)
            {
                infoText.text = "Must contain 12 digits,the 13th digit is automatically added !";

            }
            else if (errorlog == -8)
            {
                infoText.text = "Must contain 7 digits,the 8th digit is automatically added !";
            }
            else if (errorlog == -39)
            {
                infoText.text = "Only support digits";
            }
            else if (errorlog == -128)
            {
                infoText.text = "Contents length should be between 1 and 80 characters !";

            }
            else if (errorlog == -1)
            {
                infoText.text = "Please select one code type !";
            }
            else if (errorlog == 0)
            {
                infoText.color = Color.green;
                infoText.text = "Encode successfully !";
                qrEncodeFinished(e_qrController.GetQRTexture());
                SaveCode();
            }
        }
    }

    public void ClearCode()
    {
        qrCodeImage.texture = null;
        infoText.text = "";
    }

    public void SaveCode()
    {
        GalleryController.SaveImageToGallery(codeTex);
    }

    public void GotoNextScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

    public int GenerateRandomNo()
    {
        int min = 100000;
        int max = 999999;
        return new System.Random().Next(min, max);
    }
    public string GetRandomIdString()
    {
        if (randomId != null)
        {
            return randomId;
        }
        return "";
    }

}
