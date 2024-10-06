using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;

[System.Serializable]
public class SpawnableFish
{
    [Expandable]
    public FishData fishData; // Le prefab de l'objet à spawn
    [Range(0, 100)]
    public float spawnChance = 0; // Le pourcentage de chance de spawn (en pourcentage)
}

public class FishSpawner : MonoBehaviour
{
    public Fish fishPrefab; // Le prefab de l'objet à spawn
    public Transform fishTank;
    public int maxObjects = 10; // Nombre maximum d'objets à spawn
    public List<SpawnableFish> spawnableObjects; // Liste des objets à spawn avec leurs probabilités
    private List<Fish> spawnedObjects = new List<Fish>(); // Liste des objets déjà spawnés
    public float spawnInterval = 2f; // Intervalle de temps entre les spawns

    private Coroutine spawnLoop;
    public FishZone spawnArea;
    public FishZone moveArea;

    private void Start()
    {
        spawnLoop = StartCoroutine(SpawningLoop());
        NormalisedChance();
    }

    public IEnumerator SpawningLoop()
    {
        while (true)
        {
            // Lancer le spawn répétitif
            SpawnObject();
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    private FishData ChooseObjectBasedOnChance()
    {
        // Calculer la somme totale des pourcentages de tous les objets
        float totalChance = spawnableObjects.Sum(obj => obj.spawnChance);

        // Générer un nombre aléatoire entre 0 et le total des chances
        float randomValue = Random.Range(0f, totalChance);

        // Parcourir les objets et sélectionner celui correspondant à la valeur aléatoire
        foreach (var spawnableObject in spawnableObjects)
        {
            if (randomValue < spawnableObject.spawnChance)
            {
                return spawnableObject.fishData;
            }
            randomValue -= spawnableObject.spawnChance;
        }
        return null; // Sécurité au cas où
    }
    private void SpawnObject()
    {
        // Vérifier s'il y a trop d'objets
        if (spawnedObjects.Count >= maxObjects)
        {
            return;
        }

        // Choisir un objet basé sur les chances de spawn
        FishData fishDataToSpawn = ChooseObjectBasedOnChance();
        if (fishDataToSpawn == null)// Sécurité si aucun objet n'est sélectionné
        {
            Debug.LogWarning("No object to spawn", this);
            return;
        }

        // Générer une position aléatoire dans la zone définie
        Vector3 randomPosition = spawnArea.Area.RandomPointInRect();

        // Instancier l'objet
        Fish newFish = Instantiate(fishPrefab, randomPosition, Quaternion.identity, fishTank);
        newFish.Init(moveArea, fishDataToSpawn);
        spawnedObjects.Add(newFish);
    }

    public void RemoveObject(Fish fish)
    {
        if (spawnedObjects.Contains(fish))
        {
            spawnedObjects.Remove(fish);
            Destroy(fish);
        }
    }
    public void RemoveAllObjects()
    {
        foreach (Fish fish in spawnedObjects)
        {
            Destroy(fish);
        }
        spawnedObjects.Clear();
    }

    [Button]
    private void NormalisedChance()
    {
        // Make sum of spawn chances equal to 100
        float totalChance = 0f;
        foreach (var spawnableObject in spawnableObjects)
        {
            totalChance += spawnableObject.spawnChance;
        }

        if (totalChance != 100)
        {
            float ratio = 100 / totalChance;
            foreach (var spawnableObject in spawnableObjects)
            {
                spawnableObject.spawnChance *= ratio;
            }
        }
    }
}