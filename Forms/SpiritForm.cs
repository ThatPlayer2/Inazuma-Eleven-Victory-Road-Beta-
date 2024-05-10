using Inazuma_Eleven_Victory_Road_Beta.Logic;
using InazumaElevenVictoryRoad.InazumaEleven_VR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InazumaElevenVictoryRoad
{
    public partial class SpiritForm : Form
    {
        public SpiritForm()
        {

            InitializeComponent();
            NormalGrade.CellEndEdit += DataGridView1_CellEndEdit;
            GrowithGrade.CellEndEdit += DataGridView2_1_CellEndEdit;
            AdvanceGrade.CellEndEdit += DataGridView3_CellEndEdit;
            TopGrade.CellEndEdit += DataGridView4_CellEndEdit;
            LegendaryGrade.CellEndEdit += DataGridView5_CellEndEdit;
            Hero1Grade.CellEndEdit += DataGridView6_CellEndEdit;
            Hero2Grade.CellEndEdit += DataGridView7_CellEndEdit;
            Hero3Grade.CellEndEdit += DataGridView8_CellEndEdit;
        }

        public void FindNormalCard()
        {
            NormalGrade.Rows.Clear();

            var normalCard = new NormalCard();

            foreach (var kvp in normalCard.Normalcard)
            {
                string idToFind = kvp.Key;
                Spirit spirit = kvp.Value;

                byte[] idBytes = FileReader.StringToByteArray(idToFind);

                int objectIndex = 0;

                while ((objectIndex = FileReader.FindPattern(FileReader.fileBytes, idBytes, objectIndex, 0x000343D0)) != -1)
                {

                    int valueCheckIndex = objectIndex + 34;
                    byte valueToCheck = FileReader.fileBytes[valueCheckIndex];

                    if (valueToCheck == spirit.ValueToCheck)
                    {

                        int quantityIndex = objectIndex + 24;
                        byte quantity = FileReader.fileBytes[quantityIndex];

                        NormalGrade.Rows.Add(idToFind, spirit.ID, quantity);

                    }

                    objectIndex += idBytes.Length;
                }
            }
        }



        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                string idToFind = NormalGrade.Rows[e.RowIndex].Cells[0].Value.ToString();
                string quantityStr = NormalGrade.Rows[e.RowIndex].Cells[2].Value.ToString();

                if (byte.TryParse(quantityStr, out byte quantity))
                {
                    NormalCard normalCard = new();


                    if (normalCard.Normalcard.TryGetValue(idToFind, out _))
                    {
                        byte[] idBytes = FileReader.StringToByteArray(idToFind);

                        // Loop through all occurrences of the ID in the file
                        int objectIndex = 0;
                        while ((objectIndex = FileReader.FindPattern(FileReader.fileBytes, idBytes, objectIndex, 0x000343D0)) != -1)
                        {
                            int valueCheckIndex = objectIndex + 34;
                            byte valueToCheck = FileReader.fileBytes[valueCheckIndex];

                            if (valueToCheck == normalCard.Normalcard[idToFind].ValueToCheck)
                            {
                                int quantityIndex = objectIndex + 24;
                                FileReader.fileBytes[quantityIndex] = quantity;
                            }

                            objectIndex += idBytes.Length;
                        }
                    }
                }
            }
        }

        //=============================================================


        public void FindGrowithCard()
        {
            GrowithGrade.Rows.Clear();

            var growithCard = new GrowithCard();

            foreach (var kvp in growithCard.Growithcard)
            {
                string idToFind = kvp.Key;
                Spirit spirit = kvp.Value;

                byte[] idBytes = FileReader.StringToByteArray(idToFind);

                int objectIndex = 0;

                while ((objectIndex = FileReader.FindPattern(FileReader.fileBytes, idBytes, objectIndex, 0x000343D0)) != -1)
                {

                    int valueCheckIndex = objectIndex + 34;
                    byte valueToCheck = FileReader.fileBytes[valueCheckIndex];

                    if (valueToCheck == spirit.ValueToCheck)
                    {

                        int quantityIndex = objectIndex + 24;
                        byte quantity = FileReader.fileBytes[quantityIndex];

                        GrowithGrade.Rows.Add(idToFind, spirit.ID, quantity);
                    }

                    objectIndex += idBytes.Length;
                }
            }
        }

        private void DataGridView2_1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                string idToFind = GrowithGrade.Rows[e.RowIndex].Cells[0].Value.ToString();
                string quantityStr = GrowithGrade.Rows[e.RowIndex].Cells[2].Value.ToString();

                if (byte.TryParse(quantityStr, out byte quantity))
                {
                    GrowithCard growithCard = new();

                    if (growithCard.Growithcard.TryGetValue(idToFind, out _))
                    {
                        byte[] idBytes = FileReader.StringToByteArray(idToFind);

                        // Loop through all occurrences of the ID in the file
                        int objectIndex = 0;
                        while ((objectIndex = FileReader.FindPattern(FileReader.fileBytes, idBytes, objectIndex, 0x000343D0)) != -1)
                        {
                            int valueCheckIndex = objectIndex + 34;
                            byte valueToCheck = FileReader.fileBytes[valueCheckIndex];

                            if (valueToCheck == growithCard.Growithcard[idToFind].ValueToCheck)
                            {
                                int quantityIndex = objectIndex + 24;
                                FileReader.fileBytes[quantityIndex] = quantity;
                            }

                            objectIndex += idBytes.Length;
                        }
                    }
                }
            }
        }

        //========================================================================================

        public void FindAdvanceCard()
        {
            AdvanceGrade.Rows.Clear();

            AdvanceCard advanceCard = new();

            foreach (var kvp in advanceCard.Advancecard)
            {
                string idToFind = kvp.Key;
                Spirit spirit = kvp.Value;

                byte[] idBytes = FileReader.StringToByteArray(idToFind);

                int objectIndex = 0;

                while ((objectIndex = FileReader.FindPattern(FileReader.fileBytes, idBytes, objectIndex, 0x000343D0)) != -1)
                {

                    int valueCheckIndex = objectIndex + 34;
                    byte valueToCheck = FileReader.fileBytes[valueCheckIndex];

                    if (valueToCheck == spirit.ValueToCheck)
                    {

                        int quantityIndex = objectIndex + 24;
                        byte quantity = FileReader.fileBytes[quantityIndex];

                        AdvanceGrade.Rows.Add(idToFind, spirit.ID, quantity);
                    }

                    objectIndex += idBytes.Length;
                }
            }
        }


        private void DataGridView3_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                string idToFind = AdvanceGrade.Rows[e.RowIndex].Cells[0].Value.ToString();
                string quantityStr = AdvanceGrade.Rows[e.RowIndex].Cells[2].Value.ToString();

                if (byte.TryParse(quantityStr, out byte quantity))
                {
                    AdvanceCard advanceCard = new();

                    if (advanceCard.Advancecard.TryGetValue(idToFind, out _))
                    {
                        byte[] idBytes = FileReader.StringToByteArray(idToFind);

                        // Loop through all occurrences of the ID in the file
                        int objectIndex = 0;
                        while ((objectIndex = FileReader.FindPattern(FileReader.fileBytes, idBytes, objectIndex, 0x000343D0)) != -1)
                        {
                            int valueCheckIndex = objectIndex + 34;
                            byte valueToCheck = FileReader.fileBytes[valueCheckIndex];

                            if (valueToCheck == advanceCard.Advancecard[idToFind].ValueToCheck)
                            {
                                int quantityIndex = objectIndex + 24;
                                FileReader.fileBytes[quantityIndex] = quantity;
                            }

                            objectIndex += idBytes.Length;
                        }
                    }
                }
            }
        }

        //==================================================================================================

        public void FindTopCard()
        {
            TopGrade.Rows.Clear();

            TopCard topCard = new();

            foreach (var kvp in topCard.Topcard)
            {
                string idToFind = kvp.Key;
                Spirit spirit = kvp.Value;

                byte[] idBytes = FileReader.StringToByteArray(idToFind);

                int objectIndex = 0;


                while ((objectIndex = FileReader.FindPattern(FileReader.fileBytes, idBytes, objectIndex, 0x000343D0)) != -1)
                {

                    int valueCheckIndex = objectIndex + 34;
                    byte valueToCheck = FileReader.fileBytes[valueCheckIndex];


                    if (valueToCheck == spirit.ValueToCheck)
                    {

                        int quantityIndex = objectIndex + 24;
                        byte quantity = FileReader.fileBytes[quantityIndex];

                        TopGrade.Rows.Add(idToFind, spirit.ID, quantity);
                    }

                    objectIndex += idBytes.Length;
                }
            }
        }

        private void DataGridView4_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 2)
            {
                string idToFind = TopGrade.Rows[e.RowIndex].Cells[0].Value.ToString();
                string quantityStr = TopGrade.Rows[e.RowIndex].Cells[2].Value.ToString();

                if (byte.TryParse(quantityStr, out byte quantity))
                {
                    TopCard topCard = new();

                    if (topCard.Topcard.TryGetValue(idToFind, out _))
                    {
                        byte[] idBytes = FileReader.StringToByteArray(idToFind);

                        // Loop through all occurrences of the ID in the file
                        int objectIndex = 0;
                        while ((objectIndex = FileReader.FindPattern(FileReader.fileBytes, idBytes, objectIndex, 0x000343D0)) != -1)
                        {
                            int valueCheckIndex = objectIndex + 34;
                            byte valueToCheck = FileReader.fileBytes[valueCheckIndex];

                            if (valueToCheck == topCard.Topcard[idToFind].ValueToCheck)
                            {
                                int quantityIndex = objectIndex + 24;
                                FileReader.fileBytes[quantityIndex] = quantity;
                            }

                            objectIndex += idBytes.Length;
                        }
                    }
                }
            }
        }

        //==========================================================================

        public void FindLegendaryCard()
        {
            LegendaryGrade.Rows.Clear();

            LegendaryCard legendaryCard = new();

            foreach (var kvp in legendaryCard.Legendarycard)
            {
                string idToFind = kvp.Key;
                Spirit spirit = kvp.Value;

                byte[] idBytes = FileReader.StringToByteArray(idToFind);

                int objectIndex = 0;


                while ((objectIndex = FileReader.FindPattern(FileReader.fileBytes, idBytes, objectIndex, 0x000343D0)) != -1)
                {

                    int valueCheckIndex = objectIndex + 34;
                    byte valueToCheck = FileReader.fileBytes[valueCheckIndex];


                    if (valueToCheck == spirit.ValueToCheck)
                    {

                        int quantityIndex = objectIndex + 24;
                        byte quantity = FileReader.fileBytes[quantityIndex];


                        LegendaryGrade.Rows.Add(idToFind, spirit.ID, quantity);
                    }

                    objectIndex += idBytes.Length;
                }
            }
        }

        private void DataGridView5_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                string idToFind = LegendaryGrade.Rows[e.RowIndex].Cells[0].Value.ToString();
                string quantityStr = LegendaryGrade.Rows[e.RowIndex].Cells[2].Value.ToString();

                if (byte.TryParse(quantityStr, out byte quantity))
                {
                    LegendaryCard legendaryCard = new();

                    if (legendaryCard.Legendarycard.TryGetValue(idToFind, out _))
                    {
                        byte[] idBytes = FileReader.StringToByteArray(idToFind);

                        // Loop through all occurrences of the ID in the file
                        int objectIndex = 0;
                        while ((objectIndex = FileReader.FindPattern(FileReader.fileBytes, idBytes, objectIndex, 0x000343D0)) != -1)
                        {
                            int valueCheckIndex = objectIndex + 34;
                            byte valueToCheck = FileReader.fileBytes[valueCheckIndex];

                            if (valueToCheck == legendaryCard.Legendarycard[idToFind].ValueToCheck)
                            {
                                int quantityIndex = objectIndex + 24;
                                FileReader.fileBytes[quantityIndex] = quantity;
                            }

                            objectIndex += idBytes.Length;
                        }
                    }
                }
            }
        }

        //====================================================================
        public void FindHero1Card()
        {
            Hero1Grade.Rows.Clear();

            Hero1Card hero1Card = new();

            foreach (var kvp in hero1Card.Hero1card)
            {
                string idToFind = kvp.Key;
                Spirit spirit = kvp.Value;

                byte[] idBytes = FileReader.StringToByteArray(idToFind);

                int objectIndex = 0;

                while ((objectIndex = FileReader.FindPattern(FileReader.fileBytes, idBytes, objectIndex, 0x000343D0)) != -1)
                {

                    int valueCheckIndex = objectIndex + 34;
                    byte valueToCheck = FileReader.fileBytes[valueCheckIndex];


                    if (valueToCheck == spirit.ValueToCheck)
                    {

                        int quantityIndex = objectIndex + 24;
                        byte quantity = FileReader.fileBytes[quantityIndex];


                        Hero1Grade.Rows.Add(idToFind, spirit.ID, quantity);
                    }

                    objectIndex += idBytes.Length;
                }
            }
        }

        private void DataGridView6_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 2)
            {
                string idToFind = Hero1Grade.Rows[e.RowIndex].Cells[0].Value.ToString();
                string quantityStr = Hero1Grade.Rows[e.RowIndex].Cells[2].Value.ToString();

                if (byte.TryParse(quantityStr, out byte quantity))
                {
                    Hero1Card hero1Card = new();

                    if (hero1Card.Hero1card.TryGetValue(idToFind, out _))
                    {
                        byte[] idBytes = FileReader.StringToByteArray(idToFind);

                        // Loop through all occurrences of the ID in the file
                        int objectIndex = 0;
                        while ((objectIndex = FileReader.FindPattern(FileReader.fileBytes, idBytes, objectIndex, 0x000343D0)) != -1)
                        {
                            int valueCheckIndex = objectIndex + 34;
                            byte valueToCheck = FileReader.fileBytes[valueCheckIndex];

                            if (valueToCheck == hero1Card.Hero1card[idToFind].ValueToCheck)
                            {
                                int quantityIndex = objectIndex + 24;
                                FileReader.fileBytes[quantityIndex] = quantity;
                            }

                            objectIndex += idBytes.Length;
                        }
                    }
                }
            }
        }

        //=========================================================================

        public void FindHero2Card()
        {
            Hero2Grade.Rows.Clear();

            Hero2Card hero2Card = new();

            foreach (var kvp in hero2Card.Hero2card)
            {
                string idToFind = kvp.Key;
                Spirit spirit = kvp.Value;

                byte[] idBytes = FileReader.StringToByteArray(idToFind);

                int objectIndex = 0;


                while ((objectIndex = FileReader.FindPattern(FileReader.fileBytes, idBytes, objectIndex, 0x000343D0)) != -1)
                {

                    int valueCheckIndex = objectIndex + 34;
                    byte valueToCheck = FileReader.fileBytes[valueCheckIndex];


                    if (valueToCheck == spirit.ValueToCheck)
                    {

                        int quantityIndex = objectIndex + 24;
                        byte quantity = FileReader.fileBytes[quantityIndex];

                        Hero2Grade.Rows.Add(idToFind, spirit.ID, quantity);
                    }


                    objectIndex += idBytes.Length;
                }
            }
        }

        private void DataGridView7_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                string idToFind = Hero2Grade.Rows[e.RowIndex].Cells[0].Value.ToString();
                string quantityStr = Hero2Grade.Rows[e.RowIndex].Cells[2].Value.ToString();

                if (byte.TryParse(quantityStr, out byte quantity))
                {
                    Hero2Card hero2Card = new();

                    if (hero2Card.Hero2card.TryGetValue(idToFind, out _))
                    {
                        byte[] idBytes = FileReader.StringToByteArray(idToFind);

                        // Loop through all occurrences of the ID in the file
                        int objectIndex = 0;
                        while ((objectIndex = FileReader.FindPattern(FileReader.fileBytes, idBytes, objectIndex, 0x000343D0)) != -1)
                        {
                            int valueCheckIndex = objectIndex + 34;
                            byte valueToCheck = FileReader.fileBytes[valueCheckIndex];

                            if (valueToCheck == hero2Card.Hero2card[idToFind].ValueToCheck)
                            {
                                int quantityIndex = objectIndex + 24;
                                FileReader.fileBytes[quantityIndex] = quantity;
                            }

                            objectIndex += idBytes.Length;
                        }
                    }
                }
            }
        }

        //=========================================================================

        public void FindHero3Card()
        {
            Hero3Grade.Rows.Clear();

            Hero3Card hero3Card = new();

            foreach (var kvp in hero3Card.Hero3card)
            {
                string idToFind = kvp.Key;
                Spirit spirit = kvp.Value;

                byte[] idBytes = FileReader.StringToByteArray(idToFind);

                int objectIndex = 0;


                while ((objectIndex = FileReader.FindPattern(FileReader.fileBytes, idBytes, objectIndex, 0x000343D0)) != -1)
                {

                    int valueCheckIndex = objectIndex + 34;
                    byte valueToCheck = FileReader.fileBytes[valueCheckIndex];

                    if (valueToCheck == spirit.ValueToCheck)
                    {

                        int quantityIndex = objectIndex + 24;
                        byte quantity = FileReader.fileBytes[quantityIndex];

                        Hero3Grade.Rows.Add(idToFind, spirit.ID, quantity);
                    }

                    objectIndex += idBytes.Length;
                }
            }
        }

        private void DataGridView8_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                string idToFind = Hero3Grade.Rows[e.RowIndex].Cells[0].Value.ToString();
                string quantityStr = Hero3Grade.Rows[e.RowIndex].Cells[2].Value.ToString();

                if (byte.TryParse(quantityStr, out byte quantity))
                {
                    Hero3Card hero3Card = new();

                    if (hero3Card.Hero3card.TryGetValue(idToFind, out _))
                    {
                        byte[] idBytes = FileReader.StringToByteArray(idToFind);

                        // Loop through all occurrences of the ID in the file
                        int objectIndex = 0;
                        while ((objectIndex = FileReader.FindPattern(FileReader.fileBytes, idBytes, objectIndex, 0x000343D0)) != -1)
                        {
                            int valueCheckIndex = objectIndex + 34;
                            byte valueToCheck = FileReader.fileBytes[valueCheckIndex];

                            if (valueToCheck == hero3Card.Hero3card[idToFind].ValueToCheck)
                            {
                                int quantityIndex = objectIndex + 24;
                                FileReader.fileBytes[quantityIndex] = quantity;
                            }

                            objectIndex += idBytes.Length;
                        }
                    }
                }
            }
        }
    }
}
