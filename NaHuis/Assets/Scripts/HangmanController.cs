using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangmanController : MonoBehaviour
{
    public GameObject head;
    public GameObject torso;
    public GameObject leftarm;
    public GameObject rightarm;
    public GameObject rightleg;
    public GameObject leftleg;

    private int tries;
    private GameObject[] parts;
    // Start is called before the first frame update
    void Start()
    {
        parts = new GameObject[] { leftleg, rightleg, rightarm, leftarm, torso, head };
        tries = parts.Length - 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
