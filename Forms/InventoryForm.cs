using Inazuma_Eleven_Victory_Road_Beta.InazumaEleven_V1._0._0;
using Inazuma_Eleven_Victory_Road_Beta.Logic;
using InazumaElevenVictoryRoad;
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

namespace Inazuma_Eleven_Victory_Road_Beta
{

    public partial class InventoryForm : Form
    {
        private Items italianItems;
        readonly Items italianitems = new("italian");
        private readonly EnglishItems englishitems;

        public InventoryForm()
        {
            InitializeComponent();
            
            italianItems = new Items("italian");
            englishitems = new EnglishItems();
            dataGridView1.CellEndEdit += DataGridView1_CellEndEdit;
            dataGridView2.CellEndEdit += DataGridView2_CellEndEdit;
            dataGridView3.CellEndEdit += DataGridView3_CellEndEdit;
            dataGridView4.CellEndEdit += DataGridView4_CellEndEdit;
        }
    

        public  void SearchAndPopulateItem(bool isItalian)
        {
            if (FileReader.fileBytes is null)
            {
                MessageBox.Show("FileReader.fileBytes is null. Please load the file first.");
                return;
            }
            else
            {
                dataGridView1.Rows.Clear();
                dataGridView2.Rows.Clear();
                dataGridView3.Rows.Clear();
                dataGridView4.Rows.Clear();

                Dictionary<string, string> boots = isItalian ? italianItems.Boots : englishitems.Boots;
                Dictionary<string, string> bracelet = isItalian ? italianItems.Bracelet : englishitems.Bracelet;
                Dictionary<string, string> pendant = isItalian ? italianItems.Pendant : englishitems.Pendant;
                Dictionary<string, string> special = isItalian ? italianItems.Special : englishitems.Special;

                foreach (var kvp in boots)
                {
                    string objectId = kvp.Key;
                    string objectName = kvp.Value;

                    int objectIndex = FileReader.FindPattern(FileReader.fileBytes, FileReader.StringToByteArray(objectId), 0x00022497, 0x000227F7);

                    if (objectIndex != -1)
                    {
                        int quantityIndex = objectIndex + 24;

                        byte quantity = FileReader.fileBytes[quantityIndex];

                        dataGridView1.Rows.Add(objectId, objectName, quantity);

                    }
                }

                foreach (var kvp in bracelet)
                {
                    
                    string objectId = kvp.Key;
                    string objectName = kvp.Value;

                    int objectIndex = FileReader.FindPattern(FileReader.fileBytes, FileReader.StringToByteArray(objectId), 0x000240BF, 0x000243AF);

                    if (objectIndex != -1)
                    {
                        int quantityIndex = objectIndex + 24;

                        byte quantity = FileReader.fileBytes[quantityIndex];

                        dataGridView2.Rows.Add(objectId, objectName, quantity);

                    }
                }
                foreach (var kvp in pendant)
                {
                  
                    string objectId = kvp.Key;
                    string objectName = kvp.Value;

                    int objectIndex = FileReader.FindPattern(FileReader.fileBytes, FileReader.StringToByteArray(objectId), 0x00025BBF, 0x00025FCF);

                    if (objectIndex != -1)
                    {
                        int quantityIndex = objectIndex + 24;

                        byte quantity = FileReader.fileBytes[quantityIndex];

                        dataGridView3.Rows.Add(objectId, objectName, quantity);

                    }
                }

                foreach (var kvp in special)
                {
                   
                    string objectId = kvp.Key;
                    string objectName = kvp.Value;

                    int objectIndex = FileReader.FindPattern(FileReader.fileBytes, FileReader.StringToByteArray(objectId), 0x0002790F, 0x00027C1A);

                    if (objectIndex != -1)
                    {
                        int quantityIndex = objectIndex + 24;

                        byte quantity = FileReader.fileBytes[quantityIndex];

                        dataGridView4.Rows.Add(objectId, objectName, quantity);

                    }
                }

            }

        }
        //==========================================================================================================
        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 2)
            {
                try
                {
                    string objectId = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    string objectName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();

                    if (!sbyte.TryParse(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString(), out sbyte quantity))
                    {
                        MessageBox.Show("Invalid quantity format. Please enter a valid integer.");
                        return;
                    }

                    if (quantity < -128 || quantity > 127)
                    {
                        MessageBox.Show("Quantity must be between -128 and 127");
                        return;
                    }

                    int objectIndex = FileReader.FindPattern(FileReader.fileBytes, FileReader.StringToByteArray(objectId), 0x00022497, 0x000227F7);

                    if (objectIndex != -1)
                    {
                        int quantityIndex = objectIndex + 24;

                       
                       FileReader.fileBytes[quantityIndex] = (byte)quantity;
                    }
                    else
                    {
                        MessageBox.Show("Object ID not in allowed range");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An Error occurred while changing the quantity: " + ex.Message);
                }
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

                    if (!sbyte.TryParse(dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString(), out sbyte quantity))
                    {
                        MessageBox.Show("Invalid quantity format. Please enter a valid integer.");
                        return;
                    }

                    if (quantity < -128 || quantity > 127)
                    {
                        MessageBox.Show("Quantity must be between -128 and 127");
                        return;
                    }

                    int objectIndex = FileReader.FindPattern(FileReader.fileBytes, FileReader.StringToByteArray(objectId), 0x00022497, 0x000227F7);

                    if (objectIndex != -1)
                    {
                        int quantityIndex = objectIndex + 24;

                        
                        FileReader.fileBytes[quantityIndex] = (byte)quantity;
                    }
                    else
                    {
                        MessageBox.Show("Object ID not in allowed range");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An Error occurred while changing the quantity: " + ex.Message);
                }
            }

        }

        private void DataGridView3_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 2)
            {
                try
                {
                    string objectId = dataGridView3.Rows[e.RowIndex].Cells[0].Value.ToString();
                    string objectName = dataGridView3.Rows[e.RowIndex].Cells[1].Value.ToString();

                    if (!sbyte.TryParse(dataGridView3.Rows[e.RowIndex].Cells[2].Value.ToString(), out sbyte quantity))
                    {
                        MessageBox.Show("Invalid quantity format. Please enter a valid integer.");
                        return;
                    }

                    if (quantity < -128 || quantity > 127)
                    {
                        MessageBox.Show("Quantity must be between -128 and 127");
                        return;
                    }

                    int objectIndex = FileReader.FindPattern(FileReader.fileBytes, FileReader.StringToByteArray(objectId), 0x00022497, 0x000227F7);

                    if (objectIndex != -1)
                    {
                        int quantityIndex = objectIndex + 24;

                       
                        FileReader.fileBytes[quantityIndex] = (byte)quantity;
                    }
                    else
                    {
                        MessageBox.Show("Object ID not in allowed range");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An Error occurred while changing the quantity: " + ex.Message);
                }
            }

        }

        private void DataGridView4_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 2)
            {
                try
                {
                    string objectId = dataGridView4.Rows[e.RowIndex].Cells[0].Value.ToString();
                    string objectName = dataGridView4.Rows[e.RowIndex].Cells[1].Value.ToString();

                    if (!sbyte.TryParse(dataGridView4.Rows[e.RowIndex].Cells[2].Value.ToString(), out sbyte quantity))
                    {
                        MessageBox.Show("Invalid quantity format. Please enter a valid integer.");
                        return;
                    }

                    if (quantity < -128 || quantity > 127)
                    {
                        MessageBox.Show("Quantity must be between -128 and 127");
                        return;
                    }

                    int objectIndex = FileReader.FindPattern(FileReader.fileBytes, FileReader.StringToByteArray(objectId), 0x00022497, 0x000227F7);

                    if (objectIndex != -1)
                    {
                        int quantityIndex = objectIndex + 24;

                        // Aggiorna la quantità nell'array di byte del file
                        FileReader.fileBytes[quantityIndex] = (byte)quantity;
                    }
                    else
                    {
                        MessageBox.Show("Object ID not in allowed range");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An Error occurred while changing the quantity: " + ex.Message);
                }
            }

        }
    }

}
