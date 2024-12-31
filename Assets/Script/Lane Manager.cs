using UnityEngine;

public class LaneManager : MonoBehaviour
{
    [SerializeField] private GameObject[] laneObjects; // GameObjects des lignes
    [SerializeField] private Material defaultMaterial; // Matériau par défaut
    [SerializeField] private Material highlightedMaterial; // Matériau avec forte émission

    private int previousLane = -1; // Garder trace de la précédente ligne active

    public void HighlightLane(int currentLane)
    {
        // Réinitialiser la précédente ligne
        if (previousLane != -1 && previousLane < laneObjects.Length)
        {
            laneObjects[previousLane].GetComponent<Renderer>().material = defaultMaterial;
        }

        // Appliquer le matériau lumineux à la ligne actuelle
        if (currentLane >= 0 && currentLane < laneObjects.Length)
        {
            laneObjects[currentLane].GetComponent<Renderer>().material = highlightedMaterial;
            previousLane = currentLane;
        }
    }
}
