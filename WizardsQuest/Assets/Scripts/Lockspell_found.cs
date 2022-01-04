
using UnityEngine;

public class Lockspell_found : MonoBehaviour
{
    private GameObject player;
    private Spells spellScript;
    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.Find("Player");
        player = GameObject.FindWithTag("Player");
        spellScript = player.GetComponent<Spells>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.LogWarning(transform.position.x);
        //Debug.LogWarning(transform.name);
        if (transform.position.x < 3.39f)
        {
            spellScript.schlossZauber = true;
        }
    }
}
