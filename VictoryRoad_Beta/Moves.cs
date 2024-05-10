using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inazuma_Eleven_Victory_Road_Beta.InazumaEleven_V1._0._0
{
    internal class Moves
    {
       public static Dictionary< string,string> Specialmoves = new Dictionary< string ,string>();

        public static void InitializeMoves()
        {
            //============================================================SHOTS
            Specialmoves.Add("00000000", "No Move");
            // AIR 
            Specialmoves.Add("BF96F52B", "Tsunami boost");
            Specialmoves.Add("B8E28C1C", "God knows");
            Specialmoves.Add("57E52B7E", "Legendary Wolf");
            Specialmoves.Add("467E091C", "Dragon Driver");
            Specialmoves.Add("11A04950", "Spinning Shot");
            Specialmoves.Add("A9F15A5E", "Northern Impact ");

            //MOUNTAIN 
            Specialmoves.Add("9FBB7E0F", "Royal Lancer/King’s Lance");
            Specialmoves.Add("A3DCC454", "Butterfly Dance");
            Specialmoves.Add("66DB7618", "Disaster Strike");

            //FIRE
            Specialmoves.Add("26CA8B51", "Fire Tornado");
            Specialmoves.Add("9BC0528A", "Doomsword Slash");
            Specialmoves.Add("834F0F91", "Fire Tornado DD");
            Specialmoves.Add("4F6A7665", "Meteor blade");
            Specialmoves.Add("29342BAE", "Atomic Flare");

            //FOREST
            Specialmoves.Add("0B00A7A9", "Bouncing Bunny");
            Specialmoves.Add("633EFC2C", "Dragon crash");
            Specialmoves.Add("ED137FB2", "Death zone");
            Specialmoves.Add("3931AF7D", "Emperor Penguin No. 2");
            Specialmoves.Add("ACAA908B", "Fortissimo Foot");
            Specialmoves.Add("C6BB78EC", "Black Ash");
            Specialmoves.Add("1CF9AE6D", "Supernova");
            Specialmoves.Add("0E5B6D7C", "Astro break");
            Specialmoves.Add("608FE97F", "Ganymede Ray");

            //=========================================================DRIBBLES
           
            //AIR
            Specialmoves.Add("6B8F360D", "Heaven's Time");
            Specialmoves.Add("0DD16BC6", "ZigZag Spark");
            Specialmoves.Add("8739843C", "Flurry Dash ");
            Specialmoves.Add("BC9407B1", "Flash dash/Inabikari Dash");
            Specialmoves.Add("2DCA5423", "Wind God's Dance");
            Specialmoves.Add("5393A0D3", "Hoopsie-Dais");

            //MOUNTAIN
            Specialmoves.Add("FC3BBA19", "Dance on Air");

            //FIRE
            Specialmoves.Add("C2CDF341", "Heat Tackle");
            Specialmoves.Add("49334E2A", "Heart Beat");
            Specialmoves.Add("9B73B543", "Lightning Bolt");
            Specialmoves.Add("603C0EB6", "Plasma Cut");

            //FOREST
            Specialmoves.Add("431ED020", "Hey Presto");
            Specialmoves.Add("1AA09622", "Southern Cross");
            Specialmoves.Add("019EDE6A", "Illusion ball");

            //====================================================BLOCKS

            //AIR
            Specialmoves.Add("4B37E3A9", "Whirly-Whirly");
            Specialmoves.Add("32929A4E", "Spinning Cut");
            Specialmoves.Add("ACBA9D03", "Land of Ice");
            Specialmoves.Add("A0F528AF", "The Tower");
            Specialmoves.Add("2D69BE62", "Whirlwind force");
            Specialmoves.Add("AF0B8850", "Frozen steal");

            //MOUNTAIN
            Specialmoves.Add("879A6E7D", "The Wall");
            Specialmoves.Add("3FCB7D73", "Flapjack Defence");
            
            //FIRE
            Specialmoves.Add("B96C2725", "Solar Surprise");
            Specialmoves.Add("256B938A", "Goopy Gloopy Goo");
            Specialmoves.Add("1289A5AB", "Ignite Steal");

            //FOREST
            Specialmoves.Add("44C94356", "Killer Slide");
            Specialmoves.Add("80EE174A", "Hunter's Net");
            Specialmoves.Add("C1DF0C53", "Mystifying Mist");

            //======================================================SAVES

            //MOUNTAIN
            Specialmoves.Add("5228388C", "God Hand (Mountain)");
            Specialmoves.Add("EEBC47E6", "God Hand V");

            //FIRE
            Specialmoves.Add("10A836C6", "Combustion Catch");
            Specialmoves.Add("A3C2E953", "Capable Hands");
            Specialmoves.Add("F0D9C57B", "Rotary Sander");

            //FOREST
            Specialmoves.Add("C73BF35A", "God Hand (Forest)");
            Specialmoves.Add("7908CBF2", "Mugen The Hand");
            Specialmoves.Add("8E80318B", "Wormhole");

           
        }
    }

 }
