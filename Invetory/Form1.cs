using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            }
            else
            {
                MessageBox.Show("its not connected!", "Un-successful", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = inventoryCrud.getData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            New_Product f2 = new New_Product();
            f2.Show();
            this.Hide();
        }
    }
}
