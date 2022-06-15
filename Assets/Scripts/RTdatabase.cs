using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Firebase;
using Firebase.Database;
using Firebase.Extensions; // for ContinueWithOnMainThread
using HearXR; // for ContinueWithOnMainThread

public class RTdatabase : MonoBehaviour
{

    [SerializeField] public GameObject headphoneMotionObject;

    private HeadphoneMotionExample headphoneMotion;

    // DisplayManager displayManager;
    public float x, y, z = 0;
    private string addressX = "/rotation/x/";
    private string addressY = "/rotation/y/";
    private string addressZ = "/rotation/z/";
    private string addressRotation = "/rotation/";
    private string addressId = "/id/";
    private string idString = "";

    private FirebaseDatabase db;
    // Start is called before the first frame update

    void Awake()
    {
        headphoneMotion = headphoneMotionObject.GetComponent<HeadphoneMotionExample>();

    }
    void Start()
    {

        db = FirebaseDatabase.GetInstance("https://immersive-navigation-default-rtdb.firebaseio.com/");
        // DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        // GetRemoteValues(addressX);
        GetRemoteValues(addressRotation);
        db.GetReference(addressRotation).ValueChanged += HandleValueChanged;
        db.GetReference(addressId).ValueChanged += HandleValueChanged;

    }

    void GetRemoteValues(String address)
    {
        Debug.Log("get remote values...");
        db.GetReference(address)
          .GetValueAsync().ContinueWithOnMainThread(task =>
          {
              if (task.IsFaulted)
              {
                  Debug.Log("error getting values...");
              }
              else if (task.IsCompleted)
              {
                  DataSnapshot snapshot = task.Result;
                  Debug.Log(snapshot.GetRawJsonValue());

              }
          });
    }
    public void GetRemoteRotationValues()
    {
        db.GetReference("/rotation/")
          .GetValueAsync().ContinueWithOnMainThread(task =>
          {
              if (task.IsFaulted)
              {
                  Debug.Log("error getting values...");
              }
              else if (task.IsCompleted)
              {
                  DataSnapshot snapshot = task.Result;
                  //   Debug.Log(snapshot.GetRawJßsonValue());

              }
          });
    }
    public void GetRemoteDeviceId()
    {
        Debug.Log("GetRemoteDeviceId");
        db.GetReference("/id/")
          .GetValueAsync().ContinueWithOnMainThread(task =>
          {
              if (task.IsFaulted)
              {
                  Debug.Log("error getting values...");
              }
              else if (task.IsCompleted)
              {
                  DataSnapshot snapshot = task.Result;
                  idString = snapshot.Value.ToString();
                  Debug.Log("recieved queried id value: " + idString);
                  headphoneMotion.SyncDeviceSyncID(idString);

              }
          });
    }

    public void SetUniqueId(String qrCode)
    {
        Debug.Log("SetUniqueId: " + qrCode);
        db.GetReference("/id/").SetValueAsync(qrCode);
    }
    public void SetRemoteRotationValues(float valueX, float valueY, float valueZ)
    {
        Debug.Log("SetRemoteRotationValues: " + valueX);
        db.GetReference("/rotation/x/").SetValueAsync(valueX);
        db.GetReference("/rotation/y/").SetValueAsync(valueY);
        db.GetReference("/rotation/z/").SetValueAsync(valueZ);
    }

    void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        foreach (var child in args.Snapshot.Children)
        {
            String key = (child.Key).ToString();
            // Single.TryParse(args.Snapshot.Value);
            float val = float.Parse(child.Value.ToString());
            if (key == "x")
            {
                x = val;
            }
            else if (key == "y")
            {
                y = val;
            }
            else if (key == "z")
            {
                z = val;
            }
            else if (key == "id")
            {
                Debug.Log("recieved id value: " + child.Value.ToString());
                idString = child.Value.ToString();
            }
        }
        // Debug.Log("x: " + x + " y: " + y + " z: " + z);
        Quaternion newRotation = new Quaternion(x, y, z, 1);
        // Debug.Log(newRotation);

        if (headphoneMotion != null)
        {

            headphoneMotion.UpdateRotationFromFirebaseDB(newRotation);
        }
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        // Debug.Log(args.Snapshot);

        // turn automode off on displayManager script object
        // displayManager.ResetIdleMode();
    }


    // Update is called once per frame
    void Update()
    {

    }
}
