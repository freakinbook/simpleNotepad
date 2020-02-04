using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace Notepad
{
	public partial class Form1 : Form
	{
		private Stream fileStream = null;
		private bool changes = false;

		public Form1()
		{
			InitializeComponent();
		}

		private void richTextBox1_TextChanged(object sender, EventArgs e)
		{
			changes = true;
		}

		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DialogResult dialogResult = MessageBox.Show("Clear text and create new?", "New file", MessageBoxButtons.YesNo);
			if (dialogResult == DialogResult.Yes)
			{
				richTextBox1.Clear();
				fileStream = null;
			} else
			{
				return;
			}
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			openFileDialog1.ShowDialog();
		}

		private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
		{
			fileStream = openFileDialog1.OpenFile();
			
			StreamReader reader = new StreamReader(fileStream);
			richTextBox1.Clear();
			richTextBox1.Text = reader.ReadToEnd();
			reader.Close();
			changes = false;
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (fileStream == null)
			{
				saveAsToolStripMenuItem_Click(sender, e);
				return;
			}
			File.WriteAllText(openFileDialog1.FileName,richTextBox1.Text);
			changes = false;
		}

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			saveFileDialog1.ShowDialog();
		}

		private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
		{
			File.WriteAllText(saveFileDialog1.FileName, richTextBox1.Text);
			changes = false;

		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (changes)
			{
				DialogResult dialogResult = MessageBox.Show("Discard changes?", "Exit", MessageBoxButtons.YesNo);
				if (dialogResult == DialogResult.Yes)
				{
					Application.Exit();
				}
				else
				{
					return;
				}
			} else
			{
				Application.Exit();
			}
			
		}
	}
}
