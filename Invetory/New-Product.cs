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

            int wholeNumberOne, wholeNumberTwo;
            //check if price, quantity accept positive number.
            if(int.TryParse(textBox2.Text, out wholeNumberOne) && int.TryParse(textBox3.Text, out wholeNumberTwo))
            {

                if(wholeNumberOne > 0 && wholeNumberTwo > 0)
                {
                    //positive here
                    //sending data to class named 'AddProduct'
                    isSucces = inventoryCrud.AddProduct(textBox1.Text, Int32.Parse(textBox2.Text), Int32.Parse(textBox3.Text));
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
                else
                {
                    MessageBox.Show("Price and Quantity Field doesn't accept negative integers.");
                    textBox2.Text = textBox3.Text = "";
                }
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            /*
             * one decimal place
            if (ch == 46 && textBox2.Text.IndexOf('.') != -1)
            {
                e.Handled = true;
                return;
            }            
            */
            if(!Char.IsDigit(ch) && ch != 8)
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
