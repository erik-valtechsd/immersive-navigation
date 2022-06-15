using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QRCodeGenTask : MonoBehaviour
{

    Texture2D qrTexture; 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void qrEncodeFinished(Texture2D tex)
    {
        if (tex != null && tex != null)
        {
            // do something after encode
            Debug.Log("created QR code..");//
            qrTexture = tex;
            SaveQRAsTexture();
        }
    }

    public void SaveQRAsTexture()
    {
        GalleryController.SaveImageToGallery(qrTexture);
    }
}
