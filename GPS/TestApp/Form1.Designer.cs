namespace TestApp
{
    partial class Form1
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 30D);
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.propertyGridSat = new System.Windows.Forms.PropertyGrid();
            this.chartSatelist = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartSky = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.listBoxMessages = new System.Windows.Forms.ListBox();
            this.listBoxUnknownMessages = new System.Windows.Forms.ListBox();
            this.propertyGridGGA = new System.Windows.Forms.PropertyGrid();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chartSatelist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartSky)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(63, 58);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(7);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(277, 37);
            this.comboBox1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(359, 58);
            this.button1.Margin = new System.Windows.Forms.Padding(7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(175, 51);
            this.button1.TabIndex = 1;
            this.button1.Text = "Open";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(548, 54);
            this.button2.Margin = new System.Windows.Forms.Padding(7);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(175, 51);
            this.button2.TabIndex = 2;
            this.button2.Text = "Search ports";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 29;
            this.listBox1.Location = new System.Drawing.Point(65, 178);
            this.listBox1.Margin = new System.Windows.Forms.Padding(7);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(412, 1193);
            this.listBox1.TabIndex = 3;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(761, 58);
            this.button3.Margin = new System.Windows.Forms.Padding(7);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(175, 51);
            this.button3.TabIndex = 6;
            this.button3.Text = "Write to log";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(950, 58);
            this.button4.Margin = new System.Windows.Forms.Padding(7);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(175, 51);
            this.button4.TabIndex = 7;
            this.button4.Text = "Stop writing";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // propertyGridSat
            // 
            this.propertyGridSat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGridSat.LineColor = System.Drawing.SystemColors.ControlDark;
            this.propertyGridSat.Location = new System.Drawing.Point(2599, 174);
            this.propertyGridSat.Margin = new System.Windows.Forms.Padding(7);
            this.propertyGridSat.Name = "propertyGridSat";
            this.propertyGridSat.Size = new System.Drawing.Size(380, 858);
            this.propertyGridSat.TabIndex = 9;
            // 
            // chartSatelist
            // 
            this.chartSatelist.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.Name = "ChartArea1";
            this.chartSatelist.ChartAreas.Add(chartArea1);
            this.chartSatelist.Location = new System.Drawing.Point(491, 1051);
            this.chartSatelist.Margin = new System.Windows.Forms.Padding(7);
            this.chartSatelist.Name = "chartSatelist";
            series1.ChartArea = "ChartArea1";
            series1.IsVisibleInLegend = false;
            series1.Name = "Series1";
            dataPoint1.IsValueShownAsLabel = false;
            dataPoint1.Label = "aaa";
            dataPoint1.LabelAngle = 90;
            series1.Points.Add(dataPoint1);
            this.chartSatelist.Series.Add(series1);
            this.chartSatelist.Size = new System.Drawing.Size(2489, 317);
            this.chartSatelist.TabIndex = 10;
            this.chartSatelist.Text = "chart1";
            this.chartSatelist.Click += new System.EventHandler(this.chartSatelist_Click);
            this.chartSatelist.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chartSatelist_MouseClick);
            // 
            // chartSky
            // 
            this.chartSky.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea2.Name = "ChartArea1";
            this.chartSky.ChartAreas.Add(chartArea2);
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.chartSky.Legends.Add(legend1);
            this.chartSky.Location = new System.Drawing.Point(923, 174);
            this.chartSky.Margin = new System.Windows.Forms.Padding(7);
            this.chartSky.Name = "chartSky";
            series2.BorderColor = System.Drawing.Color.Transparent;
            series2.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
            series2.BorderWidth = 0;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Polar;
            series2.Color = System.Drawing.Color.Transparent;
            series2.Legend = "Legend1";
            series2.MarkerColor = System.Drawing.Color.Red;
            series2.MarkerSize = 20;
            series2.Name = "Series1";
            this.chartSky.Series.Add(series2);
            this.chartSky.Size = new System.Drawing.Size(926, 863);
            this.chartSky.TabIndex = 11;
            this.chartSky.Text = "chart1";
            // 
            // listBoxMessages
            // 
            this.listBoxMessages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxMessages.FormattingEnabled = true;
            this.listBoxMessages.ItemHeight = 29;
            this.listBoxMessages.Location = new System.Drawing.Point(2305, 178);
            this.listBoxMessages.Margin = new System.Windows.Forms.Padding(7);
            this.listBoxMessages.Name = "listBoxMessages";
            this.listBoxMessages.Size = new System.Drawing.Size(275, 410);
            this.listBoxMessages.TabIndex = 12;
            this.listBoxMessages.SelectedIndexChanged += new System.EventHandler(this.listBoxMessages_SelectedIndexChanged);
            // 
            // listBoxUnknownMessages
            // 
            this.listBoxUnknownMessages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxUnknownMessages.FormattingEnabled = true;
            this.listBoxUnknownMessages.ItemHeight = 29;
            this.listBoxUnknownMessages.Location = new System.Drawing.Point(1863, 622);
            this.listBoxUnknownMessages.Margin = new System.Windows.Forms.Padding(7);
            this.listBoxUnknownMessages.Name = "listBoxUnknownMessages";
            this.listBoxUnknownMessages.Size = new System.Drawing.Size(717, 410);
            this.listBoxUnknownMessages.TabIndex = 13;
            this.listBoxUnknownMessages.SelectedIndexChanged += new System.EventHandler(this.listBox3_SelectedIndexChanged);
            // 
            // propertyGridGGA
            // 
            this.propertyGridGGA.Location = new System.Drawing.Point(491, 178);
            this.propertyGridGGA.Margin = new System.Windows.Forms.Padding(7);
            this.propertyGridGGA.Name = "propertyGridGGA";
            this.propertyGridGGA.Size = new System.Drawing.Size(404, 859);
            this.propertyGridGGA.TabIndex = 14;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(1183, 58);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(150, 51);
            this.button5.TabIndex = 15;
            this.button5.Text = "Hot Restart";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(1350, 58);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(196, 51);
            this.button6.TabIndex = 16;
            this.button6.Text = "Warm Restart";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(1552, 58);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(196, 51);
            this.button7.TabIndex = 17;
            this.button7.Text = "Cold Restart";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(3003, 1394);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.propertyGridGGA);
            this.Controls.Add(this.listBoxUnknownMessages);
            this.Controls.Add(this.listBoxMessages);
            this.Controls.Add(this.chartSky);
            this.Controls.Add(this.chartSatelist);
            this.Controls.Add(this.propertyGridSat);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox1);
            this.Margin = new System.Windows.Forms.Padding(7);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartSatelist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartSky)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.PropertyGrid propertyGridSat;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSatelist;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSky;
        private System.Windows.Forms.ListBox listBoxMessages;
        private System.Windows.Forms.ListBox listBoxUnknownMessages;
        private System.Windows.Forms.PropertyGrid propertyGridGGA;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
    }
}

