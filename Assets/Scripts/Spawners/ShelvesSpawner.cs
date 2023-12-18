using System.Collections.Generic;
using EmployerOfTheMonth.Common;
using UnityEngine;

namespace EmployerOfTheMonth.Spawners
{
    public class ShelvesSpawner : MonoBehaviour
    {
        [SerializeField] private Shelve[] shelvePrefabs;

        private List<Shelve> shelves = new List<Shelve>();


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E)) Spawn();
        }

        [ContextMenu("Spawn")]
        private void Spawn()
        {
            DestroyShelves();

            var initialX = 3;
            var initialY = 4.5f;

            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 12; y++)
                {
                    var shelve = Instantiate(shelvePrefabs[Random.Range(0, shelvePrefabs.Length - 1)]);

                    shelve.transform.position = new Vector3(4 * x + initialX, 0, initialY + y);

                    shelves.Add(shelve);
                }
            }
        }

        private void DestroyShelves()
        {
            for (int i = 0; i < shelves.Count; i++) Destroy(shelves[i].gameObject);

            shelves.Clear();
        }
    }
}