using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [Serializable]
    public class PlayerSpawnLocation
    {
        public string name;
        public Transform spawnPoint;
    }

    public List<PlayerSpawnLocation> spawnLocations;
    public Transform defaultSpawn;

    public Transform GetSpawnLocation(string spawnPointName)
    {
        var location = spawnLocations.Find(p => p.name == spawnPointName);
        if (location != null)
        {
            return location.spawnPoint;
        }

        return defaultSpawn;
    }
}
