using System.Collections;
using Dialog;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
//fucking unity
namespace Minigames.Hacking_Minigame
{
    public class GameController : Minigame
    {

        //variables to add animations
        [Header("Animations")]
        //list of animations to play
        public Animator[] Animations;

        //variables related to the locationfile
        [Header("Locationfile variables")]
        //button to hide the locationfile
        public Button HideLocationFileButton;
        //boolean to check if it is hidden
        private bool locationFileClosed;

        //background of the game
        public Canvas GameCanvas;

        //variables related to the behaviour of the game
        [Header("Game variables")]
        //health counter
        private int _health = 3;

        //property of health with connection to the healthbar
        public int Health
        {
            get { return _health; }
            set
            {
                _health = value;
                HealthControllerProperty.ChangeHealthSprite(_health);
            }
        }

        //delay between enemy spawns
        public float SpawnDelay;

        //total amount of enemies that will spawn
        public int SpawnAmount;

        //count of enemies currently alive
        public int EnemyAlive;

        //enemy type 1
        public GameObject StrongEnemy;

        //enemy type 2
        public GameObject WeakEnemy;

        //check for if the game is currently active
        public bool PlayingGame;

        //height of the spawner
        public GameObject SpawnHeight;

        //lanes for the game to take place in
        public GameObject[] Lanes;

        //the endscreen
        public GameObject EndScreen;

        //lane the player is currently in
        public int CurrentLane;

        // static instance of the gamecontroller
        public static GameController gameController;

        //the health controller
        public  HealthController HealthControllerProperty;

        //the combat controller
        public CombatController CombatControllerProperty;

        // timer to track passing of time
        private float _timer;
        //check to see if the animation has finished
        private bool _animationDone;



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

            if (!locationFileClosed) return;
            // increase time count with time passed
            _timer += Time.deltaTime;

            //stop if the game is not in play
            if (!_animationDone||!PlayingGame) return;

            //if requirements are met call the spawn enemy method
            if (_timer > SpawnDelay && SpawnAmount != 0) SpawnEnemy();
            
            //check losing condition
            if (Health == 0) GameLost();

            //check winning condition
            else if (EnemyAlive == 0) GameWon();
        }

        public void SpawnEnemy()
        {
            SpawnAmount--;
            GameObject spawn;
            //randomizer for what type of enemy gets spawned
            int type = Random.Range(0, 3);
            if (type < 2)
            {
                spawn = WeakEnemy;
            }
            else
            {
                spawn = StrongEnemy;
            }
            //reset timer
            _timer = 0;

            //spawn enemy on position
            int spawnLocation = Random.Range(0, 3);
            Instantiate(spawn, LanePos(spawnLocation), Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
        }

        //the position of a lane 
        public Vector3 LanePos(int lane)
        {
            return new Vector3(Lanes[lane].transform.position.x, SpawnHeight.transform.position.y, Lanes[lane].transform.position.z);
        }

        //return the position of the textbox
        public Vector3 TextPos()
        {
            return new Vector3(Lanes[1].transform.position.x, Lanes[1].transform.position.y, Lanes[1].transform.position.z);

        }
        // method to stop playing the game and open the winscreen when the user wins
        void GameWon()
        {
            PlayingGame = false;
            DataManager.SetMinigameStatus(MinigameName.Hacking, true);
            GameObject winScreen = EndScreen.GetNamedChild("WinText");
            Instantiate(winScreen, TextPos(), Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);

            StartCoroutine(Wait());
        }

        // method to stop the game and show the losescreen when the user has lost
        void GameLost()
        {
            PlayingGame = false;
            GameObject _loseScreen = EndScreen.GetNamedChild("LoseText");
            Instantiate(_loseScreen, TextPos(), Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);

            StartCoroutine(BackToNavigation());
        }

        //coroutine to return to the map
        IEnumerator BackToNavigation()
        {
            yield return new WaitForSeconds(3);
            SceneLoader.LoadScene(GameScene.Navigation);
        }

        //coroutine to wait
        IEnumerator Wait()
        {
            yield return new WaitForSeconds(3);
            CompleteGameStep();
        
        }

        //coroutine to wait 3 seconds
        IEnumerator WaitForAnimation()
        {
            yield return new WaitForSeconds(3);
            _animationDone = true;
            PlayingGame = true;
        }

        IEnumerator CombatStartDelay()
        {
            yield return new WaitForSeconds(3);
            CombatControllerProperty.enabled = true;
        }

        //setup method
        public override void PrepareStep()
        {
            gameController = this;
            PlayingGame = false;
            EnemyAlive = SpawnAmount;
            SetLocationFile();
            HideLocationFileButton.onClick.AddListener(HideLocationFile);
        }

        //starting the game
        public override void StartGameStep()
        {
            ShowLocationFile();
        }

        //ending the game
        public override void CompleteGameStep()
        {
            LocationUIHintNextLocation.text = "Hint for next location \n" + LocationFile.HintNextLocation;
            LocationFile.IsCompleted = true;
            ShowLocationFile();
        }
   
        //show the locationfile before the game begins
        public override void ShowLocationFile()
        {
            PlayingGame = false;
            LocationFileUI.SetActive(true);
        }

        //hide the locationfile
        //and return to the map if the file is completed
        public override void HideLocationFile()
        {
            LocationFileUI.SetActive(false);
            StartCoroutine(WaitForAnimation());
            PlayingGame = true;
            locationFileClosed= true;
            if (LocationFile.IsCompleted)
            {
                SceneLoader.LoadScene(GameScene.Navigation);
            }

            foreach (Animator item in Animations)
            {
                //start all animations in the list
                item.enabled = true;
                //this trigger activates the animation for the hacking base
                item.SetTrigger("Test");
            }

            StartCoroutine(CombatStartDelay());
        }

        //receive the dialogue used in the minigame
        public override void SplitDialogue()
        {
            TutorialDialogues.Add(DialogueManagerV2.GetDialogue("LocalizationDialogue", "hackerMinigame_0"));
            TutorialDialogues.Add(DialogueManagerV2.GetDialogue("LocalizationDialogue", "hackerMinigame_1"));
            WonDialogues.Add(DialogueManagerV2.GetDialogue("LocalizationDialogue", "hackerMinigame_2"));
        }
    }
}
