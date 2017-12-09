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
            ((System.ComponentModel.ISupportInitialize)(this.chartSatelist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartSky)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(27, 26);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(154, 26);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Open";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(235, 24);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
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
            this.listBox1.Location = new System.Drawing.Point(28, 80);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(373, 537);
            this.listBox1.TabIndex = 3;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(326, 26);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "Write to log";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(407, 26);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 7;
            this.button4.Text = "Stop writing";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // propertyGridSat
            // 
            this.propertyGridSat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGridSat.LineColor = System.Drawing.SystemColors.ControlDark;
            this.propertyGridSat.Location = new System.Drawing.Point(1114, 78);
            this.propertyGridSat.Name = "propertyGridSat";
            this.propertyGridSat.Size = new System.Drawing.Size(163, 188);
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
            this.chartSatelist.Location = new System.Drawing.Point(407, 471);
            this.chartSatelist.Name = "chartSatelist";
            series1.ChartArea = "ChartArea1";
            series1.IsVisibleInLegend = false;
            series1.Name = "Series1";
            dataPoint1.IsValueShownAsLabel = false;
            dataPoint1.Label = "aaa";
            dataPoint1.LabelAngle = 90;
            series1.Points.Add(dataPoint1);
            this.chartSatelist.Series.Add(series1);
            this.chartSatelist.Size = new System.Drawing.Size(870, 142);
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
            this.chartSky.Location = new System.Drawing.Point(596, 78);
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
            this.chartSky.Size = new System.Drawing.Size(386, 387);
            this.chartSky.TabIndex = 11;
            this.chartSky.Text = "chart1";
            // 
            // listBoxMessages
            // 
            this.listBoxMessages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxMessages.FormattingEnabled = true;
            this.listBoxMessages.Location = new System.Drawing.Point(988, 80);
            this.listBoxMessages.Name = "listBoxMessages";
            this.listBoxMessages.Size = new System.Drawing.Size(120, 186);
            this.listBoxMessages.TabIndex = 12;
            this.listBoxMessages.SelectedIndexChanged += new System.EventHandler(this.listBoxMessages_SelectedIndexChanged);
            // 
            // listBoxUnknownMessages
            // 
            this.listBoxUnknownMessages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxUnknownMessages.FormattingEnabled = true;
            this.listBoxUnknownMessages.Location = new System.Drawing.Point(988, 279);
            this.listBoxUnknownMessages.Name = "listBoxUnknownMessages";
            this.listBoxUnknownMessages.Size = new System.Drawing.Size(120, 186);
            this.listBoxUnknownMessages.TabIndex = 13;
            this.listBoxUnknownMessages.SelectedIndexChanged += new System.EventHandler(this.listBox3_SelectedIndexChanged);
            // 
            // propertyGridGGA
            // 
            this.propertyGridGGA.Location = new System.Drawing.Point(407, 80);
            this.propertyGridGGA.Name = "propertyGridGGA";
            this.propertyGridGGA.Size = new System.Drawing.Size(173, 385);
            this.propertyGridGGA.TabIndex = 14;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1287, 625);
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
    }
}

