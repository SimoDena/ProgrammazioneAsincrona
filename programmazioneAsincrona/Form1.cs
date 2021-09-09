using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace programmazioneAsincrona
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string CalcolaSaluto()
        {
            string saluto = "Ciao a tutti!";
            Thread.Sleep(5000);
            return saluto;
        }

        private async void btnSaluta_Click(object sender, EventArgs e)
        {

            //Task<string> task = new Task<string>(CalcolaSaluto);
            //task.Start();

            var task = Task.Run(() => CalcolaSaluto()); // stessa cosa del codice sopra

            lblSaluto.Text = "Calcolando Saluto....";
            string saluto = await task;
            lblSaluto.Text = saluto;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var progress = new Progress<int>(value =>
            {
                progressBar1.Value = value;
                label1.Text = $"{value}%";
            });

            await Task.Run(() => LoopNumers(100, progress));

            label1.Text = "Completato";
        }

        void LoopNumers(int count, IProgress<int> progress)
        {
            for (int i = 0; i < count; i++)
            {
                Thread.Sleep(100);
                var percentComplete = (i * 100) / count;
                progress.Report(percentComplete);
            }
        }
    }
}
