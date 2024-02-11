using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

namespace EmployerOfTheMonth.Quests
{
    public class QuestInitializer : MonoBehaviour
    {
        [System.Serializable]
        private struct PrefabsOfQuestInitializer
        {
            public GameObject Prefab;
            public bool DestroyOnFinish;
        }
        [SerializeField] protected QuestScriptable questScriptable;
        [SerializeField] protected UnityEvent[] initializeEvents;
        [SerializeField] protected UnityEvent[] finishEvents;
        [SerializeField] private PrefabsOfQuestInitializer[] prefabsOfQuestInitializers;

        private List<GameObject> gameObjectsToDestroyOnFinish = new List<GameObject>();

        public virtual void Initialize()
        {
            QuestManager.InitializeQuest(questScriptable, OnComplete, OnFail, OnInitializeQuest);

            foreach (UnityEvent initializeEvent in initializeEvents)
                initializeEvent?.Invoke();

            foreach (PrefabsOfQuestInitializer prefabsOfQuestInitializer in prefabsOfQuestInitializers)
            {
                var instantiatedPrefab = Instantiate(prefabsOfQuestInitializer.Prefab);

                if (prefabsOfQuestInitializer.DestroyOnFinish)
                    gameObjectsToDestroyOnFinish.Add(instantiatedPrefab);
            }
        }

        public virtual void OnFail()
        {

        }

        public virtual void OnInitializeQuest()
        {

        }

        public virtual void OnComplete()
        {
            foreach (UnityEvent finishEvent in finishEvents)
                finishEvent?.Invoke();

            for (int i = 0; i < gameObjectsToDestroyOnFinish.Count; i++)
            {
                Destroy(gameObjectsToDestroyOnFinish[i]);
            }

            Destroy(gameObject);
        }

    }
}
