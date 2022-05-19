using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using nonogram_final;

namespace nonogram_final_v2._0
{

	public class NonogramClass
	{
		private NonogramObject                  _checkNonogramObject = null!;
		protected internal static int            Index;
		private readonly int                    _width;
		private readonly int                    _height;
		private readonly int                    _overallHeight;
		private readonly int                    _overallWidth;
		private readonly int                    _topIndicesLegnth;
		private readonly int                    _leftIndicesLength;
		private PictureBox                      _pictureBox         = null!;
		private Label                           _label              = null!;
		private readonly gameBoard              _thisGameBoard;
		private readonly List<List<PictureBox>> _pBoxes             = new();
		private readonly List<List<Label>>      _labels             = new();
		private readonly List<int>              _singleNonogramList = new();
		private readonly Panel                  _mainPanel          = new();



		// ReSharper disable once IdentifierTypo
		public NonogramClass(int width, int height, gameBoard gameBoard)
		{
			gameBoard.StartPosition = FormStartPosition.CenterScreen;
			gameBoard.Controls.Add(_mainPanel);
			_width              = width;
			_height             = height;
			_thisGameBoard      = gameBoard;
			_topIndicesLegnth   = height / 2;
			_leftIndicesLength  = width  / 2;
			if (height % 2 != 0)
				_topIndicesLegnth++;
			if (width % 2 != 0)
				_leftIndicesLength++;
			_overallHeight      = height + _topIndicesLegnth;
			_overallWidth       = width + _leftIndicesLength;
			gameBoard.Size      = new Size(38 * _overallWidth + 80, 
				                           38 * _overallHeight + 80);
			_mainPanel.Location = new Point(30, 30);

		}


		public void MakeGameFields()
		{
			int x = 0, y = 0;
			_mainPanel.Size = new Size(38 *  _overallWidth + 200, 
				                       38 * _overallHeight + 200);
			for (int i = 0; i < _overallHeight; i++)
			{
				List<PictureBox> linePictureBoxes         = new List<PictureBox>();
				List<Label> lineLabels                    = new List<Label>();
				for (int j = 0; j < _overallWidth; j++)
				{
					if (i < _topIndicesLegnth ^ j < _leftIndicesLength)
					{
						_label                            = new Label();
						_mainPanel.Controls.Add(_label);
						_label.Size                       = new Size(38, 38);
						_label.BorderStyle                = BorderStyle.FixedSingle;
						_label.Location                   = new Point(x, y);
						_label.BackColor                  = Color.AliceBlue;
						lineLabels.Add(_label);
					}
					else if (i >= _topIndicesLegnth && j >= _leftIndicesLength)
					{
						_pictureBox                       = new PictureBox();
						_pictureBox.BackgroundImageLayout = ImageLayout.Zoom;
						_mainPanel.Controls.Add(_pictureBox);
						_pictureBox.Size                  = new Size(38, 38);
						_pictureBox.BorderStyle           = BorderStyle.FixedSingle;
						_pictureBox.Location              = new Point(x, y);
						_pictureBox.BackColor             = Color.WhiteSmoke;
						_pictureBox.MouseClick           += PictureBox_MouseClick!;
						_pictureBox.MouseDoubleClick     += PictureBox_MouseDoubleClick!;
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

		[Obsolete("Obsolete")]
		public void MakeGameFields(NonogramObject nObj)
		{
			_checkNonogramObject = nObj;
			int x                           = 0;
			int y                           = 0;
			int curIndex                    = 0;
			Button btnBack                  = new Button();
			_mainPanel.Controls.Add(btnBack);
			btnBack.Size                    = new Size(20, 20);
			IFormatter formatter            = new BinaryFormatter();
			using (var writer               = new FileStream("index.txt", FileMode.Open))
			{
				NonogramClass.Index         = (int)formatter.Deserialize(writer);
			}
			ComboBox cmbBox                 = new ComboBox();
			cmbBox.SelectedIndexChanged    += CmbBox_SelectedIndexChanged;
			for (int i = 0; i < Index; i++)
			{
				cmbBox.Items.Add(i.ToString());
			}
			_mainPanel.Controls.Add(cmbBox);
			cmbBox.Size                     = new Size(40, 10);
			cmbBox.Location                 = new Point(0,20);
			btnBack.Location                = new Point(20, 0);
			btnBack.FlatStyle               = FlatStyle.Popup;
			btnBack.BackgroundImageLayout   = ImageLayout.Stretch;
			btnBack.BackgroundImage         = Image.FromFile("keyboard_backspace_FILL0_wght400_GRAD0_opsz48.png");
			Button btnCheck                 = new Button();
			btnBack.Click                  += BtnBack_Click;
			btnCheck.Click                 += BtnCheck_Click;
			_mainPanel.Controls.Add(btnCheck);
			btnCheck.FlatStyle              = FlatStyle.Popup;
			btnCheck.Location               = new Point(0, 0);
			btnCheck.BackgroundImageLayout  = ImageLayout.Stretch;
			btnCheck.BackgroundImage        = Image.FromFile("done_FILL0_wght400_GRAD0_opsz48.png");
			btnCheck.Size = new Size(20, 20);
			_mainPanel.Size = new Size(38 * _overallWidth + 200, 38 * _overallHeight + 200);
			for (int i = 0; i < _overallHeight; i++)
			{
				List<PictureBox> linePictureBoxes = new List<PictureBox>();
				List<Label> lineLabels            = new List<Label>();
				for (int j = 0; j < _overallWidth; j++)
				{
					if (i < _topIndicesLegnth ^ j < _leftIndicesLength)
					{
						_label = new Label();
						_mainPanel.Controls.Add(_label);
						if (nObj.Lst[curIndex] != 0)
							_label.Text = nObj.Lst[curIndex].ToString();
						else
						{
							_label.Hide();
						}
						_label.Size                = new Size(38, 38);
						_label.BorderStyle         = BorderStyle.FixedSingle;
						_label.Location            = new Point(x, y);
						_label.BackColor           = Color.AliceBlue;
						lineLabels.Add(_label);
						curIndex++;
					}
					else if (i >= _topIndicesLegnth && j >= _leftIndicesLength)
					{
						_pictureBox                       = new PictureBox();
						_pictureBox.BackgroundImageLayout = ImageLayout.Zoom;
						_mainPanel.Controls.Add(_pictureBox);
						_pictureBox.Size                  = new Size(38, 38);
						_pictureBox.BorderStyle           = BorderStyle.FixedSingle;
						_pictureBox.Location              = new Point(x, y);
						_pictureBox.BackColor             = Color.WhiteSmoke;
						_pictureBox.MouseClick           += PictureBox_MouseClick!;
						_pictureBox.MouseDoubleClick     += PictureBox_MouseDoubleClick!;
						linePictureBoxes.Add(_pictureBox);
						curIndex++;
					}
					x += 38;
				}
				x  = 0;
				y += 38;
				_pBoxes.Add(linePictureBoxes);
				_labels.Add(lineLabels);
			}
		}

		private void BtnBack_Click(object? sender, EventArgs e)
		{
			StartForm startForm = new StartForm();
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
						    _labels[i][j - _leftIndicesLength].Text != @"0" && _labels[i][j - _leftIndicesLength].Text != "")
							_singleNonogramList.Add(Convert.ToInt32(_labels[i][j - _leftIndicesLength].Text));
						else if (i >= _topIndicesLegnth && j < _leftIndicesLength && _labels[i][j].Text != @"0" &&  _labels[i][j].Text != "")
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
				if (_singleNonogramList[ind] != _checkNonogramObject.Lst[ind])
					break;
				ind++;
			}

			if (ind == _singleNonogramList.Count)
				MessageBox.Show(@"Правильно!");
			else
				MessageBox.Show(@"Неправильно!");
			_singleNonogramList.Clear();
		}


		[Obsolete("Obsolete")]
		private void CmbBox_SelectedIndexChanged(object? sender, EventArgs e)
		{
			NonogramObject obj;
			ComboBox? cmbBox          = sender as ComboBox;
			XmlSerializer serializer  = new XmlSerializer(typeof(NonogramObject));
			string path = "nonogram"  + cmbBox!.SelectedIndex.ToString();
			using (FileStream fs = new FileStream(path, FileMode.Open))
			{
				obj = (NonogramObject)serializer.Deserialize(fs)!;
				fs.Close();
			}
			_thisGameBoard.Hide();
			gameBoard gameBoard       = new gameBoard();
			NonogramClass ngr         = new NonogramClass(obj.Width, obj.Height, gameBoard);
			ngr.MakeGameFields(obj);
			gameBoard.ShowDialog();
		}

		public void MakeGameFields(NonogramClass ngr)
		{
			for (int i = 0; i < _topIndicesLegnth; i++)
			{
				for (int j = 0; j < _width; j++)
				{
					if (_labels[i][j].Text == @"0")
						_labels[i][j].Hide();
					else
						_labels[i][j].Show();
				}
			}

			for (int i = 0; i < _overallHeight; i++)
			{
				for (int j = 0; j < _leftIndicesLength; j++)
				{
					if (_labels[i][j].Text == @"0")
						_labels[i][j].Hide();
					else
						_labels[i][j].Show();
				}
			}
		}

		private TextBox _txtWidth = null!;
		private TextBox _txtHeight = null!;

		[Obsolete("Obsolete")]
		public void AddAddButton()
		{
			Button btnAdd                 = new Button();
			Button btnUpdate              = new Button();
			btnUpdate.FlatStyle           = FlatStyle.Popup;
			btnAdd.FlatStyle              = FlatStyle.Popup;
			btnUpdate.Size                = new Size(20, 20);
			btnUpdate.Anchor              = AnchorStyles.Left | AnchorStyles.Top;
			btnUpdate.BackgroundImageLayout = ImageLayout.Stretch;
			btnUpdate.Click              += BtnUpdate_Click!;
			btnUpdate.Location = new Point(0, 20);
			btnUpdate.BackgroundImage     = Image.FromFile("update_FILL0_wght400_GRAD0_opsz48.png");
			_mainPanel.Controls.Add(btnUpdate);
			btnAdd.Anchor                 = AnchorStyles.Top | AnchorStyles.Left;
			btnAdd.Size                   = new Size(20, 20);
			btnAdd.Location               = new Point(0, 0);
			btnAdd.Click                 += BtnAdd_Click!;
			btnAdd.BackgroundImage = Image.FromFile("outline_add_black_48dp.png");
			btnAdd.BackgroundImageLayout  = ImageLayout.Stretch;
			Button btnback                = new Button();
			_mainPanel.Controls.Add(btnback);
			btnback.Location              = new Point(btnUpdate.Location.X, btnUpdate.Location.Y + 20);
			btnback.BackgroundImageLayout = ImageLayout.Stretch;
			btnback.FlatStyle             = FlatStyle.Popup;
			btnback.Size                  = new Size(20, 20);
			btnback.BackgroundImage       = Image.FromFile("keyboard_backspace_FILL0_wght400_GRAD0_opsz48.png");
			btnback.Click                += Btnback_Click;
			_txtWidth                      = new TextBox();
			_txtHeight                     = new TextBox();
			_mainPanel.Controls.Add(_txtHeight);
			_txtHeight.Size                = new Size(20, 20);
			_txtWidth.Size                 = new Size(20, 20);
			_txtHeight.Location            = new Point(20, 0);
			_txtWidth.Location             = new Point(20, 20);
			_mainPanel.Controls.Add(_txtWidth);
			_mainPanel.Controls.Add(btnAdd);

		}

		private void Btnback_Click(object? sender, EventArgs e)
		{
			StartForm startForm = new StartForm();
			_thisGameBoard.Hide();
			startForm.ShowDialog();
			_thisGameBoard.Dispose();
		}


		[Obsolete("Obsolete")]
		private void BtnUpdate_Click(object sender, EventArgs e)
		{
			gameBoard newGameBoard = new gameBoard();
			if (_txtWidth.Text == "" || _txtHeight.Text == "")
			{
				_txtHeight.Text = _height.ToString();
				_txtWidth.Text  = _width.ToString();
			}
			NonogramClass ngr      = new NonogramClass(Convert.ToInt32(_txtWidth.Text),
				                                       Convert.ToInt32(_txtHeight.Text), newGameBoard);
			_thisGameBoard.Hide();
			ngr.AddAddButton();
			ngr.MakeGameFields();
			newGameBoard.ShowDialog();
		}


		[Obsolete("Obsolete")]
		private void BtnAdd_Click(object sender, EventArgs e)
		{
			IFormatter formatter              = new BinaryFormatter();
			using (var writer                 = new FileStream("index.txt", FileMode.Open))
			{
				NonogramClass.Index           = (int)formatter.Deserialize(writer);
			}
			FillIndices();
			MakeGameFields(this);
			NonogramObject newNonogramObject = new NonogramObject("name", _width, _height, _singleNonogramList);
			string path = "nonogram" + (Index++).ToString();
			XmlSerializer serializer = new XmlSerializer(typeof(NonogramObject));
			using (var writer = new FileStream(path, FileMode.OpenOrCreate))
			{
				serializer.Serialize(writer, newNonogramObject);
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
					_labels[i][leftIndicesIndex--].Text = @"0";
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
					_labels[topIndicesIndex--][i].Text = @"0";
				}
			}
			for (int i = 0; i < _overallHeight; i++)
			{
				for (int j = 0; j < _overallWidth; j++)
				{
					if (i < _topIndicesLegnth ^ j < _leftIndicesLength)
					{
						if (i < _topIndicesLegnth && j >= _leftIndicesLength &&
							_labels[i][j - _leftIndicesLength].Text != @"0")
							_singleNonogramList.Add(Convert.ToInt32(_labels[i][j - _leftIndicesLength].Text));
						else if (i >= _topIndicesLegnth && j < _leftIndicesLength && _labels[i][j].Text != @"0")
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
			//_allNonograms.Add(_singleNonogramList);
		}

		private void PictureBox_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			PictureBox? pictureBox = sender as PictureBox;
			if (e.Button == MouseButtons.Left)
			{
				pictureBox!.BackgroundImage = Image.FromFile("outline_close_black_48dp.png");
				pictureBox.BackColor = Color.WhiteSmoke;
			}
		}

		private void PictureBox_MouseClick(object sender, MouseEventArgs e)
		{
			PictureBox? pictureBox = sender as PictureBox;
			if (e.Button == MouseButtons.Left)
			{
				pictureBox!.BackgroundImage = null;
				pictureBox.BackColor = Color.Black;
			}
			else if (e.Button == MouseButtons.Right)
			{
				pictureBox!.BackgroundImage = Image.FromFile("outline_close_black_48dp.png");
				pictureBox.BackColor = Color.WhiteSmoke;
			}

		}


	}
}