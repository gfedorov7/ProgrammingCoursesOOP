namespace CourseProject
{
    partial class PerformanceForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PerformanceForm));
            labelNameOfPerformance = new Label();
            tbNameOfPerformance = new TextBox();
            labelDirectorName = new Label();
            tbDirectorName = new TextBox();
            labelGenre = new Label();
            cbGenre = new ComboBox();
            labelPremiereDate = new Label();
            dtpPremiereDate = new DateTimePicker();
            labelDuration = new Label();
            labelTicketCost = new Label();
            nudTicketCost = new NumericUpDown();
            btnAdd = new Button();
            dtpDuration = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)nudTicketCost).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dtpDuration).BeginInit();
            SuspendLayout();
            // 
            // labelNameOfPerformance
            // 
            labelNameOfPerformance.AutoSize = true;
            labelNameOfPerformance.Location = new Point(10, 18);
            labelNameOfPerformance.Margin = new Padding(2, 0, 2, 0);
            labelNameOfPerformance.Name = "labelNameOfPerformance";
            labelNameOfPerformance.Size = new Size(119, 20);
            labelNameOfPerformance.TabIndex = 0;
            labelNameOfPerformance.Text = "Название курса";
            // 
            // tbNameOfPerformance
            // 
            tbNameOfPerformance.Location = new Point(167, 16);
            tbNameOfPerformance.Margin = new Padding(2);
            tbNameOfPerformance.Name = "tbNameOfPerformance";
            tbNameOfPerformance.Size = new Size(243, 27);
            tbNameOfPerformance.TabIndex = 1;
            // 
            // labelDirectorName
            // 
            labelDirectorName.AutoSize = true;
            labelDirectorName.Location = new Point(10, 60);
            labelDirectorName.Margin = new Padding(2, 0, 2, 0);
            labelDirectorName.Name = "labelDirectorName";
            labelDirectorName.Size = new Size(91, 20);
            labelDirectorName.TabIndex = 2;
            labelDirectorName.Text = "Имя автора";
            // 
            // tbDirectorName
            // 
            tbDirectorName.Location = new Point(167, 58);
            tbDirectorName.Margin = new Padding(2);
            tbDirectorName.Name = "tbDirectorName";
            tbDirectorName.Size = new Size(243, 27);
            tbDirectorName.TabIndex = 3;
            // 
            // labelGenre
            // 
            labelGenre.AutoSize = true;
            labelGenre.Location = new Point(10, 99);
            labelGenre.Margin = new Padding(2, 0, 2, 0);
            labelGenre.Name = "labelGenre";
            labelGenre.Size = new Size(48, 20);
            labelGenre.TabIndex = 4;
            labelGenre.Text = "Жанр";
            // 
            // cbGenre
            // 
            cbGenre.DropDownStyle = ComboBoxStyle.DropDownList;
            cbGenre.FormattingEnabled = true;
            cbGenre.Items.AddRange(new object[] { "Теоретический", "Практический", "Теория + практика" });
            cbGenre.Location = new Point(167, 99);
            cbGenre.Margin = new Padding(2);
            cbGenre.MaxDropDownItems = 4;
            cbGenre.Name = "cbGenre";
            cbGenre.Size = new Size(243, 28);
            cbGenre.TabIndex = 5;
            // 
            // labelPremiereDate
            // 
            labelPremiereDate.AutoSize = true;
            labelPremiereDate.Location = new Point(10, 144);
            labelPremiereDate.Margin = new Padding(2, 0, 2, 0);
            labelPremiereDate.Name = "labelPremiereDate";
            labelPremiereDate.Size = new Size(110, 20);
            labelPremiereDate.TabIndex = 6;
            labelPremiereDate.Text = "Дата создания";
            // 
            // dtpPremiereDate
            // 
            dtpPremiereDate.Location = new Point(167, 140);
            dtpPremiereDate.Margin = new Padding(2);
            dtpPremiereDate.Name = "dtpPremiereDate";
            dtpPremiereDate.Size = new Size(241, 27);
            dtpPremiereDate.TabIndex = 7;
            // 
            // labelDuration
            // 
            labelDuration.AutoSize = true;
            labelDuration.Location = new Point(10, 186);
            labelDuration.Margin = new Padding(2, 0, 2, 0);
            labelDuration.Name = "labelDuration";
            labelDuration.Size = new Size(152, 20);
            labelDuration.TabIndex = 8;
            labelDuration.Text = "Продолжительность";
            // 
            // labelTicketCost
            // 
            labelTicketCost.AutoSize = true;
            labelTicketCost.Location = new Point(10, 228);
            labelTicketCost.Margin = new Padding(2, 0, 2, 0);
            labelTicketCost.Name = "labelTicketCost";
            labelTicketCost.Size = new Size(125, 20);
            labelTicketCost.TabIndex = 10;
            labelTicketCost.Text = "Стоимость курса";
            // 
            // nudTicketCost
            // 
            nudTicketCost.DecimalPlaces = 2;
            nudTicketCost.Location = new Point(167, 228);
            nudTicketCost.Margin = new Padding(2);
            nudTicketCost.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            nudTicketCost.Name = "nudTicketCost";
            nudTicketCost.Size = new Size(243, 27);
            nudTicketCost.TabIndex = 11;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(145, 271);
            btnAdd.Margin = new Padding(2);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(122, 27);
            btnAdd.TabIndex = 12;
            btnAdd.Text = "Добавить";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // dtpDuration
            // 
            dtpDuration.Location = new Point(167, 187);
            dtpDuration.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            dtpDuration.Name = "dtpDuration";
            dtpDuration.Size = new Size(243, 27);
            dtpDuration.TabIndex = 13;
            // 
            // PerformanceForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(417, 317);
            Controls.Add(dtpDuration);
            Controls.Add(btnAdd);
            Controls.Add(nudTicketCost);
            Controls.Add(labelTicketCost);
            Controls.Add(labelDuration);
            Controls.Add(dtpPremiereDate);
            Controls.Add(labelPremiereDate);
            Controls.Add(cbGenre);
            Controls.Add(labelGenre);
            Controls.Add(tbDirectorName);
            Controls.Add(labelDirectorName);
            Controls.Add(tbNameOfPerformance);
            Controls.Add(labelNameOfPerformance);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PerformanceForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Добавление записи";
            ((System.ComponentModel.ISupportInitialize)nudTicketCost).EndInit();
            ((System.ComponentModel.ISupportInitialize)dtpDuration).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelNameOfPerformance;
        private TextBox tbNameOfPerformance;
        private Label labelDirectorName;
        private TextBox tbDirectorName;
        private Label labelGenre;
        private ComboBox cbGenre;
        private Label labelPremiereDate;
        private DateTimePicker dtpPremiereDate;
        private Label labelDuration;
        private Label labelTicketCost;
        private NumericUpDown nudTicketCost;
        private Button btnAdd;
        private NumericUpDown dtpDuration;
    }
}