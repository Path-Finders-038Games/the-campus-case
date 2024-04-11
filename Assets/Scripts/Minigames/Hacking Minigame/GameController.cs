using System.Collections;
using Dialog;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;

namespace Minigames.Hacking_Minigame
{
    public class GameController : Minigame
    {
        public float Health;
        public float SpawnDelay;
        public int SpawnAmount;
        public int EnemyAlive;
        public GameObject Enemy;
        public GameObject Weak;
        public bool PlayGame;
        public GameObject SpawnHeight;
        public GameObject[] Lanes;
        public GameObject EndScreen;
        public int CurrentLane;
        public static GameController gameController;

        public Animator[] Animations;
        public AnimationClip Animation;
        private bool _animationDone;

        public Button HideLocationFileButton;
        bool locationFileClosed;
        public Canvas GameCanvas;


        private float _timer;
        // Start is called before the first frame update
        public override void Start()
        {
            gameController = this;
            _animationDone = false;
            locationFileClosed = false;
            
            base.Start();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateDialogue();
            if (locationFileClosed)
            {
                _timer += Time.deltaTime;
                if (!_animationDone)
                {
                    StartCoroutine(WaitForAnimation());
                }
                if (_animationDone)
                {


                    if (PlayGame)
                    {

                        if (_timer > SpawnDelay && SpawnAmount != 0)
                        {
                            SpawnAmount--;
                            GameObject spawn;
                            int type = Random.Range(0, 3);
                            if (type < 2)
                            {
                                spawn = Weak;
                            }
                            else
                            {
                                spawn = Enemy;
                            }

                            _timer = 0;
                            int spawnLocation = Random.Range(0, 3);
                            Instantiate(spawn, LanePos(spawnLocation), Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
                        }

                        if (Health == 0)
                        {
                            GameLost();
                        }
                        else if (EnemyAlive == 0)
                        {
                            GameWon();
                        }


                    }
                }
            }
        }

        public Vector3 LanePos(int lane)
        {
            return new Vector3(Lanes[lane].transform.position.x, SpawnHeight.transform.position.y, Lanes[lane].transform.position.z);
        }

        public Vector3 TextPos()
        {
            return new Vector3(Lanes[1].transform.position.x, Lanes[1].transform.position.y, Lanes[1].transform.position.z);

        }

        void GameWon()
        {
            PlayGame = false;
            
            DataManager.SetMinigameStatus(MinigameName.Hacking, true);
            
            GameObject winScreen = EndScreen.GetNamedChild("WinText");
            Instantiate(winScreen, TextPos(), Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);

            StartCoroutine(Wait());
        }

        void GameLost()
        {
            PlayGame = false;
            GameObject winScreen = EndScreen.GetNamedChild("LoseText");
            Instantiate(winScreen, TextPos(), Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);

            StartCoroutine(BackToNavigation());
        }

        IEnumerator BackToNavigation()
        {
            yield return new WaitForSeconds(3);
            SceneLoader.LoadScene(GameScene.Navigation);
        }

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(3);
            CompleteGameStep();
        
        }

        IEnumerator WaitForAnimation()
        {
            yield return new WaitForSeconds(3);
            _animationDone = true;
            PlayGame = true;
        }

        public override void PrepareStep()
        {
            gameController = this;
            PlayGame = false;
            EnemyAlive = SpawnAmount;
            SetLocationFile();
            HideLocationFileButton.onClick.AddListener(HideLocationFile);
        }

        public override void StartGameStep()
        {
            ShowLocationFile();
        }

        public override void CompleteGameStep()
        {
            LocationUIHintNextLocation.text = "Hint for next location \n" + LocationFile.HintNextLocation;
            LocationFile.IsCompleted = true;
            ShowLocationFile();
        }
   
        public override void ShowLocationFile()
        {
            PlayGame = false;
            LocationFileUI.SetActive(true);
        }
        public override void HideLocationFile()
        {
            LocationFileUI.SetActive(false);
            PlayGame = true;
            locationFileClosed= true;
            if (LocationFile.IsCompleted)
            {
                SceneLoader.LoadScene(GameScene.Navigation);
            }

            foreach (Animator item in Animations)
            {
                item.SetTrigger("Test");
            }
        }
        public override void SplitDialogue()
        {
            TutorialDialogues.Add(DialogueManagerV2.GetDialogue("LocalizationDialogue", "hackerMinigame_0"));
            TutorialDialogues.Add(DialogueManagerV2.GetDialogue("LocalizationDialogue", "hackerMinigame_1"));
            WonDialogues.Add(DialogueManagerV2.GetDialogue("LocalizationDialogue", "hackerMinigame_2"));
        }
    }
}
