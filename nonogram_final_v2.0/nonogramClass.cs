using System.Xml.Serialization;
using nonogram_final;

namespace nonogram_final_v2._0
{

	public class NonogramClass
	{
		private NonogramObject                  _checkNonogramObject = null!;
		private readonly int                    _width;
		private readonly int                    _height;
		private readonly int                    _overallHeight;
		private readonly int                    _overallWidth;
		private readonly int                    _topIndicesLegnth;
		private readonly int                    _leftIndicesLength;
		private readonly ComboBox               _cmbBox             = new();
		private PictureBox                      _pictureBox         = null!;
		private Label                           _label              = null!;
		private readonly gameBoard              _thisGameBoard;
		private readonly List<List<PictureBox>> _pBoxes             = new();
		private readonly List<List<Label>>      _labels             = new();
		private readonly List<int>              _singleNonogramList = new();
		private readonly Panel                  _mainPanel          = new();
		private TextBox                         _txtWidth           = null!;
		private TextBox                         _txtHeight          = null!;
		private TextBox                         _name               = null!;



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
			gameBoard.Size      = new Size(800,800);
			_mainPanel.Location = new Point(200,20);

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
						_label.BackColor                  = Color.Aquamarine;
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

		public void MakeGameFields(NonogramObject nObj)
		{
			_mainPanel.Location = _mainPanel.Location with { Y = _mainPanel.Location.Y + 38 * _height / 2 };
			_checkNonogramObject = nObj;
			int x                           = 0;
			int y                           = 0;
			int curIndex                    = 0;
			Button btnBack                  = new Button();
			_thisGameBoard.Controls.Add(btnBack);
			//btnBack.Size                    = new Size(20, 20);
			CmbBoxItems cmbBoxItems;
			_cmbBox.SelectedIndexChanged    += CmbBox_SelectedIndexChanged;
			XmlSerializer serializer = new XmlSerializer(typeof(CmbBoxItems));
			using (var writer = new FileStream("items.xml", FileMode.Open))
			{
				cmbBoxItems = (CmbBoxItems)serializer.Deserialize(writer)!;
			}

			for (int i = 0; i < cmbBoxItems.Items.Count; ++i)
			{
				_cmbBox.Items.Add(cmbBoxItems.Items[i]);
			}
			_thisGameBoard.Controls.Add(_cmbBox);
			_cmbBox.BringToFront();
			_cmbBox.Location                 = new Point(20,20);
			btnBack.Location                = new Point(20, 50);
			btnBack.FlatStyle               = FlatStyle.Popup;
			btnBack.BackgroundImageLayout   = ImageLayout.Stretch;
			btnBack.Text                    = "Назад";
			Button btnCheck                 = new Button();
			btnBack.Click                  += BtnBack_Click;
			btnCheck.Click                 += BtnCheck_Click;
			_thisGameBoard.Controls.Add(btnCheck);
			btnCheck.FlatStyle              = FlatStyle.Popup;
			btnCheck.Location               = new Point(20, 80);
			btnCheck.BackgroundImageLayout  = ImageLayout.Stretch;
			btnCheck.Text                   = "Проверить";
			//btnCheck.Size                   = new Size(20, 20);
			_mainPanel.Size                 = new Size(38 * _overallWidth + 200, 38 * _overallHeight + 200);
			_mainPanel.Location             = new Point(_mainPanel.Location.X - 100, _mainPanel.Location.Y - 80);
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
						_label.BackColor           = Color.Aquamarine;
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
				{
					string ans = "Неправильно!\r\nПроверьте столбец " + (((ind - 2) % _overallHeight - _height/2 + 1));
					MessageBox.Show(ans);
					_singleNonogramList.Clear();
					break;
				}
				ind++;
			}

			if (ind == _singleNonogramList.Count)
				MessageBox.Show(@"Правильно!");
			_singleNonogramList.Clear();
		}


		private void CmbBox_SelectedIndexChanged(object? sender, EventArgs e)
		{
			NonogramObject obj;
			XmlSerializer serializer  = new XmlSerializer(typeof(NonogramObject));
			string path = _cmbBox.SelectedItem.ToString()!;
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


		public void AddAddButton()
		{
			Label lblWidth                  = new Label();
			Label lblHeigth                 = new Label();
			Label lblName                   = new Label();
			_thisGameBoard.Controls.Add(lblWidth);
			_thisGameBoard.Controls.Add(lblHeigth);
			_thisGameBoard.Controls.Add(lblName);
			lblWidth.Text                   = "Ширина";
			lblHeigth.Text                  = "Высота";
			lblName.Text                    = "Название";
			lblWidth.Location               = new Point(20,120);
			lblHeigth.Location              = new Point(20,140);
			lblName.Location                = new Point(20,170);
			Button btnAdd                   = new Button();
			Button btnUpdate                = new Button();
			Button btnback                  = new Button();
			_thisGameBoard.Controls.Add(btnback);
			_thisGameBoard.Controls.Add(btnAdd);
			_thisGameBoard.Controls.Add(btnUpdate);
			btnUpdate.FlatStyle             = FlatStyle.Popup;
			btnAdd.FlatStyle                = FlatStyle.Popup;
			//btnUpdate.Size                  = new Size(20, 20);
			btnUpdate.Anchor                = AnchorStyles.Left | AnchorStyles.Top;
			btnUpdate.BackgroundImageLayout = ImageLayout.Stretch;
			btnUpdate.Click                += BtnUpdate_Click!;
			btnUpdate.Location              = new Point(20, 80);
			btnUpdate.Text                  = "Обновить";
			btnAdd.Anchor                   = AnchorStyles.Top | AnchorStyles.Left;
			//btnAdd.Size                     = new Size(20, 20);
			btnAdd.Location                 = new Point(20, 20);
			btnAdd.Click                   += BtnAdd_Click!;
			btnAdd.Text                     = "Добавить";
			btnAdd.BackgroundImageLayout    = ImageLayout.Stretch;
			btnback.Location                = btnAdd.Location with { Y = btnAdd.Location.Y + 30 };
			btnback.BackgroundImageLayout   = ImageLayout.Stretch;
			btnback.FlatStyle               = FlatStyle.Popup;
			//btnback.Size                    = new Size(20, 20);
			btnback.Text                    = "Назад";
			btnback.Click                  += Btnback_Click;
			_txtWidth                       = new TextBox();
			_txtHeight                      = new TextBox();
			_name                           = new TextBox();
			_txtHeight.Size                 = new Size(30, 20);
			_txtWidth.Size                  = new Size(30, 20);
			_name.Size                      = new Size(50, 20);
			_name.Location                  = lblName.Location with { X = lblName.Location.X + 100, 
																	  Y = lblName.Location.Y - 5 };
			_txtWidth.Location              = lblWidth.Location  with { X = lblWidth.Location.X  + 100 + 20};
			_txtHeight.Location             = lblHeigth.Location with { X = lblHeigth.Location.X + 100 + 20};
			_thisGameBoard.Controls.Add(_txtHeight);
			_thisGameBoard.Controls.Add(_txtWidth);
			_thisGameBoard.Controls.Add(_name);
			_name.BringToFront();
			_txtWidth.BringToFront();
			_txtHeight.BringToFront();
			lblName.BringToFront();
			lblHeigth.BringToFront();
			lblWidth.BringToFront();
			btnback.BringToFront();
			btnAdd.BringToFront();
			btnUpdate.BringToFront();

		}

		private void Btnback_Click(object? sender, EventArgs e)
		{
			StartForm startForm = new StartForm();
			_thisGameBoard.Hide();
			startForm.ShowDialog();
			_thisGameBoard.Dispose();
		}


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


		private void BtnAdd_Click(object sender, EventArgs e)
		{

			CmbBoxItems cmbBoxItems = new CmbBoxItems();
			XmlSerializer serializer = new XmlSerializer(typeof(CmbBoxItems));
			if (File.Exists("items.xml"))
			{
				using (var writer = new FileStream("items.xml", FileMode.Open))
				{
					cmbBoxItems = (CmbBoxItems)serializer.Deserialize(writer)!;
				}
				File.Delete("items.xml");
			}

			if (cmbBoxItems.Items.Contains(_name.Text))
			{
				MessageBox.Show("Кроссворд с таким названием существует! Выберите другое название!");
				return;
			}
			FillIndices();
			MakeGameFields(this);
			_cmbBox.Items.Add(_name.Text);
			cmbBoxItems.Items.Add(_name.Text);
			using (var writer = new FileStream("items.xml", FileMode.Create))
			{
				serializer.Serialize(writer, cmbBoxItems);
			}
			NonogramObject newNonogramObject = new NonogramObject(_name.Text, _width, _height, _singleNonogramList);
			serializer = new XmlSerializer(typeof(NonogramObject));
			using (var writer = new FileStream(_name.Text, FileMode.CreateNew))
			{
				serializer.Serialize(writer, newNonogramObject);
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
		}

		private void PictureBox_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			PictureBox? pictureBox          = sender as PictureBox;
			pictureBox!.BackgroundImage     = null;
			pictureBox.BackColor            = Color.WhiteSmoke;
		}

		private void PictureBox_MouseClick(object sender, MouseEventArgs e)
		{
			PictureBox? pictureBox = sender as PictureBox;
			if (e.Button == MouseButtons.Right)
			{
				pictureBox!.BackgroundImage = Image.FromFile("outline_close_black_48dp.png");
				pictureBox.BackColor        = Color.WhiteSmoke;
			}
			else if (e.Button == MouseButtons.Left)
			{
				pictureBox!.BackgroundImage = null;
				pictureBox.BackColor        = Color.Black;
			}
		}

		[Serializable]
	
		public class CmbBoxItems
		{
			public CmbBoxItems (){}

			public List<string> Items = new();
		}
	}
}