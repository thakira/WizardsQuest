
using UnityEngine;
using PathCreation;

public class Spells : MonoBehaviour
{
    [SerializeField] private PathCreator lockPath;
    [SerializeField] private PathCreator shrinkPath;
    [SerializeField] private PathCreator floatPath;

    [SerializeField] private EndOfPathInstruction end;

    [SerializeField] private float speed;

    private float dstTravelled;
    [SerializeField] private AudioClip paperFly;

    [SerializeField] private GameObject schrumpfRolle;
    [SerializeField] private GameObject schrumpfTafel;

    [SerializeField] private GameObject schwebeRolle;
    [SerializeField] private GameObject schwebeTafel;

    [SerializeField] private GameObject verschlussRolle;
    [SerializeField] private GameObject verschlussTafel;
    
    public bool schwebeZauber;
    public bool schrumpfZauber;
    public bool schlossZauber;

    public bool floatCast;
    public bool shrinkCast;
    public bool lockCast;


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        /*dstTravelled += speed * Time.deltaTime;
        scroll.transform.position = path2Use.path.GetPointAtDistance(dstTravelled, end);
        //Debug.LogWarning("Orc-Position:" + transform.position);
        //Debug.LogWarning(path2Use.path.GetPoint(path2Use.path.NumPoints -1));
        //Debug.LogWarning(scroll.transform.position);
        if (scroll.transform.position == path2Use.path.GetPoint(path2Use.path.NumPoints - 1))
        {
            //Debug.LogWarning(scroll.name);
            if (scroll.name == "Schrumpfzauber_Rolle")
            {
                schrumpfTafel.SetActive(true);
                schrumpfRolle.SetActive(false);
                shrinkCast = true;
            } else if(scroll.name == "Schwebezauber_Rolle") 
            {
                schwebeTafel.SetActive(true);
                schwebeRolle.SetActive(false);
                floatCast = true;
            } else if (scroll.name == "Verschlusszauber_Rolle")
            {
                verschlussTafel.SetActive(true);
                verschlussRolle.SetActive(false);
                lockCast = true;
            }
        }*/

        if (schwebeZauber && !floatCast)
        {
            
            //AudioSource.PlayClipAtPoint(paperFly, schwebeRolle.transform.position);
            AudioSource.PlayClipAtPoint(paperFly, schwebeRolle.transform.position);
            dstTravelled += speed * Time.deltaTime;
            schwebeRolle.transform.position = floatPath.path.GetPointAtDistance(dstTravelled, end);
            //Debug.LogWarning("Orc-Position:" + transform.position);
            //Debug.LogWarning(path2Use.path.GetPoint(path2Use.path.NumPoints -1));
            //Debug.LogWarning(scroll.transform.position);
            if (schwebeRolle.transform.position == floatPath.path.GetPoint(floatPath.path.NumPoints - 1))
            {
                schwebeTafel.SetActive(true);
                schwebeRolle.SetActive(false);
                floatCast = true;
                dstTravelled = 0f;
                // StartCoroutine(spellFly(2f, schwebeRolle, floatPath));
            }
        }

        if (schrumpfZauber && !shrinkCast)
        {
            AudioSource.PlayClipAtPoint(paperFly, schrumpfRolle.transform.position);
            //paperFly.Play();
            dstTravelled += speed * Time.deltaTime;
            schrumpfRolle.transform.position = shrinkPath.path.GetPointAtDistance(dstTravelled, end);
            //Debug.LogWarning("Orc-Position:" + transform.position);
            //Debug.LogWarning(path2Use.path.GetPoint(path2Use.path.NumPoints -1));
            //Debug.LogWarning(scroll.transform.position);
            if (schrumpfRolle.transform.position == shrinkPath.path.GetPoint(shrinkPath.path.NumPoints - 1))
            {
                schrumpfTafel.SetActive(true);
                schrumpfRolle.SetActive(false);
                shrinkCast = true;
                dstTravelled = 0f;
                //StartCoroutine(spellFly(2f, schrumpfRolle, shrinkPath));

            }
        }

        if (schlossZauber && !lockCast)
        {
            AudioSource.PlayClipAtPoint(paperFly, verschlussRolle.transform.position);
            dstTravelled += speed * Time.deltaTime;
            verschlussRolle.transform.position = lockPath.path.GetPointAtDistance(dstTravelled, end);
            //Debug.LogWarning("Orc-Position:" + transform.position);
            //Debug.LogWarning(path2Use.path.GetPoint(path2Use.path.NumPoints -1));
            //Debug.LogWarning(scroll.transform.position);
            if (verschlussRolle.transform.position == lockPath.path.GetPoint(lockPath.path.NumPoints - 1))
            {
                verschlussTafel.SetActive(true);
                verschlussRolle.SetActive(false);
                lockCast = true;
                dstTravelled = 0f;
                //StartCoroutine(spellFly(2f, verschlussRolle, lockPath));

            }

        }
    }
}

/*IEnumerator spellFly(float waitTime, GameObject scroll, PathCreator path2Use)
{
    //Debug.LogWarning(path2Use.path.GetPointAtDistance(dstTravelled));
    //Debug.LogWarning("Ende?: " + pathCreator.path.GetPoint(pathCreator.path.NumPoints - 1));
    //Debug.LogWarning(pathCreator.path.NumPoints - 1);
    dstTravelled += speed * Time.deltaTime;
    scroll.transform.position = path2Use.path.GetPointAtDistance(dstTravelled, end);
    //Debug.LogWarning("Orc-Position:" + transform.position);
    //Debug.LogWarning(path2Use.path.GetPoint(path2Use.path.NumPoints -1));
    //Debug.LogWarning(scroll.transform.position);
    if (scroll.transform.position == path2Use.path.GetPoint(path2Use.path.NumPoints - 1))
    {
        //Debug.LogWarning(scroll.name);
        if (scroll.name == "Schrumpfzauber_Rolle")
        {
            schrumpfTafel.SetActive(true);
            schrumpfRolle.SetActive(false);
            shrinkCast = true;
        } else if(scroll.name == "Schwebezauber_Rolle") 
        {
            schwebeTafel.SetActive(true);
            schwebeRolle.SetActive(false);
            floatCast = true;
        } else if (scroll.name == "Verschlusszauber_Rolle")
        {
            verschlussTafel.SetActive(true);
            verschlussRolle.SetActive(false);
            lockCast = true;
        }
    }

    dstTravelled = 0f;
    path2Use = null;
    scroll = null;
    yield return new WaitForSeconds(waitTime);
    //transform.rotation = pathCreator.path.GetRotationAtDistance(dstTravelled, end);
    /*if(end == EndOfPathInstruction.Stop) {
    castingScript.orcTransport = false;
    Debug.LogWarning("Orc fertig transportiert " + castingScript.orcTransport);
    }#1#
}

}
*/