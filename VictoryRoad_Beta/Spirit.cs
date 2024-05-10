using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static InazumaElevenVictoryRoad.Main;

namespace InazumaElevenVictoryRoad.InazumaEleven_VR
{
    public class Spirit(string id, int valueToCheck)
    {
        public string ID { get; set; } = id;

        public int ValueToCheck { get; set; } = valueToCheck;
    }

    public class NormalCard
    {

        public Dictionary<string, Spirit> Normalcard = new()
        {
           // { "50EC3B42", new Spirit("Beta Placehorder", 0x00) },
            { "8D3451B0", new Spirit("Lisa", 0x00) },//2CF77(08)
            { "22B358DD", new Spirit("Neppten", 0x00) },//2D16F         
            { "91517368", new Spirit("Zell", 0x00) },
            { "1CCDE5A5", new Spirit("Wittz", 0x00) },
            { "7C4FD329", new Spirit("Lean", 0x00) },
            { "90F96118", new Spirit("Icer", 0x00) },
            { "232DFE6C", new Spirit("Xene", 0x00) },
            { "1D534314", new Spirit("Gazelle", 0x00) },
            { "638243C4", new Spirit("Torch", 0x00) },//2DF37
            { "621CE575", new Spirit("Bellatrix", 0x00) },//2E12F
            { "BEB4EC72", new Spirit("Janus", 0x00) },//2E327
            { "D3D17D22", new Spirit("Kormer", 0x00) },//2E51F
            { "111CF6B8", new Spirit("Bomber", 0x00) },//2E717
            { "51B34B10", new Spirit("Zohen", 0x00) },//2E90F
            { "502DEDA1", new Spirit("Jocker", 0x00) },//2EB07
            { "97BBACEE", new Spirit("Clear", 0x00) },//2ECFF
            { "A63BB169", new Spirit("Dvalin", 0x00) },//2EEF7
            //===============================================
            { "69752537", new Spirit("Hillman", 0x00) },//2F0EF
            { "6E3CDF06", new Spirit("Jordan", 0x00) },//2F2E7
            { "EC5EE934", new Spirit("Xavier", 0x00) },//2F4DF
            { "14DCEC16", new Spirit("Fei Rune", 0x00) },//2F6D7
            { "FB762FCE", new Spirit("Darren", 0x00) },//2F8CF
            { "BFEB9C20", new Spirit("Hurley Kane", 0x00) },//2FAC7
            { "269A7881", new Spirit("Axel Blaze", 0x00) },//2FCBF
            { "81B540D3", new Spirit("Victor Blade", 000) },
            { "BA8ED1BD", new Spirit("Heath Moore", 0x00) },//300AF
            { "BE555B1A", new Spirit("Arion", 0x00) },//301AB //CONTROLLALO PLEASE
            { "FBACD737", new Spirit("Byron Love", 0x00) },//3049F
            { "DADD11C7", new Spirit("Sonny", 0x00) },//30697
            { "F32BF5AF", new Spirit("JP", 0x00) },//3088F
            { "93928CB2", new Spirit("Nathan Swift", 0x00) },//30A87
            { "D2A397AB", new Spirit("Jack Wallside", 0x00) },//30C7F
            { "51289B6B", new Spirit("Goldie Lemon", 0x00) },//30E77
            { "50C1A199", new Spirit("Mark Evans", 0x00) },//31228         
            //===========================================
            { "8916462B", new Spirit("Trevis", 0x00) },//31420
            { "50EC3B42", new Spirit("Suzette", 0x00) },//3145F
            { "6FD310AB", new Spirit("Erik", 0x00) },//31810
            { "DD6678C5", new Spirit("Tezcat", 0x00) },//31A08
            { "B3B2FCC6", new Spirit("Bailong", 0x00) },//31C00
            { "EC2242B0", new Spirit("Zanark", 0x00) },//31FF0
            { "0AA7B180", new Spirit("Elliot", 0x00) },//31FB1
            { "67AB6398", new Spirit("Kevin", 0x00) },//321E8
            { "5322B813", new Spirit("Shawn", 0x00) },//323E0
            { "484EFC82", new Spirit("Jude", 0x00) },//325D8
            { "AD6FF22D", new Spirit("Caleb", 0x00) },//327D0
            { "FF644003", new Spirit("Riccardo", 0x00) },//329C8
            { "6BB6DAED", new Spirit("Scott", 0x00) },//32BC0
            { "357CE5D8", new Spirit("Victoria", 0x00) },//32DB8
            { "758CAFF9", new Spirit("Gabi", 0x00) },//32FB0
            { "C44137AE", new Spirit("Aitor", 0x00) },//331A8
            { "F7EE99CB", new Spirit("Samguk", 0x00) },//333EA0
           
         };


    }

    public class GrowithCard
    {

        public Dictionary<string, Spirit> Growithcard = new()
        {
           // { "50EC3B42", new Spirit("Beta Placehorder", 0x01) },
            { "8D3451B0", new Spirit("Lisa", 0x01) },//2CF77(08)
            { "22B358DD", new Spirit("Neppten", 0x01) },//2D16F         
            { "91517368", new Spirit("Zell", 0x01) },
            { "1CCDE5A5", new Spirit("Wittz", 0x01) },
            { "7C4FD329", new Spirit("Lean", 0x01) },
            { "90F96118", new Spirit("Icer", 0x01) },
            { "232DFE6C", new Spirit("Xene", 0x01) },
            { "1D534314", new Spirit("Gazelle", 0x01) },
            { "638243C4", new Spirit("Torch", 0x01) },//2DF37
            { "621CE575", new Spirit("Bellatrix", 0x01) },//2E12F
            { "BEB4EC72", new Spirit("Janus", 0x01) },//2E327
            { "D3D17D22", new Spirit("Kormer", 0x01) },//2E51F
            { "111CF6B8", new Spirit("Bomber", 0x01) },//2E717
            { "51B34B10", new Spirit("Zohen", 0x01) },//2E90F
            { "502DEDA1", new Spirit("Jocker", 0x01) },//2EB07
            { "97BBACEE", new Spirit("Clear", 0x01) },//2ECFF
            { "A63BB169", new Spirit("Dvalin", 0x01) },//2EEF7
            //===============================================
            { "69752537", new Spirit("Hillman", 0x01) },//2F0EF
            { "6E3CDF06", new Spirit("Jordan", 0x01) },//2F2E7
            { "EC5EE934", new Spirit("Xavier", 0x01) },//2F4DF
            { "14DCEC16", new Spirit("Fei Rune", 0x01) },//2F6D7
            { "FB762FCE", new Spirit("Darren", 0x01) },//2F8CF
            { "BFEB9C20", new Spirit("Hurley Kane", 0x01) },//2FAC7
            { "269A7881", new Spirit("Axel Blaze", 0x01) },//2FCBF
            { "81B540D3", new Spirit("Victor Blade", 001) },
            { "BA8ED1BD", new Spirit("Heath Moore", 0x01) },//300AF
            { "BE555B1A", new Spirit("Arion", 0x01) },//301AB //CONTROLLALO PLEASE
            { "FBACD737", new Spirit("Byron Love", 0x01) },//3049F
            { "DADD11C7", new Spirit("Sonny", 0x01) },//30697
            { "F32BF5AF", new Spirit("JP", 0x01) },//3088F
            { "93928CB2", new Spirit("Nathan Swift", 0x01) },//30A87
            { "D2A397AB", new Spirit("Jack Wallside", 0x01) },//30C7F
            { "51289B6B", new Spirit("Goldie Lemon", 0x01) },//30E77
            { "50C1A199", new Spirit("Mark Evans", 0x01) },//31228         
            //===========================================
            { "8916462B", new Spirit("Trevis", 0x01) },//31420
            { "50EC3B42", new Spirit("Suzette", 0x01) },//3145F
            { "6FD310AB", new Spirit("Erik", 0x01) },//31810
            { "DD6678C5", new Spirit("Tezcat", 0x01) },//31A08
            { "B3B2FCC6", new Spirit("Bailong", 0x01) },//31C00
            { "EC2242B0", new Spirit("Zanark", 0x01) },//31FF0
            { "0AA7B180", new Spirit("Elliot", 0x01) },//31FB1
            { "67AB6398", new Spirit("Kevin", 0x01) },//321E8
            { "5322B813", new Spirit("Shawn", 0x01) },//323E0
            { "484EFC82", new Spirit("Jude", 0x01) },//325D8
            { "AD6FF22D", new Spirit("Caleb", 0x01) },//327D0
            { "FF644003", new Spirit("Riccardo", 0x01) },//329C8
            { "6BB6DAED", new Spirit("Scott", 0x01) },//32BC0
            { "357CE5D8", new Spirit("Victoria", 0x01) },//32DB8
            { "758CAFF9", new Spirit("Gabi", 0x01) },//32FB0
            { "C44137AE", new Spirit("Aitor", 0x01) },//331A8
            { "F7EE99CB", new Spirit("Samguk", 0x01) },//333EA0

         };
    }

    public class AdvanceCard
    {

        public Dictionary<string, Spirit> Advancecard = new()
        {
           // { "50EC3B42", new Spirit("Beta Placehorder", 0x02) },
            { "8D3451B0", new Spirit("Lisa", 0x02) },//2CF77(08)
            { "22B358DD", new Spirit("Neppten", 0x02) },//2D16F         
            { "91517368", new Spirit("Zell", 0x02) },
            { "1CCDE5A5", new Spirit("Wittz", 0x02) },
            { "7C4FD329", new Spirit("Lean", 0x02) },
            { "90F96118", new Spirit("Icer", 0x02) },
            { "232DFE6C", new Spirit("Xene", 0x02) },
            { "1D534314", new Spirit("Gazelle", 0x02) },
            { "638243C4", new Spirit("Torch", 0x02) },//2DF37
            { "621CE575", new Spirit("Bellatrix", 0x02) },//2E12F
            { "BEB4EC72", new Spirit("Janus", 0x02) },//2E327
            { "D3D17D22", new Spirit("Kormer", 0x02) },//2E51F
            { "111CF6B8", new Spirit("Bomber", 0x02) },//2E717
            { "51B34B10", new Spirit("Zohen", 0x02) },//2E90F
            { "502DEDA1", new Spirit("Jocker", 0x02) },//2EB07
            { "97BBACEE", new Spirit("Clear", 0x02) },//2ECFF
            { "A63BB169", new Spirit("Dvalin", 0x02) },//2EEF7
            //===============================================
            { "69752537", new Spirit("Hillman", 0x02) },//2F0EF
            { "6E3CDF06", new Spirit("Jordan", 0x02) },//2F2E7
            { "EC5EE934", new Spirit("Xavier", 0x02) },//2F4DF
            { "14DCEC16", new Spirit("Fei Rune", 0x02) },//2F6D7
            { "FB762FCE", new Spirit("Darren", 0x02) },//2F8CF
            { "BFEB9C20", new Spirit("Hurley Kane", 0x02) },//2FAC7
            { "269A7881", new Spirit("Axel Blaze", 0x02) },//2FCBF
            { "81B540D3", new Spirit("Victor Blade", 0x02) },
            { "BA8ED1BD", new Spirit("Heath Moore", 0x02) },//300AF
            { "BE555B1A", new Spirit("Arion", 0x02) },//301AB //CONTROLLALO PLEASE
            { "FBACD737", new Spirit("Byron Love", 0x02) },//3049F
            { "DADD11C7", new Spirit("Sonny", 0x02) },//30697
            { "F32BF5AF", new Spirit("JP", 0x02) },//3088F
            { "93928CB2", new Spirit("Nathan Swift", 0x02) },//30A87
            { "D2A397AB", new Spirit("Jack Wallside", 0x02) },//30C7F
            { "51289B6B", new Spirit("Goldie Lemon", 0x02) },//30E77
            { "50C1A199", new Spirit("Mark Evans", 0x02) },//31228         
            //===========================================
            { "8916462B", new Spirit("Trevis", 0x02) },//31420
            { "50EC3B42", new Spirit("Suzette", 0x02) },//3145F
            { "6FD310AB", new Spirit("Erik", 0x02) },//31810
            { "DD6678C5", new Spirit("Tezcat", 0x02) },//31A08
            { "B3B2FCC6", new Spirit("Bailong", 0x02) },//31C00
            { "EC2242B0", new Spirit("Zanark", 0x02) },//31FF0
            { "0AA7B180", new Spirit("Elliot", 0x02) },//31FB1
            { "67AB6398", new Spirit("Kevin", 0x02) },//321E8
            { "5322B813", new Spirit("Shawn", 0x02) },//323E0
            { "484EFC82", new Spirit("Jude", 0x02) },//325D8
            { "AD6FF22D", new Spirit("Caleb", 0x02) },//327D0
            { "FF644003", new Spirit("Riccardo", 0x02) },//329C8
            { "6BB6DAED", new Spirit("Scott", 0x02) },//32BC0
            { "357CE5D8", new Spirit("Victoria", 0x02) },//32DB8
            { "758CAFF9", new Spirit("Gabi", 0x02) },//32FB0
            { "C44137AE", new Spirit("Aitor", 0x02) },//331A8
            { "F7EE99CB", new Spirit("Samguk", 0x02) },//333EA0
          
         };

    }

    public class TopCard
    {

        public Dictionary<string, Spirit> Topcard = new()
        {
          //  { "50EC3B42", new Spirit("Beta Placehorder", 0x03) },
            { "8D3451B0", new Spirit("Lisa", 0x03) },//2CF77(08)
            { "22B358DD", new Spirit("Neppten", 0x03) },//2D16F         
            { "91517368", new Spirit("Zell", 0x03) },
            { "1CCDE5A5", new Spirit("Wittz", 0x03) },
            { "7C4FD329", new Spirit("Lean", 0x03) },
            { "90F96118", new Spirit("Icer", 0x03) },
            { "232DFE6C", new Spirit("Xene", 0x03) },
            { "1D534314", new Spirit("Gazelle", 0x03) },
            { "638243C4", new Spirit("Torch", 0x03) },//2DF37
            { "621CE575", new Spirit("Bellatrix", 0x03) },//2E12F
            { "BEB4EC72", new Spirit("Janus", 0x03) },//2E327
            { "D3D17D22", new Spirit("Kormer", 0x03) },//2E51F
            { "111CF6B8", new Spirit("Bomber", 0x03) },//2E717
            { "51B34B10", new Spirit("Zohen", 0x03) },//2E90F
            { "502DEDA1", new Spirit("Jocker", 0x03) },//2EB07
            { "97BBACEE", new Spirit("Clear", 0x03) },//2ECFF
            { "A63BB169", new Spirit("Dvalin", 0x03) },//2EEF7
            //===============================================
            { "69752537", new Spirit("Hillman", 0x03) },//2F0EF
            { "6E3CDF06", new Spirit("Jordan", 0x03) },//2F2E7
            { "EC5EE934", new Spirit("Xavier", 0x03) },//2F4DF
            { "14DCEC16", new Spirit("Fei Rune", 0x03) },//2F6D7
            { "FB762FCE", new Spirit("Darren", 0x03) },//2F8CF
            { "BFEB9C20", new Spirit("Hurley Kane", 0x03) },//2FAC7
            { "269A7881", new Spirit("Axel Blaze", 0x03) },//2FCBF
            { "81B540D3", new Spirit("Victor Blade", 0x03) },
            { "BA8ED1BD", new Spirit("Heath Moore", 0x03) },//300AF
            { "BE555B1A", new Spirit("Arion", 0x03) },//301AB //CONTROLLALO PLEASE
            { "FBACD737", new Spirit("Byron Love", 0x03) },//3049F
            { "DADD11C7", new Spirit("Sonny", 0x03) },//30697
            { "F32BF5AF", new Spirit("JP", 0x03) },//3088F
            { "93928CB2", new Spirit("Nathan Swift", 0x03) },//30A87
            { "D2A397AB", new Spirit("Jack Wallside", 0x03) },//30C7F
            { "51289B6B", new Spirit("Goldie Lemon", 0x03) },//30E77
            { "50C1A199", new Spirit("Mark Evans", 0x03) },//31228
           
            //===========================================
            { "8916462B", new Spirit("Trevis", 0x03) },//31420
            { "50EC3B42", new Spirit("Suzette", 0x03) },//3145F
            { "6FD310AB", new Spirit("Erik", 0x03) },//31810
            { "DD6678C5", new Spirit("Tezcat", 0x03) },//31A08
            { "B3B2FCC6", new Spirit("Bailong", 0x03) },//31C00
            { "EC2242B0", new Spirit("Zanark", 0x03) },//31FF0
            { "0AA7B180", new Spirit("Elliot", 0x03) },//31FB1
            { "67AB6398", new Spirit("Kevin", 0x03) },//321E8
            { "5322B813", new Spirit("Shawn", 0x03) },//323E0
            { "484EFC82", new Spirit("Jude", 0x03) },//325D8
            { "AD6FF22D", new Spirit("Caleb", 0x03) },//327D0
            { "FF644003", new Spirit("Riccardo", 0x03) },//329C8
            { "6BB6DAED", new Spirit("Scott", 0x03) },//32BC0
            { "357CE5D8", new Spirit("Victoria", 0x03) },//32DB8
            { "758CAFF9", new Spirit("Gabi", 0x03) },//32FB0
            { "C44137AE", new Spirit("Aitor", 0x03) },//331A8
            { "F7EE99CB", new Spirit("Samguk", 0x03) },//333EA0
         };
    }

    public class LegendaryCard
    {

        public Dictionary<string, Spirit> Legendarycard = new()
        {
           // { "50EC3B42", new Spirit("Beta Placehorder", 0x04) },
            { "8D3451B0", new Spirit("Lisa", 0x04) },//2CF77(08)
            { "22B358DD", new Spirit("Neppten", 0x04) },//2D16F         
            { "91517368", new Spirit("Zell", 0x04) },
            { "1CCDE5A5", new Spirit("Wittz", 0x04) },
            { "7C4FD329", new Spirit("Lean", 0x04) },
            { "90F96118", new Spirit("Icer", 0x04) },
            { "232DFE6C", new Spirit("Xene", 0x04) },
            { "1D534314", new Spirit("Gazelle", 0x04) },
            { "638243C4", new Spirit("Torch", 0x04) },//2DF37
            { "621CE575", new Spirit("Bellatrix", 0x04) },//2E12F
            { "BEB4EC72", new Spirit("Janus", 0x04) },//2E327
            { "D3D17D22", new Spirit("Kormer", 0x04) },//2E51F
            { "111CF6B8", new Spirit("Bomber", 0x04) },//2E717
            { "51B34B10", new Spirit("Zohen", 0x04) },//2E90F
            { "502DEDA1", new Spirit("Jocker", 0x04) },//2EB07
            { "97BBACEE", new Spirit("Clear", 0x04) },//2ECFF
            { "A63BB169", new Spirit("Dvalin", 0x04) },//2EEF7
            //===============================================
            { "69752537", new Spirit("Hillman", 0x04) },//2F0EF
            { "6E3CDF06", new Spirit("Jordan", 0x04) },//2F2E7
            { "EC5EE934", new Spirit("Xavier", 0x04) },//2F4DF
            { "14DCEC16", new Spirit("Fei Rune", 0x04) },//2F6D7
            { "FB762FCE", new Spirit("Darren", 0x04) },//2F8CF
            { "BFEB9C20", new Spirit("Hurley Kane", 0x04) },//2FAC7
            { "269A7881", new Spirit("Axel Blaze", 0x04) },//2FCBF
            { "81B540D3", new Spirit("Victor Blade", 0x04) },
            { "BA8ED1BD", new Spirit("Heath Moore", 0x04) },//300AF
            { "BE555B1A", new Spirit("Arion", 0x04) },//301AB //CONTROLLALO PLEASE
            { "FBACD737", new Spirit("Byron Love", 0x04) },//3049F
            { "DADD11C7", new Spirit("Sonny", 0x04) },//30697
            { "F32BF5AF", new Spirit("JP", 0x04) },//3088F
            { "93928CB2", new Spirit("Nathan Swift", 0x04) },//30A87
            { "D2A397AB", new Spirit("Jack Wallside", 0x04) },//30C7F
            { "51289B6B", new Spirit("Goldie Lemon", 0x04) },//30E77
            { "50C1A199", new Spirit("Mark Evans", 0x04) },//31228         
            //===========================================
            { "8916462B", new Spirit("Trevis", 0x04) },//31420
            { "50EC3B42", new Spirit("Suzette", 0x04) },//3145F
            { "6FD310AB", new Spirit("Erik", 0x04) },//31810
            { "DD6678C5", new Spirit("Tezcat", 0x04) },//31A08
            { "B3B2FCC6", new Spirit("Bailong", 0x04) },//31C00
            { "EC2242B0", new Spirit("Zanark", 0x04) },//31FF0
            { "0AA7B180", new Spirit("Elliot", 0x04) },//31FB1
            { "67AB6398", new Spirit("Kevin", 0x04) },//321E8
            { "5322B813", new Spirit("Shawn", 0x04) },//323E0
            { "484EFC82", new Spirit("Jude", 0x04) },//325D8
            { "AD6FF22D", new Spirit("Caleb", 0x04) },//327D0
            { "FF644003", new Spirit("Riccardo", 0x04) },//329C8
            { "6BB6DAED", new Spirit("Scott", 0x04) },//32BC0
            { "357CE5D8", new Spirit("Victoria", 0x04) },//32DB8
            { "758CAFF9", new Spirit("Gabi", 0x04) },//32FB0
            { "C44137AE", new Spirit("Aitor", 0x04) },//331A8
            { "F7EE99CB", new Spirit("Samguk", 0x04) },//333EA0
         };


    }

    public class Hero1Card
    {

        public Dictionary<string, Spirit> Hero1card = new()
        {
          //  { "50EC3B42", new Spirit("Beta Placehorder", 0x05) },
            { "8D3451B0", new Spirit("Lisa", 0x05) },//2CF77(08)
            { "22B358DD", new Spirit("Neppten", 0x05) },//2D16F         
            { "91517368", new Spirit("Zell", 0x05) },
            { "1CCDE5A5", new Spirit("Wittz", 0x05) },
            { "7C4FD329", new Spirit("Lean", 0x05) },
            { "90F96118", new Spirit("Icer", 0x05) },
            { "232DFE6C", new Spirit("Xene", 0x05) },
            { "1D534314", new Spirit("Gazelle", 0x05) },
            { "638243C4", new Spirit("Torch", 0x05) },//2DF37
            { "621CE575", new Spirit("Bellatrix", 0x05) },//2E12F
            { "BEB4EC72", new Spirit("Janus", 0x05) },//2E327
            { "D3D17D22", new Spirit("Kormer", 0x05) },//2E51F
            { "111CF6B8", new Spirit("Bomber", 0x05) },//2E717
            { "51B34B10", new Spirit("Zohen", 0x05) },//2E90F
            { "502DEDA1", new Spirit("Jocker", 0x05) },//2EB07
            { "97BBACEE", new Spirit("Clear", 0x05) },//2ECFF
            { "A63BB169", new Spirit("Dvalin", 0x05) },//2EEF7
            //===============================================
            { "69752537", new Spirit("Hillman", 0x05) },//2F0EF
            { "6E3CDF06", new Spirit("Jordan", 0x05) },//2F2E7
            { "EC5EE934", new Spirit("Xavier", 0x05) },//2F4DF
            { "14DCEC16", new Spirit("Fei Rune", 0x05) },//2F6D7
            { "FB762FCE", new Spirit("Darren", 0x05) },//2F8CF
            { "BFEB9C20", new Spirit("Hurley Kane", 0x05) },//2FAC7
            { "269A7881", new Spirit("Axel Blaze", 0x05) },//2FCBF
            { "81B540D3", new Spirit("Victor Blade", 0x05) },
            { "BA8ED1BD", new Spirit("Heath Moore", 0x05) },//300AF
            { "BE555B1A", new Spirit("Arion", 0x05) },//301AB //CONTROLLALO PLEASE
            { "FBACD737", new Spirit("Byron Love", 0x05) },//3049F
            { "DADD11C7", new Spirit("Sonny", 0x05) },//30697
            { "F32BF5AF", new Spirit("JP", 0x05) },//3088F
            { "93928CB2", new Spirit("Nathan Swift", 0x05) },//30A87
            { "D2A397AB", new Spirit("Jack Wallside", 0x05) },//30C7F
            { "51289B6B", new Spirit("Goldie Lemon", 0x05) },//30E77
            { "50C1A199", new Spirit("Mark Evans", 0x05) },//31228          
            //===========================================
            { "8916462B", new Spirit("Trevis", 0x05) },//31420
            { "50EC3B42", new Spirit("Suzette", 0x05) },//3145F
            { "6FD310AB", new Spirit("Erik", 0x05) },//31810
            { "DD6678C5", new Spirit("Tezcat", 0x05) },//31A08
            { "B3B2FCC6", new Spirit("Bailong", 0x05) },//31C00
            { "EC2242B0", new Spirit("Zanark", 0x05) },//31FF0
            { "0AA7B180", new Spirit("Elliot", 0x05) },//31FB1
            { "67AB6398", new Spirit("Kevin", 0x05) },//321E8
            { "5322B813", new Spirit("Shawn", 0x05) },//323E0
            { "484EFC82", new Spirit("Jude", 0x05) },//325D8
            { "AD6FF22D", new Spirit("Caleb", 0x05) },//327D0
            { "FF644003", new Spirit("Riccardo", 0x05) },//329C8
            { "6BB6DAED", new Spirit("Scott", 0x05) },//32BC0
            { "357CE5D8", new Spirit("Victoria", 0x05) },//32DB8
            { "758CAFF9", new Spirit("Gabi", 0x05) },//32FB0
            { "C44137AE", new Spirit("Aitor", 0x05) },//331A8
            { "F7EE99CB", new Spirit("Samguk", 0x05) },//333EA0
         };


    }

    public class Hero2Card
    {

        public Dictionary<string, Spirit> Hero2card = new()
        {
          //  { "50EC3B42", new Spirit("Beta Placehorder", 0x06) },
            { "8D3451B0", new Spirit("Lisa", 0x06) },//2CF77(08)
            { "22B358DD", new Spirit("Neppten", 0x06) },//2D16F         
            { "91517368", new Spirit("Zell", 0x06) },
            { "1CCDE5A5", new Spirit("Wittz", 0x06) },
            { "7C4FD329", new Spirit("Lean", 0x06) },
            { "90F96118", new Spirit("Icer", 0x06) },
            { "232DFE6C", new Spirit("Xene", 0x06) },
            { "1D534314", new Spirit("Gazelle", 0x06) },
            { "638243C4", new Spirit("Torch", 0x06) },//2DF37
            { "621CE575", new Spirit("Bellatrix", 0x06) },//2E12F
            { "BEB4EC72", new Spirit("Janus", 0x06) },//2E327
            { "D3D17D22", new Spirit("Kormer", 0x06) },//2E51F
            { "111CF6B8", new Spirit("Bomber", 0x06) },//2E717
            { "51B34B10", new Spirit("Zohen", 0x06) },//2E90F
            { "502DEDA1", new Spirit("Jocker", 0x06) },//2EB07
            { "97BBACEE", new Spirit("Clear", 0x06) },//2ECFF
            { "A63BB169", new Spirit("Dvalin", 0x06) },//2EEF7
            //===============================================
            { "69752537", new Spirit("Hillman", 0x06) },//2F0EF
            { "6E3CDF06", new Spirit("Jordan", 0x06) },//2F2E7
            { "EC5EE934", new Spirit("Xavier", 0x06) },//2F4DF
            { "14DCEC16", new Spirit("Fei Rune", 0x06) },//2F6D7
            { "FB762FCE", new Spirit("Darren", 0x06) },//2F8CF
            { "BFEB9C20", new Spirit("Hurley Kane", 0x06) },//2FAC7
            { "269A7881", new Spirit("Axel Blaze", 0x06) },//2FCBF
            { "81B540D3", new Spirit("Victor Blade", 0x06) },
            { "BA8ED1BD", new Spirit("Heath Moore", 0x06) },//300AF
            { "BE555B1A", new Spirit("Arion", 0x06) },//301AB //CONTROLLALO PLEASE
            { "FBACD737", new Spirit("Byron Love", 0x06) },//3049F
            { "DADD11C7", new Spirit("Sonny", 0x06) },//30697
            { "F32BF5AF", new Spirit("JP", 0x06) },//3088F
            { "93928CB2", new Spirit("Nathan Swift", 0x06) },//30A87
            { "D2A397AB", new Spirit("Jack Wallside", 0x06) },//30C7F
            { "51289B6B", new Spirit("Goldie Lemon", 0x06) },//30E77
            { "50C1A199", new Spirit("Mark Evans", 0x06) },//31228         
            //===========================================
            { "8916462B", new Spirit("Trevis", 0x06) },//31420
            { "50EC3B42", new Spirit("Suzette", 0x06) },//3145F
            { "6FD310AB", new Spirit("Erik", 0x06) },//31810
            { "DD6678C5", new Spirit("Tezcat", 0x06) },//31A08
            { "B3B2FCC6", new Spirit("Bailong", 0x06) },//31C00
            { "EC2242B0", new Spirit("Zanark", 0x06) },//31FF0
            { "0AA7B180", new Spirit("Elliot", 0x06) },//31FB1
            { "67AB6398", new Spirit("Kevin", 0x06) },//321E8
            { "5322B813", new Spirit("Shawn", 0x06) },//323E0
            { "484EFC82", new Spirit("Jude", 0x06) },//325D8
            { "AD6FF22D", new Spirit("Caleb", 0x06) },//327D0
            { "FF644003", new Spirit("Riccardo", 0x06) },//329C8
            { "6BB6DAED", new Spirit("Scott", 0x06) },//32BC0
            { "357CE5D8", new Spirit("Victoria", 0x06) },//32DB8
            { "758CAFF9", new Spirit("Gabi", 0x06) },//32FB0
            { "C44137AE", new Spirit("Aitor", 0x06) },//331A8
            { "F7EE99CB", new Spirit("Samguk", 0x06) },//333EA0
         };
    }

    public class Hero3Card
    {

        public Dictionary<string, Spirit> Hero3card = new()
        {
          //  { "50EC3B42", new Spirit("Beta Placehorder", 0x07) },
            { "8D3451B0", new Spirit("Lisa", 0x07) },//2CF77(08)
            { "22B358DD", new Spirit("Neppten", 0x07) },//2D16F         
            { "91517368", new Spirit("Zell", 0x07) },
            { "1CCDE5A5", new Spirit("Wittz", 0x07) },
            { "7C4FD329", new Spirit("Lean", 0x07) },
            { "90F96118", new Spirit("Icer", 0x07) },
            { "232DFE6C", new Spirit("Xene", 0x07) },
            { "1D534314", new Spirit("Gazelle", 0x07) },
            { "638243C4", new Spirit("Torch", 0x07) },//2DF37
            { "621CE575", new Spirit("Bellatrix", 0x07) },//2E12F
            { "BEB4EC72", new Spirit("Janus", 0x07) },//2E327
            { "D3D17D22", new Spirit("Kormer", 0x07) },//2E51F
            { "111CF6B8", new Spirit("Bomber", 0x07) },//2E717
            { "51B34B10", new Spirit("Zohen", 0x07) },//2E90F
            { "502DEDA1", new Spirit("Jocker", 0x07) },//2EB07
            { "97BBACEE", new Spirit("Clear", 0x07) },//2ECFF
            { "A63BB169", new Spirit("Dvalin", 0x07) },//2EEF7
            //===============================================
            { "69752537", new Spirit("Hillman", 0x07) },//2F0EF
            { "6E3CDF06", new Spirit("Jordan", 0x07) },//2F2E7
            { "EC5EE934", new Spirit("Xavier", 0x07) },//2F4DF
            { "14DCEC16", new Spirit("Fei Rune", 0x07) },//2F6D7
            { "FB762FCE", new Spirit("Darren", 0x07) },//2F8CF
            { "BFEB9C20", new Spirit("Hurley Kane", 0x07) },//2FAC7
            { "269A7881", new Spirit("Axel Blaze", 0x07) },//2FCBF
            { "81B540D3", new Spirit("Victor Blade", 0x07) },
            { "BA8ED1BD", new Spirit("Heath Moore", 0x07) },//300AF
            { "BE555B1A", new Spirit("Arion", 0x07) },//301AB //CONTROLLALO PLEASE
            { "FBACD737", new Spirit("Byron Love", 0x07) },//3049F
            { "DADD11C7", new Spirit("Sonny", 0x07) },//30697
            { "F32BF5AF", new Spirit("JP", 0x07) },//3088F
            { "93928CB2", new Spirit("Nathan Swift", 0x07) },//30A87
            { "D2A397AB", new Spirit("Jack Wallside", 0x07) },//30C7F
            { "51289B6B", new Spirit("Goldie Lemon", 0x07) },//30E77
            { "50C1A199", new Spirit("Mark Evans", 0x07) },//31228          
            //===========================================
            { "8916462B", new Spirit("Trevis", 0x07) },//31420
            { "50EC3B42", new Spirit("Suzette", 0x07) },//3145F
            { "6FD310AB", new Spirit("Erik", 0x07) },//31810
            { "DD6678C5", new Spirit("Tezcat", 0x07) },//31A08
            { "B3B2FCC6", new Spirit("Bailong", 0x07) },//31C00
            { "EC2242B0", new Spirit("Zanark", 0x07) },//31FF0
            { "0AA7B180", new Spirit("Elliot", 0x07) },//31FB1
            { "67AB6398", new Spirit("Kevin", 0x07) },//321E8
            { "5322B813", new Spirit("Shawn", 0x07) },//323E0
            { "484EFC82", new Spirit("Jude", 0x07) },//325D8
            { "AD6FF22D", new Spirit("Caleb", 0x07) },//327D0
            { "FF644003", new Spirit("Riccardo", 0x07) },//329C8
            { "6BB6DAED", new Spirit("Scott", 0x07) },//32BC0
            { "357CE5D8", new Spirit("Victoria", 0x07) },//32DB8
            { "758CAFF9", new Spirit("Gabi", 0x07) },//32FB0
            { "C44137AE", new Spirit("Aitor", 0x07) },//331A8
            { "F7EE99CB", new Spirit("Samguk", 0x07) },//333EA0
         };
    }
}



