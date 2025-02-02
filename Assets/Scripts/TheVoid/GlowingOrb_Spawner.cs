using UnityEngine;

public class GlowingOrb_Spawner : MonoBehaviour
{
    public GameObject glowingOrb;

    public void SpawnGlowingOrb() {
        Instantiate(glowingOrb);
    }
}
