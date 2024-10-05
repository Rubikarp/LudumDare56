using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnableObject
{
    public GameObject objectPrefab; // Le prefab de l'objet à spawn
    public float spawnChance; // Le pourcentage de chance de spawn (en pourcentage)
}

public class SpawnManager : MonoBehaviour
{
 public List<SpawnableObject> spawnableObjects; // Liste des objets à spawn avec leurs probabilités
    public int maxObjects = 10; // Nombre maximum d'objets à spawn
    public Vector3 spawnAreaMin; // Coin inférieur gauche de la zone de spawn
    public Vector3 spawnAreaMax; // Coin supérieur droit de la zone de spawn
    public float spawnInterval = 2f; // Intervalle de temps entre les spawns
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

        // Choisir un objet basé sur les chances de spawn
        GameObject objectToSpawn = ChooseObjectBasedOnChance();
        if (objectToSpawn == null) return; // Sécurité si aucun objet n'est sélectionné

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

    GameObject ChooseObjectBasedOnChance()
    {
        float totalChance = 0f;
        
        // Calculer la somme totale des pourcentages de tous les objets
        foreach (var spawnableObject in spawnableObjects)
        {
            totalChance += spawnableObject.spawnChance;
        }

        // Générer un nombre aléatoire entre 0 et le total des chances
        float randomValue = Random.Range(0f, totalChance);

        // Parcourir les objets et sélectionner celui correspondant à la valeur aléatoire
        float cumulativeChance = 0f;
        foreach (var spawnableObject in spawnableObjects)
        {
            cumulativeChance += spawnableObject.spawnChance;
            if (randomValue <= cumulativeChance)
            {
                return spawnableObject.objectPrefab; // Retourner le prefab sélectionné
            }
        }

        return null; // Sécurité au cas où
    }

    public void RemoveObject(GameObject obj)
    {
        if (spawnedObjects.Contains(obj))
        {
            spawnedObjects.Remove(obj);
            Destroy(obj);
        }
    }

    public void RemoveAllObjects()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            Destroy(obj);
        }
        spawnedObjects.Clear();
    }

    // Afficher la zone de spawn dans l'éditeur
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Vector3 center = (spawnAreaMin + spawnAreaMax) / 2;
        Vector3 size = spawnAreaMax - spawnAreaMin;

        Gizmos.DrawWireCube(center, size);
    }
}