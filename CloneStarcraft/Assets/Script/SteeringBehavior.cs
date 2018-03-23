using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehavior : MonoBehaviour {

    public float MaxForce;
    public float MaxSpeed;
    public float Mass;

    public Vector3 ? Target { get; set; }
    public Vector3 ? Destination { get; set; }
    public Vector3 velocity;
    private Vector3 orientation;
    public Vector3 steering;
    public float SlowingRadius { get; private set; }

    // Use this for initialization
    void Start () {
        velocity = new Vector3();
        SlowingRadius = MaxSpeed * 4;
        orientation = new Vector3();
        Target = null;
        Destination = null;
	}

    private void Seek()
    {
        Rect rectZone = new Rect(new Vector2(Destination.Value.x - 1, Destination.Value.z - 1), new Vector2(2, 2));
        if (rectZone.Contains(new Vector2(gameObject.transform.position.x, gameObject.transform.position.z)))
        {
            Destination = null;
            velocity = new Vector3();
            GetComponent<Actions>().Stay();
            return;
        }

        Vector3 targetOffset = Destination.Value - gameObject.transform.position;
        float distance = targetOffset.magnitude;
        float rampedSpeed = MaxSpeed * (distance / SlowingRadius);
        float clippedSpeed = Mathf.Min(rampedSpeed, MaxSpeed);
        Vector3 desiredVelocity = (clippedSpeed / distance) * targetOffset;

        steering = desiredVelocity - velocity;
    }

    private void Flee(Vector3 point)
    {

    }

    private void Pursuit(GameObject obj)
    {

    }

    private void Evasion()
    {

    }

    private void Arrival()
    {

    }
	
	// Update is called once per frame
	void Update () {
        steering = Vector3.zero;
        // Steering calcul

        if (Destination.HasValue)
            Seek();

        steering = Vector3.ClampMagnitude(steering, MaxSpeed);
        Vector3 acceleration = steering / Mass;
        velocity = Vector3.ClampMagnitude(velocity + acceleration, MaxSpeed);
        orientation = (gameObject.transform.position + velocity) - gameObject.transform.position;
        orientation.y = 0;
        gameObject.transform.position += velocity;

        if (orientation != Vector3.zero)
        {
            gameObject.transform.rotation = Quaternion.LookRotation(orientation);
        }

        GetComponent<WorldObject>().CalculateBounds();
	}
}
