using System.Collections;
using System.Collections.Generic;
using System.Text;
using Dialog;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        public LocationInfoScriptableObject LocationInfo;
        public TMP_Text LocationUIName;
        public TMP_Text LocationUIDescription;
        public TMP_Text LocationUIFacts;
        public TMP_Text LocationUIHintNextLocation;
        public GameObject LocationFileUI;
        public Button HideLocationFileButton;
        bool locationFileClosed;
        public Canvas GameCanvas;

        public TMP_Text BuddyTextBlock;
        public GameObject BuddyDialogueObject;
        public GameObject BuddyImage;
        public Sprite BuddyDogSprite;
        public Sprite BuddyCatSprite;
        private List<Dialogue> _startMinigame = new();
        private List<Dialogue> _endMinigame = new();

        private float _timer;
        // Start is called before the first frame update
        void Start()
        {
            gameController = this;
            _animationDone = false;
            locationFileClosed = false;
            GameSetup();
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

        public override void SetLocationFile()
        {
            if (PlayerPrefs.GetString("Language").Equals("NL"))
            {
                LocationFile = DutchFile();
            }
            else
            {
                LocationFile = EnglishFile();
            }
            LocationUIName.text = LocationFile.Name;
            LocationUIDescription.text = LocationFile.Description;
            StringBuilder locationFacts = new StringBuilder("Facts\n");
            foreach (string fact in LocationFile.Facts)
            {
                locationFacts.AppendLine(fact + "\n");
            }
            LocationUIFacts.text = locationFacts.ToString();
        }

        public override LocationFile DutchFile()
        {
            return new LocationFile(LocationInfo.Data_NL.Name, LocationInfo.Data_NL.Description, LocationInfo.Data_NL.Facts, LocationInfo.Data_NL.HintNextLocation, false);

        }

        public override LocationFile EnglishFile()
        {
            return new LocationFile(LocationInfo.Data_EN.Name, LocationInfo.Data_EN.Description, LocationInfo.Data_EN.Facts, LocationInfo.Data_EN.HintNextLocation, false);

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
                SceneManager.LoadScene(1);
            }

            foreach (var item in Animations)
            {
                item.SetTrigger("Test");
            }
        }
        public override void SplitDialogue()
        {
            if (PlayerPrefs.GetString("Language").Equals("NL"))
            {
                _startMinigame.Add(DialogueManager.Instance.DutchBuddyDialogue[-4][0]);
                _startMinigame.Add(DialogueManager.Instance.DutchBuddyDialogue[-4][1]);
                _endMinigame.Add(DialogueManager.Instance.DutchBuddyDialogue[-4][2]);
            }
            if (PlayerPrefs.GetString("Language").Equals("EN"))
            {
                _startMinigame.Add(DialogueManager.Instance.EnglishBuddyDialogue[-4][0]);
                _startMinigame.Add(DialogueManager.Instance.EnglishBuddyDialogue[-4][1]);
                _endMinigame.Add(DialogueManager.Instance.EnglishBuddyDialogue[-4][2]);
            }
        }
        public override void SetBuddy()
        {
            string buddyChoice = PlayerPrefs.GetString("Buddy");
            if (buddyChoice.Equals("Cat"))
            {
                BuddyImage.GetComponent<Image>().sprite = BuddyCatSprite;
            }
            if (buddyChoice.Equals("Dog"))
            {
                BuddyImage.GetComponent<Image>().sprite = BuddyDogSprite;
            }
        }
        public override void SetBuddyDialogueText(string Dialogue)
        {
            BuddyTextBlock.text = Dialogue;
            BuddyDialogueObject.SetActive(true);
        }
        public override void OnTextBlockClick()
        {
            string text = BuddyTextBlock.text;
            if (!LocationFile.IsCompleted)
            {
                _startMinigame.Find(x => x.Text.Equals(text)).IsRead = true;
            }
            else
            {
                _endMinigame.Find(x => x.Text.Equals(text)).IsRead = true;
            }
            BuddyDialogueObject.SetActive(false);
        }
        public override void UpdateDialogue()
        {
            if (!LocationFile.IsCompleted)
            {
                foreach (Dialogue dialogue in _startMinigame)
                {
                    if (dialogue.IsRead != true)
                    {
                        SetBuddyDialogueText(dialogue.Text);
                        break;
                    }
                }
            }
            else
            {
                foreach (Dialogue dialogue in _endMinigame)
                {
                    if (dialogue.IsRead != true)
                    {
                        SetBuddyDialogueText(dialogue.Text);
                        break;
                    }
                }
            }

        }
    }
}
