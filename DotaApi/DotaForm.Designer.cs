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
			dgView1 = new System.Windows.Forms.DataGridView();
			((System.ComponentModel.ISupportInitialize)(dgView1)).BeginInit();
			SuspendLayout();
			//
			// dataGridView1
			//
			dgView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgView1.Location = new System.Drawing.Point(12, 41);
			dgView1.Name = "dataGridView1";
			dgView1.Size = new System.Drawing.Size(720, 331);
			dgView1.TabIndex = 0;
			//
			// DotaForm
			//
			AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(744, 384);
			Controls.Add(dgView1);
			Name = "DotaForm";
			Text = "Match Details";
			((System.ComponentModel.ISupportInitialize)(dgView1)).EndInit();
			ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dgView1;
	}
}

