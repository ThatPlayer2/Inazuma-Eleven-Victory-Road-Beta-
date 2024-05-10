using InazumaElevenVictoryRoad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using static InazumaElevenVictoryRoad.Main;

namespace InazumaElevenVictoryRoad.InazumaEleven_VR
{
    public class Player(string name, int offset, byte[] id)
    {
        public string Name { get; } = name;
        public int Offset { get; } = offset;
        public byte[] Data { get; }
        public byte[] Id { get; set; } = id;
        public int Indice { get; set; } = -1;
        public string MoveId1 { get; set; }  
        public string MoveId2 { get; set; }

        public List<string> Moves { get; set; }
    }

    public class PlayerManager
    {
        public List<Player> Players { get; } = [];

        public PlayerManager()
        {
          
            Players.Add(new Player("Heath Moore", 0x1FD83B, [0x06, 0x5C, 0xB4, 0x79]));
            Players.Add(new Player("Axel Blaze", 0x1FD97B, [0x9A, 0x48, 0x1D, 0x45]));
            Players.Add(new Player("Victor Blade", 0x1FDABB, [0x3D, 0x67, 0x25, 0x17]));
            Players.Add(new Player("Mark Evans", 0x1FDBFB, [0xEC, 0x13, 0xC4, 0x5D]));
            Players.Add(new Player("Sonny Wright", 0x1FDD3B, [0x66, 0x0F, 0x74, 0x03]));
            Players.Add(new Player("Byron Love", 0x1FDE7B, [0x47, 0x7E, 0xB2, 0xF3]));
            Players.Add(new Player("Arion Sherwind", 0x1FDFBB, [0x02, 0x87, 0x3E, 0xDE]));
            Players.Add(new Player("Jack Wallside", 0x1FE0FB, [0x6E, 0x71, 0xF2, 0x6F]));
            Players.Add(new Player("Jean-Perre Lapin", 0x1FE23B, [0x4F, 0xF9, 0x90, 0x6B]));
            Players.Add(new Player("Goldie Lemmon", 0x1FE37B, [0xED, 0xFA, 0xFE, 0xAF]));
            Players.Add(new Player("Nathan Swift", 0x1FE4BB, [0x2F, 0x40, 0xE9, 0x76]));
            Players.Add(new Player("Hurley Kane", 0x1FE5FB, [0x03, 0x39, 0xF9, 0xE4]));
            Players.Add(new Player("Darren LaChange", 0x1FE73B, [0x47, 0xA4, 0x4A, 0x0A]));
            Players.Add(new Player("Fei Rune", 0x1FE87B, [0x86, 0x02, 0x2C, 0x3A]));
            Players.Add(new Player("Xavie Foster", 0x1FE9BB, [0x50, 0x8C, 0x8C, 0xF0]));
            Players.Add(new Player("Jordan Greenway", 0x1FEAFB, [0xD2, 0xEE, 0xBA, 0xC2]));
            Players.Add(new Player("Seymour Hillman", 0x1FEC3B, [0xD5, 0xA7, 0x40, 0xF3]));
            Players.Add(new Player("Kevin Dragonfly", 0x1FED7B, [0xDA, 0x1F, 0xE4, 0xC5]));
            Players.Add(new Player("Elliot Ember", 0x1FEEBB, [0xB7, 0x13, 0x36, 0xDD]));
            Players.Add(new Player("Shawn Froste", 0x1FEFFB, [0xA6, 0x22, 0xE6, 0xAA]));
            Players.Add(new Player("Caleb Stonewall", 0x1FF13B, [0x10, 0xDB, 0x75, 0x70]));
            Players.Add(new Player("Aitor Cazador", 0x1FF27B, [0x35, 0x96, 0x7B, 0x10]));
            Players.Add(new Player("Jude Sharp", 0x1FF3BB, [0xF5, 0xFA, 0x7B, 0xDF]));
            Players.Add(new Player("Riccardo di Rigo", 0x1FF4FB, [0x42, 0xD0, 0xC7, 0x5E]));
            Players.Add(new Player("Victoria Vanguard", 0x1FF63B, [0x88, 0xC8, 0x62, 0x85]));
            Players.Add(new Player("Gabriel Garcia", 0x1FF77B, [0xC8, 0x38, 0x28, 0xA4]));
            Players.Add(new Player("Scott Banyan", 0x1FF8BB, [0xD6, 0x02, 0x5D, 0xB0]));
            Players.Add(new Player("Samguk Han", 0x1FF9FB, [0x4A, 0x5A, 0x1E, 0x96]));
            Players.Add(new Player("Zanark Avalonic", 0x1FFB3B, [0x51, 0x96, 0xC5, 0xED]));
            Players.Add(new Player("Bailong", 0x1FFC7B, [0x0E, 0x06, 0x7B, 0x9B]));
            Players.Add(new Player("Tezcat", 0x1FFDBB, [0x60, 0xD2, 0xFF, 0x98]));
            Players.Add(new Player("Erik Eagle", 0x1FFEFB, [0xD2, 0x67, 0x97, 0xF6]));
            Players.Add(new Player("Percival Travis", 0x20017B, [0x34, 0xA2, 0xC1, 0x76]));
            Players.Add(new Player("Gazelle", 0x2002BB, [0x60, 0x38, 0x4A, 0x88]));
            Players.Add(new Player("Torch", 0x2003FB, [0x1E, 0xE9, 0x4A, 0x58]));
            Players.Add(new Player("Xene", 0x20053B, [0x5E, 0x46, 0xF7, 0xF0]));
            Players.Add(new Player("Bellatrix", 0x20067B, [0x1F, 0x77, 0xEC, 0xE9]));
            Players.Add(new Player("Janus", 0x2007BB, [0xC3, 0xDF, 0xE5, 0xEE]));
            Players.Add(new Player("Clear", 0x2008FB, [0xEA, 0xD0, 0xA5, 0x72]));
            Players.Add(new Player("Dvalin", 0x200A3B, [0xDB, 0x50, 0xB8, 0xF5]));
            Players.Add(new Player("Gocker", 0x200B7B, [0x2D, 0x46, 0xE4, 0x3D]));
            Players.Add(new Player("Bomber", 0x200CBB, [0x6C, 0x77, 0xFF, 0x24]));
            Players.Add(new Player("Kormer", 0x200DFB, [0xAE, 0xBA, 0x74, 0xBE]));
            Players.Add(new Player("Zohen", 0x200F3B, [0x2C, 0xD8, 0x42, 0x8C]));
            Players.Add(new Player("Icer", 0x20107B, [0xED, 0x92, 0x68, 0x84]));
            Players.Add(new Player("Lean", 0x2011BB, [0x01, 0x24, 0xDA, 0xB5]));
            Players.Add(new Player("Wittz", 0x2012FB, [0x61, 0xA6, 0xEC, 0x39]));
            Players.Add(new Player("Zell", 0x20143B, [0xEC, 0x3A, 0x7A, 0xF4]));
            Players.Add(new Player("Neppten", 0x20157B, [0x5F, 0xD8, 0x51, 0x41]));
            Players.Add(new Player("Acquilina Schiller", 0x2016BB, [0xF0, 0x5F, 0x58, 0x2C]));
        }
    }


}

