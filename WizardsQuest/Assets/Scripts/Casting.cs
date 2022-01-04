using System.Collections;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.Networking;

using System.Net.Mime;
using OculusSampleFramework;
using OVR.OpenVR;
using System.Runtime.InteropServices;

using System.Security.Cryptography.X509Certificates;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Zinnia.Action;
using Zinnia.Event.Proxy;
using Action = System.Action;


public class Casting : MonoBehaviour
{

    
    // Variablen für das Casten
    [SerializeField] private string LoadGesturesFile;
    
    [SerializeField] private AudioClip rattling;

    private GestureRecognition gr = new GestureRecognition();
    private Text HUDText;
    private GameObject active_controller = null;
    List<string> stroke = new List<string>();
    int stroke_index = 0; 
    List<GameObject> created_objects = new List<GameObject>();
    private string castingHand;
    private float pitchheight = 1f;
    
    private bool triggerButton;
// Handle to this object/script instance, so that callbacks from the plug-in arrive at the correct instance.
    GCHandle me;
    
    // True wenn Zauberspruch gefunden
    private GameObject _player;
    private Spells SpellsScript;
    
    private bool scaled;
    public bool orcTransport;
    public bool orcPosition;
    public bool cageClosed;

    // externe Referenzen
    private GrabChecker grabChecker;

    private GameObject orc;
    private QuickOutline.Outline outline;
    private GameObject wand;
    private GameObject padlock;
    private GameObject cageDoor;
    private CloseCage closeCage;

    private GameObject cage;

    public GameObject castStar;
    [SerializeField] private AudioClip twinkleSound;
    private AudioSource twinkle;



    void Start()
    {
        twinkle = GetComponent<AudioSource>();
        orc = GameObject.Find("Orc");
        wand = GameObject.Find("Zauberstab");
        cage = GameObject.Find("Kaefig");
        padlock = cage.transform.Find("Padlock").gameObject;
        cageDoor = cage.transform.Find("Door").gameObject;
        closeCage = cageDoor.GetComponent<CloseCage>();

        _player = GameObject.FindWithTag("Player");
        SpellsScript = _player.GetComponent<Spells>();
        grabChecker = wand.GetComponent<GrabChecker>();

        outline = orc.AddComponent<QuickOutline.Outline>();
        outline.enabled = false;

        me = GCHandle.Alloc(this);
        
        // Datenbank laden

        LoadGesturesFile = "WizardsQuest2.dat";


        // Find the location for the gesture database (.dat) file
#if UNITY_EDITOR
        // When running the scene inside the Unity editor,
        // we can just load the file from the Assets/ folder:
        string GesturesFilePath = "Assets/GestureRecognition";
#elif UNITY_ANDROID
        // On android, the file is in the .apk,
        // so we need to first "download" it to the apps' cache folder.
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        string GesturesFilePath = activity.Call <AndroidJavaObject>("getCacheDir").Call<string>("getCanonicalPath");
        UnityWebRequest request = UnityWebRequest.Get(Application.streamingAssetsPath + "/" + LoadGesturesFile);
        request.SendWebRequest();
        while (!request.isDone) {
            // wait for file extraction to finish
        }
        if (request.isNetworkError)
        {
            HUDText.text = "Failed to extract sample gesture database file from apk.";
            return;
        }
        File.WriteAllBytes(GesturesFilePath + "/" + LoadGesturesFile, request.downloadHandler.data);
#else
        // This will be the case when exporting a stand-alone PC app.
        // In this case, we can load the gesture database file from the streamingAssets folder.
        string GesturesFilePath = Application.streamingAssetsPath;
#endif
        if (gr.loadFromFile(GesturesFilePath + "/" + LoadGesturesFile) == false)
        {
            HUDText.text = (gr.loadFromFile(GesturesFilePath + "/" + LoadGesturesFile)).ToString();
            return;
        }


    }

    void Update()
    {
        //************************************** ZAUBERN **************************************//
        //Zauberstab ausgerüstet?

        if (orc.GetComponent<Renderer>().isVisible && grabChecker.controlledBy != "" && SpellsScript.schlossZauber && SpellsScript.schwebeZauber && SpellsScript.schrumpfZauber)
        {
 
            castingHand = grabChecker.controlledBy;
 

            //Zauberstab zeigt auf Orc?
            RaycastHit raycastHit;
            bool rayHitted = Physics.Raycast(transform.position, transform.forward, out raycastHit, 5f);

            if (rayHitted && raycastHit.collider.gameObject.name == "Orc")
            {
                
                //Zauberstab in rechter oder linker Hand?
                if (castingHand == "Interactor.R")
                {
                    // Vibration des aktiven Controllers wenn Orc anvisiert
                 
                    StartCoroutine(Haptics(1, 0.5f, 0.4f, true, false));


                    // Vibration des aktiven Controllers wenn Orc anvisiert
                }
                else if (castingHand == "Interactor.L")
                {
                    
                    StartCoroutine(Haptics(1, 0.5f, 0.4f, false, true));
                }

                //Orc highlighten
                outline.enabled = true;
            }
            else
            {
                outline.enabled = false;
            }

            //Triggerbutton gedrückt zum Zaubern?
            if (active_controller == null)
            {
                if (IndexButtonPressed())

                {
                    print("Controller null, IndexButton gedrückt");
                    active_controller = GameObject.Find(castingHand);
                }
                else
                {
                    return;
                }

                Vector3 hmd_p = Camera.main.gameObject.transform.position;
                Quaternion hmd_q = Camera.main.gameObject.transform.rotation;
                gr.startStroke(hmd_p, hmd_q);
            }

            if (IndexButtonPressed())
            {
                twinkle.PlayOneShot(twinkleSound, .5f);
                // solange User Button drückt, weiter zaubern
                Vector3 p = active_controller.transform.position;
                Quaternion q = active_controller.transform.rotation;
                gr.contdStrokeQ(p, q);
                // Sterne anzeigen
                GameObject star_instance = Instantiate(GameObject.FindWithTag("castStar"));
                GameObject star = new GameObject("stroke_" + stroke_index++);
                star_instance.name = star.name + "_instance";
                star_instance.transform.SetParent(star.transform, false);
                System.Random random = new System.Random();
                star.transform.localPosition = new Vector3(p.x + (float) random.NextDouble() / 80,
                    p.y + (float) random.NextDouble() / 80, p.z + (float) random.NextDouble() / 80);
                star.transform.localRotation = new Quaternion((float) random.NextDouble() - 0.5f,
                    (float) random.NextDouble() - 0.5f, (float) random.NextDouble() - 0.5f,
                    (float) random.NextDouble() - 0.5f).normalized;
                float star_scale = (float) random.NextDouble() + 0.3f;
                star.transform.localScale = new Vector3(star_scale, star_scale, star_scale);
                stroke.Add(star.name);
                return;
            }
            
            // else: User hat den Trigger losgelassen - Spruch beendet
            twinkle.Stop();
            active_controller = null;

            // Sternchen löschen
            foreach (var star in stroke)
            {
                GameObject star_object = GameObject.Find(star);
                if (star_object != null)
                {
                    Destroy(star_object);
                }
            }

            stroke.Clear();
            stroke_index = 0;

            double
                similarity =
                    0; // This will receive a value of how similar the performed gesture was to previous recordings.
            Vector3 pos = Vector3.zero; // This will receive the position where the gesture was performed.
            double scale = 0; // This will receive the scale at which the gesture was performed.
            Vector3
                dir0 = Vector3
                    .zero; // This will receive the primary direction in which the gesture was performed (greatest expansion).
            Vector3 dir1 = Vector3.zero; // This will receive the secondary direction of the gesture.
            Vector3
                dir2 = Vector3
                    .zero; // This will receive the minor direction of the gesture (direction of smallest expansion).
            int gesture_id = gr.endStroke(ref similarity, ref pos, ref scale, ref dir0, ref dir1, ref dir2);

            if (gesture_id < 0)
            {
                Debug.Log("Zauberspruch nicht gefunden");

            }
            else
            {
                string gesture_name = gr.getGestureName(gesture_id);

                CastIt(gesture_id);
            }
            
            

        }


    } 
    
    //Controller-Vibration
    IEnumerator Haptics(float frequency, float amplitude, float duration, bool rightHand, bool leftHand)
    {
        if(rightHand) OVRInput.SetControllerVibration(frequency, amplitude, OVRInput.Controller.RTouch);
        if(leftHand) OVRInput.SetControllerVibration(frequency, amplitude, OVRInput.Controller.LTouch);

        yield return new WaitForSeconds(duration);

        if (rightHand) OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        if (leftHand) OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
    }

    bool IndexButtonPressed()
    {
        if (castingHand == "Interactor.L")
        {
            triggerButton = OVRInput.Get(OVRInput.RawButton.LIndexTrigger);
            return triggerButton;
        }
        else if (castingHand == "Interactor.R")
        {
            triggerButton = OVRInput.Get(OVRInput.RawButton.RIndexTrigger);
            return triggerButton;
        }

        return false;
    }

    void CastIt(int gesture_id)
    {
        if (gesture_id == 2)
        {
            // geteilt durch 2,7
            if (!scaled)
            {
                orc.transform.localScale = new Vector3((orc.transform.localScale.x / 2.7f), (orc.transform.localScale.y / 2.7f), (orc.transform.localScale.z / 2.7f));
                scaled = true;
            }
            
        } 
        if(gesture_id == 0 || gesture_id == 1)
        {

            if(orcPosition) {
                StartCoroutine(CloseCage(2f));

            }
        }
        if (gesture_id == 3)
        { 
            //Debug.Log("Schweben");
            if(scaled) {
                orcTransport = true;
                //StartCoroutine(MoveOrc());
            }

        }
    }

    IEnumerator CloseCage(float waitTime)
    
    {
        if (!cageClosed)
        {
            closeCage.open = false;
            yield return new WaitForSeconds(waitTime);
            AudioSource.PlayClipAtPoint(rattling, transform.position);
            padlock.SetActive(true);
            cageClosed = true;

        }
    }



    
}