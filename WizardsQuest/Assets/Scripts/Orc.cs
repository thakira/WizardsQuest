using System;
using System.Collections;
/*using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Media;
using System.Xml.XPath;
using UnityEditor;*/
using UnityEngine;
using CameraFading;
using PathCreation;


public class Orc : MonoBehaviour
{
    [SerializeField] private AudioClip groalAudioClip;
    [SerializeField] private AudioClip orcHit;

    [SerializeField] private PathCreator pathCreator;

    [SerializeField] private EndOfPathInstruction end;

    [SerializeField] private float speed;

    private float dstTravelled;
    
    /*[SerializeField] private Vector3 startPos = new Vector3(0,1,5);
    [SerializeField] private Quaternion startRot = new Quaternion(0,0,0, 0);
    [SerializeField] private GameObject blackScreen;*/

    //private AudioSource _audioSource;
    private GameObject _player;
    private GameObject _playerMove;
    private GameObject castRay;
    private Casting castingScript;
    private GameObject _spawnPoint;
    //[SerializeField] private Transform follow = null;
    //[SerializeField] private CapsuleCollider playerCollider;
    /*private Player _playerScript;*/
    //private Color _blackScreen;
    private Vector3 originalLocalPosition;
    private GameObject _playArea;

    public bool interact;

    private Animator _anim;
    /*private float _time;*/

    private void Awake()
    {
        //originalLocalPosition = follow.localPosition;

    }

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        castRay = GameObject.Find("CastRay");
        castingScript = castRay.GetComponent<Casting>();
        _playerMove = GameObject.FindGameObjectWithTag("Headset");
        _player = GameObject.FindGameObjectWithTag("Player");
        _playArea = GameObject.Find("PlayAreaAlias");
        //_player = GameObject.FindGameObjectWithTag("Player");
        _spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
        CameraFade.In(3f);
    }

    // Update is called once per frame
    void Update()
    {
        
        /*Debug.LogWarning("Player: pos vorher " + _player.transform.position);
        _player.transform.position += -_player.transform.right * originalLocalPosition.x;
        _player.transform.position += -_player.transform.forward * originalLocalPosition.z;
        Debug.LogWarning("Player: pos nachher " + _player.transform.position);*/
        
        
        
        
        if (interact)
        {
            StartCoroutine(HitEm(1f, MovePlayer));
            interact = false;
        }
        else
        {
            _anim.SetBool("hit", false);
        }

        if (castingScript.orcTransport)
        {
            StartCoroutine(orc2Cage(2f));
            /*castingScript.orcTransport = false;
            transform.localScale = new Vector3((transform.localScale.x * 2f), (transform.localScale.y * 2f), (transform.localScale.z * 2f));
            castingScript.orcPosition = true;*/
           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if (other.CompareTag("Player"))
        {*/
        /*if(other == playerCollider)
        {*/
        interact = true;
        /*}*/
        /*}*/
    }

    /*private void OnTriggerStay(Collider other)
    {
        if (other == playerCollider)
        {
            interact = false;
        }
    }*/
    private void OnTriggerExit(Collider other)
    {
        /*if (other.CompareTag("Player"))
        {*/
        /*if (other == playerCollider)
        {*/
            interact = false;
        /*}*/

    /*}*/
    }
    
    IEnumerator HitEm(float waitTime, Action movePlayer)
    {
        //Vector3 playerPos = GameObject.FindWithTag("Headset").transform.position;
        Vector3 playerPos = _playerMove.transform.position;
        Vector3 npcPos = gameObject.transform.position;
        Vector3 direction = new Vector3(playerPos.x - npcPos.x, 0.0f, playerPos.z - npcPos.z);
        //Vector3 direction = playerPos - npcPos;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
        yield return new WaitForSeconds(1f);
        _anim.SetBool("hit", true);
        AudioSource.PlayClipAtPoint(groalAudioClip, transform.position);
        AudioSource.PlayClipAtPoint(orcHit, transform.position);
        yield return new WaitForSeconds(waitTime);
        CameraFade.Out(0.2f);
        yield return new WaitForSeconds(waitTime);
        Vector3 backRot = -npcPos;
        Quaternion rotBack = Quaternion.LookRotation(backRot);
        gameObject.transform.rotation = rotBack;
        movePlayer();
        yield return new WaitForSeconds(waitTime);
        CameraFade.In(3f);
    }

    void MovePlayer()
    {
        //_player.transform.position += -_player.transform.position - _playArea.transform.localPosition;
        _player.transform.position = _spawnPoint.transform.position;
        _player.transform.rotation = _spawnPoint.transform.rotation;
    }

    IEnumerator orc2Cage(float waitTime)
    {
        //Debug.LogWarning(pathCreator.path.GetPointAtDistance(dstTravelled) + "Orctransport: " + castingScript.orcTransport);
        //Debug.LogWarning("Ende?: " + pathCreator.path.GetPoint(pathCreator.path.NumPoints - 1));
        //Debug.LogWarning(pathCreator.path.NumPoints - 1);
        dstTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(dstTravelled, end);
        //Debug.LogWarning("Orc-Position:" + transform.position);
        if (transform.position == pathCreator.path.GetPoint(pathCreator.path.NumPoints - 1))
        {
            //Debug.LogWarning("Orc ist angekommen");
            castingScript.orcTransport = false;
            castingScript.orcPosition = true;
        }
        
        yield return new WaitForSeconds(waitTime);
        //transform.rotation = pathCreator.path.GetRotationAtDistance(dstTravelled, end);
        /*if(end == EndOfPathInstruction.Stop) {
        castingScript.orcTransport = false;
        Debug.LogWarning("Orc fertig transportiert " + castingScript.orcTransport);
        }*/
    }
}
 



