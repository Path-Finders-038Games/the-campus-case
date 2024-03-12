using UnityEngine;

namespace Minigames
{
    public abstract class Minigame : MonoBehaviour
    {
        public LocationFile LocationFile;

        public void GameSetup()
        {
            SetBuddy();
            SplitDialogue();
            PrepareStep();
            StartGameStep();
        }

        public abstract void PrepareStep();
        public abstract void StartGameStep();
        public abstract void CompleteGameStep();
        public abstract void SetLocationFile();
        public abstract void ShowLocationFile();
        public abstract void HideLocationFile();
        public abstract void SetBuddy();
        public abstract void SplitDialogue();
        public abstract void SetBuddyDialogueText(string Dialogue);
        public abstract void OnTextBlockClick();
        public abstract void UpdateDialogue();
        public abstract LocationFile DutchFile();
        public abstract LocationFile EnglishFile();
    }
}