using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataGridView;

namespace Invetory
{
    public partial class Form1 : Form
    {
        //Call class
        classes.InventoryManager inventoryCrud = new classes.InventoryManager();

        //General variable/s
        bool checkerDB;
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void checkDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkerDB = inventoryCrud.checkDatabaseIfConnected();
            if(checkerDB == true)
            {
                MessageBox.Show("It is connected!","Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                checkDatabaseToolStripMenuItem.BackColor = Color.Green;
                button1.Enabled = button3.Enabled = button4.Enabled = true;
            }
            else
            {
                MessageBox.Show("its not connected!", "Un-successful", MessageBoxButtons.OK, MessageBoxIcon.Error);
                checkDatabaseToolStripMenuItem.BackColor = Color.Red;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = inventoryCrud.getData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            New_Product f2 = new New_Product();
            f2.ShowDialog();
            dataGridView1.DataSource = inventoryCrud.getData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //pass the highlighted value from datagridview. to form remove-product
            Update_Product f3 = new Update_Product(dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), dataGridView1.SelectedRows[0].Cells[1].Value.ToString(), dataGridView1.SelectedRows[0].Cells[2].Value.ToString(), dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
            f3.ShowDialog();
            dataGridView1.DataSource = inventoryCrud.getData();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //bool variable
            bool isSucces;

            //sending data to class named 'CreateProduct'
            isSucces = inventoryCrud.RemoveProduct(Int32.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
            if (isSucces == true)
            {
                MessageBox.Show("Product has been removed!", "Removed Product", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = inventoryCrud.getData();

            }
            else
            {
                MessageBox.Show("Failed to remove product!", "Try-Again", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            //'#See if the left mouse button was clicked
            if (e.Button == MouseButtons.Left)
            {
                button2.Enabled = true;
                button5.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
                button5.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int ttl = 0;
            ttl = inventoryCrud.GetTotalValue();
            MessageBox.Show("The Over-Total of the inventory is around: " + ttl, "Inventory-Total: " + ttl, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
