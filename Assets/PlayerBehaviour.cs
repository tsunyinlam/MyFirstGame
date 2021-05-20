using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    // Never set the value of a public variable here lmao
    // Inspector would override it without informing you
    // Use Start() to override inspector if necessary
    public float expQuotient;
    public float speed; //Meter per second / Unit per second? Apparantely
    public GameObject bulletPrefab;
    public float bulletSpawnDistance;

    float MoveMultiplier(float maxDistanceTravelled, float expQuotient, float axis)
    {
        float moveMultiplier;
        moveMultiplier = (float)((float)maxDistanceTravelled * (1 / (1 + Mathf.Exp(-expQuotient * axis)) - 0.5) / (1 / (1 + Mathf.Exp(-expQuotient)) - 0.5));
        return moveMultiplier;
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        // Movement
        Debug.Log(Input.GetAxis("Vertical"));

        float maxDistanceTravelled = speed * Time.deltaTime;

        float vertAxis = Input.GetAxis("Vertical");
        float vertMoveMultiplier = MoveMultiplier(maxDistanceTravelled, expQuotient, vertAxis);

        float horzAxis = Input.GetAxis("Horizontal");
        float horzMoveMultiplier = MoveMultiplier(maxDistanceTravelled, expQuotient, horzAxis);

        Vector3 movementVector = new Vector3(horzMoveMultiplier, 0, vertMoveMultiplier);
        Vector3 newPosition = transform.position + movementVector;

        transform.LookAt(newPosition);

        //Move
        transform.position = newPosition;

        //Click to fire
        //If clicked, create a bullet at current position

        if (Input.GetButton("Fire1")) {
            Instantiate(bulletPrefab, transform.position + bulletSpawnDistance * transform.forward, transform.rotation);
        }

        

    }
}
