using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGolfTest : MonoBehaviour
{
    [Header("Forces Settings")]
    public float force;
    public float maxForce = 30;
    public bool isPressed;         

    [Header("Shot Settings")]
    public bool canShot;
    public float maxShotDistance;
    public float minVelocety;

    public float timeToMaxShotLV = 4f;
    float timeToMaxShotLVReference = 4f;
    bool released;

    [Header("References")]
    public LayerMask groundLayer;
    public Transform playerCamFollowBox;
    public GameObject shotRadius;
    public Camera cam;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            if(Input.GetMouseButtonDown(0))
            {
                timeToMaxShotLVReference += Time.deltaTime;

                //HoldDownTimer
                if(timeToMaxShotLVReference >= timeToMaxShotLV)
                {
                    Shot(CalculateDirection(hit));
                }
                //shotRadius.SetActive(true);
            }

            if(Input.GetMouseButtonUp(0))
            {
                force *= timeToMaxShotLVReference;

                released = true;
                Shot(CalculateDirection(hit));

                timeToMaxShotLVReference = timeToMaxShotLV;
            }

            /*
            if(Vector3.Distance(transform.position, hit.point)  <= maxShotDistance)
            {
                CalculateForce(hit);
                playerCamFollowBox.position = hit.point;

                if(Input.GetMouseButtonDown(0))
                {
                    playerCamFollowBox.gameObject.SetActive(true);
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
                    
                }

                if(Input.GetMouseButtonUp(0))
                {
                    rb.AddForce(new Vector3(playerCamFollowBox.position.x - hit.point.x, 0, playerCamFollowBox.position.z - hit.point.z).normalized * force, ForceMode.Impulse);
                    playerCamFollowBox.gameObject.SetActive(false);
                    shotRadius.SetActive(false);
                }
            }
            */
        }

        
        if(rb.velocity.x < minVelocety && rb.velocity.x >= -minVelocety)
        {
            if(rb.velocity.z < minVelocety && rb.velocity.z >= -minVelocety)
            {
                Debug.Log(rb.velocity.z);

                shotRadius.SetActive(true);
            }
            else
            {
                shotRadius.SetActive(false);
                playerCamFollowBox.gameObject.SetActive(false);
            }
        }
        else
        {
            shotRadius.SetActive(false);
            playerCamFollowBox.gameObject.SetActive(false);
        }


        
    }

    void CalculateForce(RaycastHit hit2)
    {
        force = Mathf.Lerp(0, maxForce, Mathf.InverseLerp(0, maxShotDistance, Vector3.Distance(transform.position, hit2.point)));
    }

    Vector3 CalculateDirection(RaycastHit hit)
    {
        return new Vector3(transform.position.x - hit.point.x, 0, transform.position.z - hit.point.z).normalized;
    }

    void Shot(Vector3 direction)
    {
        rb.AddForce(direction * force, ForceMode.Impulse);    
    }

    void LoadUpShot(RaycastHit hit)
    {
        if(Vector3.Distance(transform.position, hit.point)  <= maxShotDistance)
        {
            CalculateForce(hit);
            playerCamFollowBox.position = hit.point;

            if(Input.GetMouseButtonDown(0))
            {
                playerCamFollowBox.gameObject.SetActive(true);
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
                
            }

            if(Input.GetMouseButtonUp(0))
            {
                rb.AddForce(new Vector3(playerCamFollowBox.position.x - hit.point.x, 0, playerCamFollowBox.position.z - hit.point.z).normalized * force, ForceMode.Impulse);
                playerCamFollowBox.gameObject.SetActive(false);
                shotRadius.SetActive(false);
            }
        }
    }

    /*
    string SimpleRayCast(Vector2 origin, Vector2 direction, float maxDistance,LayerMask layer)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxDistance, layer);
        if(hit.collider != null)
        {
            return hit.collider.name;
        }
        else
        {
            return null;
        }
    }
    */
}