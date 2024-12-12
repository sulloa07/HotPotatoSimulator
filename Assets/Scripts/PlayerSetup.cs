using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    [SerializeField] 
    private GameObject diamondPrefab;
    private GameObject playerDiamond;
    private Light playerSpotlight;

    void Start()
    {
        CreateSpotlight();
        SpawnDiamond();
    }

    private void CreateSpotlight()
    {
        GameObject spotlightObj = new GameObject("PlayerSpotlight");
        spotlightObj.transform.parent = transform;
        spotlightObj.transform.localPosition = new Vector3(0, 4f, 0);
        spotlightObj.transform.localRotation = Quaternion.Euler(90, 0, 0);

        playerSpotlight = spotlightObj.AddComponent<Light>();
        playerSpotlight.type = LightType.Spot;
        playerSpotlight.intensity = 5f;
        playerSpotlight.range = 10f;
        playerSpotlight.spotAngle = 60f;
        playerSpotlight.color = Color.green;
    }

        private void SpawnDiamond()
    {
        if (diamondPrefab != null)
        {
            // Adjust the rotation to ensure the diamond is upright
            Quaternion rotation = Quaternion.Euler(-90, 0, 0); // Rotate 90 degrees around X axis to make it upright
            playerDiamond = Instantiate(diamondPrefab, transform.position + new Vector3(0, 2.5f, 0), rotation);
            playerDiamond.transform.parent = transform;
    
            if (!playerDiamond.GetComponent<AnimationScript>())
            {
                AnimationScript anim = playerDiamond.AddComponent<AnimationScript>();
                anim.isRotating = true;
                anim.rotationAngle = new Vector3(0, 1, 0);
                anim.rotationSpeed = 50f;
            }
        }
    }
}