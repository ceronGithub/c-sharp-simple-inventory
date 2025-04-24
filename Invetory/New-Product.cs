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
    public partial class New_Product : Form
    {
        //Call class
        classes.InventoryManager inventoryCrud = new classes.InventoryManager();

        public New_Product()
        {
            InitializeComponent();
        }

        private void New_Product_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //bool variable
            bool isSucces;

            //sending data to class named 'CreateProduct'
            isSucces = inventoryCrud.CreateProduct(textBox1.Text, Int32.Parse(textBox2.Text), Int32.Parse(textBox3.Text));
            if (isSucces == true)
            {
                MessageBox.Show("New product has been created!", "Created new product", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to create new product!", "Try-Again", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
