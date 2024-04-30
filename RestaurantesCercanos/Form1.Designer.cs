namespace RestaurantesCercanos
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            gMapControl1 = new GMap.NET.WindowsForms.GMapControl();
            dataGrid = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            labelTime = new Label();
            buttonSort = new Button();
            comboBox1 = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dataGrid).BeginInit();
            SuspendLayout();
            // 
            // gMapControl1
            // 
            gMapControl1.Bearing = 0F;
            gMapControl1.CanDragMap = true;
            gMapControl1.EmptyTileColor = Color.Navy;
            gMapControl1.GrayScaleMode = false;
            gMapControl1.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            gMapControl1.LevelsKeepInMemory = 5;
            gMapControl1.Location = new Point(302, 92);
            gMapControl1.MarkersEnabled = true;
            gMapControl1.MaxZoom = 2;
            gMapControl1.MinZoom = 2;
            gMapControl1.MouseWheelZoomEnabled = true;
            gMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            gMapControl1.Name = "gMapControl1";
            gMapControl1.NegativeMode = false;
            gMapControl1.PolygonsEnabled = true;
            gMapControl1.RetryLoadTile = 0;
            gMapControl1.RoutesEnabled = true;
            gMapControl1.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            gMapControl1.SelectedAreaFillColor = Color.FromArgb(33, 65, 105, 225);
            gMapControl1.ShowTileGridLines = false;
            gMapControl1.Size = new Size(936, 548);
            gMapControl1.TabIndex = 0;
            gMapControl1.Zoom = 0D;
            gMapControl1.Load += gMapControl1_Load;
            gMapControl1.MouseDoubleClick += gMapControl1_MouseDoubleClick;
            // 
            // dataGrid
            // 
            dataGrid.AllowUserToAddRows = false;
            dataGrid.AllowUserToDeleteRows = false;
            dataGrid.AllowUserToResizeColumns = false;
            dataGrid.AllowUserToResizeRows = false;
            dataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGrid.Location = new Point(12, 92);
            dataGrid.Name = "dataGrid";
            dataGrid.RowTemplate.Height = 25;
            dataGrid.Size = new Size(274, 530);
            dataGrid.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("FiraCode Nerd Font", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(12, 14);
            label1.Name = "label1";
            label1.Size = new Size(216, 19);
            label1.TabIndex = 3;
            label1.Text = "Métodos de Ordenamiento";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("FiraCode Nerd Font Propo", 26.2499962F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(512, 29);
            label2.Name = "label2";
            label2.Size = new Size(525, 43);
            label2.TabIndex = 4;
            label2.Text = "Seleccione su Ubicación";
            // 
            // labelTime
            // 
            labelTime.AutoSize = true;
            labelTime.Location = new Point(12, 628);
            labelTime.Name = "labelTime";
            labelTime.Size = new Size(144, 15);
            labelTime.TabIndex = 5;
            labelTime.Text = "Tiempo de ordenamiento:";
            // 
            // buttonSort
            // 
            buttonSort.Location = new Point(234, 14);
            buttonSort.Name = "buttonSort";
            buttonSort.Size = new Size(62, 58);
            buttonSort.TabIndex = 6;
            buttonSort.Text = "Ordenar";
            buttonSort.UseVisualStyleBackColor = true;
            buttonSort.Click += buttonSort_Click;
            // 
            // comboBox1
            // 
            comboBox1.Font = new Font("FiraCode Nerd Font", 8.999999F, FontStyle.Regular, GraphicsUnit.Point);
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Método Intercambio", "Método Selección", "Método Inserción" });
            comboBox1.Location = new Point(12, 46);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(216, 23);
            comboBox1.TabIndex = 7;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1250, 652);
            Controls.Add(comboBox1);
            Controls.Add(buttonSort);
            Controls.Add(labelTime);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dataGrid);
            Controls.Add(gMapControl1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GMap.NET.WindowsForms.GMapControl gMapControl1;
        private DataGridView dataGrid;
        private Label label1;
        private Label label2;
        private Label labelTime;
        private Button buttonSort;
        private ComboBox comboBox1;
    }
}
