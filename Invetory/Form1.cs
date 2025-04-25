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
            // checks if visual studio and mysql are connected.
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
            // get data from database.
            dataGridView1.DataSource = inventoryCrud.getData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // call the form named : New_Product
            New_Product f2 = new New_Product();

            // hide the parent form, but user have no control on parent form just the child form.
            f2.ShowDialog();

            // refresh gridview
            dataGridView1.DataSource = inventoryCrud.getData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //pass the highlighted value from datagridview. to new form named : remove-product
            Update_Product f3 = new Update_Product(dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), dataGridView1.SelectedRows[0].Cells[1].Value.ToString(), dataGridView1.SelectedRows[0].Cells[2].Value.ToString(), dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
            
            // hide the parent form, but user have no control on parent form just the child form.
            f3.ShowDialog();

            // refresh gridview
            dataGridView1.DataSource = inventoryCrud.getData();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //bool variable
            bool isSucces;

            //send data to method 'CreateProduct', get result from method. 
            isSucces = inventoryCrud.RemoveProduct(Int32.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
            if (isSucces == true)
            {
                // prompt message if success
                MessageBox.Show("Product has been removed!", "Removed Product", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // refresh datagridview data
                dataGridView1.DataSource = inventoryCrud.getData();

            }
            else
            {
                //vice-versa
                MessageBox.Show("Failed to remove product!", "Try-Again", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            //detects if a data is selected inside datagridview            
            if (e.Button == MouseButtons.Left)
            {
                //enable true, when a user selected a data.
                button2.Enabled = true;
                button5.Enabled = true;
            }
            else
            {
                // vice-versa
                button2.Enabled = false;
                button5.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // assign int variable to get the ttl from the class
            int ttl = 0;

            // assign the variable, and call the Method GetTotalValue from class 
            ttl = inventoryCrud.GetTotalValue();

            //prompt message
            MessageBox.Show("The Over-Total of the inventory is around: " + ttl, "Inventory-Total: " + ttl, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
