using InazumaElevenVictoryRoad;
using InazumaElevenVictoryRoad.InazumaEleven_VR;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Inazuma_Eleven_Victory_Road_Beta.InazumaEleven_V1._0._0
{
    internal class Items
    {
        public Dictionary<string, string> Boots { get; private set; }
        public Dictionary<string, string> Bracelet { get; private set; }
        public Dictionary<string, string> Pendant { get; private set; }
        public Dictionary<string, string> Special { get; private set; }

       

        public Items(string language)
        {
            Boots = [];
            Bracelet = [];
            Pendant = [];
            Special = [];

            if (language == "italian")
            {
                Boots = new Dictionary<string, string>()
                {
                    { "A0115D6D", "Scarpini Semplici"},
                    { "1E03795B", "Scarpini folata di vento"},
                    { "9B15365E", "Scarpini Verdi"},
                    { "32A8C3A7", "Scarpini Raimon"},
                    { "4227236F", "Scarpini Raimon della Rivoluzione"},
                    { "925D8328", "Scarpini Normidia Raimon" },
                    { "4BC1F6ED", "Scarpini Shinsei Inazuma Japan"},
                    { "43CAD3AA", "Scarpini del Dio dela Guerra Inazuma Japan"},
                    { "6B1685A5", "Scarpini Reali" },
                    { "CBE3C52A", "Scarpini Stella"},
                    { "60A5BD1E", "Scarpini Zeus"},
                    { "DE0B995A", "Scarpini Genesis "},
                    { "FC89072B", "Scarpini Altaluna"}
                };

                Bracelet = new Dictionary<string, string>()
                {
                    { "B83667DB", "Braccialetto Colorato"},
                    { "0D391BA2", "Guanti in Pelle"},
                    { "14FD76A5", "Braccialetto del Vento"},
                    { "AEAC7F3C", "Braccialetto della Foresta "},
                    { "389C784B", "Braccialetto di Fuoco"},
                    { "A981C7DB", "Braccialettoe della Montagna "},
                    { "B768123B", "Guanti Possenti"},
                    { "2158154C", "Guanti Meteora"},
                    { "DA3807CC", "Braccialetto Portafortuna"},
                    { "3FB1C0AC", "Braccialetto Genesis "},
                    { "82CD71D2", "Guanti del Nonno"},
                };

                Pendant = new Dictionary<string, string>()
                {
                    { "D8C28897", "Ciondolo a Medaglia"},
                    { "AFE6A0CF", "Ciondolo ruota Crash Course"},
                    { "39D6A7B8", "Girocollo Selvaggio"},
                    { "0C73C451", "Ciondolo Promessa"},
                    { "B622CDC8", "Ciondolo dei Legami"},
                    { "8387AE21", "Mantelllo di Jude"},
                    { "DB72D83F", "Collana delle Avversità"},
                    { "15B7A956", "Mascotte Scintillante" },
                    { "2012CABF", "Collana di Chrono Stone "},
                    { "3EFB1F5F", "Pietra Alius "},
                    { "A8CB1828", "Sciarpa del re"},
                };

                Special = new Dictionary<string, string>()
                {
                    { "09C36C39", "Calzini Comodi "},
                    { "B39265A0", "Accessorio Rinfrescante "},
                    { "86370649", "Accessorio polpetta di Riso"},
                    { "AD7BB040", "Adesivo di Drago"},
                    { "25A262D7", "Porachiavi Autobus"},
                    { "3C660FD0", "Portachiavi Wonderbot"},
                    { "1007013E", "ascia di Evan"},
                    { "AA5608A7", "Braccialetto Omega  " },
                    { "DEC27057", "Occhialini di Jude"},
                    { "3B4BB737", "Talismano di Evans "},
                    { "48F27720", "Accessorio Professor Layton"},
                };

            }
           
            else if (language == "english")
            {

                EnglishItems englishItem = new();
                Boots = englishItem.Boots;
                Bracelet = englishItem.Bracelet;
                Pendant = englishItem.Pendant;
                Special = englishItem.Special;
            }
        }
            

    }

    public class EnglishItems
    {
        public Dictionary<string, string> Boots { get; private set; }
        public Dictionary<string, string> Bracelet { get; private set; }
        public Dictionary<string, string> Pendant { get; private set; }
        public Dictionary<string, string> Special { get; private set; }

        public EnglishItems()
        {
            Boots = new Dictionary<string, string>()
            {

            { "A0115D6D", "Plain Boots"}, 
            { "1E03795B", "Gale Boots"},
            { "9B15365E", "Verdant Boots"},
            { "32A8C3A7", "Raimon Boots"},
            { "4227236F", "Raimon Boots of Revolution"},
            { "925D8328", "Backwater Raimon Boots" },
            { "4BC1F6ED", "Inazuma Another N Boots"},
            { "43CAD3AA", "Inazuma National War God Boots"},
            { "6B1685A5", "Royal Boots" },
            { "CBE3C52A", "Polestar Boots"},
            { "60A5BD1E", "Zeus Boots"},
            { "DE0B995A", "Genesis Boots"},
            { "FC89072B", "Lunar Prime Boots"}


            };

            Bracelet = new Dictionary<string, string>()
            {

            { "B83667DB", "Colorful Bracele"},
            { "0D391BA2", "Leather Glovess"},
            { "14FD76A5", "Wind Bangle"},
            { "AEAC7F3C", "Forest Bangle "},
            { "389C784B", "Fire Banglel"},
            { "A981C7DB", "Mountain Bangle "},
            { "B768123B", "Powerful Gloves"},
            { "2158154C", "Meteor Gloves" },
            { "DA3807CC", "Lucky Bracelet"},
            { "3FB1C0AC", "Genesis Bangle"},
            { "82CD71D2", "Grandpa's Gloves"},
            

            };

            Pendant = new Dictionary<string, string>()
            {

            { "D8C28897", "Medal Pendant"},
            { "AFE6A0CF", "Crash Course Tire Pendant"},
            { "39D6A7B8", "Wild Choker"},
            { "0C73C451", "Promise Pendant"},
            { "B622CDC8", "Pendant of Bonds"},
            { "8387AE21", "Jude's Cape"},
            { "DB72D83F", "Adversity Necklace"},
            { "15B7A956", "Shiny Mascot" },
            { "2012CABF", "Chrono Stone Necklace"},
            { "3EFB1F5F", "Alius Crystal"},
            { "A8CB1828", "King's Scarf"},
           

            };

            Special = new Dictionary<string, string>()
            {

            { "09C36C39", "Comfy Socks "},
            { "B39265A0", "Cooling Supporter "},
            { "86370649", "Rice Ball Accessory "},
            { "AD7BB040", "Dragon Sticker Kick"},
            { "25A262D7", "Caravan Key Holder"},
            { "3C660FD0", "Wonderbot Key Holder "},
            { "1007013E", "Evan's Headband "},
            { "AA5608A7", "Omega Bangle " },
            { "DEC27057", "Jude's Googles "},
            { "3B4BB737", "Evans Talisman"},
            { "48F27720", "Professor Layton Accessory"},
           

            };
        }
    }

    
}


