using System;
using UnityEngine;

public static class Helper
{
    public static Vector3 RandomLocation(
        Transform self, Terrain terrain,
        float maxAngle = 90f, float radius = 100f, float minRadius = 20f)
    {
        // Use horizontal forward only (ignore pitch)
        Vector3 fwdXZ = Vector3.ProjectOnPlane(self.forward, Vector3.up).normalized;
        if (fwdXZ.sqrMagnitude < 1e-6f) fwdXZ = self.right; // fallback

        // Yaw within ±maxAngle/2 around the UP axis, not around self.forward
        float half = maxAngle * 0.5f;
        float yaw = UnityEngine.Random.Range(-half, half);
        Vector3 dir = Quaternion.AngleAxis(yaw, Vector3.up) * fwdXZ;

        // Distance within [minRadius, radius]
        float dist = UnityEngine.Random.Range(minRadius, radius);

        // Position in world space
        Vector3 pos = self.position + dir * dist;

        // Terrain height (account for terrain world Y)
        float terrainBaseY = terrain.GetPosition().y;
        pos.y = terrain.SampleHeight(pos) + terrainBaseY;

        return pos;
    }

    public static Vector3 RandomLocationWithinRadiusNoDirection(
        Vector3 self, Terrain terrain, float radius = 100f, float minRadius = 20f)
    {
        // Random point within a sphere
        Vector3 randomPoint = UnityEngine.Random.insideUnitSphere * radius;
        randomPoint.y = 0; // Flatten to 2D

        // Position in world space
        Vector3 pos = self + randomPoint;

        // Terrain height (account for terrain world Y)
        float terrainBaseY = terrain.GetPosition().y;
        pos.y = terrain.SampleHeight(pos) + terrainBaseY;

        return pos;
    }
}