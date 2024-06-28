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
using Microsoft.VisualBasic;


namespace daily_check
{
    public partial class Form1 : Form
    {
        private string tasksFilePath = Path.Combine(Application.StartupPath, "tasks.txt");
        public Form1()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            InitializeComponent();
            LoadTasksFromFile();


        }
        private void LoadTasksFromFile()
        {
            if (File.Exists(tasksFilePath))
            {
                string[] tasks = File.ReadAllLines(tasksFilePath);
                foreach (string task in tasks)
                {
                    checkedListBox1.Items.Add(task, false);
                }
            }
        }

        private void SaveTasksToFile()
        {
            using (StreamWriter writer = new StreamWriter(tasksFilePath))
            {
                foreach (var item in checkedListBox1.Items)
                {
                    writer.WriteLine(item.ToString());
                }
            }
        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                checkedListBox1.Items.Add(textBox1.Text, false);
                textBox1.Clear();
                SaveTasksToFile();
            }
            else
            {
                MessageBox.Show("Lütfen bir görev girin.");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedIndex != -1)
            {

                checkedListBox1.Items.RemoveAt(checkedListBox1.SelectedIndex);
                SaveTasksToFile();
            }
            else
            {
                MessageBox.Show("Lütfen silmek için bir görev seçin.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedIndex != -1)
            {
                string selectedTask = checkedListBox1.SelectedItem.ToString();
                string newTask = Microsoft.VisualBasic.Interaction.InputBox("Görevi düzenleyin:", "Görev Düzenleme", selectedTask);

                if (!string.IsNullOrWhiteSpace(newTask))
                {
                    int selectedIndex = checkedListBox1.SelectedIndex;
                    bool isChecked = checkedListBox1.GetItemChecked(selectedIndex);
                    checkedListBox1.Items[selectedIndex] = newTask;
                    checkedListBox1.SetItemChecked(selectedIndex, isChecked);
                    SaveTasksToFile();
                }
                else
                {
                    MessageBox.Show("Görev boş bırakılamaz.");
                }
            }
            else
            {
                MessageBox.Show("Lütfen düzenlemek için bir görev seçin.");
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveTasksToFile();
        }
        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackColor = Color.LightBlue; 
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = SystemColors.Control; 
        }
    }
}
