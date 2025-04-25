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
    public partial class Update_Product : Form
    {
        public Update_Product(string id, string name, string quantity, string price)
        {
            InitializeComponent();            
            textBox1.Text = "" + id;
            textBox2.Text = "" + name;
            textBox3.Text = "" + quantity;
            textBox4.Text = "" + price;

            textBox1.Enabled = false;
        }

        //Call class
        classes.InventoryManager inventoryCrud = new classes.InventoryManager();

        private void button1_Click(object sender, EventArgs e)
        {
            bool isSuccess;
            isSuccess = inventoryCrud.UpdateProduct(textBox2.Text, Int32.Parse(textBox4.Text), Int32.Parse(textBox3.Text), Int32.Parse(textBox1.Text)) ;
            if (isSuccess == true)
            {
                MessageBox.Show("productId :" +textBox1.Text+ " has been updated!", "Update productId: " + textBox1.Text , MessageBoxButtons.OK, MessageBoxIcon.Information);            
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to update product!", "Try-Again", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }
    }
}
