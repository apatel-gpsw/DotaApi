namespace DotaApi
{
	partial class DotaForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Cleans up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
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
			this.dgView1 = new System.Windows.Forms.DataGridView();
			((System.ComponentModel.ISupportInitialize)(this.dgView1)).BeginInit();
			this.SuspendLayout();
			// 
			// dgView1
			// 
			this.dgView1.AllowUserToAddRows = false;
			this.dgView1.AllowUserToDeleteRows = false;
			this.dgView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dgView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
			this.dgView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgView1.Location = new System.Drawing.Point(16, 50);
			this.dgView1.Margin = new System.Windows.Forms.Padding(4);
			this.dgView1.Name = "dgView1";
			this.dgView1.ReadOnly = true;
			this.dgView1.Size = new System.Drawing.Size(1150, 320);
			this.dgView1.TabIndex = 0;
			// 
			// DotaForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1182, 389);
			this.Controls.Add(this.dgView1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "DotaForm";
			this.Text = "Match Details";
			((System.ComponentModel.ISupportInitialize)(this.dgView1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dgView1;
	}
}

