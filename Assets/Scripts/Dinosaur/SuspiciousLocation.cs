using UnityEngine;

public class SuspiciousLocation
{
    public Vector3 Center;
    public float Radius;
    public float Confidence;
    public float Timestamp;
    public int Hits;
    public float maxRadius => 100f;
    float growthRate = 1.002f;

    public SuspiciousLocation(Vector3 center, SuspicionType type)
    {
        Center = center;
        Timestamp = Time.time;
        Hits = 1;
    }

    public void Reinforce(Vector3 newPos, float confidenceBoost)
    {
        Center = Vector3.Lerp(Center, newPos, 0.5f);
        Radius = Mathf.Max(Radius * 0.8f, 2f);
        Confidence = Mathf.Clamp01(Confidence + confidenceBoost);
        Hits++;
        Timestamp = Time.time;
        Debug.Log("Reinforced Suspicion: " + Confidence);
    }

    public void Decay(float halfLife)
    {
        float decay = Mathf.Pow(.95f, (Time.time - Timestamp) / halfLife);
        Confidence *= decay;
        Radius = Mathf.Min(Radius * growthRate, maxRadius); // uncertainty grows again
        Debug.Log($"Decayed Suspicion: {Confidence} (decay factor {decay})");
    }
}

public enum SuspicionType
{
    Sight
}