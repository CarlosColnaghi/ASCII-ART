using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASCII_ART
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            txtEscala.Text = "1";
            txtFonte.Text = Convert.ToString(txtASCII.Font.Size);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                picImagem.Image = new Bitmap(openFileDialog.FileName);
                txtASCII.Text = Imagem.converterASCII(new Bitmap(picImagem.Image));
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtEscala.Text = Convert.ToString(Convert.ToInt32(txtEscala.Text) + 1);
            txtASCII.Text = Imagem.converterASCII(new Bitmap(picImagem.Image), Convert.ToInt32(txtEscala.Text));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(Convert.ToInt32(txtEscala.Text) > 1)
            {
                txtEscala.Text = Convert.ToString(Convert.ToInt32(txtEscala.Text) - 1);
                txtASCII.Text = Imagem.converterASCII(new Bitmap(picImagem.Image), Convert.ToInt32(txtEscala.Text));
            } 
        }

        private void button5_Click(object sender, EventArgs e)
        {
            txtFonte.Text = Convert.ToString(Convert.ToInt32(txtFonte.Text) + 1);
            txtASCII.Font = new Font(txtASCII.Font.FontFamily, Convert.ToInt32(txtFonte.Text));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtFonte.Text) > 1)
            {
                txtFonte.Text = Convert.ToString(Convert.ToInt32(txtFonte.Text) - 1);
                txtASCII.Font = new Font(txtASCII.Font.FontFamily, Convert.ToInt32(txtFonte.Text));
            }
        }
    }
}
