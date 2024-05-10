using System.Diagnostics;
using System.Windows.Forms;
using System.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Text.RegularExpressions;
using InazumaElevenVictoryRoad.InazumaEleven_VR;
using static InazumaElevenVictoryRoad.InazumaEleven_VR.Tokens;
using System.Runtime.Serialization;
using Inazuma_Eleven_Victory_Road_Beta.InazumaEleven_V1._0._0;
using System.Text;
using Inazuma_Eleven_Victory_Road_Beta;
using Inazuma_Eleven_Victory_Road_Beta.Logic;

namespace InazumaElevenVictoryRoad
{
    public partial class Main : Form
    {

        private Tokens italianTokens;
        readonly Tokens italiantokens = new("italian");
        private readonly EnglishTokens englishTokens;
        private Items italianItems;
        readonly Items italianitems = new("italian");
        private readonly EnglishItems englishitems;

        readonly PlayerManager playerManager = new();
        SpiritForm spirit = new();
        InventoryForm inventoryForm = new();

        private bool playerDataChanged = false;
        private bool comboBoxesChanged = false;
        private int currentSelectedPlayerIndex = -1;


        public Main()
        {
            Controls.Add(dataGridView1);
            Controls.Add(dataGridView2);
            Controls.Add(numericUpDown1);
            Controls.Add(numericUpDown2);


            InitializeComponent();

            italianTokens = new Tokens("italian");
            englishTokens = new EnglishTokens();
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;//Player
            dataGridView2.CellEndEdit += DataGridView2_CellEndEdit;//Token

            // TestForm.dataGridViewNormal.CellEndEdit += dataGridViewNormal_CellEndEdit;

            //Player Levels
            numericUpDown1.Minimum = 1;
            numericUpDown1.Maximum = 30;
            numericUpDown1.Enabled = false;

            //Grade Players
            numericUpDown2.Minimum = 0;
            numericUpDown2.Maximum = 7;
            numericUpDown2.Enabled = false;


            comboBoxMove1.SelectedIndexChanged += ComboBoxMove1_SelectedIndexChanged;
            comboBoxMove2.SelectedIndexChanged += ComboBoxMove2_SelectedIndexChanged;
            comboBoxMove1.Enabled = false;
            comboBoxMove2.Enabled = false;
            label4.Enabled = false;
            comboBox1.Enabled = false;
			
			Moves.InitializeMoves(); //LOAD MOVES
        }



        //OPEN

        private void OpenToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "Binary file (*.bin)|*.bin";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
				if (FileReader.fileBytes != null)
				{
					 Array.Clear(FileReader.fileBytes, 0, FileReader.fileBytes.Length);
				}
				
                string filePath = openFileDialog.FileName;
				
                FileReader.fileBytes = File.ReadAllBytes(filePath);


                foreach (Player player in playerManager.Players)
                {
                    player.Indice = FileReader.FindPattern(FileReader.fileBytes, player.Id, player.Offset, FileReader.fileBytes.Length);
                    if (player.Indice == -1)
                    {
                        MessageBox.Show($"ID of {player.Name} Not found.");
                        return;
                    }
                }

                SearchAndPopulateObjects(true);
                ShowData();

                numericUpDown1.Enabled = true;
                numericUpDown2.Enabled = true;
                comboBoxMove1.Enabled = true;
                comboBoxMove2.Enabled = true;
                label4.Enabled = true;

                inventoryForm.SearchAndPopulateItem(true);
                spirit.FindNormalCard();
                spirit.FindGrowithCard();
                spirit.FindAdvanceCard();
                spirit.FindTopCard();
                spirit.FindLegendaryCard();
                spirit.FindHero1Card();
                spirit.FindHero2Card();
                spirit.FindHero3Card();

            }
        }



        //SAVE

        private void SaveChangesToFile()
        {
            if (FileReader.fileBytes != null && playerManager.Players.Count > 0)
            {
                SaveFileDialog saveFileDialog = new()
                {
                    Filter = "Binary File (*.bin)|*.bin"
                };
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {

                    if (FileReader.fileBytes.Length < playerManager.Players.Max(g => g.Offset) + 92)
                    {
                        MessageBox.Show("Insufficient size of fileBytes array to hold player data.");
                        return;
                    }

                    foreach (Player player in playerManager.Players)
                    {
                        int currentPlayerIndex = player.Indice;
                        int currentPlayerOffset = player.Offset;

                        byte level = 1;
                        byte grade = 0;

                        DataGridViewRow row = dataGridView1.Rows.Cast<DataGridViewRow>().FirstOrDefault(r => BitConverter.ToString(FileReader.fileBytes, currentPlayerIndex, 4).Replace("-", "") == r.Cells[0].Value?.ToString());

                        if (row != null)
                        {
                            level = Convert.ToByte(row.Cells[2].Value);
                            grade = Convert.ToByte(row.Cells[3].Value);
                        }

                        FileReader.fileBytes[currentPlayerOffset + 56] = level;
                        FileReader.fileBytes[currentPlayerOffset + 92] = grade;
                    }
                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        if (row != null && !row.IsNewRow)
                        {
                            if (row.Cells[0].Value != null && row.Cells[1].Value != null && row.Cells[2].Value != null)
                            {
                                string objectId = row.Cells[0].Value.ToString();
                                string quantityString = row.Cells[2].Value.ToString();
                                ushort quantity = ushort.Parse(quantityString);

                                int objectIndex = FileReader.FindPattern(FileReader.fileBytes, FileReader.StringToByteArray(objectId), 0x0001BB17, 0x0001BCDF);

                                if (objectIndex != -1)
                                {

                                    int quantityIndex = objectIndex + 24;

                                    byte[] quantityBytes = BitConverter.GetBytes(quantity);
                                    Array.Copy(quantityBytes, 0, FileReader.fileBytes, quantityIndex, 2);
                                }
                                else
                                {
                                    MessageBox.Show("Object ID not in allowed range.");
                                }
                            }
                        }
                    }
                    File.WriteAllBytes(saveFileDialog.FileName, FileReader.fileBytes);
                    MessageBox.Show("Save successifull!");
                    playerDataChanged = false;
                }
            }
        }

        //MENU SAVE
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveChangesToFile();
        }


        //================================================================================


        private void UpdateValueDataGridView()
        {
            if (FileReader.fileBytes == null)
            {
                MessageBox.Show("Load Game File.");
                return;
            }
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;

                if (selectedIndex >= 0 && selectedIndex < dataGridView1.Rows.Count)
                {

                    comboBoxMove1.Items.Clear();
                    comboBoxMove2.Items.Clear();


                    int currentPlayerIndex = playerManager.Players[selectedIndex].Indice;

                    byte[] move1Bytes = new byte[4];
                    Array.Copy(FileReader.fileBytes, currentPlayerIndex + 12, move1Bytes, 0, 4);
                    string move1Id = BitConverter.ToString(move1Bytes).Replace("-", "");

                    byte[] move2Bytes = new byte[4];
                    Array.Copy(FileReader.fileBytes, currentPlayerIndex + 16, move2Bytes, 0, 4);
                    string move2Id = BitConverter.ToString(move2Bytes).Replace("-", "");

                    comboBoxMove1.Items.AddRange(Moves.Specialmoves.Values.ToArray());
                    comboBoxMove2.Items.AddRange(Moves.Specialmoves.Values.ToArray());


                    comboBoxMove1.SelectedItem = GetMoveName(move1Id);
                    comboBoxMove2.SelectedItem = GetMoveName(move2Id);
                }
            }
        }

        private static string GetMoveName(string moveId)
        {
            if (moveId != null && Moves.Specialmoves.TryGetValue(moveId, out string? value))
            {
                string moveName = value;

                return moveName;
            }
            else
            {
                return "No Move";
            }
        }

        private void ComboBoxMove1_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.ComboBox comboBox = sender as System.Windows.Forms.ComboBox;
            string selectedMoveName = comboBox.SelectedItem.ToString();
            string selectedMoveId = Moves.Specialmoves.FirstOrDefault(x => x.Value == selectedMoveName).Key;
            Player currentPlayer = GetSelectedPlayer();
            if (currentPlayer != null)
            {

                UpdatePlayerMove(currentPlayer, selectedMoveId, 1); // Passa 1 per indicare la prima mossa
                UpdateComboBoxMove(currentPlayer, selectedMoveId, comboBoxMove1);
                comboBoxesChanged = true;
            }
        }

        private void ComboBoxMove2_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.ComboBox comboBox = sender as System.Windows.Forms.ComboBox;
            string selectedMoveName = comboBox.SelectedItem.ToString();
            string selectedMoveId = Moves.Specialmoves.FirstOrDefault(x => x.Value == selectedMoveName).Key;
            Player currentPlayer = GetSelectedPlayer();
            if (currentPlayer != null)
            {

                UpdatePlayerMove(currentPlayer, selectedMoveId, 2); // Passa 2 per indicare la seconda mossa
                UpdateComboBoxMove(currentPlayer, selectedMoveId, comboBoxMove2);
                comboBoxesChanged = true;
            }
        }

        private static void UpdatePlayerMove(Player currentPlayer, string selectedMoveId, int moveNumber)
        {


            if (currentPlayer.Indice != -1)
            {
                int moveIndex = currentPlayer.Indice + (moveNumber == 1 ? 12 : 16); // Calcola l'indice corretto in base al numero della mossa

                byte[] moveBytes = FileReader.StringToByteArray(selectedMoveId);

                if (moveBytes.Length == 4)
                {

                    Array.Copy(moveBytes, 0, FileReader.fileBytes, moveIndex, 4);
                }


                if (moveNumber == 1)
                {
                    currentPlayer.MoveId1 = selectedMoveId;
                }
                else if (moveNumber == 2)
                {
                    currentPlayer.MoveId2 = selectedMoveId;
                }
            }



        }

        private Player GetSelectedPlayer()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;
                if (selectedIndex >= 0 && selectedIndex < playerManager.Players.Count)
                {
                    return playerManager.Players[selectedIndex];
                }
            }

            return null;
        }

        private void UpdateComboBoxMove(Player currentPlayer, string selectedMoveId, System.Windows.Forms.ComboBox comboBox)
        {
            //Remove event Handler to prevent it from being called during update
            comboBox.SelectedIndexChanged -= ComboBoxMove1_SelectedIndexChanged;
            comboBox.SelectedIndexChanged -= ComboBoxMove2_SelectedIndexChanged;

            // Set element id on ComboBox
            if (comboBox == comboBoxMove1)
            {
                comboBoxMove1.SelectedItem = GetMoveName(currentPlayer.MoveId1);
                // Update the first move ID in the player only if ComboBox 1 has been updated
                currentPlayer.MoveId1 = selectedMoveId;
            }
			
			if (currentPlayer.MoveId1 == null || !Moves.Specialmoves.TryGetValue(currentPlayer.MoveId1, out string? moveName1))
			{
				comboBoxMove1.SelectedItem = "No Move"; 
			}
			
            else if (comboBox == comboBoxMove2)
            {
                comboBoxMove2.SelectedItem = GetMoveName(currentPlayer.MoveId2);
                // Update the first move ID in the player only if ComboBox 2 has been updated
                currentPlayer.MoveId2 = selectedMoveId;
            }
			
			if (currentPlayer.MoveId2 == null || !Moves.Specialmoves.TryGetValue(currentPlayer.MoveId2, out string? moveName2))
		    {
				comboBoxMove2.SelectedItem = "No Move";
			}

            //update event
            comboBox.SelectedIndexChanged += (comboBox == comboBoxMove1) ? ComboBoxMove1_SelectedIndexChanged : ComboBoxMove2_SelectedIndexChanged;
        }



        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (playerDataChanged)
            {
                SaveChangesToFile();
                playerDataChanged = false;
            }
            else
            {
                int selectedIndex = dataGridView1.SelectedRows.Count > 0 ? dataGridView1.SelectedRows[0].Index : -1;
                if (selectedIndex != currentSelectedPlayerIndex)
                {
                    currentSelectedPlayerIndex = selectedIndex;
                    if (selectedIndex >= 0)
                    {
                        UpdateValueDataGridView();
                    }
                }
            }
        }



        public void ShowData()
        {
            dataGridView1.Rows.Clear();

            foreach (Player player in playerManager.Players)
            {
                AddPlayer(dataGridView1, player.Indice, player.Name);
            }


            numericUpDown2.Value = FileReader.fileBytes[playerManager.Players[0].Indice + 92];

            UpdateValueDataGridView();


            foreach (Player player in playerManager.Players)
            {
                int currentPlayerIndex = player.Indice;
                int livello = FileReader.fileBytes[currentPlayerIndex + 56];
                int grado = FileReader.fileBytes[currentPlayerIndex + 92];


            }

        }

        private static void AddPlayer(DataGridView dataGridView, int index, string nome)
        {
            dataGridView.Rows.Add(BitConverter.ToString(FileReader.fileBytes, index, 4).Replace("-", ""), nome,
           FileReader.fileBytes[index + 56],
           FileReader.fileBytes[index + 92]);
        }




        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;
                if (selectedIndex >= 0 && selectedIndex < dataGridView1.Rows.Count)
                {
                    dataGridView1.Rows[selectedIndex].Cells[2].Value = numericUpDown1.Value;
                }
            }
        }

        //=========================================================================================



        private void NumericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;
                if (selectedIndex >= 0 && selectedIndex < dataGridView1.Rows.Count)
                {
                    dataGridView1.Rows[selectedIndex].Cells[3].Value = numericUpDown2.Value;
                }
            }

            if (numericUpDown2.Value == 0)
            {
                label4.Text = "Normal Grade";
            }
            if (numericUpDown2.Value == 1)
            {
                label4.Text = "Growith Grade";
            }
            if (numericUpDown2.Value == 2)
            {
                label4.Text = "Advance Grade";
            }
            if (numericUpDown2.Value == 3)
            {
                label4.Text = "Top Grade";
            }
            if (numericUpDown2.Value == 4)
            {
                label4.Text = "Legendary Grade";
            }
            if (numericUpDown2.Value == 5)
            {
                label4.Text = "Hero 1 Grade";
            }
            if (numericUpDown2.Value == 6)
            {
                label4.Text = "Hero 2 Grade";
            }
            if (numericUpDown2.Value == 7)
            {
                label4.Text = "Hero 3 Grade";
            }
        }



        //=====================================Tokens

        private void SearchAndPopulateObjects(bool isItalian)
        {

            Dictionary<string, string> tokens = isItalian ? italianTokens.tokens : englishTokens.Tokens;

            bool steleFound = false;
            dataGridView2.Rows.Clear();

            foreach (var kvp in tokens)
            {
                string objectId = kvp.Key;
                string objectName = kvp.Value;

                int objectIndex = FileReader.FindPattern(FileReader.fileBytes, FileReader.StringToByteArray(objectId), 0x0001BB17, 0x0001BCDF);
                if (objectIndex != -1)
                {
                    int quantityIndex = objectIndex + 24;

                    byte[] quantityBytes = new byte[2];
                    Array.Copy(FileReader.fileBytes, quantityIndex, quantityBytes, 0, 2);
                    ushort quantity = BitConverter.ToUInt16(quantityBytes, 0);

                    dataGridView2.Rows.Add(objectId, objectName, quantity);

                    if (objectId == "C6B7365F")
                    {
                        steleFound = true;
                    }
                }
            }

            if (!steleFound)
            {
                UnlockVictoryStar();
            }
        }


        private void DataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 2)
            {
                try
                {

                    string objectId = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                    string objectName = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
                    int quantity = int.Parse(dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString());


                    int objectIndex = FileReader.FindPattern(FileReader.fileBytes, FileReader.StringToByteArray(objectId), 0x0001BB17, 0x0001BCDF);


                    if (objectIndex != -1)
                    {

                        int quantityIndex = objectIndex + 24;

                        // update quantity array di byte 

                        byte[] quantityBytes = BitConverter.GetBytes(quantity);
                        Array.Copy(quantityBytes, 0, FileReader.fileBytes, quantityIndex, 4);
                    }
                    else
                    {
                        MessageBox.Show("Object ID not in allowed range");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An Error occured while changing the quantity: " + ex.Message);
                }
            }
        }

        private void UnlockVictoryStar()
        {

            var lastObject = italiantokens.tokens.Last();
            string lastId = lastObject.Key;
            _ = lastObject.Value;


            int lastOffset = FileReader.FindPattern(FileReader.fileBytes, FileReader.StringToByteArray(lastId), 0x0001BB17, 0x0001BCDF);
            if (lastOffset != -1)
            {

                FileReader.fileBytes[lastOffset + 42]++;

                FileReader.fileBytes[lastOffset + 12]++;


                int newIdOffset = lastOffset + 54;
                byte[] idBytes = FileReader.StringToByteArray("C6B7365F");
                Array.Copy(idBytes, 0, FileReader.fileBytes, newIdOffset, idBytes.Length);


                FileReader.fileBytes[newIdOffset + 24] = 01;
            }

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (FileReader.fileBytes == null)
            {
                MessageBox.Show("Load Game File");
                return;
            }
            else
            {
                UnlockVictoryStar();

                SearchAndPopulateObjects(true);
                MessageBox.Show("Unlock Victory Star");
            }

        }



        //======================================================= Spirit

        private void SpiritToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FileReader.fileBytes == null)
            {
                MessageBox.Show("Carica il File di Gioco.");
                return;
            }
            else
            {

                spirit.ShowDialog();

            }

        }


        //=========================================================================================================


        //=================================== Max Rank
        private static void MaxWorldRank()
        {
            _ = FileReader.fileBytes[0x000000E0];
            _ = FileReader.fileBytes[0x000000E1];
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (FileReader.fileBytes == null)
            {
                MessageBox.Show("Load Game File");
                return;
            }

            DialogResult result = MessageBox.Show("Obtein a 2200 Points in your Rank", "Max Rank", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                FileReader.fileBytes[0x000000E0] = 0x98;
                FileReader.fileBytes[0x000000E1] = 0x08;


                MaxWorldRank();
                MessageBox.Show("Rank World obtained");
            }
            else if (result == DialogResult.No)
            {
                return;
            }


        }

        //=================================== VICTORY GALLERY
        private static void VcitoryGallery()
        {
            _ = FileReader.fileBytes[0x00010E9B];
            _ = FileReader.fileBytes[0x00010E9C];
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (FileReader.fileBytes == null)
            {
                MessageBox.Show("Load Game File.");
                return;
            }
            DialogResult result = MessageBox.Show("Obtein 999 Win ", "Victory Gallery", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                FileReader.fileBytes[0x00010E9B] = 0x98;
                FileReader.fileBytes[0x00010E9C] = 0x08;

                VcitoryGallery();
                MessageBox.Show("999  Victory Gallery");
            }
            else if (result == DialogResult.No) { return; }


        }


        private static void UnlockAlius()
        {
            _ = FileReader.fileBytes[0x0000EAD2];
            _ = FileReader.fileBytes[0x0000EED1];
            _ = FileReader.fileBytes[0x000112CA];
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (FileReader.fileBytes == null)
            {
                MessageBox.Show("Load Game File.");
                return;
            }
            DialogResult result = MessageBox.Show("Unlock Alius?", "Unlock Alius Academy", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                FileReader.fileBytes[0x0000EAD2] = 0x02;
                FileReader.fileBytes[0x0000EED1] = 0x01; //Value for Shop
                FileReader.fileBytes[0x000112CA] = 0x02;

                UnlockAlius();
                MessageBox.Show("Alius Academy Unlocked ");
            }
            else if (result == DialogResult.No)
            {
                return;
            }



        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (FileReader.fileBytes == null)
            {
                MessageBox.Show("Load Game File.");
                return;
            }

            DialogResult result = MessageBox.Show("Unlock All Spirits?", "Unlock Spirits", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                
                string newHexSequence = "50 EC 3B 42 83 7C F4 90 04 00 00 00 FC 02 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 01 E0 08 00 5E 52 6F 12 04 00 00 00 50 EC 3B 42 83 7C F4 90 04 00 00 00 FD 02 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 02 E0 0C 00 5E 52 6F 12 04 00 00 00 50 EC 3B 42 83 7C F4 90 04 00 00 00 FE 02 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 03 E0 10 00 5E 52 6F 12 04 00 00 00 50 EC 3B 42 83 7C F4 90 04 00 00 00 FF 02 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 04 E0 14 00 5E 52 6F 12 04 00 00 00 50 EC 3B 42 83 7C F4 90 04 00 00 00 00 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 05 E0 18 00 5E 52 6F 12 04 00 00 00 50 EC 3B 42 83 7C F4 90 04 00 00 00 01 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 06 E0 1C 00 5E 52 6F 12 04 00 00 00 50 EC 3B 42 83 7C F4 90 04 00 00 00 02 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 07 E0 20 00 5E 52 6F 12 04 00 00 00 50 EC 3B 42 83 7C F4 90 04 00 00 00 03 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 08 E0 24 00 5E 52 6F 12 04 00 00 00 8D 34 51 B0 83 7C F4 90 04 00 00 00 04 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 09 E0 28 00 5E 52 6F 12 04 00 00 00 8D 34 51 B0 83 7C F4 90 04 00 00 00 05 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 0A E0 2C 00 5E 52 6F 12 04 00 00 00 8D 34 51 B0 83 7C F4 90 04 00 00 00 06 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 0B E0 30 00 5E 52 6F 12 04 00 00 00 8D 34 51 B0 83 7C F4 90 04 00 00 00 07 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 0C E0 34 00 5E 52 6F 12 04 00 00 00 8D 34 51 B0 83 7C F4 90 04 00 00 00 08 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 0D E0 38 00 5E 52 6F 12 04 00 00 00 8D 34 51 B0 83 7C F4 90 04 00 00 00 09 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 0E E0 3C 00 5E 52 6F 12 04 00 00 00 8D 34 51 B0 83 7C F4 90 04 00 00 00 0A 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 0F E0 40 00 5E 52 6F 12 04 00 00 00 8D 34 51 B0 83 7C F4 90 04 00 00 00 0B 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 10 E0 44 00 5E 52 6F 12 04 00 00 00 22 B3 58 DD 83 7C F4 90 04 00 00 00 0C 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 11 E0 48 00 5E 52 6F 12 04 00 00 00 22 B3 58 DD 83 7C F4 90 04 00 00 00 0D 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 12 E0 4C 00 5E 52 6F 12 04 00 00 00 22 B3 58 DD 83 7C F4 90 04 00 00 00 0E 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 13 E0 50 00 5E 52 6F 12 04 00 00 00 22 B3 58 DD 83 7C F4 90 04 00 00 00 0F 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 14 E0 54 00 5E 52 6F 12 04 00 00 00 22 B3 58 DD 83 7C F4 90 04 00 00 00 10 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 15 E0 58 00 5E 52 6F 12 04 00 00 00 22 B3 58 DD 83 7C F4 90 04 00 00 00 11 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 16 E0 5C 00 5E 52 6F 12 04 00 00 00 22 B3 58 DD 83 7C F4 90 04 00 00 00 12 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 17 E0 60 00 5E 52 6F 12 04 00 00 00 22 B3 58 DD 83 7C F4 90 04 00 00 00 13 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 18 E0 64 00 5E 52 6F 12 04 00 00 00 91 51 73 68 83 7C F4 90 04 00 00 00 14 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 19 E0 68 00 5E 52 6F 12 04 00 00 00 91 51 73 68 83 7C F4 90 04 00 00 00 15 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 1A E0 6C 00 5E 52 6F 12 04 00 00 00 91 51 73 68 83 7C F4 90 04 00 00 00 16 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 1B E0 70 00 5E 52 6F 12 04 00 00 00 91 51 73 68 83 7C F4 90 04 00 00 00 17 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 1C E0 74 00 5E 52 6F 12 04 00 00 00 91 51 73 68 83 7C F4 90 04 00 00 00 18 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 1D E0 78 00 5E 52 6F 12 04 00 00 00 91 51 73 68 83 7C F4 90 04 00 00 00 19 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 1E E0 7C 00 5E 52 6F 12 04 00 00 00 91 51 73 68 83 7C F4 90 04 00 00 00 1A 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 1F E0 80 00 5E 52 6F 12 04 00 00 00 91 51 73 68 83 7C F4 90 04 00 00 00 1B 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 20 E0 84 00 5E 52 6F 12 04 00 00 00 1C CD E5 A5 83 7C F4 90 04 00 00 00 1C 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 21 E0 88 00 5E 52 6F 12 04 00 00 00 1C CD E5 A5 83 7C F4 90 04 00 00 00 1D 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 22 E0 8C 00 5E 52 6F 12 04 00 00 00 1C CD E5 A5 83 7C F4 90 04 00 00 00 1E 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 23 E0 90 00 5E 52 6F 12 04 00 00 00 1C CD E5 A5 83 7C F4 90 04 00 00 00 1F 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 24 E0 94 00 5E 52 6F 12 04 00 00 00 1C CD E5 A5 83 7C F4 90 04 00 00 00 20 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 25 E0 98 00 5E 52 6F 12 04 00 00 00 1C CD E5 A5 83 7C F4 90 04 00 00 00 21 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 26 E0 9C 00 5E 52 6F 12 04 00 00 00 1C CD E5 A5 83 7C F4 90 04 00 00 00 22 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 27 E0 A0 00 5E 52 6F 12 04 00 00 00 1C CD E5 A5 83 7C F4 90 04 00 00 00 23 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 28 E0 A4 00 5E 52 6F 12 04 00 00 00 7C 4F D3 29 83 7C F4 90 04 00 00 00 24 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 29 E0 A8 00 5E 52 6F 12 04 00 00 00 7C 4F D3 29 83 7C F4 90 04 00 00 00 25 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 2A E0 AC 00 5E 52 6F 12 04 00 00 00 7C 4F D3 29 83 7C F4 90 04 00 00 00 26 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 2B E0 B0 00 5E 52 6F 12 04 00 00 00 7C 4F D3 29 83 7C F4 90 04 00 00 00 27 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 2C E0 B4 00 5E 52 6F 12 04 00 00 00 7C 4F D3 29 83 7C F4 90 04 00 00 00 28 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 2D E0 B8 00 5E 52 6F 12 04 00 00 00 7C 4F D3 29 83 7C F4 90 04 00 00 00 29 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 2E E0 BC 00 5E 52 6F 12 04 00 00 00 7C 4F D3 29 83 7C F4 90 04 00 00 00 2A 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 2F E0 C0 00 5E 52 6F 12 04 00 00 00 7C 4F D3 29 83 7C F4 90 04 00 00 00 2B 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 30 E0 C4 00 5E 52 6F 12 04 00 00 00 90 F9 61 18 83 7C F4 90 04 00 00 00 2C 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 31 E0 C8 00 5E 52 6F 12 04 00 00 00 90 F9 61 18 83 7C F4 90 04 00 00 00 2D 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 32 E0 CC 00 5E 52 6F 12 04 00 00 00 90 F9 61 18 83 7C F4 90 04 00 00 00 2E 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 33 E0 D0 00 5E 52 6F 12 04 00 00 00 90 F9 61 18 83 7C F4 90 04 00 00 00 2F 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 34 E0 D4 00 5E 52 6F 12 04 00 00 00 90 F9 61 18 83 7C F4 90 04 00 00 00 30 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 35 E0 D8 00 5E 52 6F 12 04 00 00 00 90 F9 61 18 83 7C F4 90 04 00 00 00 31 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 36 E0 DC 00 5E 52 6F 12 04 00 00 00 90 F9 61 18 83 7C F4 90 04 00 00 00 32 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 37 E0 E0 00 5E 52 6F 12 04 00 00 00 90 F9 61 18 83 7C F4 90 04 00 00 00 33 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 38 E0 E4 00 5E 52 6F 12 04 00 00 00 23 2D FE 6C 83 7C F4 90 04 00 00 00 34 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 39 E0 E8 00 5E 52 6F 12 04 00 00 00 23 2D FE 6C 83 7C F4 90 04 00 00 00 35 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 3A E0 EC 00 5E 52 6F 12 04 00 00 00 23 2D FE 6C 83 7C F4 90 04 00 00 00 36 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 3B E0 F0 00 5E 52 6F 12 04 00 00 00 23 2D FE 6C 83 7C F4 90 04 00 00 00 37 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 3C E0 F4 00 5E 52 6F 12 04 00 00 00 23 2D FE 6C 83 7C F4 90 04 00 00 00 38 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 3D E0 F8 00 5E 52 6F 12 04 00 00 00 23 2D FE 6C 83 7C F4 90 04 00 00 00 39 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 3E E0 FC 00 5E 52 6F 12 04 00 00 00 23 2D FE 6C 83 7C F4 90 04 00 00 00 3A 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 3F E0 00 01 5E 52 6F 12 04 00 00 00 23 2D FE 6C 83 7C F4 90 04 00 00 00 3B 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 40 E0 04 01 5E 52 6F 12 04 00 00 00 1D 53 43 14 83 7C F4 90 04 00 00 00 3C 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 41 E0 08 01 5E 52 6F 12 04 00 00 00 1D 53 43 14 83 7C F4 90 04 00 00 00 3D 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 42 E0 0C 01 5E 52 6F 12 04 00 00 00 1D 53 43 14 83 7C F4 90 04 00 00 00 3E 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 43 E0 10 01 5E 52 6F 12 04 00 00 00 1D 53 43 14 83 7C F4 90 04 00 00 00 3F 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 44 E0 14 01 5E 52 6F 12 04 00 00 00 1D 53 43 14 83 7C F4 90 04 00 00 00 40 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 45 E0 18 01 5E 52 6F 12 04 00 00 00 1D 53 43 14 83 7C F4 90 04 00 00 00 41 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 46 E0 1C 01 5E 52 6F 12 04 00 00 00 1D 53 43 14 83 7C F4 90 04 00 00 00 42 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 47 E0 20 01 5E 52 6F 12 04 00 00 00 1D 53 43 14 83 7C F4 90 04 00 00 00 43 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 48 E0 24 01 5E 52 6F 12 04 00 00 00 63 82 43 C4 83 7C F4 90 04 00 00 00 44 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 49 E0 28 01 5E 52 6F 12 04 00 00 00 63 82 43 C4 83 7C F4 90 04 00 00 00 45 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 4A E0 2C 01 5E 52 6F 12 04 00 00 00 63 82 43 C4 83 7C F4 90 04 00 00 00 46 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 4B E0 30 01 5E 52 6F 12 04 00 00 00 63 82 43 C4 83 7C F4 90 04 00 00 00 47 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 4C E0 34 01 5E 52 6F 12 04 00 00 00 63 82 43 C4 83 7C F4 90 04 00 00 00 48 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 4D E0 38 01 5E 52 6F 12 04 00 00 00 63 82 43 C4 83 7C F4 90 04 00 00 00 49 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 4E E0 3C 01 5E 52 6F 12 04 00 00 00 63 82 43 C4 83 7C F4 90 04 00 00 00 4A 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 4F E0 40 01 5E 52 6F 12 04 00 00 00 63 82 43 C4 83 7C F4 90 04 00 00 00 4B 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 50 E0 44 01 5E 52 6F 12 04 00 00 00 62 1C E5 75 83 7C F4 90 04 00 00 00 4C 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 51 E0 48 01 5E 52 6F 12 04 00 00 00 62 1C E5 75 83 7C F4 90 04 00 00 00 4D 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 52 E0 4C 01 5E 52 6F 12 04 00 00 00 62 1C E5 75 83 7C F4 90 04 00 00 00 4E 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 53 E0 50 01 5E 52 6F 12 04 00 00 00 62 1C E5 75 83 7C F4 90 04 00 00 00 4F 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 54 E0 54 01 5E 52 6F 12 04 00 00 00 62 1C E5 75 83 7C F4 90 04 00 00 00 50 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 55 E0 58 01 5E 52 6F 12 04 00 00 00 62 1C E5 75 83 7C F4 90 04 00 00 00 51 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 56 E0 5C 01 5E 52 6F 12 04 00 00 00 62 1C E5 75 83 7C F4 90 04 00 00 00 52 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 57 E0 60 01 5E 52 6F 12 04 00 00 00 62 1C E5 75 83 7C F4 90 04 00 00 00 53 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 58 E0 64 01 5E 52 6F 12 04 00 00 00 BE B4 EC 72 83 7C F4 90 04 00 00 00 54 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 59 E0 68 01 5E 52 6F 12 04 00 00 00 BE B4 EC 72 83 7C F4 90 04 00 00 00 55 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 5A E0 6C 01 5E 52 6F 12 04 00 00 00 BE B4 EC 72 83 7C F4 90 04 00 00 00 56 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 5B E0 70 01 5E 52 6F 12 04 00 00 00 BE B4 EC 72 83 7C F4 90 04 00 00 00 57 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 5C E0 74 01 5E 52 6F 12 04 00 00 00 BE B4 EC 72 83 7C F4 90 04 00 00 00 58 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 5D E0 78 01 5E 52 6F 12 04 00 00 00 BE B4 EC 72 83 7C F4 90 04 00 00 00 59 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 5E E0 7C 01 5E 52 6F 12 04 00 00 00 BE B4 EC 72 83 7C F4 90 04 00 00 00 5A 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 5F E0 80 01 5E 52 6F 12 04 00 00 00 BE B4 EC 72 83 7C F4 90 04 00 00 00 5B 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 60 E0 84 01 5E 52 6F 12 04 00 00 00 D3 D1 7D 22 83 7C F4 90 04 00 00 00 5C 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 61 E0 88 01 5E 52 6F 12 04 00 00 00 D3 D1 7D 22 83 7C F4 90 04 00 00 00 5D 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 62 E0 8C 01 5E 52 6F 12 04 00 00 00 D3 D1 7D 22 83 7C F4 90 04 00 00 00 5E 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 63 E0 90 01 5E 52 6F 12 04 00 00 00 D3 D1 7D 22 83 7C F4 90 04 00 00 00 5F 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 64 E0 94 01 5E 52 6F 12 04 00 00 00 D3 D1 7D 22 83 7C F4 90 04 00 00 00 60 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 65 E0 98 01 5E 52 6F 12 04 00 00 00 D3 D1 7D 22 83 7C F4 90 04 00 00 00 61 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 66 E0 9C 01 5E 52 6F 12 04 00 00 00 D3 D1 7D 22 83 7C F4 90 04 00 00 00 62 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 67 E0 A0 01 5E 52 6F 12 04 00 00 00 D3 D1 7D 22 83 7C F4 90 04 00 00 00 63 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 68 E0 A4 01 5E 52 6F 12 04 00 00 00 11 1C F6 B8 83 7C F4 90 04 00 00 00 64 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 69 E0 A8 01 5E 52 6F 12 04 00 00 00 11 1C F6 B8 83 7C F4 90 04 00 00 00 65 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 6A E0 AC 01 5E 52 6F 12 04 00 00 00 11 1C F6 B8 83 7C F4 90 04 00 00 00 66 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 6B E0 B0 01 5E 52 6F 12 04 00 00 00 11 1C F6 B8 83 7C F4 90 04 00 00 00 67 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 6C E0 B4 01 5E 52 6F 12 04 00 00 00 11 1C F6 B8 83 7C F4 90 04 00 00 00 68 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 6D E0 B8 01 5E 52 6F 12 04 00 00 00 11 1C F6 B8 83 7C F4 90 04 00 00 00 69 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 6E E0 BC 01 5E 52 6F 12 04 00 00 00 11 1C F6 B8 83 7C F4 90 04 00 00 00 6A 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 6F E0 C0 01 5E 52 6F 12 04 00 00 00 11 1C F6 B8 83 7C F4 90 04 00 00 00 6B 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 70 E0 C4 01 5E 52 6F 12 04 00 00 00 51 B3 4B 10 83 7C F4 90 04 00 00 00 6C 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 71 E0 C8 01 5E 52 6F 12 04 00 00 00 51 B3 4B 10 83 7C F4 90 04 00 00 00 6D 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 72 E0 CC 01 5E 52 6F 12 04 00 00 00 51 B3 4B 10 83 7C F4 90 04 00 00 00 6E 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 73 E0 D0 01 5E 52 6F 12 04 00 00 00 51 B3 4B 10 83 7C F4 90 04 00 00 00 6F 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 74 E0 D4 01 5E 52 6F 12 04 00 00 00 51 B3 4B 10 83 7C F4 90 04 00 00 00 70 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 75 E0 D8 01 5E 52 6F 12 04 00 00 00 51 B3 4B 10 83 7C F4 90 04 00 00 00 71 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 76 E0 DC 01 5E 52 6F 12 04 00 00 00 51 B3 4B 10 83 7C F4 90 04 00 00 00 72 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 77 E0 E0 01 5E 52 6F 12 04 00 00 00 51 B3 4B 10 83 7C F4 90 04 00 00 00 73 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 78 E0 E4 01 5E 52 6F 12 04 00 00 00 50 2D ED A1 83 7C F4 90 04 00 00 00 74 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 79 E0 E8 01 5E 52 6F 12 04 00 00 00 50 2D ED A1 83 7C F4 90 04 00 00 00 75 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 7A E0 EC 01 5E 52 6F 12 04 00 00 00 50 2D ED A1 83 7C F4 90 04 00 00 00 76 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 7B E0 F0 01 5E 52 6F 12 04 00 00 00 50 2D ED A1 83 7C F4 90 04 00 00 00 77 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 7C E0 F4 01 5E 52 6F 12 04 00 00 00 50 2D ED A1 83 7C F4 90 04 00 00 00 78 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 7D E0 F8 01 5E 52 6F 12 04 00 00 00 50 2D ED A1 83 7C F4 90 04 00 00 00 79 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 7E E0 FC 01 5E 52 6F 12 04 00 00 00 50 2D ED A1 83 7C F4 90 04 00 00 00 7A 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 7F E0 00 02 5E 52 6F 12 04 00 00 00 50 2D ED A1 83 7C F4 90 04 00 00 00 7B 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 80 E0 04 02 5E 52 6F 12 04 00 00 00 97 BB AC EE 83 7C F4 90 04 00 00 00 7C 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 81 E0 08 02 5E 52 6F 12 04 00 00 00 97 BB AC EE 83 7C F4 90 04 00 00 00 7D 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 82 E0 0C 02 5E 52 6F 12 04 00 00 00 97 BB AC EE 83 7C F4 90 04 00 00 00 7E 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 83 E0 10 02 5E 52 6F 12 04 00 00 00 97 BB AC EE 83 7C F4 90 04 00 00 00 7F 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 84 E0 14 02 5E 52 6F 12 04 00 00 00 97 BB AC EE 83 7C F4 90 04 00 00 00 80 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 85 E0 18 02 5E 52 6F 12 04 00 00 00 97 BB AC EE 83 7C F4 90 04 00 00 00 81 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 86 E0 1C 02 5E 52 6F 12 04 00 00 00 97 BB AC EE 83 7C F4 90 04 00 00 00 82 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 87 E0 20 02 5E 52 6F 12 04 00 00 00 97 BB AC EE 83 7C F4 90 04 00 00 00 83 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 88 E0 24 02 5E 52 6F 12 04 00 00 00 A6 3B B1 69 83 7C F4 90 04 00 00 00 84 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 89 E0 28 02 5E 52 6F 12 04 00 00 00 A6 3B B1 69 83 7C F4 90 04 00 00 00 85 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 8A E0 2C 02 5E 52 6F 12 04 00 00 00 A6 3B B1 69 83 7C F4 90 04 00 00 00 86 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 8B E0 30 02 5E 52 6F 12 04 00 00 00 A6 3B B1 69 83 7C F4 90 04 00 00 00 87 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 8C E0 34 02 5E 52 6F 12 04 00 00 00 A6 3B B1 69 83 7C F4 90 04 00 00 00 88 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 8D E0 38 02 5E 52 6F 12 04 00 00 00 A6 3B B1 69 83 7C F4 90 04 00 00 00 89 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 8E E0 3C 02 5E 52 6F 12 04 00 00 00 A6 3B B1 69 83 7C F4 90 04 00 00 00 8A 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 8F E0 40 02 5E 52 6F 12 04 00 00 00 A6 3B B1 69 83 7C F4 90 04 00 00 00 8B 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 90 E0 44 02 5E 52 6F 12 04 00 00 00 69 75 25 37 83 7C F4 90 04 00 00 00 8C 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 91 E0 48 02 5E 52 6F 12 04 00 00 00 69 75 25 37 83 7C F4 90 04 00 00 00 8D 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 92 E0 4C 02 5E 52 6F 12 04 00 00 00 69 75 25 37 83 7C F4 90 04 00 00 00 8E 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 93 E0 50 02 5E 52 6F 12 04 00 00 00 69 75 25 37 83 7C F4 90 04 00 00 00 8F 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 94 E0 54 02 5E 52 6F 12 04 00 00 00 69 75 25 37 83 7C F4 90 04 00 00 00 90 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 95 E0 58 02 5E 52 6F 12 04 00 00 00 69 75 25 37 83 7C F4 90 04 00 00 00 91 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 96 E0 5C 02 5E 52 6F 12 04 00 00 00 69 75 25 37 83 7C F4 90 04 00 00 00 92 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 97 E0 60 02 5E 52 6F 12 04 00 00 00 69 75 25 37 83 7C F4 90 04 00 00 00 93 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 98 E0 64 02 5E 52 6F 12 04 00 00 00 6E 3C DF 06 83 7C F4 90 04 00 00 00 94 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 99 E0 68 02 5E 52 6F 12 04 00 00 00 6E 3C DF 06 83 7C F4 90 04 00 00 00 95 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 9A E0 6C 02 5E 52 6F 12 04 00 00 00 6E 3C DF 06 83 7C F4 90 04 00 00 00 96 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 9B E0 70 02 5E 52 6F 12 04 00 00 00 6E 3C DF 06 83 7C F4 90 04 00 00 00 97 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 9C E0 74 02 5E 52 6F 12 04 00 00 00 6E 3C DF 06 83 7C F4 90 04 00 00 00 98 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 9D E0 78 02 5E 52 6F 12 04 00 00 00 6E 3C DF 06 83 7C F4 90 04 00 00 00 99 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 9E E0 7C 02 5E 52 6F 12 04 00 00 00 6E 3C DF 06 83 7C F4 90 04 00 00 00 9A 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 9F E0 80 02 5E 52 6F 12 04 00 00 00 6E 3C DF 06 83 7C F4 90 04 00 00 00 9B 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 A0 E0 84 02 5E 52 6F 12 04 00 00 00 EC 5E E9 34 83 7C F4 90 04 00 00 00 9C 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 A1 E0 88 02 5E 52 6F 12 04 00 00 00 EC 5E E9 34 83 7C F4 90 04 00 00 00 9D 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 A2 E0 8C 02 5E 52 6F 12 04 00 00 00 EC 5E E9 34 83 7C F4 90 04 00 00 00 9E 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 A3 E0 90 02 5E 52 6F 12 04 00 00 00 EC 5E E9 34 83 7C F4 90 04 00 00 00 9F 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 A4 E0 94 02 5E 52 6F 12 04 00 00 00 EC 5E E9 34 83 7C F4 90 04 00 00 00 A0 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 A5 E0 98 02 5E 52 6F 12 04 00 00 00 EC 5E E9 34 83 7C F4 90 04 00 00 00 A1 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 A6 E0 9C 02 5E 52 6F 12 04 00 00 00 EC 5E E9 34 83 7C F4 90 04 00 00 00 A2 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 A7 E0 A0 02 5E 52 6F 12 04 00 00 00 EC 5E E9 34 83 7C F4 90 04 00 00 00 A3 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 A8 E0 A4 02 5E 52 6F 12 04 00 00 00 14 DC EC 16 83 7C F4 90 04 00 00 00 A4 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 A9 E0 A8 02 5E 52 6F 12 04 00 00 00 14 DC EC 16 83 7C F4 90 04 00 00 00 A5 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 AA E0 AC 02 5E 52 6F 12 04 00 00 00 14 DC EC 16 83 7C F4 90 04 00 00 00 A6 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 AB E0 B0 02 5E 52 6F 12 04 00 00 00 14 DC EC 16 83 7C F4 90 04 00 00 00 A7 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 AC E0 B4 02 5E 52 6F 12 04 00 00 00 14 DC EC 16 83 7C F4 90 04 00 00 00 A8 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 AD E0 B8 02 5E 52 6F 12 04 00 00 00 14 DC EC 16 83 7C F4 90 04 00 00 00 A9 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 AE E0 BC 02 5E 52 6F 12 04 00 00 00 14 DC EC 16 83 7C F4 90 04 00 00 00 AA 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 AF E0 C0 02 5E 52 6F 12 04 00 00 00 14 DC EC 16 83 7C F4 90 04 00 00 00 AB 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 B0 E0 C4 02 5E 52 6F 12 04 00 00 00 FB 76 2F CE 83 7C F4 90 04 00 00 00 AC 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 B1 E0 C8 02 5E 52 6F 12 04 00 00 00 FB 76 2F CE 83 7C F4 90 04 00 00 00 AD 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 B2 E0 CC 02 5E 52 6F 12 04 00 00 00 FB 76 2F CE 83 7C F4 90 04 00 00 00 AE 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 B3 E0 D0 02 5E 52 6F 12 04 00 00 00 FB 76 2F CE 83 7C F4 90 04 00 00 00 AF 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 B4 E0 D4 02 5E 52 6F 12 04 00 00 00 FB 76 2F CE 83 7C F4 90 04 00 00 00 B0 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 B5 E0 D8 02 5E 52 6F 12 04 00 00 00 FB 76 2F CE 83 7C F4 90 04 00 00 00 B1 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 B6 E0 DC 02 5E 52 6F 12 04 00 00 00 FB 76 2F CE 83 7C F4 90 04 00 00 00 B2 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 B7 E0 E0 02 5E 52 6F 12 04 00 00 00 FB 76 2F CE 83 7C F4 90 04 00 00 00 B3 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 B8 E0 E4 02 5E 52 6F 12 04 00 00 00 BF EB 9C 20 83 7C F4 90 04 00 00 00 B4 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 B9 E0 E8 02 5E 52 6F 12 04 00 00 00 BF EB 9C 20 83 7C F4 90 04 00 00 00 B5 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 BA E0 EC 02 5E 52 6F 12 04 00 00 00 BF EB 9C 20 83 7C F4 90 04 00 00 00 B6 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 BB E0 F0 02 5E 52 6F 12 04 00 00 00 BF EB 9C 20 83 7C F4 90 04 00 00 00 B7 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 BC E0 F4 02 5E 52 6F 12 04 00 00 00 BF EB 9C 20 83 7C F4 90 04 00 00 00 B8 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 BD E0 F8 02 5E 52 6F 12 04 00 00 00 BF EB 9C 20 83 7C F4 90 04 00 00 00 B9 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 BE E0 FC 02 5E 52 6F 12 04 00 00 00 BF EB 9C 20 83 7C F4 90 04 00 00 00 BA 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 BF E0 00 03 5E 52 6F 12 04 00 00 00 BF EB 9C 20 83 7C F4 90 04 00 00 00 BB 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 C0 E0 04 03 5E 52 6F 12 04 00 00 00 26 9A 78 81 83 7C F4 90 04 00 00 00 BC 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 C1 E0 08 03 5E 52 6F 12 04 00 00 00 26 9A 78 81 83 7C F4 90 04 00 00 00 BD 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 C2 E0 0C 03 5E 52 6F 12 04 00 00 00 26 9A 78 81 83 7C F4 90 04 00 00 00 BE 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 C3 E0 10 03 5E 52 6F 12 04 00 00 00 26 9A 78 81 83 7C F4 90 04 00 00 00 BF 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 C4 E0 14 03 5E 52 6F 12 04 00 00 00 26 9A 78 81 83 7C F4 90 04 00 00 00 C0 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 C5 E0 18 03 5E 52 6F 12 04 00 00 00 26 9A 78 81 83 7C F4 90 04 00 00 00 C1 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 C6 E0 1C 03 5E 52 6F 12 04 00 00 00 26 9A 78 81 83 7C F4 90 04 00 00 00 C2 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 C7 E0 20 03 5E 52 6F 12 04 00 00 00 26 9A 78 81 83 7C F4 90 04 00 00 00 C3 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 C8 E0 24 03 5E 52 6F 12 04 00 00 00 81 B5 40 D3 83 7C F4 90 04 00 00 00 C4 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 C9 E0 28 03 5E 52 6F 12 04 00 00 00 81 B5 40 D3 83 7C F4 90 04 00 00 00 C5 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 CA E0 2C 03 5E 52 6F 12 04 00 00 00 81 B5 40 D3 83 7C F4 90 04 00 00 00 C6 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 CB E0 30 03 5E 52 6F 12 04 00 00 00 81 B5 40 D3 83 7C F4 90 04 00 00 00 C7 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 CC E0 34 03 5E 52 6F 12 04 00 00 00 81 B5 40 D3 83 7C F4 90 04 00 00 00 C8 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 CD E0 38 03 5E 52 6F 12 04 00 00 00 81 B5 40 D3 83 7C F4 90 04 00 00 00 C9 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 CE E0 3C 03 5E 52 6F 12 04 00 00 00 81 B5 40 D3 83 7C F4 90 04 00 00 00 CA 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 CF E0 40 03 5E 52 6F 12 04 00 00 00 81 B5 40 D3 83 7C F4 90 04 00 00 00 CB 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 D0 E0 44 03 5E 52 6F 12 04 00 00 00 BA 8E D1 BD 83 7C F4 90 04 00 00 00 CC 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 D1 E0 48 03 5E 52 6F 12 04 00 00 00 BA 8E D1 BD 83 7C F4 90 04 00 00 00 CD 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 D2 E0 4C 03 5E 52 6F 12 04 00 00 00 BA 8E D1 BD 83 7C F4 90 04 00 00 00 CE 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 D3 E0 50 03 5E 52 6F 12 04 00 00 00 BA 8E D1 BD 83 7C F4 90 04 00 00 00 CF 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 D4 E0 54 03 5E 52 6F 12 04 00 00 00 BE 55 5B 1A 83 7C F4 90 04 00 00 00 DA 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 D5 E0 58 03 5E 52 6F 12 04 00 00 00 BA 8E D1 BD 83 7C F4 90 04 00 00 00 D1 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 D6 E0 5C 03 5E 52 6F 12 04 00 00 00 BA 8E D1 BD 83 7C F4 90 04 00 00 00 D2 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 D7 E0 60 03 5E 52 6F 12 04 00 00 00 BA 8E D1 BD 83 7C F4 90 04 00 00 00 D3 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 D8 E0 64 03 5E 52 6F 12 04 00 00 00 BA 8E D1 BD 83 7C F4 90 04 00 00 00 D4 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 D9 E0 68 03 5E 52 6F 12 04 00 00 00 BE 55 5B 1A 83 7C F4 90 04 00 00 00 D5 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 DA E0 6C 03 5E 52 6F 12 04 00 00 00 BE 55 5B 1A 83 7C F4 90 04 00 00 00 D6 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 DB E0 70 03 5E 52 6F 12 04 00 00 00 BE 55 5B 1A 83 7C F4 90 04 00 00 00 D7 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 DC E0 74 03 5E 52 6F 12 04 00 00 00 BE 55 5B 1A 83 7C F4 90 04 00 00 00 D8 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 DD E0 78 03 5E 52 6F 12 04 00 00 00 BE 55 5B 1A 83 7C F4 90 04 00 00 00 D9 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 DE E0 7C 03 5E 52 6F 12 04 00 00 00 BE 55 5B 1A 83 7C F4 90 04 00 00 00 DA 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 DF E0 80 03 5E 52 6F 12 04 00 00 00 BE 55 5B 1A 83 7C F4 90 04 00 00 00 DB 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 E0 E0 84 03 5E 52 6F 12 04 00 00 00 FB AC D7 37 83 7C F4 90 04 00 00 00 DC 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 E1 E0 88 03 5E 52 6F 12 04 00 00 00 FB AC D7 37 83 7C F4 90 04 00 00 00 DD 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 E2 E0 8C 03 5E 52 6F 12 04 00 00 00 FB AC D7 37 83 7C F4 90 04 00 00 00 DE 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 E3 E0 90 03 5E 52 6F 12 04 00 00 00 FB AC D7 37 83 7C F4 90 04 00 00 00 DF 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 E4 E0 94 03 5E 52 6F 12 04 00 00 00 FB AC D7 37 83 7C F4 90 04 00 00 00 E0 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 E5 E0 98 03 5E 52 6F 12 04 00 00 00 FB AC D7 37 83 7C F4 90 04 00 00 00 E1 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 E6 E0 9C 03 5E 52 6F 12 04 00 00 00 FB AC D7 37 83 7C F4 90 04 00 00 00 E2 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 E7 E0 A0 03 5E 52 6F 12 04 00 00 00 FB AC D7 37 83 7C F4 90 04 00 00 00 E3 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 E8 E0 A4 03 5E 52 6F 12 04 00 00 00 DA DD 11 C7 83 7C F4 90 04 00 00 00 E4 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 E9 E0 A8 03 5E 52 6F 12 04 00 00 00 DA DD 11 C7 83 7C F4 90 04 00 00 00 E5 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 EA E0 AC 03 5E 52 6F 12 04 00 00 00 DA DD 11 C7 83 7C F4 90 04 00 00 00 E6 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 EB E0 B0 03 5E 52 6F 12 04 00 00 00 DA DD 11 C7 83 7C F4 90 04 00 00 00 E7 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 EC E0 B4 03 5E 52 6F 12 04 00 00 00 DA DD 11 C7 83 7C F4 90 04 00 00 00 E8 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 ED E0 B8 03 5E 52 6F 12 04 00 00 00 DA DD 11 C7 83 7C F4 90 04 00 00 00 E9 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 EE E0 BC 03 5E 52 6F 12 04 00 00 00 DA DD 11 C7 83 7C F4 90 04 00 00 00 EA 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 EF E0 C0 03 5E 52 6F 12 04 00 00 00 DA DD 11 C7 83 7C F4 90 04 00 00 00 EB 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 F0 E0 C4 03 5E 52 6F 12 04 00 00 00 F3 2B F5 AF 83 7C F4 90 04 00 00 00 EC 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 F1 E0 C8 03 5E 52 6F 12 04 00 00 00 F3 2B F5 AF 83 7C F4 90 04 00 00 00 ED 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 F2 E0 CC 03 5E 52 6F 12 04 00 00 00 F3 2B F5 AF 83 7C F4 90 04 00 00 00 EE 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 F3 E0 D0 03 5E 52 6F 12 04 00 00 00 F3 2B F5 AF 83 7C F4 90 04 00 00 00 EF 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 F4 E0 D4 03 5E 52 6F 12 04 00 00 00 F3 2B F5 AF 83 7C F4 90 04 00 00 00 F0 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 F5 E0 D8 03 5E 52 6F 12 04 00 00 00 F3 2B F5 AF 83 7C F4 90 04 00 00 00 F1 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 F6 E0 DC 03 5E 52 6F 12 04 00 00 00 F3 2B F5 AF 83 7C F4 90 04 00 00 00 F2 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 F7 E0 E0 03 5E 52 6F 12 04 00 00 00 F3 2B F5 AF 83 7C F4 90 04 00 00 00 F3 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 F8 E0 E4 03 5E 52 6F 12 04 00 00 00 93 92 8C B2 83 7C F4 90 04 00 00 00 F4 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 F9 E0 E8 03 5E 52 6F 12 04 00 00 00 93 92 8C B2 83 7C F4 90 04 00 00 00 F5 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 FA E0 EC 03 5E 52 6F 12 04 00 00 00 93 92 8C B2 83 7C F4 90 04 00 00 00 F6 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 FB E0 F0 03 5E 52 6F 12 04 00 00 00 93 92 8C B2 83 7C F4 90 04 00 00 00 F7 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 FC E0 F4 03 5E 52 6F 12 04 00 00 00 93 92 8C B2 83 7C F4 90 04 00 00 00 F8 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 FD E0 F8 03 5E 52 6F 12 04 00 00 00 93 92 8C B2 83 7C F4 90 04 00 00 00 F9 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 FE E0 FC 03 5E 52 6F 12 04 00 00 00 93 92 8C B2 83 7C F4 90 04 00 00 00 FA 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 FF E0 00 04 5E 52 6F 12 04 00 00 00 93 92 8C B2 83 7C F4 90 04 00 00 00 FB 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 00 E1 04 04 5E 52 6F 12 04 00 00 00 D2 A3 97 AB 83 7C F4 90 04 00 00 00 FC 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 01 E1 08 04 5E 52 6F 12 04 00 00 00 D2 A3 97 AB 83 7C F4 90 04 00 00 00 FD 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 02 E1 0C 04 5E 52 6F 12 04 00 00 00 D2 A3 97 AB 83 7C F4 90 04 00 00 00 FE 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 03 E1 10 04 5E 52 6F 12 04 00 00 00 D2 A3 97 AB 83 7C F4 90 04 00 00 00 FF 03 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 04 E1 14 04 5E 52 6F 12 04 00 00 00 D2 A3 97 AB 83 7C F4 90 04 00 00 00 00 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 05 E1 18 04 5E 52 6F 12 04 00 00 00 D2 A3 97 AB 83 7C F4 90 04 00 00 00 01 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 06 E1 1C 04 5E 52 6F 12 04 00 00 00 D2 A3 97 AB 83 7C F4 90 04 00 00 00 02 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 07 E1 20 04 5E 52 6F 12 04 00 00 00 D2 A3 97 AB 83 7C F4 90 04 00 00 00 03 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 08 E1 24 04 5E 52 6F 12 04 00 00 00 51 28 9B 6B 83 7C F4 90 04 00 00 00 04 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 09 E1 28 04 5E 52 6F 12 04 00 00 00 51 28 9B 6B 83 7C F4 90 04 00 00 00 05 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 0A E1 2C 04 5E 52 6F 12 04 00 00 00 51 28 9B 6B 83 7C F4 90 04 00 00 00 06 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 0B E1 30 04 5E 52 6F 12 04 00 00 00 51 28 9B 6B 83 7C F4 90 04 00 00 00 07 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 0C E1 34 04 5E 52 6F 12 04 00 00 00 51 28 9B 6B 83 7C F4 90 04 00 00 00 08 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 0D E1 38 04 5E 52 6F 12 04 00 00 00 51 28 9B 6B 83 7C F4 90 04 00 00 00 09 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 0E E1 3C 04 5E 52 6F 12 04 00 00 00 51 28 9B 6B 83 7C F4 90 04 00 00 00 0A 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 0F E1 40 04 5E 52 6F 12 04 00 00 00 51 28 9B 6B 83 7C F4 90 04 00 00 00 0B 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 10 E1 44 04 5E 52 6F 12 04 00 00 00 50 C1 A1 99 83 7C F4 90 04 00 00 00 0C 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 11 E1 48 04 5E 52 6F 12 04 00 00 00 50 C1 A1 99 83 7C F4 90 04 00 00 00 0D 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 12 E1 4C 04 5E 52 6F 12 04 00 00 00 50 C1 A1 99 83 7C F4 90 04 00 00 00 0E 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 13 E1 50 04 5E 52 6F 12 04 00 00 00 50 C1 A1 99 83 7C F4 90 04 00 00 00 0F 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 14 E1 54 04 5E 52 6F 12 04 00 00 00 50 C1 A1 99 83 7C F4 90 04 00 00 00 10 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 15 E1 58 04 5E 52 6F 12 04 00 00 00 50 C1 A1 99 83 7C F4 90 04 00 00 00 11 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 16 E1 5C 04 5E 52 6F 12 04 00 00 00 50 C1 A1 99 83 7C F4 90 04 00 00 00 12 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 17 E1 60 04 5E 52 6F 12 04 00 00 00 50 C1 A1 99 83 7C F4 90 04 00 00 00 13 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 18 E1 64 04 5E 52 6F 12 04 00 00 00 89 16 46 2B 83 7C F4 90 04 00 00 00 14 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 19 E1 68 04 5E 52 6F 12 04 00 00 00 89 16 46 2B 83 7C F4 90 04 00 00 00 15 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 1A E1 6C 04 5E 52 6F 12 04 00 00 00 89 16 46 2B 83 7C F4 90 04 00 00 00 16 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 1B E1 70 04 5E 52 6F 12 04 00 00 00 89 16 46 2B 83 7C F4 90 04 00 00 00 17 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 1C E1 74 04 5E 52 6F 12 04 00 00 00 89 16 46 2B 83 7C F4 90 04 00 00 00 18 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 1D E1 78 04 5E 52 6F 12 04 00 00 00 89 16 46 2B 83 7C F4 90 04 00 00 00 19 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 1E E1 7C 04 5E 52 6F 12 04 00 00 00 89 16 46 2B 83 7C F4 90 04 00 00 00 1A 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 1F E1 80 04 5E 52 6F 12 04 00 00 00 89 16 46 2B 83 7C F4 90 04 00 00 00 1B 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 20 E1 84 04 5E 52 6F 12 04 00 00 00 50 EC 3B 42 83 7C F4 90 04 00 00 00 1C 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 21 E1 88 04 5E 52 6F 12 04 00 00 00 50 EC 3B 42 83 7C F4 90 04 00 00 00 1D 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 22 E1 8C 04 5E 52 6F 12 04 00 00 00 50 EC 3B 42 83 7C F4 90 04 00 00 00 1E 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 23 E1 90 04 5E 52 6F 12 04 00 00 00 50 EC 3B 42 83 7C F4 90 04 00 00 00 1F 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 24 E1 94 04 5E 52 6F 12 04 00 00 00 50 EC 3B 42 83 7C F4 90 04 00 00 00 20 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 25 E1 98 04 5E 52 6F 12 04 00 00 00 50 EC 3B 42 83 7C F4 90 04 00 00 00 21 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 26 E1 9C 04 5E 52 6F 12 04 00 00 00 50 EC 3B 42 83 7C F4 90 04 00 00 00 22 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 27 E1 A0 04 5E 52 6F 12 04 00 00 00 50 EC 3B 42 83 7C F4 90 04 00 00 00 23 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 28 E1 A4 04 5E 52 6F 12 04 00 00 00 6F D3 10 AB 83 7C F4 90 04 00 00 00 24 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 29 E1 A8 04 5E 52 6F 12 04 00 00 00 6F D3 10 AB 83 7C F4 90 04 00 00 00 25 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 2A E1 AC 04 5E 52 6F 12 04 00 00 00 6F D3 10 AB 83 7C F4 90 04 00 00 00 26 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 2B E1 B0 04 5E 52 6F 12 04 00 00 00 6F D3 10 AB 83 7C F4 90 04 00 00 00 27 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 2C E1 B4 04 5E 52 6F 12 04 00 00 00 6F D3 10 AB 83 7C F4 90 04 00 00 00 28 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 2D E1 B8 04 5E 52 6F 12 04 00 00 00 6F D3 10 AB 83 7C F4 90 04 00 00 00 29 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 2E E1 BC 04 5E 52 6F 12 04 00 00 00 6F D3 10 AB 83 7C F4 90 04 00 00 00 2A 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 2F E1 C0 04 5E 52 6F 12 04 00 00 00 6F D3 10 AB 83 7C F4 90 04 00 00 00 2B 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 30 E1 C4 04 5E 52 6F 12 04 00 00 00 DD 66 78 C5 83 7C F4 90 04 00 00 00 2C 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 31 E1 C8 04 5E 52 6F 12 04 00 00 00 DD 66 78 C5 83 7C F4 90 04 00 00 00 2D 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 32 E1 CC 04 5E 52 6F 12 04 00 00 00 DD 66 78 C5 83 7C F4 90 04 00 00 00 2E 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 33 E1 D0 04 5E 52 6F 12 04 00 00 00 DD 66 78 C5 83 7C F4 90 04 00 00 00 2F 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 34 E1 D4 04 5E 52 6F 12 04 00 00 00 DD 66 78 C5 83 7C F4 90 04 00 00 00 30 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 35 E1 D8 04 5E 52 6F 12 04 00 00 00 DD 66 78 C5 83 7C F4 90 04 00 00 00 31 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 36 E1 DC 04 5E 52 6F 12 04 00 00 00 DD 66 78 C5 83 7C F4 90 04 00 00 00 32 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 37 E1 E0 04 5E 52 6F 12 04 00 00 00 DD 66 78 C5 83 7C F4 90 04 00 00 00 33 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 38 E1 E4 04 5E 52 6F 12 04 00 00 00 B3 B2 FC C6 83 7C F4 90 04 00 00 00 34 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 39 E1 E8 04 5E 52 6F 12 04 00 00 00 B3 B2 FC C6 83 7C F4 90 04 00 00 00 35 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 3A E1 EC 04 5E 52 6F 12 04 00 00 00 B3 B2 FC C6 83 7C F4 90 04 00 00 00 36 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 3B E1 F0 04 5E 52 6F 12 04 00 00 00 B3 B2 FC C6 83 7C F4 90 04 00 00 00 37 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 3C E1 F4 04 5E 52 6F 12 04 00 00 00 B3 B2 FC C6 83 7C F4 90 04 00 00 00 38 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 3D E1 F8 04 5E 52 6F 12 04 00 00 00 B3 B2 FC C6 83 7C F4 90 04 00 00 00 39 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 3E E1 FC 04 5E 52 6F 12 04 00 00 00 B3 B2 FC C6 83 7C F4 90 04 00 00 00 3A 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 3F E1 00 05 5E 52 6F 12 04 00 00 00 B3 B2 FC C6 83 7C F4 90 04 00 00 00 3B 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 40 E1 04 05 5E 52 6F 12 04 00 00 00 EC 22 42 B0 83 7C F4 90 04 00 00 00 3C 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 41 E1 08 05 5E 52 6F 12 04 00 00 00 EC 22 42 B0 83 7C F4 90 04 00 00 00 3D 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 42 E1 0C 05 5E 52 6F 12 04 00 00 00 EC 22 42 B0 83 7C F4 90 04 00 00 00 3E 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 43 E1 10 05 5E 52 6F 12 04 00 00 00 EC 22 42 B0 83 7C F4 90 04 00 00 00 3F 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 44 E1 14 05 5E 52 6F 12 04 00 00 00 EC 22 42 B0 83 7C F4 90 04 00 00 00 40 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 45 E1 18 05 5E 52 6F 12 04 00 00 00 EC 22 42 B0 83 7C F4 90 04 00 00 00 41 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 46 E1 1C 05 5E 52 6F 12 04 00 00 00 EC 22 42 B0 83 7C F4 90 04 00 00 00 42 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 47 E1 20 05 5E 52 6F 12 04 00 00 00 EC 22 42 B0 83 7C F4 90 04 00 00 00 43 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 48 E1 24 05 5E 52 6F 12 04 00 00 00 0A A7 B1 80 83 7C F4 90 04 00 00 00 44 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 49 E1 28 05 5E 52 6F 12 04 00 00 00 0A A7 B1 80 83 7C F4 90 04 00 00 00 45 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 4A E1 2C 05 5E 52 6F 12 04 00 00 00 0A A7 B1 80 83 7C F4 90 04 00 00 00 46 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 4B E1 30 05 5E 52 6F 12 04 00 00 00 0A A7 B1 80 83 7C F4 90 04 00 00 00 47 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 4C E1 34 05 5E 52 6F 12 04 00 00 00 0A A7 B1 80 83 7C F4 90 04 00 00 00 48 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 4D E1 38 05 5E 52 6F 12 04 00 00 00 0A A7 B1 80 83 7C F4 90 04 00 00 00 49 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 4E E1 3C 05 5E 52 6F 12 04 00 00 00 0A A7 B1 80 83 7C F4 90 04 00 00 00 4A 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 4F E1 40 05 5E 52 6F 12 04 00 00 00 0A A7 B1 80 83 7C F4 90 04 00 00 00 4B 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 50 E1 44 05 5E 52 6F 12 04 00 00 00 67 AB 63 98 83 7C F4 90 04 00 00 00 4C 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 51 E1 48 05 5E 52 6F 12 04 00 00 00 67 AB 63 98 83 7C F4 90 04 00 00 00 4D 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 52 E1 4C 05 5E 52 6F 12 04 00 00 00 67 AB 63 98 83 7C F4 90 04 00 00 00 4E 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 53 E1 50 05 5E 52 6F 12 04 00 00 00 67 AB 63 98 83 7C F4 90 04 00 00 00 4F 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 54 E1 54 05 5E 52 6F 12 04 00 00 00 67 AB 63 98 83 7C F4 90 04 00 00 00 50 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 55 E1 58 05 5E 52 6F 12 04 00 00 00 67 AB 63 98 83 7C F4 90 04 00 00 00 51 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 56 E1 5C 05 5E 52 6F 12 04 00 00 00 67 AB 63 98 83 7C F4 90 04 00 00 00 52 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 57 E1 60 05 5E 52 6F 12 04 00 00 00 67 AB 63 98 83 7C F4 90 04 00 00 00 53 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 58 E1 64 05 5E 52 6F 12 04 00 00 00 53 22 B8 13 83 7C F4 90 04 00 00 00 54 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 59 E1 68 05 5E 52 6F 12 04 00 00 00 53 22 B8 13 83 7C F4 90 04 00 00 00 55 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 5A E1 6C 05 5E 52 6F 12 04 00 00 00 53 22 B8 13 83 7C F4 90 04 00 00 00 56 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 5B E1 70 05 5E 52 6F 12 04 00 00 00 53 22 B8 13 83 7C F4 90 04 00 00 00 57 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 5C E1 74 05 5E 52 6F 12 04 00 00 00 53 22 B8 13 83 7C F4 90 04 00 00 00 58 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 5D E1 78 05 5E 52 6F 12 04 00 00 00 53 22 B8 13 83 7C F4 90 04 00 00 00 59 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 5E E1 7C 05 5E 52 6F 12 04 00 00 00 53 22 B8 13 83 7C F4 90 04 00 00 00 5A 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 5F E1 80 05 5E 52 6F 12 04 00 00 00 53 22 B8 13 83 7C F4 90 04 00 00 00 5B 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 60 E1 84 05 5E 52 6F 12 04 00 00 00 48 4E FC 82 83 7C F4 90 04 00 00 00 5C 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 61 E1 88 05 5E 52 6F 12 04 00 00 00 48 4E FC 82 83 7C F4 90 04 00 00 00 5D 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 62 E1 8C 05 5E 52 6F 12 04 00 00 00 48 4E FC 82 83 7C F4 90 04 00 00 00 5E 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 63 E1 90 05 5E 52 6F 12 04 00 00 00 48 4E FC 82 83 7C F4 90 04 00 00 00 5F 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 64 E1 94 05 5E 52 6F 12 04 00 00 00 48 4E FC 82 83 7C F4 90 04 00 00 00 60 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 65 E1 98 05 5E 52 6F 12 04 00 00 00 48 4E FC 82 83 7C F4 90 04 00 00 00 61 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 66 E1 9C 05 5E 52 6F 12 04 00 00 00 48 4E FC 82 83 7C F4 90 04 00 00 00 62 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 67 E1 A0 05 5E 52 6F 12 04 00 00 00 48 4E FC 82 83 7C F4 90 04 00 00 00 63 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 68 E1 A4 05 5E 52 6F 12 04 00 00 00 AD 6F F2 2D 83 7C F4 90 04 00 00 00 64 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 69 E1 A8 05 5E 52 6F 12 04 00 00 00 AD 6F F2 2D 83 7C F4 90 04 00 00 00 65 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 6A E1 AC 05 5E 52 6F 12 04 00 00 00 AD 6F F2 2D 83 7C F4 90 04 00 00 00 66 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 6B E1 B0 05 5E 52 6F 12 04 00 00 00 AD 6F F2 2D 83 7C F4 90 04 00 00 00 67 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 6C E1 B4 05 5E 52 6F 12 04 00 00 00 AD 6F F2 2D 83 7C F4 90 04 00 00 00 68 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 6D E1 B8 05 5E 52 6F 12 04 00 00 00 AD 6F F2 2D 83 7C F4 90 04 00 00 00 69 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 6E E1 BC 05 5E 52 6F 12 04 00 00 00 AD 6F F2 2D 83 7C F4 90 04 00 00 00 6A 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 6F E1 C0 05 5E 52 6F 12 04 00 00 00 AD 6F F2 2D 83 7C F4 90 04 00 00 00 6B 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 70 E1 C4 05 5E 52 6F 12 04 00 00 00 FF 64 40 03 83 7C F4 90 04 00 00 00 6C 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 71 E1 C8 05 5E 52 6F 12 04 00 00 00 FF 64 40 03 83 7C F4 90 04 00 00 00 6D 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 72 E1 CC 05 5E 52 6F 12 04 00 00 00 FF 64 40 03 83 7C F4 90 04 00 00 00 6E 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 73 E1 D0 05 5E 52 6F 12 04 00 00 00 FF 64 40 03 83 7C F4 90 04 00 00 00 6F 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 74 E1 D4 05 5E 52 6F 12 04 00 00 00 FF 64 40 03 83 7C F4 90 04 00 00 00 70 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 75 E1 D8 05 5E 52 6F 12 04 00 00 00 FF 64 40 03 83 7C F4 90 04 00 00 00 71 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 76 E1 DC 05 5E 52 6F 12 04 00 00 00 FF 64 40 03 83 7C F4 90 04 00 00 00 72 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 77 E1 E0 05 5E 52 6F 12 04 00 00 00 FF 64 40 03 83 7C F4 90 04 00 00 00 73 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 78 E1 E4 05 5E 52 6F 12 04 00 00 00 6B B6 DA ED 83 7C F4 90 04 00 00 00 74 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 79 E1 E8 05 5E 52 6F 12 04 00 00 00 6B B6 DA ED 83 7C F4 90 04 00 00 00 75 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 7A E1 EC 05 5E 52 6F 12 04 00 00 00 6B B6 DA ED 83 7C F4 90 04 00 00 00 76 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 7B E1 F0 05 5E 52 6F 12 04 00 00 00 6B B6 DA ED 83 7C F4 90 04 00 00 00 77 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 7C E1 F4 05 5E 52 6F 12 04 00 00 00 6B B6 DA ED 83 7C F4 90 04 00 00 00 78 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 7D E1 F8 05 5E 52 6F 12 04 00 00 00 6B B6 DA ED 83 7C F4 90 04 00 00 00 79 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 7E E1 FC 05 5E 52 6F 12 04 00 00 00 6B B6 DA ED 83 7C F4 90 04 00 00 00 7A 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 7F E1 00 06 5E 52 6F 12 04 00 00 00 6B B6 DA ED 83 7C F4 90 04 00 00 00 7B 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 80 E1 04 06 5E 52 6F 12 04 00 00 00 35 7C E5 D8 83 7C F4 90 04 00 00 00 7C 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 81 E1 08 06 5E 52 6F 12 04 00 00 00 35 7C E5 D8 83 7C F4 90 04 00 00 00 7D 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 82 E1 0C 06 5E 52 6F 12 04 00 00 00 35 7C E5 D8 83 7C F4 90 04 00 00 00 7E 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 83 E1 10 06 5E 52 6F 12 04 00 00 00 35 7C E5 D8 83 7C F4 90 04 00 00 00 7F 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 84 E1 14 06 5E 52 6F 12 04 00 00 00 35 7C E5 D8 83 7C F4 90 04 00 00 00 80 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 85 E1 18 06 5E 52 6F 12 04 00 00 00 35 7C E5 D8 83 7C F4 90 04 00 00 00 81 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 86 E1 1C 06 5E 52 6F 12 04 00 00 00 35 7C E5 D8 83 7C F4 90 04 00 00 00 82 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 87 E1 20 06 5E 52 6F 12 04 00 00 00 35 7C E5 D8 83 7C F4 90 04 00 00 00 83 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 88 E1 24 06 5E 52 6F 12 04 00 00 00 75 8C AF F9 83 7C F4 90 04 00 00 00 84 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 89 E1 28 06 5E 52 6F 12 04 00 00 00 75 8C AF F9 83 7C F4 90 04 00 00 00 85 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 8A E1 2C 06 5E 52 6F 12 04 00 00 00 75 8C AF F9 83 7C F4 90 04 00 00 00 86 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 8B E1 30 06 5E 52 6F 12 04 00 00 00 75 8C AF F9 83 7C F4 90 04 00 00 00 87 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 8C E1 34 06 5E 52 6F 12 04 00 00 00 75 8C AF F9 83 7C F4 90 04 00 00 00 88 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 8D E1 38 06 5E 52 6F 12 04 00 00 00 75 8C AF F9 83 7C F4 90 04 00 00 00 89 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 8E E1 3C 06 5E 52 6F 12 04 00 00 00 75 8C AF F9 83 7C F4 90 04 00 00 00 8A 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 8F E1 40 06 5E 52 6F 12 04 00 00 00 75 8C AF F9 83 7C F4 90 04 00 00 00 8B 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 90 E1 44 06 5E 52 6F 12 04 00 00 00 C4 41 37 AE 83 7C F4 90 04 00 00 00 8C 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 91 E1 48 06 5E 52 6F 12 04 00 00 00 C4 41 37 AE 83 7C F4 90 04 00 00 00 8D 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 92 E1 4C 06 5E 52 6F 12 04 00 00 00 C4 41 37 AE 83 7C F4 90 04 00 00 00 8E 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 93 E1 50 06 5E 52 6F 12 04 00 00 00 C4 41 37 AE 83 7C F4 90 04 00 00 00 8F 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 94 E1 54 06 5E 52 6F 12 04 00 00 00 C4 41 37 AE 83 7C F4 90 04 00 00 00 90 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 95 E1 58 06 5E 52 6F 12 04 00 00 00 C4 41 37 AE 83 7C F4 90 04 00 00 00 91 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 96 E1 5C 06 5E 52 6F 12 04 00 00 00 C4 41 37 AE 83 7C F4 90 04 00 00 00 92 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 97 E1 60 06 5E 52 6F 12 04 00 00 00 C4 41 37 AE 83 7C F4 90 04 00 00 00 93 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 98 E1 64 06 5E 52 6F 12 04 00 00 00 F7 EE 99 CB 83 7C F4 90 04 00 00 00 94 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 07 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 99 E1 68 06 5E 52 6F 12 04 00 00 00 F7 EE 99 CB 83 7C F4 90 04 00 00 00 95 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 06 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 9A E1 6C 06 5E 52 6F 12 04 00 00 00 F7 EE 99 CB 83 7C F4 90 04 00 00 00 96 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 05 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 9B E1 70 06 5E 52 6F 12 04 00 00 00 F7 EE 99 CB 83 7C F4 90 04 00 00 00 97 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 04 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 9C E1 74 06 5E 52 6F 12 04 00 00 00 F7 EE 99 CB 83 7C F4 90 04 00 00 00 98 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 03 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 9D E1 78 06 5E 52 6F 12 04 00 00 00 F7 EE 99 CB 83 7C F4 90 04 00 00 00 99 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 02 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 9E E1 7C 06 5E 52 6F 12 04 00 00 00 F7 EE 99 CB 83 7C F4 90 04 00 00 00 9A 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 01 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 9F E1 80 06 5E 52 6F 12 04 00 00 00 F7 EE 99 CB 83 7C F4 90 04 00 00 00 9B 04 00 00 F8 1A 2D AA 02 00 00 00 01 00 46 BE C0 B7 01 00 00 00 00 23 8F D4 DD 30 75 00 04 D9 20 80 91 04 00 00 00 00 00 00 00 5E 52 6F 12 04 00 00 00 00";
                // Rimuovi gli spazi dalla sequenza esadecimale
                newHexSequence = RemoveSpaces(newHexSequence);

                // Converti la sequenza esadecimale senza spazi in array di byte
                byte[] newBytes = FileReader.StringToByteArray(newHexSequence);

                // Calcola l'offset inizio e fine della parte da sostituire
                int startOffset = 0x2CF77;
                int endOffset = 0x333E0;

                // Assicurati che la lunghezza della sequenza da sostituire corrisponda alla lunghezza dell'intervallo da sostituire
                if (newBytes.Length != (endOffset - startOffset))
                {
                    MessageBox.Show("La lunghezza della sequenza da sostituire non corrisponde alla lunghezza dell'intervallo specificato nel file.");
                    return;
                }

                // Sostituisci i byte nell'array fileBytes con i nuovi byte
                for (int i = 0; i < newBytes.Length; i++)
                {
                    FileReader.fileBytes[startOffset + i] = newBytes[i];
                }

                MessageBox.Show("All Spirit Unlocked");
            }
            else if (result == DialogResult.No) { return; }
        }


        private static string RemoveSpaces(string hexWithSpaces)
        {
            return hexWithSpaces.Replace(" ", "");
        }


        // ============================================== ABOUT============================
        private void AboutSaveEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new();
            about.ShowDialog();
        }

        //==========================Language
        private void ItalianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(FileReader.fileBytes ==  null) 
            {
                MessageBox.Show("Load Game File.");
                return;
            }
            italianTokens = new Tokens("italian");
            italianItems = new Items("italian");
            SearchAndPopulateObjects(true);
            inventoryForm.SearchAndPopulateItem(true);
        }

        private void EnglishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FileReader.fileBytes == null)
            {
                MessageBox.Show("Load Game File.");
                return;
            }
            italianTokens = new Tokens("english");
            italianItems = new Items("english");
            SearchAndPopulateObjects(false);
            inventoryForm.SearchAndPopulateItem(false);
        }

        //=================================================================TEST======================

        private void itemEditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FileReader.fileBytes == null)
            {
                MessageBox.Show("Load Game File.");
                return;
            }
            inventoryForm.ShowDialog();
        }



    }
}

