using UnityEngine;

public static class Helper
{
    public static Vector3 RandomLocation(Transform self, Terrain terrain, float radius = 50f, float minRadius = 20f)
    {
        UnityEngine.Vector2 randomOffset = UnityEngine.Random.insideUnitCircle * Random.Range(minRadius, radius);
        
        UnityEngine.Vector3 randomPosition =
            new UnityEngine.Vector3(self.position.x, 0.0f, self.position.z)
            + new UnityEngine.Vector3(randomOffset.x, 0.0f, randomOffset.y);

        randomPosition.y = terrain.SampleHeight(randomPosition);

        return randomPosition;
    }    

}