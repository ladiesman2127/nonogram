﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace nonogram_final
{
	public partial class startForm : Form
	{
		public startForm()
		{
			InitializeComponent();
		}

		private void startForm_Load(object sender, EventArgs e)
		{	
			Button btnStartGame = new Button();
			Controls.Add(btnStartGame);
			btnStartGame.FlatStyle = FlatStyle.Popup;
			btnStartGame.Font = new Font("Hack NF",12,FontStyle.Bold);
			btnStartGame.Anchor = AnchorStyles.Right | AnchorStyles.Left;
			btnStartGame.Size = new Size(120, 40);
			btnStartGame.Location = new Point(Width/2 - btnStartGame.Width/2, Height/2 - btnStartGame.Height/2);
			btnStartGame.Text = "Играть";
			btnStartGame.BackColor = Color.Transparent;
			btnStartGame.Click += BtnStart_Click;
			Button btnAddNgr = new Button();
			Controls.Add( btnAddNgr);
			btnAddNgr.FlatStyle = FlatStyle.Popup;
			btnAddNgr.Anchor = AnchorStyles.Right | AnchorStyles.Left;
			btnAddNgr.Size = new Size(120, 40);
			btnAddNgr.Font = new Font("Hack NF",12,FontStyle.Bold);
			btnAddNgr.Location = new Point(Width/2 - btnAddNgr.Width/2, Height/2 + btnAddNgr.Height);
			btnAddNgr.BackColor = Color.Transparent;
			btnAddNgr.Text = "Добавить кроссворд";
			btnAddNgr.Click += BtnAddNgr_Click;
			Button btnExit = new Button();
			Controls.Add(btnExit);
			btnExit.FlatStyle = FlatStyle.Popup;
			btnExit.Anchor = AnchorStyles.Right | AnchorStyles.Left;
			btnExit.Size = new Size(120, 40);
			btnExit.Location = new Point(Width/2 - btnExit.Width/2, Height/2 + btnExit.Height + 60);
			btnExit.BackColor = Color.Transparent;
			btnExit.Text = "Выход";
			btnExit.Font = new Font("Hack NF", 12, FontStyle.Bold);
			btnExit.Click += BtnExit_Click;

		}

		private void BtnExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void BtnAddNgr_Click(object sender, EventArgs e)
		{
			gameBoard gameBoard = new gameBoard();
			nonogramClass ngr = new nonogramClass(10, 10,gameBoard);
			Hide(); 
			ngr.MakeGameFields();
			ngr.addAddButton();
			gameBoard.ShowDialog();
			Dispose();
		}

		private void BtnStart_Click(object sender, EventArgs e)
		{
			nonogram_object obj;
			BinaryFormatter bf = new BinaryFormatter();
			Random rnd = new Random();
			int rand = rnd.Next(nonogramClass.Index);
			string path = "nonogram" + rand.ToString();
			using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
			{
				obj = (nonogram_object)bf.Deserialize(fs);
				fs.Close();
			}
			Hide();
			gameBoard gameBoard = new gameBoard();
			nonogramClass ngr = new nonogramClass(obj._width, obj._height, gameBoard);
			ngr.MakeGameFields(obj);
			gameBoard.ShowDialog();
			Dispose();
		}
	}
}
