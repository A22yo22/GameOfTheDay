using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGolfTest : MonoBehaviour
{
    public float force;
    public float maxForce = 30;
    public bool isPressed;         

    public bool canShot;
    public float maxShotDistance;
    public float minVelocety;

    public LayerMask groundLayer;
    public Transform playerCamFollowBox;
    public GameObject shotRadius;
    public Camera cam;


    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        
        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            if(Vector3.Distance(transform.position, hit.point)  <= maxShotDistance)
            {
                CalculateForce(hit);
                playerCamFollowBox.position = hit.point;

                if(Input.GetMouseButtonDown(0))
                {
                    playerCamFollowBox.gameObject.SetActive(true);
                    shotRadius.SetActive(true);
                    
                }

                if(Input.GetMouseButtonUp(0))
                {
                    rb.AddForce(new Vector3(transform.position.x - hit.point.x, 0, transform.position.z - hit.point.z).normalized * force, ForceMode.Impulse);
                    playerCamFollowBox.gameObject.SetActive(true);
                    shotRadius.SetActive(false);
                }
            }
            else
            {
                if(Input.GetMouseButtonDown(0))
                {
                    playerCamFollowBox.gameObject.SetActive(true);
                    shotRadius.SetActive(true);
                    
                }

                if(Input.GetMouseButtonUp(0))
                {
                    rb.AddForce(new Vector3(playerCamFollowBox.position.x - hit.point.x, 0, playerCamFollowBox.position.z - hit.point.z).normalized * force, ForceMode.Impulse);
                    playerCamFollowBox.gameObject.SetActive(true);
                    shotRadius.SetActive(false);
                }
            }
        }

        if(rb.velocity.x <= minVelocety && rb.velocity.z <= minVelocety)
        {
            //playerCamFollowBox.gameObject.SetActive(false);
        }
    }

    public void CalculateForce(RaycastHit hit2)
    {
        force = Mathf.Lerp(0, maxForce, Mathf.InverseLerp(0, maxShotDistance, Vector3.Distance(transform.position, hit2.point)));
    }
}
