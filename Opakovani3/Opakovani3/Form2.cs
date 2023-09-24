using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Opakovani3
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.CenterToScreen();
            button2.Visible=false;
            button3.Visible=false;
            button4.Visible=false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            button3.Visible = false;
            button4.Visible = false;
            File.Delete("Cisla.dat");
            OpenFileDialog op = new OpenFileDialog();
            op.DefaultExt = "txt";
            op.InitialDirectory= Path.GetDirectoryName(Application.ExecutablePath);
            string file="";
            if (op.ShowDialog() == DialogResult.OK)
            {
                file = op.FileName;
                button2.Visible = true;
                StreamReader cteni = new StreamReader(file);
                FileStream tok = new FileStream("Cisla.dat", FileMode.OpenOrCreate, FileAccess.Write);
                BinaryWriter zapis = new BinaryWriter(tok);
                zapis.BaseStream.Position = 0;
                while (!cteni.EndOfStream)
                {
                    string line = cteni.ReadLine();
                    string[] pole = line.Split(';');
                    double max = 0;
                    for (int i = 0; i < pole.Count(); i++)
                    {
                        if (max < pole[i].Length)
                        {
                            max = pole[i].Length;
                        }
                    }
                    max /= 10;
                    zapis.Write(max);
                    max = 0;
                }
                cteni.Close();

                tok.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button3.Visible=true;
            listBox1.Items.Clear();
            FileStream tok = new FileStream("Cisla.dat", FileMode.OpenOrCreate, FileAccess.Read);
            BinaryReader bincteni = new BinaryReader(tok);
            bincteni.BaseStream.Position = 0;
            while (bincteni.BaseStream.Position < bincteni.BaseStream.Length)
            {
                listBox1.Items.Add(bincteni.ReadDouble());
            }
            tok.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button4.Visible=true;
            FileStream tok = new FileStream("Cisla.dat", FileMode.OpenOrCreate, FileAccess.Write);
            BinaryWriter zapis = new BinaryWriter(tok);
            zapis.BaseStream.Position = 0;
            foreach(double i in listBox1.Items)
            {
                double cislo = i;
                if (cislo < 1) cislo *= 10;
                zapis.Write(cislo);
            }
            tok.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FileStream tok = new FileStream("Cisla.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryReader bincteni = new BinaryReader(tok);
            BinaryWriter zapis = new BinaryWriter(tok);
            bincteni.BaseStream.Position = 0;
            double pocet=0;
            double soucet = 0;
            while (bincteni.BaseStream.Position < bincteni.BaseStream.Length)
            {
                double cislo= bincteni.ReadDouble();
                if (cislo > 2)
                {
                    pocet++;
                    soucet += cislo;
                }
            }
            double prumer = soucet / pocet;
            zapis.BaseStream.Position = zapis.BaseStream.Length;
            zapis.Write(prumer);
            MessageBox.Show("Průměr: " + prumer);
            tok.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Příklad 6.\r\nButton1\r\nV textovém souboru vybraném pomocí OpenFileDialogu je na každém řádku zapsáno více\r\nslov oddělených středníkem. Tento soubor zobrazte.\r\nV každém řádku souboru najděte nejdelší slovo a délky těchto nejdelších slov podělte deseti\r\na výsledné reálné číslo zapisujte do nově vytvořeného datového souboru reálných čísel\r\nCisla.dat.\r\nPříklad textového souboru:\r\nPes;kočka;kuřátko\r\nrůže;sedmikráska,pýr\r\npavouk;brouk\r\nK němu vytvořený soubor cisla.dat bude obsahovat reálná čísla:\r\n0,7\n1,5\r\n0,6\r\nButton2\r\nZobrazte soubor Cisla.dat\r\nButton3\r\nV souboru Cisla.dat opravte všechna čísla menší než 1 na desetinásobek.\r\nOpravený soubor\r\n7\n1,5\r\n6\r\nButton4\r\nV souboru Cisla.dat spočtěte aritmetický průměr ze všech čísel větších než 2 a tento\r\naritmetický průměr zapište na konec souboru.\r\nOpravený soubor\r\n7\r\n1,1\r\n6\r\n6,5");
        }
    }
}
