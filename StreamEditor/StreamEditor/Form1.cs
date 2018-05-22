using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StreamEditor
{
    public partial class Form1 : Form
    {

        protected string filepath;

        public Form1()
        {
            InitializeComponent();
        }

        public string Filepath
        {
            get { return filepath; }
            set { filepath = value; }
        }


        private void создатьToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            open command = new open(richTextBox1);
            MyMenu menu = new MyMenu(command);
            menu.Ex();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            save command = new save(richTextBox1);
            MyMenu menu = new MyMenu(command);
            menu.Ex();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            delete command = new delete(richTextBox1);
            MyMenu menu = new MyMenu(command);
            menu.Ex();
        }

        private void выбратьВсеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectAll command = new SelectAll(richTextBox1);
            MyMenu menu = new MyMenu(command);
            menu.Ex();
        }
    }
    interface ICommand
    {
        void Execute();
    }
    class MyMenu : MenuStrip
    {
        private ICommand command;

        public MyMenu(ICommand cmd)
        {
            command = cmd;
        }

        public void Ex()
        {
            command.Execute();
        }

    }

    class open : Form1, ICommand
    {
        RichTextBox r;

        public open(RichTextBox rr)
        {
            r = rr;
        }
        public void Execute()
        {

            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Filepath = ofd.FileName;
                try
                {
                    MessageBox.Show(Filepath);
                    r.LoadFile(Filepath);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Поймано исключение: " + e + ".");
                    Filepath = null;
                }
            }


        }
    }
    class save : Form1, ICommand
    {
        RichTextBox r;

        public save(RichTextBox rr)
        {
            r = rr;
        }
        public void Execute()
        {

            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                filepath = sfd.FileName;
                try
                {
                    r.SaveFile(filepath);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Поймано исключение: " + e + ".");
                    throw;
                }
            }



        }

    }
    class SelectAll : Form1, ICommand
    {
        RichTextBox r;

        public SelectAll(RichTextBox rr)
        {
            r = rr;
        }
        public void Execute()
        {
            r.SelectAll();

        }

    }


    class delete : Form1, ICommand
    {
        RichTextBox r;

        public delete(RichTextBox rr)
        {
            r = rr;
        }

        public void Execute()
        {
            r.SelectedText = "";
        }

    }
}
