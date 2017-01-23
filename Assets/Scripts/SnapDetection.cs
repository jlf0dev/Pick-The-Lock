using UnityEngine;
using System.Collections;

public class SnapDetection : MonoBehaviour {

    public float maxSpeedUp;

	void OnTriggerExit2D (Collider2D other)
    {
        if (other.tag.Equals("Top Tumbler"))
        {
            if(other.attachedRigidbody.velocity.y < maxSpeedUp && other.attachedRigidbody.velocity.y > 0)
            {
                other.attachedRigidbody.isKinematic = true;
            }
        }
    }
}
