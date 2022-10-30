using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGolfTest : MonoBehaviour
{
    public float force;

    public bool canShot;
    public float maxShotDistance;
    public float minVelocety;

    public LayerMask groundLayer;
    public Transform playerCamFollowBox;
    public Camera cam;


    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(canShot)
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            
            
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                if(Vector3.Distance(transform.position, hit.point)  <= maxShotDistance)
                {
                    playerCamFollowBox.gameObject.SetActive(true);

                    CalculateForce(hit);
                    playerCamFollowBox.position = hit.point;

                    if(Input.GetMouseButtonDown(0))
                    {
                        rb.AddForce((transform.position - hit.point).normalized * force, ForceMode.Impulse);
                    }
                }
                else
                {
                    playerCamFollowBox.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            CalculateSpeed();
        }
        
    }

    public void CalculateForce(RaycastHit hit2)
    {
        force = Mathf.Lerp(0, 5, Mathf.InverseLerp(0, maxShotDistance, Vector3.Distance(transform.position, hit2.point)));
    }

    public void CalculateSpeed()
    {
        if(rb.velocity.x <= minVelocety && rb.velocity.y <= minVelocety && rb.velocity.z <= minVelocety)
        {
            rb.velocity = Vector3.zero;
            canShot = true;
            playerCamFollowBox.gameObject.SetActive(true);
        }
        else
        {
            canShot = false;
            playerCamFollowBox.gameObject.SetActive(false);
        }
    }
}
