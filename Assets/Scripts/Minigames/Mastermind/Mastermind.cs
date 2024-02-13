using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Minigames.Mastermind
{
    public class Mastermind : MonoBehaviour
    {
        public Sprite Yellow, Blue, Red, Green, White, Black;
        public string[] SecretCode = new string[4];
        public string[] SecretCodeTemp = new string[4];
        private Dictionary<string, Sprite> _dicoSprite = new Dictionary<string, Sprite>();
        private string[] _codePlayer = new string[4];
        public GameObject HiddenSlot;

        void Awake()
        {
            _dicoSprite.Add("Yellow", Yellow);
            _dicoSprite.Add("Blue", Blue);
            _dicoSprite.Add("Red", Red);
            _dicoSprite.Add("Green", Green);
            _dicoSprite.Add("White", White);
            _dicoSprite.Add("Black", Black);
        }

        public Array GetNewSecretCode()
        {
            for (int i = 0; i < 4; i++)
            {
                int rnd = UnityEngine.Random.Range(0, _dicoSprite.Count);
                SecretCode.SetValue(_dicoSprite.ElementAt(rnd).Key, i);
            }

            transform.Find("c1").GetComponent<Image>().sprite = _dicoSprite[SecretCode.GetValue(0).ToString()];
            transform.Find("c2").GetComponent<Image>().sprite = _dicoSprite[SecretCode.GetValue(1).ToString()];
            transform.Find("c3").GetComponent<Image>().sprite = _dicoSprite[SecretCode.GetValue(2).ToString()];
            transform.Find("c4").GetComponent<Image>().sprite = _dicoSprite[SecretCode.GetValue(3).ToString()];

            return SecretCode;
        }

        public int GetGoodPosition(string[] code)
        {
            Array.Copy(SecretCode, SecretCodeTemp, SecretCode.Length);

            int good = 0;
            for (int i = 0; i < SecretCodeTemp.Length; i++)
            {
                if (code[i] == SecretCodeTemp[i])
                {
                    good++;
                    code[i] = "good";
                    SecretCodeTemp[i] = "good";

                }
            }
            Array.Copy(code, _codePlayer, code.Length);
            return good;
        }





        public int GetWrongPosition()
        {
            int wrong = 0;
            for (int i = 0; i < _codePlayer.Length; i++)
            {
                for (int j = 0; j < SecretCodeTemp.Length; j++)
                {
                    if (_codePlayer[i] == SecretCodeTemp[j] && _codePlayer[i] != "good" && SecretCodeTemp[j] != "good")
                    {
                        SecretCodeTemp[j] = "wrong";
                        wrong++;
                        break;
                    }

                }

            }
            return wrong;
        }

    }
}