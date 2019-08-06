namespace kingkakps
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.axKHOpenAPI1 = new AxKHOpenAPILib.AxKHOpenAPI();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.MessageLogBox = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.StockName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrentPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EvaluationAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProfitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProfitPercent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PurchaseAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaymentBalance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TodayBuyCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TodaySellCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockName2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockCode2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrentPrice2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderPrice2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NotConcludedCount2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderCount2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OriginalOrderNumber2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockName3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockCode3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrentPrice3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderPrice3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NotConcludedCount3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderCount3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OriginalOrderNumber3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.axKHOpenAPI1)).BeginInit();
            this.GroupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.SuspendLayout();
            // 
            // axKHOpenAPI1
            // 
            this.axKHOpenAPI1.Enabled = true;
            this.axKHOpenAPI1.Location = new System.Drawing.Point(1369, 862);
            this.axKHOpenAPI1.Name = "axKHOpenAPI1";
            this.axKHOpenAPI1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axKHOpenAPI1.OcxState")));
            this.axKHOpenAPI1.Size = new System.Drawing.Size(75, 40);
            this.axKHOpenAPI1.TabIndex = 0;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.textBox4);
            this.GroupBox1.Controls.Add(this.label4);
            this.GroupBox1.Controls.Add(this.textBox3);
            this.GroupBox1.Controls.Add(this.label3);
            this.GroupBox1.Controls.Add(this.textBox2);
            this.GroupBox1.Controls.Add(this.label2);
            this.GroupBox1.Controls.Add(this.textBox1);
            this.GroupBox1.Controls.Add(this.label1);
            this.GroupBox1.Location = new System.Drawing.Point(1, 3);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(748, 36);
            this.GroupBox1.TabIndex = 1;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "접속정보";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(625, 9);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(116, 21);
            this.textBox4.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(554, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "예수금 :";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(433, 10);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 21);
            this.textBox3.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(362, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "접속 서버 :";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(246, 10);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 21);
            this.textBox2.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(182, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "계좌번호 :";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(67, 10);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "아이디 :";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1343, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "종료";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.MessageLogBox);
            this.groupBox2.Location = new System.Drawing.Point(3, 40);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(457, 857);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "메세지 로그";
            // 
            // MessageLogBox
            // 
            this.MessageLogBox.BackColor = System.Drawing.SystemColors.WindowText;
            this.MessageLogBox.ForeColor = System.Drawing.Color.Yellow;
            this.MessageLogBox.Location = new System.Drawing.Point(6, 20);
            this.MessageLogBox.Multiline = true;
            this.MessageLogBox.Name = "MessageLogBox";
            this.MessageLogBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.MessageLogBox.Size = new System.Drawing.Size(445, 831);
            this.MessageLogBox.TabIndex = 0;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(466, 45);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(283, 172);
            this.listBox1.TabIndex = 4;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridView1);
            this.groupBox3.Location = new System.Drawing.Point(466, 223);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(978, 172);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "보유 종목";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StockName,
            this.StockCode,
            this.Count,
            this.CurrentPrice,
            this.EvaluationAmount,
            this.ProfitPrice,
            this.ProfitPercent,
            this.PurchaseAmount,
            this.PaymentBalance,
            this.TodayBuyCount,
            this.TodaySellCount});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 17);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(972, 152);
            this.dataGridView1.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dataGridView2);
            this.groupBox4.Location = new System.Drawing.Point(463, 401);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(485, 161);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "미체결(매수) 종목";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StockName2,
            this.StockCode2,
            this.CurrentPrice2,
            this.OrderPrice2,
            this.NotConcludedCount2,
            this.OrderCount2,
            this.OriginalOrderNumber2});
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(3, 17);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(479, 141);
            this.dataGridView2.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dataGridView3);
            this.groupBox5.Location = new System.Drawing.Point(959, 401);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(485, 161);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "미체결(매도) 종목";
            // 
            // dataGridView3
            // 
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StockName3,
            this.StockCode3,
            this.CurrentPrice3,
            this.OrderPrice3,
            this.NotConcludedCount3,
            this.OrderCount3,
            this.OriginalOrderNumber3});
            this.dataGridView3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView3.Location = new System.Drawing.Point(3, 17);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowTemplate.Height = 23;
            this.dataGridView3.Size = new System.Drawing.Size(479, 141);
            this.dataGridView3.TabIndex = 0;
            // 
            // StockName
            // 
            this.StockName.HeaderText = "종목명";
            this.StockName.Name = "StockName";
            this.StockName.Width = 80;
            // 
            // StockCode
            // 
            this.StockCode.HeaderText = "종목코드";
            this.StockCode.Name = "StockCode";
            this.StockCode.Width = 80;
            // 
            // Count
            // 
            this.Count.HeaderText = "보유수량";
            this.Count.Name = "Count";
            this.Count.Width = 80;
            // 
            // CurrentPrice
            // 
            this.CurrentPrice.HeaderText = "현재가";
            this.CurrentPrice.Name = "CurrentPrice";
            this.CurrentPrice.Width = 80;
            // 
            // EvaluationAmount
            // 
            this.EvaluationAmount.HeaderText = "평가금액";
            this.EvaluationAmount.Name = "EvaluationAmount";
            this.EvaluationAmount.Width = 80;
            // 
            // ProfitPrice
            // 
            this.ProfitPrice.HeaderText = "손익금액";
            this.ProfitPrice.Name = "ProfitPrice";
            this.ProfitPrice.Width = 99;
            // 
            // ProfitPercent
            // 
            this.ProfitPercent.HeaderText = "손익율";
            this.ProfitPercent.Name = "ProfitPercent";
            this.ProfitPercent.Width = 70;
            // 
            // PurchaseAmount
            // 
            this.PurchaseAmount.HeaderText = "매입금액";
            this.PurchaseAmount.Name = "PurchaseAmount";
            this.PurchaseAmount.Width = 80;
            // 
            // PaymentBalance
            // 
            this.PaymentBalance.HeaderText = "결재잔고";
            this.PaymentBalance.Name = "PaymentBalance";
            this.PaymentBalance.Width = 80;
            // 
            // TodayBuyCount
            // 
            this.TodayBuyCount.HeaderText = "금일매수수량";
            this.TodayBuyCount.Name = "TodayBuyCount";
            // 
            // TodaySellCount
            // 
            this.TodaySellCount.HeaderText = "금일매도수량";
            this.TodaySellCount.Name = "TodaySellCount";
            // 
            // StockName2
            // 
            this.StockName2.HeaderText = "종목명";
            this.StockName2.Name = "StockName2";
            this.StockName2.Width = 80;
            // 
            // StockCode2
            // 
            this.StockCode2.HeaderText = "종목코드";
            this.StockCode2.Name = "StockCode2";
            this.StockCode2.Width = 80;
            // 
            // CurrentPrice2
            // 
            this.CurrentPrice2.HeaderText = "현재가";
            this.CurrentPrice2.Name = "CurrentPrice2";
            this.CurrentPrice2.Width = 80;
            // 
            // OrderPrice2
            // 
            this.OrderPrice2.HeaderText = "주문가격";
            this.OrderPrice2.Name = "OrderPrice2";
            // 
            // NotConcludedCount2
            // 
            this.NotConcludedCount2.HeaderText = "미체결수량";
            this.NotConcludedCount2.Name = "NotConcludedCount2";
            // 
            // OrderCount2
            // 
            this.OrderCount2.HeaderText = "주문수량";
            this.OrderCount2.Name = "OrderCount2";
            // 
            // OriginalOrderNumber2
            // 
            this.OriginalOrderNumber2.HeaderText = "원주문번호";
            this.OriginalOrderNumber2.Name = "OriginalOrderNumber2";
            // 
            // StockName3
            // 
            this.StockName3.HeaderText = "종목명";
            this.StockName3.Name = "StockName3";
            this.StockName3.Width = 80;
            // 
            // StockCode3
            // 
            this.StockCode3.HeaderText = "종목코드";
            this.StockCode3.Name = "StockCode3";
            this.StockCode3.Width = 80;
            // 
            // CurrentPrice3
            // 
            this.CurrentPrice3.HeaderText = "현재가";
            this.CurrentPrice3.Name = "CurrentPrice3";
            this.CurrentPrice3.Width = 80;
            // 
            // OrderPrice3
            // 
            this.OrderPrice3.HeaderText = "주문가격";
            this.OrderPrice3.Name = "OrderPrice3";
            // 
            // NotConcludedCount3
            // 
            this.NotConcludedCount3.HeaderText = "미체결수량";
            this.NotConcludedCount3.Name = "NotConcludedCount3";
            // 
            // OrderCount3
            // 
            this.OrderCount3.HeaderText = "주문수량";
            this.OrderCount3.Name = "OrderCount3";
            // 
            // OriginalOrderNumber3
            // 
            this.OriginalOrderNumber3.HeaderText = "원주문번호";
            this.OriginalOrderNumber3.Name = "OriginalOrderNumber3";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1456, 914);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.axKHOpenAPI1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Kingkakps_Stock";
            ((System.ComponentModel.ISupportInitialize)(this.axKHOpenAPI1)).EndInit();
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxKHOpenAPILib.AxKHOpenAPI axKHOpenAPI1;
        private System.Windows.Forms.GroupBox GroupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox MessageLogBox;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockName;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Count;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrentPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn EvaluationAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProfitPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProfitPercent;
        private System.Windows.Forms.DataGridViewTextBoxColumn PurchaseAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentBalance;
        private System.Windows.Forms.DataGridViewTextBoxColumn TodayBuyCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn TodaySellCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockName2;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockCode2;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrentPrice2;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderPrice2;
        private System.Windows.Forms.DataGridViewTextBoxColumn NotConcludedCount2;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderCount2;
        private System.Windows.Forms.DataGridViewTextBoxColumn OriginalOrderNumber2;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockName3;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockCode3;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrentPrice3;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderPrice3;
        private System.Windows.Forms.DataGridViewTextBoxColumn NotConcludedCount3;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderCount3;
        private System.Windows.Forms.DataGridViewTextBoxColumn OriginalOrderNumber3;
    }
}

