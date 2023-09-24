using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Opakovani3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        string Vek(string vstup, out double vek, ref int soucet)
        {
            try
            {
                string[] pole = vstup.Split(';');
                string rok = pole[2].Substring(0, 2);
                soucet += Convert.ToInt32(pole[1]);
                int mesic = Convert.ToInt32(pole[2].Substring(2, 2));
                int den = Convert.ToInt32(pole[2].Substring(4, 2));
                if (int.Parse(rok) > 23)
                {
                    rok = "19" + rok;
                }
                else rok = "20" + rok;
                if (mesic > 12) mesic -= 50; ;
                DateTime narozeni = new DateTime(int.Parse(rok), mesic, den);
                vek = Math.Round((DateTime.Now - narozeni).TotalDays / 365.25, 1);

                switch (mesic)
                {
                    case 1: return "Leden";
                    case 2: return "Únor";
                    case 3: return "Březen";
                    case 4: return "Duben";
                    case 5: return "Květen";
                    case 6: return "Červen";
                    case 7: return "Červenec";
                    case 8: return "Srpen";
                    case 9: return "Září";
                    case 10: return "Říjen";
                    case 11: return "Listopad";
                    case 12: return "Prosinec";
                    default: return "";
                }
            }
            catch
            {
                vek = 0;
                return "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamReader cteni;
            StreamWriter zapis;
            double vek;
            bool prvni = false;
            listBox1.Items.Clear();
            cteni = new StreamReader("rodna_cis.txt");
            int soucet = 0;
            int pocet = 0;
            string jmeno;
            List<string> seznam = new List<string>();
            while(!cteni.EndOfStream)
            { 
                string radek = cteni.ReadLine();
                listBox1.Items.Add(radek);
                string mesic = Vek(radek, out vek,ref soucet);
                seznam.Add(radek + ";" + vek);
                if (radek.Contains(";")) jmeno = radek.Substring(0, radek.IndexOf(";"));
                else jmeno = "";
                if (mesic== "Prosinec"&&!prvni)
                {
                    MessageBox.Show("první člověk narozený v prosinci: " + jmeno);
                    prvni = true;
                }
                pocet++;
            }
            cteni.Close();
            zapis = new StreamWriter("rodna_cis.txt",false);
            double prumer = soucet / pocet;
            int i = 0;
            foreach(string s in listBox1.Items)
            {
                zapis.WriteLine(seznam[i]);
                i++;
            }
            zapis.WriteLine(prumer);
            zapis.Close();
            cteni = new StreamReader("rodna_cis.txt");
            while (!cteni.EndOfStream)
            {
                listBox2.Items.Add(cteni.ReadLine());
            }
            cteni.Close();


            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            saveFileDialog.DefaultExt = "txt";
            string file = "";
            while (file == "")
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    file = saveFileDialog.FileName;
                }
            }

            zapis = new StreamWriter(file, false);
             i = 0;
            foreach (string s in listBox1.Items)
            {
                string[] pole = s.Split(';');
                if (Convert.ToInt32(pole[1]) < 3)
                {
                    seznam[i] +=" ;"+ Vek(s, out vek,ref soucet);
                    zapis.WriteLine(seznam[i]);
                }
                i++;
            }
            zapis.WriteLine(prumer);
            zapis.Close();

            cteni = new StreamReader(file);
            while (!cteni.EndOfStream)
            {
                string r = cteni.ReadLine();
                listBox3.Items.Add(r);
            }
            cteni.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Příklad 5.\r\nVytvořte metodu Vek, která z rodného čísla vrátí věk a slovně měsíc narození.\r\nJe dán textový soubor rodna_cis.txt. Na každém řádku je jméno, středník, známka,\r\nstředník, rodné číslo. Soubor vypište do ListBoxu.\r\nSpočítejte průměr známek všech lidí, který zapište na konec textového souboru\r\nVypište prvního člověka narozeného v prosinci, nezapomeňte, že muž má druhé dvojčíslí\r\nměsíc 12 a žena 62. Využijte metodu Vek\r\nSoubor rodna_cis.txt, opravte tak, že na konec každého řádku přidejte středník a za něj\r\nzapište věk. Využijte metodu Vek. Soubor vypište do dalšího ListBoxu.\r\nVytvořte nový textový soubor, který bude obsahovat jméno člověka, středník, věk,\r\nstředník, slovně měsíc jen těch lidí, kteří mají známku lepší než 3. Název nového textového\r\nsouboru vyberte pomocí SaveFileDialogu. Soubor vypište do dalšího ListBoxu.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form f2 = new Form2();
            f2.ShowDialog();
        }
    }
}
