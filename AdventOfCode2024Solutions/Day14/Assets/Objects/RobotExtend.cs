using UnityEngine;

public class RobotExtend : MonoBehaviour
{
    public Vector2 Velocity = Vector2.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision colli)
    {
        if (colli.gameObject.CompareTag("Robot"))
        {
            Physics.IgnoreCollision(GetComponentInChildren<Collider>(), colli.collider);
        }
        else
        {
            Debug.Log("Robot collided with " + colli.gameObject.name);
        }
        
        
    }
}
