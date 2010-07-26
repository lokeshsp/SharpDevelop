﻿/*
 * Erstellt mit SharpDevelop.
 * Benutzer: Peter
 * Datum: 03.10.2008
 * Zeit: 17:41
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
using System;
using System.Drawing;
using ICSharpCode.Core;
using ICSharpCode.Reports.Core;


namespace ICSharpCode.Reports.Addin.ReportWizard
{
	/// <summary>
	/// Description of LayoutPanel.
	/// </summary>
	public class LayoutPanel: AbstractWizardPanel
	{
		private LayoutPanelControl layoutControl;
		private Properties customizer;
		private ReportStructure reportStructure;
		
		public LayoutPanel()
		{
		base.EnableFinish = true;
			base.EnableCancel = true;
			base.EnableNext = true;
			base.Refresh();
			layoutControl = new LayoutPanelControl();
			layoutControl.Location = new Point (20,20);
			this.Controls.Add(layoutControl);
		
		}
		
		
		public override bool ReceiveDialogMessage(DialogMessage message)
		{
			base.EnableFinish = true;
			base.IsLastPanel = true;
			base.EnablePrevious = true;
			
			if (customizer == null) {
				customizer = (Properties)base.CustomizationObject;
			}
			
			if (message == DialogMessage.Activated) {
				
				this.layoutControl.ReportLayout = (GlobalEnums.ReportLayout)customizer.Get("ReportLayout");
				reportStructure = (ReportStructure)customizer.Get("Generator");
				layoutControl.AvailableFieldsCollection = reportStructure.AvailableFieldsCollection;
				
			}
			else if (message == DialogMessage.Next)
			{
				Console.WriteLine("aa");
			}
			else if (message == DialogMessage.Finish)
			{
				customizer.Set ("ReportLayout",this.layoutControl.ReportLayout);
				var reportStructure = (ReportStructure)customizer.Get("Generator");
				reportStructure.Grouping = layoutControl.GroupName;
			}
			return true;
		}
	}
}
