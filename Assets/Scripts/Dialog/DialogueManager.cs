using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

namespace Dialog
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance;
        public Dictionary<int, List<Dialogue>> DutchBuddyDialogue = new();
        public Dictionary<int, List<Dialogue>> EnglishBuddyDialogue = new();

        /// <summary>
        /// Initializes the dialogue lists and sets the instance to this object.
        /// </summary>
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                FillDialogueList();
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Fills the dialogue lists with the dialogue for the buddy.
        /// HOLY SH*T, this is a lot of dialogue.
        /// TODO: This should be moved to localization files.
        /// </summary>
        private void FillDialogueList()
        {
            #region Buddy Dialogue

            #region NL Dialogue

            //C canteen
            List<Dialogue> BuddyDialogueCcanteenNL = new List<Dialogue>();
            BuddyDialogueCcanteenNL.Add(new Dialogue(
                "We zijn momenteel in de C1 Kantine, Windesheim heeft ons laten weten dat er hier ergens een aanwijzing zou moeten zijn over de locatie van de vereniging... laten we die vinden!"));
            BuddyDialogueCcanteenNL.Add(new Dialogue(
                "Ik zie dat er een icoon op je kaart staat… dat ziet er interessant uit, laten we er op klikken!"));
            DutchBuddyDialogue.Add(1, BuddyDialogueCcanteenNL);
            //C canteen minigame
            List<Dialogue> BuddyDialogueCcanteenMinigameNL = new List<Dialogue>();
            BuddyDialogueCcanteenMinigameNL.Add(new Dialogue(
                "Het lijkt er op dat we dit voorwerp op een oppervlakte kunnen zetten door op de muur te klikken, laten we dat proberen!"));
            BuddyDialogueCcanteenMinigameNL.Add(new Dialogue(
                "Op het moment dat je puntjes op de muur ziet door je telefooncamera, betekent dit dat je daar een object kan plaatsen. druk op de muuroppervlakte met de puntjes om het object te plaatsen."));
            BuddyDialogueCcanteenMinigameNL.Add(
                new Dialogue("Het lijkt er op dat dit een schuifpuzzel is, laten we hem oplossen!"));
            BuddyDialogueCcanteenMinigameNL.Add(
                new Dialogue("Druk op de puzzelstukjes om ze te verwisselen met de lege, totdat de afbeelding klopt."));
            BuddyDialogueCcanteenMinigameNL.Add(new Dialogue(
                "We hebben de eerste aanwijzing vrijgespeeld! Laten we de navigator volgen naar de volgende locatie!"));
            BuddyDialogueCcanteenMinigameNL.Add(new Dialogue(
                "Volg de rode lijn en navigatie-instructies op het scherm. druk op “volgende” wanneer je aan het einde bent zodat je nieuwe instructies kan ontvangen."));
            DutchBuddyDialogue.Add(-1, BuddyDialogueCcanteenMinigameNL);
            //Sport cafe
            List<Dialogue> BuddyDialogueSportCafeNL = new List<Dialogue>();
            BuddyDialogueSportCafeNL.Add(
                new Dialogue("Nog een icoon op de kaart! misschien kunnen we het hier op de muur plaatsen."));
            DutchBuddyDialogue.Add(5, BuddyDialogueSportCafeNL);
            //Sport cafe minigame
            List<Dialogue> BuddyDialogueSportCafeMinigameNL = new List<Dialogue>();
            BuddyDialogueSportCafeMinigameNL.Add(
                new Dialogue("Het ziet ernaar uit dat je de code moet kraken door de juiste combinatie te raden."));
            BuddyDialogueSportCafeMinigameNL.Add(new Dialogue(
                "Kraak de code door de geheime kleurencombinatie te achterhalen. De zwarte pinnen aan de rechterkant geven aan hoeveel kleuren op de juiste plaats staan ​​en de witte pinnen geven aan hoeveel kleuren er in de code staan, maar niet op de juiste plaats."));
            BuddyDialogueSportCafeMinigameNL.Add(new Dialogue(
                "Je hebt het opgelost! Dat was een goede denkwijze die je daar had! laten we eens kijken waar we nu heen moeten..."));
            DutchBuddyDialogue.Add(-2, BuddyDialogueSportCafeMinigameNL);
            //Outside sport cafe
            List<Dialogue> BuddyDialogueSportCafeOutsideNL = new List<Dialogue>();
            BuddyDialogueSportCafeOutsideNL.Add(
                new Dialogue("De navigator wijst ons naar deze trap, laat we naar boven gaan."));
            BuddyDialogueSportCafeOutsideNL.Add(
                new Dialogue("Je kunt ook de lift nemen in de hoek rechts van de trap."));
            DutchBuddyDialogue.Add(6, BuddyDialogueSportCafeOutsideNL);
            //X1
            List<Dialogue> BuddyDialogueXbuildingNL = new List<Dialogue>();
            BuddyDialogueXbuildingNL.Add(
                new Dialogue("Dit is een coole plek! en het heeft ook nog eens een cafetaria!"));
            BuddyDialogueXbuildingNL.Add(
                new Dialogue("Er zit hier nog een icoontje op de map, laten we het op de muur plaatsen."));
            DutchBuddyDialogue.Add(11, BuddyDialogueXbuildingNL);
            //X1 minigame
            List<Dialogue> BuddyDialogueXbuildingMinigameNL = new List<Dialogue>();
            BuddyDialogueXbuildingMinigameNL.Add(
                new Dialogue("Deze iconen lijken op het logo van de vereniging, misschien staat die hier ergens!"));
            BuddyDialogueXbuildingMinigameNL.Add(
                new Dialogue("Zoek het logo van de vereniging in deze afbeelding en druk hier op."));
            BuddyDialogueXbuildingMinigameNL.Add(
                new Dialogue("Je hebt een goed oog voor detail, dat heb je in no-time opgelost!"));
            DutchBuddyDialogue.Add(-3, BuddyDialogueXbuildingMinigameNL);
            //X1 exit
            List<Dialogue> BuddyDialogueXbuildingExitNL = new List<Dialogue>();
            BuddyDialogueXbuildingExitNL.Add(
                new Dialogue("De navigator wijst ons naar beneden, de trap af. Laten we het verkennen!"));
            DutchBuddyDialogue.Add(12, BuddyDialogueXbuildingExitNL);
            //approaching T stairs
            List<Dialogue> BuddyDialogueTstairsNL = new List<Dialogue>();
            BuddyDialogueTstairsNL.Add(new Dialogue(
                "Volgens onze navigator is dit het T-gebouw, laten we de trap op gaan naar de tweede verdieping."));
            BuddyDialogueTstairsNL.Add(
                new Dialogue("Je kunt ook de lift nemen als je dit gebouw binnen gaat en links of rechts afslaat."));
            DutchBuddyDialogue.Add(15, BuddyDialogueTstairsNL);
            //T2 desk
            List<Dialogue> BuddyDialogueTdeskNL = new List<Dialogue>();
            BuddyDialogueTdeskNL.Add(new Dialogue(
                "We staan nu bij de T2 Balie, ik heb gehoord dat dit de beste plek is om informatie te krijgen rondom het T-Gebouw!"));
            BuddyDialogueTdeskNL.Add(
                new Dialogue("Er is hier nog een pictogram, laten we zien wat ons te wachten staat!"));
            DutchBuddyDialogue.Add(16, BuddyDialogueTdeskNL);
            //T2 desk minigame
            List<Dialogue> BuddyDialogueTdeskMinigameNL = new();
            BuddyDialogueTdeskMinigameNL.Add(new Dialogue("Het lijkt erop dat we het systeem gaan hacken!"));
            BuddyDialogueTdeskMinigameNL.Add(new Dialogue(
                "Veeg naar links en rechts om alle banen te beschermen en zorg ervoor dat je basis niet wordt overspoeld door de vijanden."));
            BuddyDialogueTdeskMinigameNL.Add(new Dialogue(
                "Ik denk dat we het einde van dit mysterie naderen. Laten we de trap op gaan naar de 5e verdieping en laatste locatie!"));
            DutchBuddyDialogue.Add(-4, BuddyDialogueTdeskMinigameNL);
            //T5 bridge
            List<Dialogue> BuddyDialogueTbridgeNL = new();
            BuddyDialogueTbridgeNL.Add(new Dialogue("In deze ruimte kunnen studenten studeren wanneer ze willen!"));
            DutchBuddyDialogue.Add(18, BuddyDialogueTbridgeNL);
            //T5 bridge minigame
            List<Dialogue> BuddyDialogueTbridgeMinigameNL = new();
            BuddyDialogueTbridgeMinigameNL.Add(new Dialogue("Ik ben erg benieuwd naar wat er in deze kluis zit!"));
            BuddyDialogueTbridgeMinigameNL.Add(new Dialogue(
                "Het lijkt erop dat je op de kleuren aan moet drukken en het patroon na moet bootsen om de kluis te ontgrendelen!"));
            DutchBuddyDialogue.Add(-5, BuddyDialogueTbridgeMinigameNL);
            //T5 bridge end
            List<Dialogue> BuddyDialogueTbridgeEndNL = new List<Dialogue>();
            BuddyDialogueTbridgeEndNL.Add(new Dialogue(
                "Je hebt het gedaan! Je hebt Windesheim met succes geholpen en alle geheimen van de vereniging onthuld!"));
            DutchBuddyDialogue.Add(19, BuddyDialogueTbridgeEndNL);

            #endregion

            #region EN Dialogue

            //C canteen
            List<Dialogue> BuddyDialogueCcanteenEN = new List<Dialogue>();
            BuddyDialogueCcanteenEN.Add(new Dialogue(
                "We are located at the C1 Canteen right now, Windesheim has let us know that there should be a clue about the society's whereabouts around here... let's find it!"));
            BuddyDialogueCcanteenEN.Add(
                new Dialogue("I see there’s an icon on your map… it seems interesting, maybe we should click it!"));
            EnglishBuddyDialogue.Add(1, BuddyDialogueCcanteenEN);
            //C Canteen minigame
            List<Dialogue> BuddyDialogueCcanteenMinigameEN = new List<Dialogue>();
            BuddyDialogueCcanteenMinigameEN.Add(
                new Dialogue("looks like we can put this item on a surface by pressing on the wall, let’s try it!"));
            BuddyDialogueCcanteenMinigameEN.Add(new Dialogue(
                "when you see dots through the camera of your phone that means you can place an object there, press on the surface with the dots to place the object."));
            BuddyDialogueCcanteenMinigameEN.Add(new Dialogue("This looks like a sliding puzzle! Let's solve it!"));
            BuddyDialogueCcanteenMinigameEN.Add(
                new Dialogue("Tab the puzzle pieces to switch them with the empty one until the picture looks right."));
            BuddyDialogueCcanteenMinigameEN.Add(
                new Dialogue("We unlocked our first clue! Let's follow the navigator to the next location!"));
            BuddyDialogueCcanteenMinigameEN.Add(new Dialogue(
                "Follow the red line and navigation instructions on your screen. press “next” when you’re at the end to get the next instruction. "));
            EnglishBuddyDialogue.Add(-1, BuddyDialogueCcanteenMinigameEN);
            //Sport cafe
            List<Dialogue> BuddyDialogueSportCafeEN = new List<Dialogue>();
            BuddyDialogueSportCafeEN.Add(
                new Dialogue("Another icon of those on the map! Maybe we can place it on the wall here."));
            EnglishBuddyDialogue.Add(5, BuddyDialogueSportCafeEN);
            //Sport cafe minigame
            List<Dialogue> BuddyDialogueSportCafeMinigameEN = new List<Dialogue>();
            BuddyDialogueSportCafeMinigameEN.Add(
                new Dialogue("It seems like you have to crack the code by guessing the right combination."));
            BuddyDialogueSportCafeMinigameEN.Add(new Dialogue(
                "Crack the code by figuring out the secret color combination, the black pins on the right indicate how many colors are in the right place and the white pins indicate how many colors are inside of the code but not in the right place."));
            BuddyDialogueSportCafeMinigameEN.Add(new Dialogue(
                "You did it! That was some great thinking you did there! Let's see where we have to go now..."));
            EnglishBuddyDialogue.Add(-2, BuddyDialogueSportCafeMinigameEN);
            //Outside sport cafe
            List<Dialogue> BuddyDialogueSportCafeOutsideEN = new List<Dialogue>();
            BuddyDialogueSportCafeOutsideEN.Add(
                new Dialogue("The navigator is pointing us towards these stairs, let's go up."));
            BuddyDialogueSportCafeOutsideEN.Add(
                new Dialogue("You can also take the elevator in the corner to the right of the stairs."));
            EnglishBuddyDialogue.Add(6, BuddyDialogueSportCafeOutsideEN);
            //X1
            List<Dialogue> BuddyDialogueXbuildingEN = new();
            BuddyDialogueXbuildingEN.Add(new Dialogue("This is a cool place! and it has got another cafeteria too!"));
            BuddyDialogueXbuildingEN.Add(
                new Dialogue("There is another icon on the map here, let’s place it on the wall."));
            EnglishBuddyDialogue.Add(11, BuddyDialogueXbuildingEN);
            //X1 minigame
            List<Dialogue> BuddyDialogueXbuildingMinigameEN = new List<Dialogue>();
            BuddyDialogueXbuildingMinigameEN.Add(
                new Dialogue("These icons look like the society's logo, maybe it's in here somewhere!"));
            BuddyDialogueXbuildingMinigameEN.Add(
                new Dialogue("Find the logo of the secret society in this image and press on it."));
            BuddyDialogueXbuildingMinigameEN.Add(
                new Dialogue("You've got a great eye for detail, you spotted that in no-time!"));
            EnglishBuddyDialogue.Add(-3, BuddyDialogueXbuildingMinigameEN);
            //X1 exit
            List<Dialogue> BuddyDialogueXbuildingExitEN = new List<Dialogue>();
            BuddyDialogueXbuildingExitEN.Add(
                new Dialogue("the navigator is pointing us down the stairs, let's go explore!"));
            EnglishBuddyDialogue.Add(12, BuddyDialogueXbuildingExitEN);
            //approaching T stairs
            List<Dialogue> BuddyDialogueTstairsEN = new List<Dialogue>();
            BuddyDialogueTstairsEN.Add(new Dialogue(
                "According to our navigator this is the T building, let's go up the stairs to the second floor."));
            BuddyDialogueTstairsEN.Add(
                new Dialogue("You can also take the elevator when you enter the building and turn left or right."));
            EnglishBuddyDialogue.Add(15, BuddyDialogueTstairsEN);
            //T2 desk
            List<Dialogue> BuddyDialogueTdeskEN = new List<Dialogue>();
            BuddyDialogueTdeskEN.Add(new Dialogue(
                "We’re at the T2 Desk right now, I heard this is the best place to get information around the T-Building!"));
            BuddyDialogueTdeskEN.Add(new Dialogue("There is another icon here, let's see what’s in store for us!"));
            EnglishBuddyDialogue.Add(16, BuddyDialogueTdeskEN);
            //T2 desk minigame
            List<Dialogue> BuddyDialogueTdeskMinigameEN = new();
            BuddyDialogueTdeskMinigameEN.Add(new Dialogue("It seems we are going to hack the system!"));
            BuddyDialogueTdeskMinigameEN.Add(new Dialogue(
                "Swipe left and right to cover all the lanes and make sure your base doesn’t get overrun by the enemies."));
            BuddyDialogueTdeskMinigameEN.Add(new Dialogue(
                "I think we’re almost at the end of this mystery. up the stairs we go to the 5th floor and final location!"));
            EnglishBuddyDialogue.Add(-4, BuddyDialogueTdeskMinigameEN);
            //T5 bridge
            List<Dialogue> BuddyDialogueTbridgeEN = new();
            BuddyDialogueTbridgeEN.Add(new Dialogue("In this room, students can study & chill whenever they like!"));
            EnglishBuddyDialogue.Add(18, BuddyDialogueTbridgeEN);
            //T5 bridge minigame
            List<Dialogue> BuddyDialogueTbridgeMinigameEN = new();
            BuddyDialogueTbridgeMinigameEN.Add(new Dialogue("I’m really curious as to what’s inside of this safe! "));
            BuddyDialogueTbridgeMinigameEN.Add(
                new Dialogue("To unlock the safe, it seems you have to press the colors and mimic the pattern!"));
            EnglishBuddyDialogue.Add(-5, BuddyDialogueTbridgeMinigameEN);
            //T5 bridge end
            List<Dialogue> BuddyDialogueTbridgeEndEN = new List<Dialogue>();
            BuddyDialogueTbridgeEndEN.Add(
                new Dialogue("You did it! you successfully helped Windesheim and exposed the society!"));
            EnglishBuddyDialogue.Add(19, BuddyDialogueTbridgeEndEN);

            #endregion

            #endregion
        }
    }
}