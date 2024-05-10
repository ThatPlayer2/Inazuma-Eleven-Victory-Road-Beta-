using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InazumaElevenVictoryRoad.InazumaEleven_VR
{
    internal class Tokens
    {
        public Dictionary<string, string> tokens { get; private set; }

        public Tokens(string language)
        {
            tokens = [];

            // Carica le traduzioni in base alla lingua selezionata
            if (language == "italian")
            {
                tokens = new Dictionary<string, string>()
            {
                { "57AA89CF", "Sotto il cielo Stellato" },
                { "620FEA26", "Pagina del quaderno delle Abilità Super del nonno" },
                { "D85EE3BF", "Citazioni per motivarsi" },
                { "4E6EE4C8", "L'allenamento è come Polpette di riso" },
                { "EDFB8056", "La leggenda del Cammino di Evans" },
                { "7BCB8721", "Piani Oscuri " },
                { "C19A8EB8", "Pagina del diario di Silvia" },
                { "C6B7365F", "Stella Vittoria" },
                { "50873128", "Stele della Vittoria" }
                };
            }
            else if (language == "english")
            {
                // Carica le traduzioni in lingua inglese da una classe separata
                EnglishTokens englishTokens = new();
                tokens = englishTokens.Tokens;
            }
        }
    }

    internal class EnglishTokens
    {
        public Dictionary<string, string> Tokens { get; private set; }

        public EnglishTokens()
        {
            Tokens = new Dictionary<string, string>()
        {
            { "57AA89CF", "Under the Starry Sky" },
            { "620FEA26", "Grandpa's Super Skills Notebook Page" },
            { "D85EE3BF", "Ispirations Quote" },
            { "4E6EE4C8", "Practice is Like Rice Balls" },
            { "EDFB8056", "The Legend of Endo's' Trail" },
            { "7BCB8721", "Shadow Plans Page" },
            { "C19A8EB8", "Silvia's Manager's Diary Page" },
            { "C6B7365F", "Victory Star" },
            { "50873128", "Victory Stone" }
            };
        }
    }
}

