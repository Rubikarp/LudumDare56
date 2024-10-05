using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
  public GameObject objectToSpawn; // L'objet à faire spawn
    public int maxObjects = 10; // Le nombre maximum d'objets à faire spawn
    public Vector3 spawnAreaMin; // Coin inférieur gauche de la zone de spawn
    public Vector3 spawnAreaMax; // Coin supérieur droit de la zone de spawn
    public float spawnInterval = 2f; // Temps entre les spawns
    private List<GameObject> spawnedObjects = new List<GameObject>(); // Liste des objets déjà spawnés

    void Start()
    {
        // Lancer le spawn répétitif
        InvokeRepeating("SpawnObject", 0, spawnInterval);
    }

    void SpawnObject()
    {
        // Vérifier s'il y a trop d'objets
        if (spawnedObjects.Count >= maxObjects)
        {
            return;
        }

        // Générer une position aléatoire dans la zone définie
        Vector3 randomPosition = new Vector3(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y),
            Random.Range(spawnAreaMin.z, spawnAreaMax.z)
        );

        // Instancier l'objet
        GameObject newObject = Instantiate(objectToSpawn, randomPosition, Quaternion.identity);

        // Ajouter l'objet à la liste
        spawnedObjects.Add(newObject);
    }

    public void RemoveObject(GameObject obj)
    {
        if (spawnedObjects.Contains(obj))
        {
            spawnedObjects.Remove(obj);
            Destroy(obj);
        }
    }

    // Si besoin d'un reset complet
    public void RemoveAllObjects()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            Destroy(obj);
        }
        spawnedObjects.Clear();
    }
}