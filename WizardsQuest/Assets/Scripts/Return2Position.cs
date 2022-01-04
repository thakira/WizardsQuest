using System.Collections;
using System.Threading;
using UnityEngine;



public class Return2Position : MonoBehaviour
{
    [SerializeField] private BoxCollider floor;

    [SerializeField] private float waitTime;
    [SerializeField] private GameObject kesselreiniger;
    private Vector3 initialLocalPosition;
    private Quaternion initialLocalRotation;

    private void Start()
    {
        initialLocalPosition = transform.position;
        initialLocalRotation = transform.rotation;
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.collider == floor)
        {
            StartCoroutine(RePosition());
        }
    }

    IEnumerator RePosition()
    {
        yield return new WaitForSeconds(waitTime);
        transform.position = initialLocalPosition;
        transform.rotation = initialLocalRotation;
    }
}