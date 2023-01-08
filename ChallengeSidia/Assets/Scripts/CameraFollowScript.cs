using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothScale = 0.2f, speed = 3f;

    public bool auto = true;

    private void LateUpdate()
    {
        if (auto)
        {
            Vector3 destination = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, destination, smoothScale);

            if(Input.GetAxis("Horizontal")!=0 || Input.GetAxis("Vertical")!=0)
            {
                auto = false;
            }
        }
        else
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            transform.position += new Vector3(x, 0, y) * speed * Time.deltaTime;

            if (Input.GetKey(KeyCode.Space))
            {
                auto = true;
            }
        }

        
    }
}
