//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.4322.2032
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

using System;
using System.Drawing;
using System.Windows.Forms;

using SharpReport.Designer;
/// <summary>
/// This Class acts as a Control to display images
/// </summary>
/// <remarks>
/// 	created by - Forstmeier Peter
/// 	created on - 04.10.2005 11:09:39
/// </remarks>
namespace SharpReport.ReportItems {	
	
	public class ReportImageControl : AbstractGraphicControl {
		
		private Image image;
		private bool scaleImageToSize;
		
		public ReportImageControl() {
			InitializeComponent();
			this.SetStyle(ControlStyles.DoubleBuffer |
			              ControlStyles.UserPaint |
			              ControlStyles.AllPaintingInWmPaint |
			              ControlStyles.ResizeRedraw,
			              true);
			this.UpdateStyles();
		}
		
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs pea) {
			base.OnPaint (pea);
			base.DrawEdges (pea);
			
			if (this.image != null) {
				if (this.scaleImageToSize) {
					pea.Graphics.DrawImageUnscaled(image,0,0);
				} else {
					pea.Graphics.DrawImage(image,0,0,this.Width,this.Height);
				}
				
			}
		}
		
		public override string ToString() {
			return this.Name;
		}
		
		
		
		public Image Image {
			get {
				return image;
			}
			set {
				image = value;
			}
		}
		
		public bool ScaleImageToSize {
			get {
				return scaleImageToSize;
			}
			set {
				scaleImageToSize = value;
			}
		}
		
		#region Windows Forms Designer generated code
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent() {
			// 
			// ReportGraphicControl
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Name = "ReportImageControl";
			this.Size = new System.Drawing.Size(72, 20);
		}
		#endregion
	}
}
