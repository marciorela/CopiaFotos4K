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

namespace CopiaFotos4K
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            listBox1.Items.Add("Iniciando a verificação...");

            DirectoryInfo dirInfo = new DirectoryInfo(txtFolder.Text.Trim());
            if (dirInfo.Exists)
            {
                listBox1.Items.Add("Criando diretório...");
                Directory.CreateDirectory(dirInfo.FullName + "\\4K");

                // PARA CADA ARQUIVO (CONF)...
                // MOVER TODOS OS ARQUIVOS COM O MESMO NOME (NAO CONF)
                // RENOMEAR O ARQUIVO ATUAL (RETIRANDO O CONF)
                listBox1.Items.Add("Verificando arquivos (CONV)...");
                var list = dirInfo.GetFiles("* (CONV).*");
                foreach (var fi in list)
                {
                    listBox1.Items.Add(fi.Name);

                    var sSimpleName = fi.Name.Replace(" (CONV)", "");
                    var sOtherFiles = sSimpleName.Replace(fi.Extension, ".*");
                    var listOther = dirInfo.GetFiles(sOtherFiles);
                    foreach (var fo in listOther)
                    {
                        listBox1.Items.Add($"Movendo arquivo {fo.Name}...");
                        File.Move(fo.FullName, $"{dirInfo.FullName}\\4K\\{fo.Name}");
                    }

                    listBox1.Items.Add("Renomeando arquivo original...");
                    File.Move(fi.FullName, $"{dirInfo.FullName}\\{sSimpleName}");
                }

                // APAGAR ARQUIVOS "AAE"
                listBox1.Items.Add("Verificando arquivos AAE...");
                list = dirInfo.GetFiles("*.AAE");
                foreach (var fi in list)
                {
                    listBox1.Items.Add($"Excluindo arquivo {fi.Name}...");
                    File.Delete(fi.FullName);
                }

            } else 
            {
                listBox1.Items.Add("Diretório não encontrado.");
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = txtFolder.Text.Trim();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFolder.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}
