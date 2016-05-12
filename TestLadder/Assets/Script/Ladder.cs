using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class Ladder : MonoBehaviour {

    public Collider Col;
    public Collider Trig;
    private GameObject Player;
    private bool climbing;
    public float speed = 200;

    void Start()
    {
        Physics.IgnoreCollision(Col, Trig);
        Player = GameObject.Find("FPSController");
        climbing = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            climbing = true;
            Player.GetComponent<Rigidbody>().useGravity = false;
            Player.GetComponent<Rigidbody>().isKinematic = false;
            Player.GetComponent<FirstPersonController>().m_WalkSpeed = 1;
            Player.GetComponent<FirstPersonController>().m_RunSpeed = 1;
            Player.GetComponent<FirstPersonController>().m_StickToGroundForce = 0;
            Player.GetComponent<FirstPersonController>().m_GravityMultiplier = 0;
            Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
        {
            climbing = false;
            Player.GetComponent<Rigidbody>().useGravity = true;
            Player.GetComponent<Rigidbody>().isKinematic = true;
            Player.GetComponent<FirstPersonController>().m_WalkSpeed = 3;
            Player.GetComponent<FirstPersonController>().m_RunSpeed = 6;
            Player.GetComponent<FirstPersonController>().m_StickToGroundForce = 6;
            Player.GetComponent<FirstPersonController>().m_GravityMultiplier = 2;
        }
    }

    void Update()
    {
        if (climbing == true)
        {
            if (Input.GetKey(KeyCode.Z) && Player.transform.GetChild(0).transform.rotation.x < 0)
            {
                Player.transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * speed);
            }
            else if(Player.transform.GetChild(0).transform.position.y > 1.8f && Player.transform.GetChild(0).transform.rotation.x >= 0)
            {
                Player.transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * speed);
            }
        }
    }
}
