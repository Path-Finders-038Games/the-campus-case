using System.Collections;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Minigames.Hacking_Minigame
{
    public class GameController : Minigame
    {
        private bool _animationDone;
        private float health;
        public float Health
        {
            get { return health; }
            set
            {
                health = value; 
                healthController.ReduceHealth();
            }
        }

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
        public HealthController healthController;

        public Animator[] Animations;
        public AnimationClip Animation;
        

        public Canvas GameCanvas;

        private float _timer;
        // Start is called before the first frame update
        void Start()
        {
            gameController = this;
            _animationDone = false;
            GameSetup();
        }

        // Update is called once per frame
        void Update()
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
                            int type = Random.RandomRange(0, 3);
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
            SceneManager.LoadScene(1);
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
        }

        public override void StartGameStep()
        {
            //TBD
        }

        public override void CompleteGameStep()
        {
            ShowLocationFile();
        }

        public override void SetLocationFile()
        {
           //TBD
        }

        public override LocationFile DutchFile()
        {
            //tbd
            return null;
        }

        public override LocationFile EnglishFile()
        {
            //tbd
            return null;
        }
   
        public override void ShowLocationFile()
        {
           //TBD
        }
        public override void HideLocationFile()
        {
        //TBD
        }
        public override void SplitDialogue()
        {
          //TBD
        }
        public override void SetBuddy()
        {
          //TBD
        }
        public override void SetBuddyDialogueText(string Dialogue)
        {
           //TBD
        }
        public override void OnTextBlockClick()
        {
          //TBD
        }
        public override void UpdateDialogue()
        {
           //TBD
        }
    }
}
