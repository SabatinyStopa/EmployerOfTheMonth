using System.Collections.Generic;
using UnityEngine;

namespace EmployerOfTheMonth.Common
{
    public class Shelve : MonoBehaviour
    {
        [SerializeField] private Item[] itemPrefabs;
        [SerializeField] private MeshRenderer[] meshRenderers;
        [SerializeField] private int numberOfItemsToSpawn = 3;
        [SerializeField] private Bounds dividerBound;

        private List<GameObject> items = new List<GameObject>();

        void Start()
        {
            dividerBound.center = new Vector3(transform.position.x, dividerBound.center.y, transform.position.z);
            SpawnItems();
        }

        [ContextMenu("Spawn")]
        void SpawnItems()
        {
            for (int i = 0; i < items.Count; i++) Destroy(items[i]);

            items.Clear();

            foreach (MeshRenderer meshRenderer in meshRenderers)
            {
                var meshBounds = meshRenderer.bounds;
                var meshCenter = meshRenderer.bounds.center;
                var offSet = meshRenderer.bounds.extents.y;

                for (int i = 0; i < numberOfItemsToSpawn; i++)
                {
                    var newItem = Instantiate(itemPrefabs[Random.Range(0, itemPrefabs.Length - 1)]).gameObject;
                    var itemBounds = newItem.GetComponent<MeshRenderer>().bounds;
                    var randomPosition = new Vector3(
                        Random.Range(meshBounds.min.x, meshBounds.max.x),
                        0,
                        Random.Range(meshBounds.min.z, meshBounds.max.z));

                    newItem.transform.parent = meshRenderer.transform;
                    newItem.transform.position = new Vector3(randomPosition.x, meshCenter.y + offSet + itemBounds.extents.y, randomPosition.z);

                    if (dividerBound.Intersects(newItem.GetComponent<MeshRenderer>().bounds))
                    {
                        i--;
                        Destroy(newItem);
                    }

                    if (newItem != null) items.Add(newItem);
                }
            }
        }

        public void OnDrawGizmosSelected()
        {
            Gizmos.matrix = Matrix4x4.identity;
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(dividerBound.center, dividerBound.extents * 2);
        }
    }
}