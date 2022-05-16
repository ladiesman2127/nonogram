using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nonogram_final
{

	public class nonogramClass
	{
		private nonogram_object                 checkNonogramObject;
		public static int                       Index;
		private readonly int                    _width;
		private readonly int                    _height;
		private readonly int                    _overallHeight;
		private readonly int                    _overallWidth;
		private readonly int                    _topIndicesLegnth;
		private readonly int                    _leftIndicesLength;
		private PictureBox                      _pictureBox;
		private Label                           _label;
		private readonly List<List<PictureBox>> _pBoxes = new List<List<PictureBox>>();
		private readonly List<List<Label>>      _labels = new List<List<Label>>();
		private readonly List<int>              _singleNonogramList = new List<int>();
		private readonly List<List<int>>        _allNonograms = new List<List<int>>();
		private readonly gameBoard              _thisGameBoard = new gameBoard();
		private readonly Panel                  _mainPanel = new Panel();



		public nonogramClass(int width, int height, gameBoard gameBoard)
		{
			_width = width;
			_height = height;
			_thisGameBoard = gameBoard;
			gameBoard.Controls.Add(_mainPanel);
			_topIndicesLegnth = height / 2;
			_leftIndicesLength = width / 2;
			if (height % 2 != 0)
				_topIndicesLegnth++;
			if (width % 2 != 0)
				_leftIndicesLength++;
			_overallHeight = height + _topIndicesLegnth;
			_overallWidth = width + _leftIndicesLength;
			gameBoard.Size = new Size(38 * _overallWidth + 80, 38 * _overallHeight + 80);
			_mainPanel.Location = new Point(30, 30);
			gameBoard.StartPosition = FormStartPosition.CenterScreen;
		}


		public void MakeGameFields()
		{
			int x = 0, y = 0;
			_mainPanel.Size = new Size(38 * _overallWidth + 200, 38 * _overallHeight + 200);
			for (int i = 0; i < _overallHeight; i++)
			{
				List<PictureBox> linePictureBoxes = new List<PictureBox>();
				List<Label> lineLabels = new List<Label>();
				for (int j = 0; j < _overallWidth; j++)
				{
					if (i < _topIndicesLegnth ^ j < _leftIndicesLength)
					{
						_label = new Label();
						_mainPanel.Controls.Add(_label);
						_label.Size = new Size(38, 38);
						_label.BorderStyle = BorderStyle.FixedSingle;
						_label.Location = new Point(x, y);
						_label.BackColor = Color.AliceBlue;
						lineLabels.Add(_label);
					}
					else if (i >= _topIndicesLegnth && j >= _leftIndicesLength)
					{
						_pictureBox = new PictureBox();
						_pictureBox.BackgroundImageLayout = ImageLayout.Zoom;
						_mainPanel.Controls.Add(_pictureBox);
						_pictureBox.Size = new Size(38, 38);
						_pictureBox.BorderStyle = BorderStyle.FixedSingle;
						_pictureBox.Location = new Point(x, y);
						_pictureBox.BackColor = Color.WhiteSmoke;
						_pictureBox.MouseClick += PictureBox_MouseClick;
						_pictureBox.MouseDoubleClick += PictureBox_MouseDoubleClick;
						linePictureBoxes.Add(_pictureBox);
					}
					x += 38;
				}
				x = 0;
				y += 38;
				_pBoxes.Add(linePictureBoxes);
				_labels.Add(lineLabels);
			}
		}
		public void MakeGameFields(nonogram_object nObj)
		{
			checkNonogramObject = nObj;
			int x = 0, y = 0;
			int cur_index = 0;
			Button btnBack = new Button();
			_mainPanel.Controls.Add(btnBack);
			btnBack.Size = new Size(20, 20);
			IFormatter formatter = new BinaryFormatter();
			using (var writer = new FileStream("index.txt", FileMode.Open))
			{
				nonogramClass.Index = (int)formatter.Deserialize(writer);

			}
			ComboBox cmbBox = new ComboBox();
			cmbBox.SelectedIndexChanged += CmbBox_SelectedIndexChanged;
			for (int i = 0; i < Index; i++)
			{
				cmbBox.Items.Add(i.ToString());
			}
			_mainPanel.Controls.Add(cmbBox);
			cmbBox.Size = new Size(40, 10);
			cmbBox.Location = new Point(0,20);
			btnBack.Location = new Point(20, 0);
			btnBack.FlatStyle = FlatStyle.Popup;
			btnBack.BackgroundImageLayout = ImageLayout.Stretch;
			btnBack.BackgroundImage = Image.FromFile("keyboard_backspace_FILL0_wght400_GRAD0_opsz48.png");
			Button btnCheck = new Button();
			btnBack.Click += BtnBack_Click;
			btnCheck.Click += BtnCheck_Click;
			_mainPanel.Controls.Add(btnCheck);
			btnCheck.FlatStyle = FlatStyle.Popup;
			btnCheck.Location = new Point(0, 0);
			btnCheck.BackgroundImageLayout = ImageLayout.Stretch;
			btnCheck.BackgroundImage = Image.FromFile("D:\\Downloads\\done_FILL0_wght400_GRAD0_opsz48.png");
			btnCheck.Size = new Size(20, 20);
			_mainPanel.Size = new Size(38 * _overallWidth + 200, 38 * _overallHeight + 200);
			for (int i = 0; i < _overallHeight; i++)
			{
				List<PictureBox> linePictureBoxes = new List<PictureBox>();
				List<Label> lineLabels = new List<Label>();
				for (int j = 0; j < _overallWidth; j++)
				{
					if (i < _topIndicesLegnth ^ j < _leftIndicesLength)
					{
						_label = new Label();
						_mainPanel.Controls.Add(_label);
						if (nObj._lst[cur_index] != 0)
							_label.Text = nObj._lst[cur_index].ToString();
						else
						{
							_label.Hide();
						}
						_label.Size = new Size(38, 38);
						_label.BorderStyle = BorderStyle.FixedSingle;
						_label.Location = new Point(x, y);
						_label.BackColor = Color.AliceBlue;
						lineLabels.Add(_label);
						cur_index++;
					}
					else if (i >= _topIndicesLegnth && j >= _leftIndicesLength)
					{
						_pictureBox = new PictureBox();
						_pictureBox.BackgroundImageLayout = ImageLayout.Zoom;
						_mainPanel.Controls.Add(_pictureBox);
						_pictureBox.Size = new Size(38, 38);
						_pictureBox.BorderStyle = BorderStyle.FixedSingle;
						_pictureBox.Location = new Point(x, y);
						_pictureBox.BackColor = Color.WhiteSmoke;
						_pictureBox.MouseClick += PictureBox_MouseClick;
						_pictureBox.MouseDoubleClick += PictureBox_MouseDoubleClick;
						linePictureBoxes.Add(_pictureBox);
						cur_index++;
					}
					x += 38;
				}
				x = 0;
				y += 38;
				_pBoxes.Add(linePictureBoxes);
				_labels.Add(lineLabels);
			}
		}

		private void BtnBack_Click(object? sender, EventArgs e)
		{
			startForm startForm = new startForm();
			_thisGameBoard.Hide();
			startForm.ShowDialog();
			_thisGameBoard.Dispose();
		}

		private void BtnCheck_Click(object? sender, EventArgs e)
		{
			for (int i = 0; i < _overallHeight; i++)
			{
				for (int j = 0; j < _overallWidth; j++)
				{
					if (i < _topIndicesLegnth ^ j < _leftIndicesLength)
					{
						if (i < _topIndicesLegnth && j >= _leftIndicesLength &&
						    _labels[i][j - _leftIndicesLength].Text != "0" && _labels[i][j - _leftIndicesLength].Text != "")
							_singleNonogramList.Add(Convert.ToInt32(_labels[i][j - _leftIndicesLength].Text));
						else if (i >= _topIndicesLegnth && j < _leftIndicesLength && _labels[i][j].Text != "0" &&  _labels[i][j].Text != "")
							_singleNonogramList.Add(Convert.ToInt32(_labels[i][j].Text));
						else
						{
							_singleNonogramList.Add(0);
						}
					}

					if (i >= _topIndicesLegnth && j >= _leftIndicesLength)
					{
						if (_pBoxes[i][j - _leftIndicesLength].BackColor == Color.Black)
							_singleNonogramList.Add(1);
						else
							_singleNonogramList.Add(0);
					}
				}
			}

			int ind = 0;
			while (ind < _singleNonogramList.Count)
			{
				if (_singleNonogramList[ind] != checkNonogramObject._lst[ind])
					break;
				ind++;
			}

			if (ind == _singleNonogramList.Count)
				MessageBox.Show("Правильно!");
			else
				MessageBox.Show("Неправильно!");
		}

		private void CmbBox_SelectedIndexChanged(object? sender, EventArgs e)
		{
			ComboBox cmbBox = sender as ComboBox;
			nonogram_object obj;
			BinaryFormatter bf = new BinaryFormatter();
			string path = "nonogram"  + cmbBox.SelectedIndex.ToString();
			using (FileStream fs = new FileStream(path, FileMode.Open))
			{
				obj = (nonogram_object)bf.Deserialize(fs);
				fs.Close();
			}
			_thisGameBoard.Hide();
			gameBoard gameBoard = new gameBoard();
			nonogramClass ngr = new nonogramClass(obj._width, obj._height, gameBoard);
			ngr.MakeGameFields(obj);
			gameBoard.ShowDialog();
		}

		public void MakeGameFields(nonogramClass ngr)
		{
			for (int i = 0; i < _topIndicesLegnth; i++)
			{
				for (int j = 0; j < _width; j++)
				{
					if (_labels[i][j].Text == "0")
						_labels[i][j].Hide();
					else
						_labels[i][j].Show();
				}
			}

			for (int i = 0; i < _overallHeight; i++)
			{
				for (int j = 0; j < _leftIndicesLength; j++)
				{
					if (_labels[i][j].Text == "0")
						_labels[i][j].Hide();
					else
						_labels[i][j].Show();
				}
			}
		}

		private TextBox txtWidth;
		private TextBox txtHeight;
		public void addAddButton()
		{
			Button btnAdd = new Button();
			Button btnUpdate = new Button();
			btnUpdate.FlatStyle = FlatStyle.Popup;
			btnAdd.FlatStyle = FlatStyle.Popup;
			btnUpdate.Size = new Size(20, 20);
			btnUpdate.Anchor = AnchorStyles.Left | AnchorStyles.Top;
			btnUpdate.BackgroundImageLayout = ImageLayout.Stretch;
			btnUpdate.Click += BtnUpdate_Click;
			btnUpdate.Location = new Point(0, 20);
			btnUpdate.BackgroundImage = Image.FromFile("update_FILL0_wght400_GRAD0_opsz48.png");
			_mainPanel.Controls.Add(btnUpdate);
			btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Left;
			btnAdd.Size = new Size(20, 20);
			btnAdd.Location = new Point(0, 0);
			btnAdd.Click += BtnAdd_Click;
			btnAdd.BackgroundImage = Image.FromFile("outline_add_black_48dp.png");
			btnAdd.BackgroundImageLayout = ImageLayout.Stretch;
			Button btnback = new Button();
			_mainPanel.Controls.Add(btnback);
			btnback.Location = new Point(btnUpdate.Location.X, btnUpdate.Location.Y + 20);
			btnback.BackgroundImageLayout = ImageLayout.Stretch;
			btnback.FlatStyle = FlatStyle.Popup;
			btnback.Size = new Size(20, 20);
			btnback.BackgroundImage = Image.FromFile("keyboard_backspace_FILL0_wght400_GRAD0_opsz48.png");
			btnback.Click += Btnback_Click;
			txtWidth = new TextBox();
			txtHeight = new TextBox();
			_mainPanel.Controls.Add(txtHeight);
			txtHeight.Size = new Size(20, 20);
			txtWidth.Size = new Size(20, 20);
			txtHeight.Location = new Point(20, 0);
			txtWidth.Location = new Point(20, 20);
			_mainPanel.Controls.Add(txtWidth);
			_mainPanel.Controls.Add(btnAdd);

		}

		private void Btnback_Click(object? sender, EventArgs e)
		{
			startForm startForm = new startForm();
			_thisGameBoard.Hide();
			startForm.ShowDialog();
			_thisGameBoard.Dispose();
		}

		private void BtnUpdate_Click(object sender, EventArgs e)
		{
			gameBoard newGameBoard = new gameBoard();
			nonogramClass ngr = new nonogramClass(Convert.ToInt32(txtWidth.Text),
				Convert.ToInt32(txtHeight.Text), newGameBoard);
			_thisGameBoard.Hide();
			ngr.addAddButton();
			ngr.MakeGameFields();
			newGameBoard.ShowDialog();
		}

		private void BtnAdd_Click(object sender, EventArgs e)
		{
			IFormatter formatter = new BinaryFormatter();
			using (var writer = new FileStream("index.txt", FileMode.Open))
			{
				nonogramClass.Index = (int)formatter.Deserialize(writer);

			}
			FillIndices();
			MakeGameFields(this);
			nonogram_object newNonogramObject = new nonogram_object("name", _width, _height, _singleNonogramList);
			string path = "nonogram" + (Index++).ToString();
			using (var writer = new FileStream(path, FileMode.OpenOrCreate))
			{
				formatter.Serialize(writer, newNonogramObject);
			}
			File.Delete("index.txt");
			using (var writer = new FileStream("index.txt", FileMode.Create))
			{
				formatter.Serialize(writer, Index);
			}
		}

		private void FillIndices()
		{

			int horizontalK,
				verticalK,
				leftIndicesIndex,
				topIndicesIndex;
			for (int i = _overallHeight - 1; i >= _topIndicesLegnth; i--)
			{
				leftIndicesIndex = _leftIndicesLength - 1;
				horizontalK = 0;
				for (int j = _width - 1; j >= 0; j--)
				{
					_labels[i][leftIndicesIndex].Text = "";
					if (_pBoxes[i][j].BackColor == Color.Black)
					{
						horizontalK++;
					}

					if (_pBoxes[i][j].BackColor != Color.Black && horizontalK != 0 ||
						j == 0 && horizontalK != 0)
					{
						_labels[i][leftIndicesIndex--].Text = horizontalK.ToString();
						horizontalK = 0;
					}
				}

				while (leftIndicesIndex >= 0)
				{
					_labels[i][leftIndicesIndex--].Text = "0";
				}
			}


			for (int i = _width - 1; i >= 0; i--)
			{
				verticalK = 0;
				topIndicesIndex = _topIndicesLegnth - 1;
				for (int j = _overallHeight - 1; j >= _topIndicesLegnth; j--)
				{
					_labels[topIndicesIndex][i].Text = "";
					if (_pBoxes[j][i].BackColor == Color.Black)
					{
						verticalK++;
					}

					if (verticalK != 0 && _pBoxes[j][i].BackColor != Color.Black ||
						verticalK != 0 && j == _topIndicesLegnth)
					{
						_labels[topIndicesIndex--][i].Text = verticalK.ToString();

						verticalK = 0;
					}
				}

				while (topIndicesIndex >= 0)
				{
					_labels[topIndicesIndex--][i].Text = "0";
				}
			}
			for (int i = 0; i < _overallHeight; i++)
			{
				for (int j = 0; j < _overallWidth; j++)
				{
					if (i < _topIndicesLegnth ^ j < _leftIndicesLength)
					{
						if (i < _topIndicesLegnth && j >= _leftIndicesLength &&
							_labels[i][j - _leftIndicesLength].Text != "0")
							_singleNonogramList.Add(Convert.ToInt32(_labels[i][j - _leftIndicesLength].Text));
						else if (i >= _topIndicesLegnth && j < _leftIndicesLength && _labels[i][j].Text != "0")
							_singleNonogramList.Add(Convert.ToInt32(_labels[i][j].Text));
						else
						{
							_singleNonogramList.Add(0);
						}
					}

					if (i >= _topIndicesLegnth && j >= _leftIndicesLength)
					{
						if (_pBoxes[i][j - _leftIndicesLength].BackColor == Color.Black)
							_singleNonogramList.Add(1);
						else
							_singleNonogramList.Add(0);
					}
				}
			}
			_allNonograms.Add(_singleNonogramList);
		}

		private void PictureBox_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			PictureBox pictureBox = sender as PictureBox;
			if (e.Button == MouseButtons.Left)
			{
				pictureBox.BackgroundImage = Image.FromFile("C:\\Users\\Abstergo\\source\\repos\\nonogram_final_v2.0\\nonogram_final_v2.0\\Resources\\outline_close_black_48dp.png");
				pictureBox.BackColor = Color.WhiteSmoke;
			}
		}

		private void PictureBox_MouseClick(object sender, MouseEventArgs e)
		{
			PictureBox pictureBox = sender as PictureBox;
			if (e.Button == MouseButtons.Left)
			{
				pictureBox.BackgroundImage = null;
				pictureBox.BackColor = Color.Black;
			}
			else if (e.Button == MouseButtons.Right)
			{
				pictureBox.BackgroundImage = Image.FromFile("C:\\Users\\Abstergo\\source\\repos\\nonogram_final_v2.0\\nonogram_final_v2.0\\Resources\\outline_close_black_48dp.png");
				pictureBox.BackColor = Color.WhiteSmoke;
			}

		}


	}
}
[Serializable]
public class nonogram_object
{
	public string _name;
	public int _width;
	public int _height;
	public List<int> _lst;

	public nonogram_object(string name, int width, int height, List<int> lst)
	{
		_name = name;
		_width = width;
		_height = height;
		_lst = lst;
	}
	public nonogram_object() { }
}


