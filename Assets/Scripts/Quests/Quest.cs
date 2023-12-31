using EmployerOfTheMonth.Common;
using System;

namespace EmployerOfTheMonth.Quests
{
    [Serializable]
    public class Quest
    {
        protected Action onComplete;
        protected Action onFail;
        protected string id;
        protected string shortDescription;
        protected string instructions;
        protected bool isRunning;

        public string Id { get => id; }

        public Quest(string id, string shortDescription, string instructions, Action onComplete, Action onFail)
        {
            this.id = id;
            this.onComplete = onComplete;
            this.shortDescription = shortDescription;
            this.instructions = instructions;
            this.onFail = onFail;
        }

        public virtual void Initialize()
        {
            if (isRunning) return;

            isRunning = true;
            UIManager.SetQuestText(shortDescription);
            UIManager.SetBottomText(instructions);
        }

        public virtual void Complete()
        {
            UIManager.SetBottomText("Congratulations! You completed!");
            onComplete?.Invoke();
        }

        public virtual void Fail()
        {
            UIManager.SetBottomText("YOU FAILED!");
            onFail?.Invoke();           
        }
    }
}