using System;
using System.Web.UI.WebControls;

namespace MonitorPageChanges
{
	public class ClientSidePage : System.Web.UI.Page
	{
		public void MonitorChanges(WebControl wc)
		{
			if (wc == null)
			{
				return;
			}

			if (wc is CheckBoxList || wc is RadioButtonList)
			{
				int tempVar = ((ListControl)wc).Items.Count;
				for (int i = 0; i < tempVar; i++)
				{
					Page.ClientScript.RegisterArrayDeclaration("monitorChangesIDs", "\"" + string.Concat(wc.ClientID, "_", i) + "\"");
					Page.ClientScript.RegisterArrayDeclaration("monitorChangesValues", "null");
				}
			}
			else
			{
				Page.ClientScript.RegisterArrayDeclaration("monitorChangesIDs", "\"" + wc.ClientID + "\"");
				Page.ClientScript.RegisterArrayDeclaration("monitorChangesValues", "null");
			}

			AssignMonitorChangeValuesOnPageLoad();
		}

		public void MonitorChangesDataGridRuntime(WebControl wc)
		{
			string ComposerId = "ctl00_contentDisplay_GridIstruzione_ctl02_";

			if (wc == null)
			{
				return;
			}

			if (wc is CheckBoxList || wc is RadioButtonList)
			{
				int tempVar = ((ListControl)wc).Items.Count;
				for (int i = 0; i < tempVar; i++)
				{
					Page.ClientScript.RegisterArrayDeclaration("monitorChangesIDs", "\"" + string.Concat(ComposerId + wc.ClientID, "_", i) + "\"");
					Page.ClientScript.RegisterArrayDeclaration("monitorChangesValues", "null");
				}
			}
			else
			{
				Page.ClientScript.RegisterArrayDeclaration("monitorChangesIDs", "\"" + (ComposerId + wc.ClientID) + "\"");
				Page.ClientScript.RegisterArrayDeclaration("monitorChangesValues", "null");
			}

			AssignMonitorChangeValuesOnPageLoad();
		}

		private void AssignMonitorChangeValuesOnPageLoad()
		{
			if (!Page.ClientScript.IsStartupScriptRegistered("monitorChangesAssignment"))
			{
				Page.RegisterStartupScript("monitorChangesAssignment", "<script language=\"JavaScript\">" + Environment.NewLine + "  assignInitialValuesForMonitorChanges();" + Environment.NewLine + "</script>");

				Page.RegisterClientScriptBlock("monitorChangesAssignmentFunction", "<script language=\"JavaScript\">" + Environment.NewLine + "  function assignInitialValuesForMonitorChanges() {" + Environment.NewLine + "    for (var i = 0; i < monitorChangesIDs.length; i++) {" + Environment.NewLine + "      var elem = document.getElementById(monitorChangesIDs[i]);" + Environment.NewLine + "      if (elem) if (elem.type == 'checkbox' || elem.type == 'radio') monitorChangesValues[i] = elem.checked; else monitorChangesValues[i] = elem.value;" + Environment.NewLine + "    }" + Environment.NewLine + "  }" + Environment.NewLine + Environment.NewLine + Environment.NewLine + "  var needToConfirm = true;" + Environment.NewLine + "  window.onbeforeunload = confirmClose;" + Environment.NewLine + Environment.NewLine + "  function confirmClose() {" + Environment.NewLine + "    if (!needToConfirm) return;" + Environment.NewLine + "    for (var i = 0; i < monitorChangesValues.length; i++) {" + Environment.NewLine + "      var elem = document.getElementById(monitorChangesIDs[i]);" + Environment.NewLine + "      if (elem) if (((elem.type == 'checkbox' || elem.type == 'radio') && elem.checked != monitorChangesValues[i]) || (elem.type != 'checkbox' && elem.type != 'radio' && elem.value != monitorChangesValues[i])) { needToConfirm = false; setTimeout('resetFlag()', 750); return \"Sono stati modificati dei campi senza averli salvati. Se esci dalla pagina i cambiamenti andranno persi. Per salvarli fai click su Annulla, per ritornare sulla pagina, e poi fai Click sul pulsante di salvattaggio.\"; }" + Environment.NewLine + "    }" + Environment.NewLine + "  }" + Environment.NewLine + Environment.NewLine + "  function resetFlag() { needToConfirm = true; } " + Environment.NewLine + "</script>");
			}
		}

		public void BypassModifiedMethod(WebControl wc)
		{
			wc.Attributes["onclick"] = "javascript:" + GetBypassModifiedMethodScript();
		}

		public string GetBypassModifiedMethodScript()
		{
			return "needToConfirm = false;";
		}

	}
}