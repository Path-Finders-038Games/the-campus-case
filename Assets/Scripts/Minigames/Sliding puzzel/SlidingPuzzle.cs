using System.Collections.Generic;
using System.Linq;
using Dialog;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Minigames.Sliding_puzzel
{
    public class SlidingPuzzle : Minigame
    {
        public Transform EmptySpace;
        public List<GameObject> TileObjects;
        
        public GameObject GameBoard;
        public Button HideLocationFileButton;

        private int[,] _correctBoard;
        private int[,] _currentBoard;
        private bool _isSolvable;
        private static int _boardTileCount;
        private bool _isChecking;
        private int _raycastRange;

        private List<SlidingPuzzleTile> _tilesScripts;

        void Start()
        {
            SetBuddy();
            SplitDialogue();
            PrepareStep();
            StartGameStep();
        }

        void Update()
        {
            UpdateDialogue();
            if (!_isChecking) return;

            //Screen touched check
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                RaycastCheck();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override void SplitDialogue()
        {
            StartMinigame.Add(DialogueManagerV2.GetDialogue("LocalizationDialogue", "slidingPuzzle_0"));
            StartMinigame.Add(DialogueManagerV2.GetDialogue("LocalizationDialogue", "slidingPuzzle_1"));
            EndMinigame.Add(DialogueManagerV2.GetDialogue("LocalizationDialogue", "slidingPuzzle_2"));
            EndMinigame.Add(DialogueManagerV2.GetDialogue("LocalizationDialogue", "slidingPuzzle_3"));
        }

        private void RaycastCheck()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (!Physics.Raycast(ray, out hit, _raycastRange)) return;
            if (!CanSwitch(hit.transform.GetComponent<SlidingPuzzleTile>().Position)) return;
            
            Vector3 lastEmptySpacePosition = EmptySpace.position;
            //moving tile
            EmptySpace.position = hit.transform.position;
            hit.transform.position = lastEmptySpacePosition;
            UpdateCurrentMatrix(ref hit);
            //checking board if it is in the correct order
            CheckBoard();
        }

        private bool CanSwitch(Vector2 tilePos)
        {
            Vector2 emptySpacePos = EmptySpace.GetComponent<SlidingPuzzleTile>().Position;

            Vector2 plusX = new(tilePos.x + 1, tilePos.y);
            Vector2 minX = new(tilePos.x - 1, tilePos.y);
            Vector2 plusY = new(tilePos.x, tilePos.y + 1);
            Vector2 minY = new(tilePos.x, tilePos.y - 1);

            return new[] { plusX, minX, plusY, minY }.Contains(emptySpacePos);
        }

        public override void PrepareStep()
        {
            _tilesScripts = new List<SlidingPuzzleTile>();
            _isSolvable = false;
            _boardTileCount = 9; //includes the empty space
            _correctBoard = new int[3, 3];
            _currentBoard = new int[3, 3];
            _raycastRange = 100;
            HideLocationFileButton.onClick.AddListener(HideLocationFile);
            FillTilesList();
            SetBoard();
            SetLocationFile();
            Shuffle();
            while (!_isSolvable)
            {
                CheckShuffle();
            }

            GameBoard.GetComponent<Canvas>().worldCamera = Camera.main;
        }

        public override void StartGameStep()
        {
            _isChecking = true;
            ShowLocationFile();
        }

        public override void CompleteGameStep()
        {
            _isChecking = false;
            GameBoard.SetActive(false);
            LocationUIHintNextLocation.text = "Hint for next location \n" + LocationFile.HintNextLocation;
            LocationFile.IsCompleted = true;
            ShowLocationFile();
        }

        private void SetTile(int row, int column, int id, SlidingPuzzleTile tile)
        {
            _correctBoard[row, column] = id;
            _currentBoard[row, column] = id;
            tile.Id = id;
        }

        //adds the SliddingPuzzleTile scripts from the tile object to the tileScripts list
        private void FillTilesList()
        {
            foreach (GameObject tile in TileObjects)
            {
                _tilesScripts.Add(tile.GetComponent<SlidingPuzzleTile>());
            }
        }

        private void SetBoard()
        {
            int rowCount = 0;
            int columnCount = 0;
            foreach (SlidingPuzzleTile tile in _tilesScripts)
            {
                tile.Position.y = rowCount;
                tile.Position.x = columnCount;
                switch (tile.name)
                {
                    case "Tile (0)":
                        SetTile(rowCount, columnCount, 1, tile);
                        break;
                    case "Tile (1)":
                        SetTile(rowCount, columnCount, 2, tile);
                        break;
                    case "Tile (2)":
                        SetTile(rowCount, columnCount, 3, tile);
                        break;
                    case "Tile (3)":
                        SetTile(rowCount, columnCount, 4, tile);
                        break;
                    case "Tile (4)":
                        SetTile(rowCount, columnCount, 5, tile);
                        break;
                    case "Tile (5)":
                        SetTile(rowCount, columnCount, 6, tile);
                        break;
                    case "Tile (6)":
                        SetTile(rowCount, columnCount, 7, tile);
                        break;
                    case "Tile (7)":
                        SetTile(rowCount, columnCount, 8, tile);
                        break;
                    case "EmptySpace":
                        SetTile(rowCount, columnCount, 0, tile);
                        break;
                }

                if (columnCount == 2)
                {
                    columnCount = 0;
                    rowCount++;
                }
                else
                {
                    columnCount++;
                }
            }
        }

        public override void ShowLocationFile()
        {
            _isChecking = false;
            LocationFileUI.SetActive(true);
        }

        public override void HideLocationFile()
        {
            _isChecking = true;
            LocationFileUI.SetActive(false);
            if (LocationFile.IsCompleted)
            {
                SceneManager.LoadScene(1);
            }
        }

        private string MatrixToString(int[,] matrix)
        {
            return $"([{matrix[0, 0]}], [{matrix[0, 1]}],[{matrix[0, 2]}]\n" +
                   $"[{matrix[1, 0]}],[{matrix[1, 1]}],[{matrix[1, 2]}])\n" +
                   $"[{matrix[2, 0]}],[{matrix[2, 1]}],[{matrix[2, 2]}])";
        }

        private void UpdateCurrentMatrix(ref RaycastHit hit)
        {
            Vector2Int hitPosition = hit.transform.GetComponent<SlidingPuzzleTile>().Position;
            Vector2Int emptyPosition = EmptySpace.GetComponent<SlidingPuzzleTile>().Position;
            //swich the place on the board
            _currentBoard[hitPosition.y, hitPosition.x] = EmptySpace.GetComponent<SlidingPuzzleTile>().Id;
            _currentBoard[emptyPosition.y, emptyPosition.x] = hit.transform.GetComponent<SlidingPuzzleTile>().Id;
            //Updates the location of were in the matrix the tiles are
            EmptySpace.GetComponent<SlidingPuzzleTile>().Position = hitPosition;
            hit.transform.GetComponent<SlidingPuzzleTile>().Position = emptyPosition;
        }

        private void CheckBoard()
        {
            string correctBoard = MatrixToString(_correctBoard);
            string currentBoard = MatrixToString(_currentBoard);
            if (correctBoard.Equals(currentBoard))
            {
                CompleteGameStep();
            }
        }

        private void Shuffle()
        {
            int tileCount = 7; //Empty space excluded
            for (int index = 0; index <= 7; index++)
            {
                Vector3 lastPosition = _tilesScripts[index].transform.position;
                Vector2Int lastMatrixPosition = _tilesScripts[index].Position;
                int randomIndex = Random.Range(0, tileCount);
                _tilesScripts[index].transform.position = _tilesScripts[randomIndex].transform.position;
                _tilesScripts[index].Position = _tilesScripts[randomIndex].Position;
                _tilesScripts[randomIndex].transform.position = lastPosition;
                _tilesScripts[randomIndex].Position = lastMatrixPosition;
            }

            foreach (SlidingPuzzleTile tile in _tilesScripts)
            {
                _currentBoard[tile.Position.y, tile.Position.x] = tile.Id;
            }
        }

        private void CheckShuffle()
        {
            if (IsSolvable(_currentBoard))
            {
                _isSolvable = true;
            }
            else
            {
                Shuffle();
            }
        }

        private static int GetInversionCount(int[] arr)
        {
            int inv_count = 0;
            for (int i = 0; i < _boardTileCount; i++)
            for (int j = i + 1; j < _boardTileCount; j++)
                // Value 0 is used for empty space
                if (arr[i] > 0 && arr[j] > 0 && arr[i] > arr[j])
                    inv_count++;
            return inv_count;
        }

        private static bool IsSolvable(int[,] puzzle)
        {
            int[] linearForm;
            linearForm = new int[_boardTileCount];
            int k = 0;
            int sizeBoard = 3;
            for (int i = 0; i < sizeBoard; i++)
            for (int j = 0; j < sizeBoard; j++)
                linearForm[k++] = puzzle[i, j];
            // Count inversions in given 8 puzzle
            int invCount = GetInversionCount(linearForm);
            // return true if inversion count is even.
            return (invCount % 2 == 0);
        }
    }
}