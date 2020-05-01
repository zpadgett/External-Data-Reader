using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinorAssignment3
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void pRODUCTBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.pRODUCTBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.product_DatabaseDataSet);

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'product_DatabaseDataSet.PRODUCT' table. You can move, or remove it, as needed.
            this.pRODUCTTableAdapter.Fill(this.product_DatabaseDataSet.PRODUCT);

        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure you want to exit?", this.Text,
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialog == DialogResult.Yes)

                this.Close();
        }

        private void displayButton_Click(object sender, EventArgs e)
        {

            this.pRODUCTTableAdapter.FillBySort(this.product_DatabaseDataSet.PRODUCT);

            double totalValue = 0, totalAmount, inventoryAmount, inventoryPercentAmount, cumulativeValue = 0;
            string name, purchaseCost, quantity, inventoryValue, inventoryPercent, cumulativePercent;

            productView.Items.Clear();

            foreach (DataRow dr in product_DatabaseDataSet.PRODUCT.Rows)
            {
                totalAmount = double.Parse(dr["Inventory_Value"].ToString());
                totalValue += totalAmount;
            }

            foreach (DataRow dr in product_DatabaseDataSet.PRODUCT.Rows)
            {
                name = dr["Product_Name"].ToString();
                purchaseCost = dr["Purchase_Cost"].ToString();
                quantity = dr["Quantity_On_Hand"].ToString();

                inventoryAmount = double.Parse(purchaseCost.ToString()) * double.Parse(quantity.ToString());
                inventoryValue = inventoryAmount.ToString();

                inventoryPercentAmount = inventoryAmount / totalValue;
                inventoryPercent = inventoryPercentAmount.ToString("P2");

                cumulativeValue += inventoryPercentAmount;
                cumulativePercent = cumulativeValue.ToString("P2");

                string[] fieldsArray = new string[6];
                fieldsArray[0] = name;
                fieldsArray[1] = purchaseCost;
                fieldsArray[2] = quantity;
                fieldsArray[3] = inventoryValue;
                fieldsArray[4] = inventoryPercent;
                fieldsArray[5] = cumulativePercent;
                ListViewItem showsLVI = new ListViewItem(fieldsArray);
                productView.Items.Add(showsLVI);

            }



            totalInventoryValue.Text = totalValue.ToString("C2");

        }
    }
}
