using UnityEngine;

public class FireSensor : MonoBehaviour
{
    public MovingWall targetWall;
    bool activated = false;

    public void OnParticleCollision(GameObject other)
    {
        Debug.Log(other.tag);
        if (activated) return;

        if (other.CompareTag("Fire"))
        {
            activated = true;
            targetWall.Open();
        }
    }
}