using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;

namespace Pacman
{
    public class Presenter
    {
        private IInputOutput _io;
        private const string dotSymbol = "🍬";
        private const string monsterSymbol = "👻";
        private const string collisionSymbol = "🤬";
        private const string deadSymbol = "☠️";

        private static readonly Dictionary<Direction, string> pacmanOpenMouthSymbol = new Dictionary<Direction, string>
        {
            {Direction.North, "V"},
            {Direction.South, "∧"},
            {Direction.East, "<"},
            {Direction.West, ">"}
        };
        
        private static readonly Dictionary<Direction, string> pacmanClosedMouthSymbol = new Dictionary<Direction, string>
        {
            {Direction.North, "|"},
            {Direction.South, "|"},
            {Direction.East, "-"},
            {Direction.West, "-"}
        };

        public Presenter(IInputOutput inputOutput)
        {
            _io = inputOutput;
        }

        public void PrintMap(Block[,] twoDMap, List<ICharacter> characters)
        {
            var numberOfRows = twoDMap.GetLength(0);
            var numberOfColumns = twoDMap.GetLength(1);
            var stringToPassIn = "";
            
            for (int row = 0; row < numberOfRows; row++)
            {
                for (int column = 0; column < numberOfColumns; column++)
                {

                    stringToPassIn += AssignSymbol(characters, row, column, twoDMap);
                }

                if (row < numberOfRows -1)
                {
                    stringToPassIn += "\n";
                }
            }

            _io.Output(stringToPassIn);
        }

        private string AssignSymbol(List<ICharacter> characters, int row, int column, Block[,] twoDMap)
        {
            var foundCharacters = characters.FindAll(x => (x.Location[0] == row && x.Location[1] == column));
            
            if (foundCharacters.Count < 1)
            {
                return twoDMap[row, column].HasDot ? dotSymbol : " ";
            }

            if (foundCharacters.Count > 1)
            {
                var pacman = foundCharacters.FirstOrDefault(x => x is Pacman);
                return pacman == null ? monsterSymbol : GetPacmanNegativeImpactSymbol((Pacman) pacman);
            }
       
            return AssignCharacterSymbol(foundCharacters[0]);
        }

        private string GetPacmanNegativeImpactSymbol(Pacman pacman)
        {
            if (pacman.IsDead)
            {
                return deadSymbol;
            }

            return collisionSymbol;
        }

        private string AssignCharacterSymbol(ICharacter character)
        {
            if (character is Pacman)
            {
                return GetPacmanSymbol((Pacman)character);
            }
            
            return monsterSymbol;
        }

        private string GetPacmanSymbol(Pacman pacman)
        {
            if (pacman.MouthStatus == Mouth.Closed)
            {
                return pacmanClosedMouthSymbol[pacman.FacingDirection];
            }
            return pacmanOpenMouthSymbol[pacman.FacingDirection];
        }
    }
}