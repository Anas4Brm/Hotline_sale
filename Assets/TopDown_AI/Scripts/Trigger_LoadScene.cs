using UnityEngine;
using UnityEngine.SceneManagement;

public class Trigger_LoadNextScene : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Vérifie si le joueur entre dans le déclencheur
        {
            LoadNextScene(); // Charge la scène suivante
        }
    }

    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Index de la scène actuelle
        int nextSceneIndex = currentSceneIndex + 1; // Index de la scène suivante

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) // Vérifie si la scène suivante existe
        {
            SceneManager.LoadScene(nextSceneIndex); // Charge la scène suivante
        }
        else
        {
            Debug.LogWarning("No next scene available. This is the last scene.");
        }
    }
}