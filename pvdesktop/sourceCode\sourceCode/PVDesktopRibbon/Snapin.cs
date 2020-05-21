using System;
using System.Windows.Forms;
using DotSpatial.Controls;
using DotSpatial.Controls.Docking;
using DotSpatial.Controls.Header;
using DotSpatial.Data.Properties;
using PvMapperPlugin;

namespace pvMapperPlugin
{
    public class Snapin : Extension
    {

        private const string UniqueKeyPluginStoredValueDate = "UniqueKey-PluginStoredValueDate";
        private const string PvMapperPanelKey = "PvMapperPanel";
        DateTime _storedValue;

        public override void Deactivate()
        {
            App.HeaderControl.RemoveAll();
            App.DockManager.Remove(PvMapperPanelKey);
           // if (App.HeaderControl != null) { App.HeaderControl.RemoveAll(); }
            base.Deactivate();

        }

        public override void Activate()
        {
            //to test slow loading
            //System.Threading.Thread.Sleep(20000);

            AddMenuItems(App.HeaderControl);

            //code for saving plugin settings...
            App.SerializationManager.Serializing += manager_Serializing;
            App.SerializationManager.Deserializing += manager_Deserializing;

            AddDockingPane();

            //
            //App.HeaderControl.Add(new SimpleActionItem(HeaderControl.HomeRootItemKey, "Measure", MeasureTool_Click) { GroupCaption = "Map Tool", SmallImage = PvMapperPlugin.Properties.Resources.LayoutTool16, LargeImage = PvMapperPlugin.Properties.Resources.LayoutTool32 });
            base.Activate();
        }

        private void AddDockingPane()
        {
            var form = new pvMapForm();
            DockablePanel mainPanel = new DockablePanel(PvMapperPanelKey, "PvMapper Desktop", form.tableLayoutPanel, DockStyle.Left);

            App.DockManager.Add(mainPanel);
            App.DockManager.ActivePanelChanged += DockManager_ActivePanelChanged;
            //pvMap = App.Map as Map;
            form.pvMap = App.Map as Map;
        }

        private void DockManager_ActivePanelChanged(object sender, DockablePanelEventArgs e)
        {
            App.HeaderControl.SelectRoot("Main");
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            App.DockManager.Remove(PvMapperPanelKey);
        }

        private void AddMenuItems(IHeaderControl header)
        {
            /*/ add sample menu items...
            if (header == null) return;

            const string pvMapMenuKey = "pvMapMenu";

            header.Add(new RootItem(pvMapMenuKey, "pvPlugin"));
            header.Add(new SimpleActionItem(pvMapMenuKey, "Site data", null));
            header.Add(new SimpleActionItem(pvMapMenuKey, "Energy production", null));
            header.Add(new SimpleActionItem(pvMapMenuKey, "Solar farm layout", null));
            header.Add(new SimpleActionItem(pvMapMenuKey, "Report", null));
             */ 
        }

        private void manager_Deserializing(object sender, SerializingEventArgs e)
        {
            var manager = sender as SerializationManager;

            _storedValue = manager.GetCustomSetting(UniqueKeyPluginStoredValueDate, DateTime.Now);
        }

        private void manager_Serializing(object sender, SerializingEventArgs e)
        {
            var manager = sender as SerializationManager;

            manager.SetCustomSetting(UniqueKeyPluginStoredValueDate, _storedValue);
        }




    }
}