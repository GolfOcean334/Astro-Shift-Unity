using UnityEngine;

public class LaneManager : MonoBehaviour
{
    [SerializeField] private GameObject[] laneObjects; // GameObjects des lignes
    [SerializeField] private Material defaultMaterial; // Mat�riau par d�faut
    [SerializeField] private Material highlightedMaterial; // Mat�riau avec forte �mission

    private int previousLane = -1; // Garder trace de la pr�c�dente ligne active

    public void HighlightLane(int currentLane)
    {
        // R�initialiser la pr�c�dente ligne
        if (previousLane != -1 && previousLane < laneObjects.Length)
        {
            laneObjects[previousLane].GetComponent<Renderer>().material = defaultMaterial;
        }

        // Appliquer le mat�riau lumineux � la ligne actuelle
        if (currentLane >= 0 && currentLane < laneObjects.Length)
        {
            laneObjects[currentLane].GetComponent<Renderer>().material = highlightedMaterial;
            previousLane = currentLane;
        }
    }
}
