namespace Better_Nike_Bot
{
	// Token: 0x02000008 RID: 8
	public partial class CheckoutProfile : global::System.Windows.Forms.Form
	{
		// Token: 0x06000035 RID: 53 RVA: 0x00002221 File Offset: 0x00000421
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00005748 File Offset: 0x00003948
		private void InitializeComponent()
		{
			this.textBoxProfileName = new global::System.Windows.Forms.TextBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.label2 = new global::System.Windows.Forms.Label();
			this.textBoxBillingFirstName = new global::System.Windows.Forms.TextBox();
			this.label3 = new global::System.Windows.Forms.Label();
			this.textBoxBillingLastName = new global::System.Windows.Forms.TextBox();
			this.label4 = new global::System.Windows.Forms.Label();
			this.textBoxBillingAddress1 = new global::System.Windows.Forms.TextBox();
			this.label5 = new global::System.Windows.Forms.Label();
			this.textBoxBillingAddress2 = new global::System.Windows.Forms.TextBox();
			this.label6 = new global::System.Windows.Forms.Label();
			this.textBoxBillingZipCode = new global::System.Windows.Forms.TextBox();
			this.label7 = new global::System.Windows.Forms.Label();
			this.textBoxBillingCity = new global::System.Windows.Forms.TextBox();
			this.label8 = new global::System.Windows.Forms.Label();
			this.label10 = new global::System.Windows.Forms.Label();
			this.textBoxBillingPhone = new global::System.Windows.Forms.TextBox();
			this.groupBox1 = new global::System.Windows.Forms.GroupBox();
			this.comboBoxBillingStateJP = new global::System.Windows.Forms.ComboBox();
			this.label13 = new global::System.Windows.Forms.Label();
			this.comboBoxBillingState = new global::System.Windows.Forms.ComboBox();
			this.checkBoxShippingSame = new global::System.Windows.Forms.CheckBox();
			this.groupBox2 = new global::System.Windows.Forms.GroupBox();
			this.comboBoxShippingStateJP = new global::System.Windows.Forms.ComboBox();
			this.label24 = new global::System.Windows.Forms.Label();
			this.comboBoxShippingState = new global::System.Windows.Forms.ComboBox();
			this.label11 = new global::System.Windows.Forms.Label();
			this.label12 = new global::System.Windows.Forms.Label();
			this.textBoxShippingFirst = new global::System.Windows.Forms.TextBox();
			this.textBoxShippingPhone = new global::System.Windows.Forms.TextBox();
			this.textBoxShippingLast = new global::System.Windows.Forms.TextBox();
			this.label14 = new global::System.Windows.Forms.Label();
			this.textBoxShippingAddress1 = new global::System.Windows.Forms.TextBox();
			this.label15 = new global::System.Windows.Forms.Label();
			this.label16 = new global::System.Windows.Forms.Label();
			this.textBoxShippingAddress2 = new global::System.Windows.Forms.TextBox();
			this.label17 = new global::System.Windows.Forms.Label();
			this.label18 = new global::System.Windows.Forms.Label();
			this.textBoxShippingCity = new global::System.Windows.Forms.TextBox();
			this.textBoxShippingPostalCode = new global::System.Windows.Forms.TextBox();
			this.label19 = new global::System.Windows.Forms.Label();
			this.groupBox3 = new global::System.Windows.Forms.GroupBox();
			this.label23 = new global::System.Windows.Forms.Label();
			this.textBoxCvv = new global::System.Windows.Forms.TextBox();
			this.comboBoxExpirationYear = new global::System.Windows.Forms.ComboBox();
			this.comboBoxExpirationMonth = new global::System.Windows.Forms.ComboBox();
			this.label22 = new global::System.Windows.Forms.Label();
			this.label21 = new global::System.Windows.Forms.Label();
			this.textBoxCardNumber = new global::System.Windows.Forms.TextBox();
			this.comboBoxCardType = new global::System.Windows.Forms.ComboBox();
			this.label20 = new global::System.Windows.Forms.Label();
			this.buttonSave = new global::System.Windows.Forms.Button();
			this.label26 = new global::System.Windows.Forms.Label();
			this.label27 = new global::System.Windows.Forms.Label();
			this.textBoxMaxCheckouts = new global::System.Windows.Forms.TextBox();
			this.label9 = new global::System.Windows.Forms.Label();
			this.textBoxBillingAddress3 = new global::System.Windows.Forms.TextBox();
			this.label25 = new global::System.Windows.Forms.Label();
			this.textBoxShippingAddress3 = new global::System.Windows.Forms.TextBox();
			this.label28 = new global::System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			base.SuspendLayout();
			this.textBoxProfileName.Location = new global::System.Drawing.Point(88, 12);
			this.textBoxProfileName.Name = "textBoxProfileName";
			this.textBoxProfileName.Size = new global::System.Drawing.Size(133, 20);
			this.textBoxProfileName.TabIndex = 0;
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(12, 15);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(70, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Profile Name:";
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(18, 27);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(60, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "First Name:";
			this.textBoxBillingFirstName.Location = new global::System.Drawing.Point(99, 24);
			this.textBoxBillingFirstName.Name = "textBoxBillingFirstName";
			this.textBoxBillingFirstName.Size = new global::System.Drawing.Size(133, 20);
			this.textBoxBillingFirstName.TabIndex = 2;
			this.label3.AutoSize = true;
			this.label3.Location = new global::System.Drawing.Point(17, 53);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(61, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Last Name:";
			this.textBoxBillingLastName.Location = new global::System.Drawing.Point(99, 50);
			this.textBoxBillingLastName.Name = "textBoxBillingLastName";
			this.textBoxBillingLastName.Size = new global::System.Drawing.Size(133, 20);
			this.textBoxBillingLastName.TabIndex = 4;
			this.label4.AutoSize = true;
			this.label4.Location = new global::System.Drawing.Point(21, 79);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(57, 13);
			this.label4.TabIndex = 7;
			this.label4.Text = "Address 1:";
			this.textBoxBillingAddress1.Location = new global::System.Drawing.Point(99, 76);
			this.textBoxBillingAddress1.Name = "textBoxBillingAddress1";
			this.textBoxBillingAddress1.Size = new global::System.Drawing.Size(133, 20);
			this.textBoxBillingAddress1.TabIndex = 6;
			this.label5.AutoSize = true;
			this.label5.Location = new global::System.Drawing.Point(21, 105);
			this.label5.Name = "label5";
			this.label5.Size = new global::System.Drawing.Size(57, 13);
			this.label5.TabIndex = 9;
			this.label5.Text = "Address 2:";
			this.textBoxBillingAddress2.Location = new global::System.Drawing.Point(99, 102);
			this.textBoxBillingAddress2.Name = "textBoxBillingAddress2";
			this.textBoxBillingAddress2.Size = new global::System.Drawing.Size(133, 20);
			this.textBoxBillingAddress2.TabIndex = 8;
			this.label6.AutoSize = true;
			this.label6.Location = new global::System.Drawing.Point(11, 131);
			this.label6.Name = "label6";
			this.label6.Size = new global::System.Drawing.Size(67, 13);
			this.label6.TabIndex = 11;
			this.label6.Text = "Postal Code:";
			this.textBoxBillingZipCode.Location = new global::System.Drawing.Point(99, 128);
			this.textBoxBillingZipCode.Name = "textBoxBillingZipCode";
			this.textBoxBillingZipCode.Size = new global::System.Drawing.Size(133, 20);
			this.textBoxBillingZipCode.TabIndex = 10;
			this.label7.AutoSize = true;
			this.label7.Location = new global::System.Drawing.Point(51, 183);
			this.label7.Name = "label7";
			this.label7.Size = new global::System.Drawing.Size(27, 13);
			this.label7.TabIndex = 13;
			this.label7.Text = "City:";
			this.textBoxBillingCity.Location = new global::System.Drawing.Point(99, 180);
			this.textBoxBillingCity.Name = "textBoxBillingCity";
			this.textBoxBillingCity.Size = new global::System.Drawing.Size(133, 20);
			this.textBoxBillingCity.TabIndex = 12;
			this.label8.AutoSize = true;
			this.label8.Location = new global::System.Drawing.Point(25, 209);
			this.label8.Name = "label8";
			this.label8.Size = new global::System.Drawing.Size(53, 13);
			this.label8.TabIndex = 15;
			this.label8.Text = "US State:";
			this.label10.AutoSize = true;
			this.label10.Location = new global::System.Drawing.Point(37, 157);
			this.label10.Name = "label10";
			this.label10.Size = new global::System.Drawing.Size(41, 13);
			this.label10.TabIndex = 19;
			this.label10.Text = "Phone:";
			this.textBoxBillingPhone.Location = new global::System.Drawing.Point(99, 154);
			this.textBoxBillingPhone.Name = "textBoxBillingPhone";
			this.textBoxBillingPhone.Size = new global::System.Drawing.Size(133, 20);
			this.textBoxBillingPhone.TabIndex = 18;
			this.groupBox1.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.groupBox1.Controls.Add(this.textBoxBillingAddress3);
			this.groupBox1.Controls.Add(this.label25);
			this.groupBox1.Controls.Add(this.comboBoxBillingStateJP);
			this.groupBox1.Controls.Add(this.label13);
			this.groupBox1.Controls.Add(this.comboBoxBillingState);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label10);
			this.groupBox1.Controls.Add(this.textBoxBillingFirstName);
			this.groupBox1.Controls.Add(this.textBoxBillingPhone);
			this.groupBox1.Controls.Add(this.textBoxBillingLastName);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.textBoxBillingAddress1);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.textBoxBillingAddress2);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.textBoxBillingCity);
			this.groupBox1.Controls.Add(this.textBoxBillingZipCode);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Location = new global::System.Drawing.Point(12, 74);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new global::System.Drawing.Size(247, 294);
			this.groupBox1.TabIndex = 20;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Billing Address";
			this.comboBoxBillingStateJP.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxBillingStateJP.FormattingEnabled = true;
			this.comboBoxBillingStateJP.Items.AddRange(new object[]
			{
				"Pick State",
				"北海道 - JP-01",
				"青森県 - JP-02",
				"岩手県 - JP-03",
				"宮城県 - JP-04",
				"秋田県 - JP-05",
				"山形県 - JP-06",
				"福島県 - JP-07",
				"茨城県 - JP-08",
				"栃木県 - JP-09",
				"群馬県 - JP-10",
				"埼玉県 - JP-11",
				"千葉県 - JP-12",
				"東京都 - JP-13",
				"神奈川県 - JP-14",
				"新潟県 - JP-15",
				"富山県 - JP-16",
				"石川県 - JP-17",
				"福井県 - JP-18",
				"山梨県 - JP-19",
				"長野県 - JP-20",
				"岐阜県 - JP-21",
				"静岡県 - JP-22",
				"愛知県 - JP-23",
				"三重県 - JP-24",
				"滋賀県 - JP-25",
				"京都府 - JP-26",
				"大阪府 - JP-27",
				"兵庫県 - JP-28",
				"奈良県 - JP-29",
				"和歌山県 - JP-30",
				"鳥取県 - JP-31",
				"島根県 - JP-32",
				"岡山県 - JP-33",
				"広島県 - JP-34",
				"山口県 - JP-35",
				"徳島県 - JP-36",
				"香川県 - JP-37",
				"愛媛県 - JP-38",
				"高知県 - JP-39",
				"福岡県 - JP-40",
				"佐賀県 - JP-41",
				"長崎県 - JP-42",
				"熊本県 - JP-43",
				"大分県 - JP-44",
				"宮崎県 - JP-45",
				"鹿児島県 - JP-46",
				"沖縄県 - JP-47"
			});
			this.comboBoxBillingStateJP.Location = new global::System.Drawing.Point(99, 232);
			this.comboBoxBillingStateJP.Name = "comboBoxBillingStateJP";
			this.comboBoxBillingStateJP.Size = new global::System.Drawing.Size(133, 21);
			this.comboBoxBillingStateJP.TabIndex = 23;
			this.label13.AutoSize = true;
			this.label13.Location = new global::System.Drawing.Point(25, 236);
			this.label13.Name = "label13";
			this.label13.Size = new global::System.Drawing.Size(50, 13);
			this.label13.TabIndex = 22;
			this.label13.Text = "JP State:";
			this.comboBoxBillingState.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxBillingState.FormattingEnabled = true;
			this.comboBoxBillingState.Items.AddRange(new object[]
			{
				"Pick State",
				"Alabama - AL",
				"Alaska - AK",
				"Arizona - AZ",
				"Arkansas - AR",
				"California - CA",
				"Colorado - CO",
				"Connecticut - CT",
				"Delaware - DE",
				"District of Columbia - DC",
				"Florida - FL",
				"Georgia - GA",
				"Hawaii - HI",
				"Idaho - ID",
				"Illinois - IL",
				"Indiana - IN",
				"Iowa - IA",
				"Kansas - KS",
				"Kentucky - KY",
				"Louisiana - LA",
				"Maine - ME",
				"Maryland - MD",
				"Massachusetts - MA",
				"Michigan - MI",
				"Minnesota - MN",
				"Mississippi - MS",
				"Missouri - MO",
				"Montana - MT",
				"Nebraska - NE",
				"Nevada - NV",
				"New Hampshire - NH",
				"New Jersey - NJ",
				"New Mexico - NM",
				"New York - NY",
				"North Carolina - NC",
				"North Dakota - ND",
				"Ohio - OH",
				"Oklahoma - OK",
				"Oregon - OR",
				"Pennsylvania - PA",
				"Rhode Island - RI",
				"South Carolina - SC",
				"South Dakota - SD",
				"Tennessee - TN",
				"Texas - TX",
				"Utah - UT",
				"Vermont - VT",
				"Virginia - VA",
				"Washington - WA",
				"West Virginia - WV",
				"Wisconsin - WI",
				"Wyoming - WY"
			});
			this.comboBoxBillingState.Location = new global::System.Drawing.Point(99, 205);
			this.comboBoxBillingState.Name = "comboBoxBillingState";
			this.comboBoxBillingState.Size = new global::System.Drawing.Size(133, 21);
			this.comboBoxBillingState.TabIndex = 21;
			this.checkBoxShippingSame.AutoSize = true;
			this.checkBoxShippingSame.Location = new global::System.Drawing.Point(281, 51);
			this.checkBoxShippingSame.Name = "checkBoxShippingSame";
			this.checkBoxShippingSame.Size = new global::System.Drawing.Size(178, 17);
			this.checkBoxShippingSame.TabIndex = 22;
			this.checkBoxShippingSame.Text = "Shipping address same as billing";
			this.checkBoxShippingSame.UseVisualStyleBackColor = true;
			this.groupBox2.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.groupBox2.Controls.Add(this.textBoxShippingAddress3);
			this.groupBox2.Controls.Add(this.label28);
			this.groupBox2.Controls.Add(this.comboBoxShippingStateJP);
			this.groupBox2.Controls.Add(this.label24);
			this.groupBox2.Controls.Add(this.comboBoxShippingState);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.Controls.Add(this.textBoxShippingFirst);
			this.groupBox2.Controls.Add(this.textBoxShippingPhone);
			this.groupBox2.Controls.Add(this.textBoxShippingLast);
			this.groupBox2.Controls.Add(this.label14);
			this.groupBox2.Controls.Add(this.textBoxShippingAddress1);
			this.groupBox2.Controls.Add(this.label15);
			this.groupBox2.Controls.Add(this.label16);
			this.groupBox2.Controls.Add(this.textBoxShippingAddress2);
			this.groupBox2.Controls.Add(this.label17);
			this.groupBox2.Controls.Add(this.label18);
			this.groupBox2.Controls.Add(this.textBoxShippingCity);
			this.groupBox2.Controls.Add(this.textBoxShippingPostalCode);
			this.groupBox2.Controls.Add(this.label19);
			this.groupBox2.Location = new global::System.Drawing.Point(281, 74);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new global::System.Drawing.Size(247, 294);
			this.groupBox2.TabIndex = 23;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Shipping Address";
			this.comboBoxShippingStateJP.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxShippingStateJP.FormattingEnabled = true;
			this.comboBoxShippingStateJP.Items.AddRange(new object[]
			{
				"Pick State",
				"北海道 - JP-01",
				"青森県 - JP-02",
				"岩手県 - JP-03",
				"宮城県 - JP-04",
				"秋田県 - JP-05",
				"山形県 - JP-06",
				"福島県 - JP-07",
				"茨城県 - JP-08",
				"栃木県 - JP-09",
				"群馬県 - JP-10",
				"埼玉県 - JP-11",
				"千葉県 - JP-12",
				"東京都 - JP-13",
				"神奈川県 - JP-14",
				"新潟県 - JP-15",
				"富山県 - JP-16",
				"石川県 - JP-17",
				"福井県 - JP-18",
				"山梨県 - JP-19",
				"長野県 - JP-20",
				"岐阜県 - JP-21",
				"静岡県 - JP-22",
				"愛知県 - JP-23",
				"三重県 - JP-24",
				"滋賀県 - JP-25",
				"京都府 - JP-26",
				"大阪府 - JP-27",
				"兵庫県 - JP-28",
				"奈良県 - JP-29",
				"和歌山県 - JP-30",
				"鳥取県 - JP-31",
				"島根県 - JP-32",
				"岡山県 - JP-33",
				"広島県 - JP-34",
				"山口県 - JP-35",
				"徳島県 - JP-36",
				"香川県 - JP-37",
				"愛媛県 - JP-38",
				"高知県 - JP-39",
				"福岡県 - JP-40",
				"佐賀県 - JP-41",
				"長崎県 - JP-42",
				"熊本県 - JP-43",
				"大分県 - JP-44",
				"宮崎県 - JP-45",
				"鹿児島県 - JP-46",
				"沖縄県 - JP-47"
			});
			this.comboBoxShippingStateJP.Location = new global::System.Drawing.Point(99, 232);
			this.comboBoxShippingStateJP.Name = "comboBoxShippingStateJP";
			this.comboBoxShippingStateJP.Size = new global::System.Drawing.Size(133, 21);
			this.comboBoxShippingStateJP.TabIndex = 25;
			this.label24.AutoSize = true;
			this.label24.Location = new global::System.Drawing.Point(25, 236);
			this.label24.Name = "label24";
			this.label24.Size = new global::System.Drawing.Size(50, 13);
			this.label24.TabIndex = 24;
			this.label24.Text = "JP State:";
			this.comboBoxShippingState.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxShippingState.FormattingEnabled = true;
			this.comboBoxShippingState.Items.AddRange(new object[]
			{
				"Pick State",
				"Alabama - AL",
				"Alaska - AK",
				"Arizona - AZ",
				"Arkansas - AR",
				"California - CA",
				"Colorado - CO",
				"Connecticut - CT",
				"Delaware - DE",
				"District of Columbia - DC",
				"Florida - FL",
				"Georgia - GA",
				"Hawaii - HI",
				"Idaho - ID",
				"Illinois - IL",
				"Indiana - IN",
				"Iowa - IA",
				"Kansas - KS",
				"Kentucky - KY",
				"Louisiana - LA",
				"Maine - ME",
				"Maryland - MD",
				"Massachusetts - MA",
				"Michigan - MI",
				"Minnesota - MN",
				"Mississippi - MS",
				"Missouri - MO",
				"Montana - MT",
				"Nebraska - NE",
				"Nevada - NV",
				"New Hampshire - NH",
				"New Jersey - NJ",
				"New Mexico - NM",
				"New York - NY",
				"North Carolina - NC",
				"North Dakota - ND",
				"Ohio - OH",
				"Oklahoma - OK",
				"Oregon - OR",
				"Pennsylvania - PA",
				"Rhode Island - RI",
				"South Carolina - SC",
				"South Dakota - SD",
				"Tennessee - TN",
				"Texas - TX",
				"Utah - UT",
				"Vermont - VT",
				"Virginia - VA",
				"Washington - WA",
				"West Virginia - WV",
				"Wisconsin - WI",
				"Wyoming - WY"
			});
			this.comboBoxShippingState.Location = new global::System.Drawing.Point(99, 205);
			this.comboBoxShippingState.Name = "comboBoxShippingState";
			this.comboBoxShippingState.Size = new global::System.Drawing.Size(133, 21);
			this.comboBoxShippingState.TabIndex = 21;
			this.label11.AutoSize = true;
			this.label11.Location = new global::System.Drawing.Point(18, 27);
			this.label11.Name = "label11";
			this.label11.Size = new global::System.Drawing.Size(60, 13);
			this.label11.TabIndex = 3;
			this.label11.Text = "First Name:";
			this.label12.AutoSize = true;
			this.label12.Location = new global::System.Drawing.Point(37, 157);
			this.label12.Name = "label12";
			this.label12.Size = new global::System.Drawing.Size(41, 13);
			this.label12.TabIndex = 19;
			this.label12.Text = "Phone:";
			this.textBoxShippingFirst.Location = new global::System.Drawing.Point(99, 24);
			this.textBoxShippingFirst.Name = "textBoxShippingFirst";
			this.textBoxShippingFirst.Size = new global::System.Drawing.Size(133, 20);
			this.textBoxShippingFirst.TabIndex = 2;
			this.textBoxShippingPhone.Location = new global::System.Drawing.Point(99, 154);
			this.textBoxShippingPhone.Name = "textBoxShippingPhone";
			this.textBoxShippingPhone.Size = new global::System.Drawing.Size(133, 20);
			this.textBoxShippingPhone.TabIndex = 18;
			this.textBoxShippingLast.Location = new global::System.Drawing.Point(99, 50);
			this.textBoxShippingLast.Name = "textBoxShippingLast";
			this.textBoxShippingLast.Size = new global::System.Drawing.Size(133, 20);
			this.textBoxShippingLast.TabIndex = 4;
			this.label14.AutoSize = true;
			this.label14.Location = new global::System.Drawing.Point(17, 53);
			this.label14.Name = "label14";
			this.label14.Size = new global::System.Drawing.Size(61, 13);
			this.label14.TabIndex = 5;
			this.label14.Text = "Last Name:";
			this.textBoxShippingAddress1.Location = new global::System.Drawing.Point(99, 76);
			this.textBoxShippingAddress1.Name = "textBoxShippingAddress1";
			this.textBoxShippingAddress1.Size = new global::System.Drawing.Size(133, 20);
			this.textBoxShippingAddress1.TabIndex = 6;
			this.label15.AutoSize = true;
			this.label15.Location = new global::System.Drawing.Point(25, 209);
			this.label15.Name = "label15";
			this.label15.Size = new global::System.Drawing.Size(53, 13);
			this.label15.TabIndex = 15;
			this.label15.Text = "US State:";
			this.label16.AutoSize = true;
			this.label16.Location = new global::System.Drawing.Point(21, 79);
			this.label16.Name = "label16";
			this.label16.Size = new global::System.Drawing.Size(57, 13);
			this.label16.TabIndex = 7;
			this.label16.Text = "Address 1:";
			this.textBoxShippingAddress2.Location = new global::System.Drawing.Point(99, 102);
			this.textBoxShippingAddress2.Name = "textBoxShippingAddress2";
			this.textBoxShippingAddress2.Size = new global::System.Drawing.Size(133, 20);
			this.textBoxShippingAddress2.TabIndex = 8;
			this.label17.AutoSize = true;
			this.label17.Location = new global::System.Drawing.Point(51, 183);
			this.label17.Name = "label17";
			this.label17.Size = new global::System.Drawing.Size(27, 13);
			this.label17.TabIndex = 13;
			this.label17.Text = "City:";
			this.label18.AutoSize = true;
			this.label18.Location = new global::System.Drawing.Point(21, 105);
			this.label18.Name = "label18";
			this.label18.Size = new global::System.Drawing.Size(57, 13);
			this.label18.TabIndex = 9;
			this.label18.Text = "Address 2:";
			this.textBoxShippingCity.Location = new global::System.Drawing.Point(99, 180);
			this.textBoxShippingCity.Name = "textBoxShippingCity";
			this.textBoxShippingCity.Size = new global::System.Drawing.Size(133, 20);
			this.textBoxShippingCity.TabIndex = 12;
			this.textBoxShippingPostalCode.Location = new global::System.Drawing.Point(99, 128);
			this.textBoxShippingPostalCode.Name = "textBoxShippingPostalCode";
			this.textBoxShippingPostalCode.Size = new global::System.Drawing.Size(133, 20);
			this.textBoxShippingPostalCode.TabIndex = 10;
			this.label19.AutoSize = true;
			this.label19.Location = new global::System.Drawing.Point(11, 131);
			this.label19.Name = "label19";
			this.label19.Size = new global::System.Drawing.Size(67, 13);
			this.label19.TabIndex = 11;
			this.label19.Text = "Postal Code:";
			this.groupBox3.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.groupBox3.Controls.Add(this.label23);
			this.groupBox3.Controls.Add(this.textBoxCvv);
			this.groupBox3.Controls.Add(this.comboBoxExpirationYear);
			this.groupBox3.Controls.Add(this.comboBoxExpirationMonth);
			this.groupBox3.Controls.Add(this.label22);
			this.groupBox3.Controls.Add(this.label21);
			this.groupBox3.Controls.Add(this.textBoxCardNumber);
			this.groupBox3.Controls.Add(this.comboBoxCardType);
			this.groupBox3.Controls.Add(this.label20);
			this.groupBox3.Location = new global::System.Drawing.Point(12, 400);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new global::System.Drawing.Size(516, 103);
			this.groupBox3.TabIndex = 24;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Card Details";
			this.label23.AutoSize = true;
			this.label23.Location = new global::System.Drawing.Point(256, 61);
			this.label23.Name = "label23";
			this.label23.Size = new global::System.Drawing.Size(31, 13);
			this.label23.TabIndex = 29;
			this.label23.Text = "CVV:";
			this.textBoxCvv.Location = new global::System.Drawing.Point(293, 57);
			this.textBoxCvv.Name = "textBoxCvv";
			this.textBoxCvv.Size = new global::System.Drawing.Size(54, 20);
			this.textBoxCvv.TabIndex = 28;
			this.comboBoxExpirationYear.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxExpirationYear.FormattingEnabled = true;
			this.comboBoxExpirationYear.Items.AddRange(new object[]
			{
				"2015",
				"2016",
				"2017",
				"2018",
				"2019",
				"2020",
				"2021",
				"2022",
				"2023",
				"2024",
				"2025",
				"2026",
				"2027",
				"2028",
				"2029",
				"2030",
				"2031",
				"2032",
				"2033",
				"2034",
				"2035",
				"2036",
				"2037",
				"2038",
				"2039",
				"2040"
			});
			this.comboBoxExpirationYear.Location = new global::System.Drawing.Point(129, 57);
			this.comboBoxExpirationYear.Name = "comboBoxExpirationYear";
			this.comboBoxExpirationYear.Size = new global::System.Drawing.Size(76, 21);
			this.comboBoxExpirationYear.TabIndex = 27;
			this.comboBoxExpirationMonth.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxExpirationMonth.FormattingEnabled = true;
			this.comboBoxExpirationMonth.Items.AddRange(new object[]
			{
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12"
			});
			this.comboBoxExpirationMonth.Location = new global::System.Drawing.Point(72, 57);
			this.comboBoxExpirationMonth.Name = "comboBoxExpirationMonth";
			this.comboBoxExpirationMonth.Size = new global::System.Drawing.Size(41, 21);
			this.comboBoxExpirationMonth.TabIndex = 26;
			this.label22.AutoSize = true;
			this.label22.Location = new global::System.Drawing.Point(10, 60);
			this.label22.Name = "label22";
			this.label22.Size = new global::System.Drawing.Size(56, 13);
			this.label22.TabIndex = 25;
			this.label22.Text = "Expiration:";
			this.label21.AutoSize = true;
			this.label21.Location = new global::System.Drawing.Point(215, 26);
			this.label21.Name = "label21";
			this.label21.Size = new global::System.Drawing.Size(72, 13);
			this.label21.TabIndex = 24;
			this.label21.Text = "Card Number:";
			this.textBoxCardNumber.Location = new global::System.Drawing.Point(292, 23);
			this.textBoxCardNumber.Name = "textBoxCardNumber";
			this.textBoxCardNumber.Size = new global::System.Drawing.Size(218, 20);
			this.textBoxCardNumber.TabIndex = 23;
			this.comboBoxCardType.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxCardType.FormattingEnabled = true;
			this.comboBoxCardType.Items.AddRange(new object[]
			{
				"Visa",
				"MasterCard",
				"AmericanExpress",
				"Discover"
			});
			this.comboBoxCardType.Location = new global::System.Drawing.Point(72, 22);
			this.comboBoxCardType.Name = "comboBoxCardType";
			this.comboBoxCardType.Size = new global::System.Drawing.Size(133, 21);
			this.comboBoxCardType.TabIndex = 22;
			this.label20.AutoSize = true;
			this.label20.Location = new global::System.Drawing.Point(11, 26);
			this.label20.Name = "label20";
			this.label20.Size = new global::System.Drawing.Size(34, 13);
			this.label20.TabIndex = 21;
			this.label20.Text = "Type:";
			this.buttonSave.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.buttonSave.Location = new global::System.Drawing.Point(456, 510);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new global::System.Drawing.Size(75, 23);
			this.buttonSave.TabIndex = 25;
			this.buttonSave.Text = "Save";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new global::System.EventHandler(this.buttonSave_Click);
			this.label26.AutoSize = true;
			this.label26.Location = new global::System.Drawing.Point(227, 15);
			this.label26.Name = "label26";
			this.label26.Size = new global::System.Drawing.Size(185, 13);
			this.label26.TabIndex = 27;
			this.label26.Text = "(Change name to save as new profile)";
			this.label26.Visible = false;
			this.label27.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.label27.AutoSize = true;
			this.label27.Location = new global::System.Drawing.Point(9, 518);
			this.label27.Name = "label27";
			this.label27.Size = new global::System.Drawing.Size(216, 13);
			this.label27.TabIndex = 31;
			this.label27.Text = "Max Checkouts (Per Session, 0 = Unlimited):";
			this.textBoxMaxCheckouts.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.textBoxMaxCheckouts.Location = new global::System.Drawing.Point(231, 515);
			this.textBoxMaxCheckouts.Name = "textBoxMaxCheckouts";
			this.textBoxMaxCheckouts.Size = new global::System.Drawing.Size(45, 20);
			this.textBoxMaxCheckouts.TabIndex = 30;
			this.textBoxMaxCheckouts.Text = "0";
			this.label9.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.label9.AutoSize = true;
			this.label9.Location = new global::System.Drawing.Point(16, 375);
			this.label9.Name = "label9";
			this.label9.Size = new global::System.Drawing.Size(345, 13);
			this.label9.TabIndex = 32;
			this.label9.Text = "***Country will be automatically selected based on chosen nike location.";
			this.textBoxBillingAddress3.Location = new global::System.Drawing.Point(99, 259);
			this.textBoxBillingAddress3.Name = "textBoxBillingAddress3";
			this.textBoxBillingAddress3.Size = new global::System.Drawing.Size(133, 20);
			this.textBoxBillingAddress3.TabIndex = 24;
			this.label25.AutoSize = true;
			this.label25.Location = new global::System.Drawing.Point(21, 262);
			this.label25.Name = "label25";
			this.label25.Size = new global::System.Drawing.Size(49, 13);
			this.label25.TabIndex = 25;
			this.label25.Text = "JP 町域:";
			this.textBoxShippingAddress3.Location = new global::System.Drawing.Point(99, 262);
			this.textBoxShippingAddress3.Name = "textBoxShippingAddress3";
			this.textBoxShippingAddress3.Size = new global::System.Drawing.Size(133, 20);
			this.textBoxShippingAddress3.TabIndex = 26;
			this.label28.AutoSize = true;
			this.label28.Location = new global::System.Drawing.Point(26, 265);
			this.label28.Name = "label28";
			this.label28.Size = new global::System.Drawing.Size(49, 13);
			this.label28.TabIndex = 27;
			this.label28.Text = "JP 町域:";
			base.AcceptButton = this.buttonSave;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(543, 545);
			base.Controls.Add(this.label9);
			base.Controls.Add(this.label27);
			base.Controls.Add(this.textBoxMaxCheckouts);
			base.Controls.Add(this.label26);
			base.Controls.Add(this.buttonSave);
			base.Controls.Add(this.groupBox3);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.checkBoxShippingSame);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.textBoxProfileName);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "CheckoutProfile";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "CC Profile";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000023 RID: 35
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000024 RID: 36
		private global::System.Windows.Forms.TextBox textBoxProfileName;

		// Token: 0x04000025 RID: 37
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000026 RID: 38
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000027 RID: 39
		private global::System.Windows.Forms.TextBox textBoxBillingFirstName;

		// Token: 0x04000028 RID: 40
		private global::System.Windows.Forms.Label label3;

		// Token: 0x04000029 RID: 41
		private global::System.Windows.Forms.TextBox textBoxBillingLastName;

		// Token: 0x0400002A RID: 42
		private global::System.Windows.Forms.Label label4;

		// Token: 0x0400002B RID: 43
		private global::System.Windows.Forms.TextBox textBoxBillingAddress1;

		// Token: 0x0400002C RID: 44
		private global::System.Windows.Forms.Label label5;

		// Token: 0x0400002D RID: 45
		private global::System.Windows.Forms.TextBox textBoxBillingAddress2;

		// Token: 0x0400002E RID: 46
		private global::System.Windows.Forms.Label label6;

		// Token: 0x0400002F RID: 47
		private global::System.Windows.Forms.TextBox textBoxBillingZipCode;

		// Token: 0x04000030 RID: 48
		private global::System.Windows.Forms.Label label7;

		// Token: 0x04000031 RID: 49
		private global::System.Windows.Forms.TextBox textBoxBillingCity;

		// Token: 0x04000032 RID: 50
		private global::System.Windows.Forms.Label label8;

		// Token: 0x04000033 RID: 51
		private global::System.Windows.Forms.Label label10;

		// Token: 0x04000034 RID: 52
		private global::System.Windows.Forms.TextBox textBoxBillingPhone;

		// Token: 0x04000035 RID: 53
		private global::System.Windows.Forms.GroupBox groupBox1;

		// Token: 0x04000036 RID: 54
		private global::System.Windows.Forms.CheckBox checkBoxShippingSame;

		// Token: 0x04000037 RID: 55
		private global::System.Windows.Forms.ComboBox comboBoxBillingState;

		// Token: 0x04000038 RID: 56
		private global::System.Windows.Forms.GroupBox groupBox2;

		// Token: 0x04000039 RID: 57
		private global::System.Windows.Forms.ComboBox comboBoxShippingState;

		// Token: 0x0400003A RID: 58
		private global::System.Windows.Forms.Label label11;

		// Token: 0x0400003B RID: 59
		private global::System.Windows.Forms.Label label12;

		// Token: 0x0400003C RID: 60
		private global::System.Windows.Forms.TextBox textBoxShippingFirst;

		// Token: 0x0400003D RID: 61
		private global::System.Windows.Forms.TextBox textBoxShippingPhone;

		// Token: 0x0400003E RID: 62
		private global::System.Windows.Forms.TextBox textBoxShippingLast;

		// Token: 0x0400003F RID: 63
		private global::System.Windows.Forms.Label label14;

		// Token: 0x04000040 RID: 64
		private global::System.Windows.Forms.TextBox textBoxShippingAddress1;

		// Token: 0x04000041 RID: 65
		private global::System.Windows.Forms.Label label15;

		// Token: 0x04000042 RID: 66
		private global::System.Windows.Forms.Label label16;

		// Token: 0x04000043 RID: 67
		private global::System.Windows.Forms.TextBox textBoxShippingAddress2;

		// Token: 0x04000044 RID: 68
		private global::System.Windows.Forms.Label label17;

		// Token: 0x04000045 RID: 69
		private global::System.Windows.Forms.Label label18;

		// Token: 0x04000046 RID: 70
		private global::System.Windows.Forms.TextBox textBoxShippingCity;

		// Token: 0x04000047 RID: 71
		private global::System.Windows.Forms.TextBox textBoxShippingPostalCode;

		// Token: 0x04000048 RID: 72
		private global::System.Windows.Forms.Label label19;

		// Token: 0x04000049 RID: 73
		private global::System.Windows.Forms.GroupBox groupBox3;

		// Token: 0x0400004A RID: 74
		private global::System.Windows.Forms.ComboBox comboBoxCardType;

		// Token: 0x0400004B RID: 75
		private global::System.Windows.Forms.Label label20;

		// Token: 0x0400004C RID: 76
		private global::System.Windows.Forms.Label label21;

		// Token: 0x0400004D RID: 77
		private global::System.Windows.Forms.TextBox textBoxCardNumber;

		// Token: 0x0400004E RID: 78
		private global::System.Windows.Forms.ComboBox comboBoxExpirationMonth;

		// Token: 0x0400004F RID: 79
		private global::System.Windows.Forms.Label label22;

		// Token: 0x04000050 RID: 80
		private global::System.Windows.Forms.ComboBox comboBoxExpirationYear;

		// Token: 0x04000051 RID: 81
		private global::System.Windows.Forms.Label label23;

		// Token: 0x04000052 RID: 82
		private global::System.Windows.Forms.TextBox textBoxCvv;

		// Token: 0x04000053 RID: 83
		private global::System.Windows.Forms.Button buttonSave;

		// Token: 0x04000054 RID: 84
		private global::System.Windows.Forms.Label label26;

		// Token: 0x04000055 RID: 85
		private global::System.Windows.Forms.Label label27;

		// Token: 0x04000056 RID: 86
		private global::System.Windows.Forms.TextBox textBoxMaxCheckouts;

		// Token: 0x04000057 RID: 87
		private global::System.Windows.Forms.Label label9;

		// Token: 0x04000058 RID: 88
		private global::System.Windows.Forms.ComboBox comboBoxBillingStateJP;

		// Token: 0x04000059 RID: 89
		private global::System.Windows.Forms.Label label13;

		// Token: 0x0400005A RID: 90
		private global::System.Windows.Forms.ComboBox comboBoxShippingStateJP;

		// Token: 0x0400005B RID: 91
		private global::System.Windows.Forms.Label label24;

		// Token: 0x0400005C RID: 92
		private global::System.Windows.Forms.TextBox textBoxBillingAddress3;

		// Token: 0x0400005D RID: 93
		private global::System.Windows.Forms.Label label25;

		// Token: 0x0400005E RID: 94
		private global::System.Windows.Forms.TextBox textBoxShippingAddress3;

		// Token: 0x0400005F RID: 95
		private global::System.Windows.Forms.Label label28;
	}
}
