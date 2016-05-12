using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class Ladder : MonoBehaviour {

    public Collider Col;
    public Collider Trig;
    private GameObject Player;
    private bool climbing;
    public float speed = 200;
    public float floorPos = 60.5f;
    private FirstPersonController before;

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
            Player.GetComponent<FirstPersonController>().m_Ladder = true;
            climbing = true;
            Player.GetComponent<Rigidbody>().useGravity = false;
            Player.GetComponent<Rigidbody>().isKinematic = false;
            Player.GetComponent<FirstPersonController>().m_WalkSpeed = 0.4f;
            Player.GetComponent<FirstPersonController>().m_RunSpeed = 0.4f;
            Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
        {
            Player.GetComponent<FirstPersonController>().m_Ladder = false;
            climbing = false;
            Player.GetComponent<Rigidbody>().useGravity = true;
            Player.GetComponent<Rigidbody>().isKinematic = true;
            Player.GetComponent<FirstPersonController>().m_WalkSpeed = 3;
            Player.GetComponent<FirstPersonController>().m_RunSpeed = 5;
        }
    }

    void Update()
    {
        if (climbing == true)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Player.GetComponent<FirstPersonController>().m_MoveDir = new Vector3(Player.GetComponent<FirstPersonController>().m_MoveDir.x, 0, Player.GetComponent<FirstPersonController>().m_MoveDir.z);
            }
            else
            {
                if (Input.GetKey(KeyCode.Z))
                {
                    Player.GetComponent<FirstPersonController>().m_MoveDir = new Vector3(Player.GetComponent<FirstPersonController>().m_MoveDir.x, speed, Player.GetComponent<FirstPersonController>().m_MoveDir.z);
                }
                else if (!Input.GetKey(KeyCode.Z) && Player.transform.GetChild(0).transform.position.y > floorPos)
                {
                    Player.GetComponent<FirstPersonController>().m_MoveDir = new Vector3(Player.GetComponent<FirstPersonController>().m_MoveDir.x, -speed, Player.GetComponent<FirstPersonController>().m_MoveDir.z);
                }
            }
        }
    }
}
