using UnityEngine;

public class BoiteDissimulation : MonoBehaviour
{
    private GameObject player;
    private bool playerNearby = false;
    private bool isHidden = false;
    public KeyCode hideKey = KeyCode.E;
    
    // Add material and color properties
    private Material boxMaterial;
    public Color boxColor = new Color(0.36f, 0.25f, 0.20f); // Brown color

    void Start()
    {
        // Get the material and set initial color
        boxMaterial = GetComponent<MeshRenderer>().material;
        boxMaterial.color = boxColor;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            player = other.gameObject;
            Debug.Log("Appuyez sur E pour vous cacher");
        }
    }

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(hideKey))
        {
            isHidden = !isHidden;
            if (player != null)
            {
                // Change le tag du player quand il est caché
                player.tag = isHidden ? "HiddenPlayer" : "Player";
            }
            Debug.Log(isHidden ? "Joueur caché" : "Joueur visible");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("HiddenPlayer"))
        {
            playerNearby = false;
            isHidden = false;
            if (player != null)
            {
                player.tag = "Player";
            }
            player = null;
            Debug.Log("Plus caché");
        }
    }
}