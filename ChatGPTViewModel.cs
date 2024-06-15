using ArcGIS.Core.CIM;
using ArcGIS.Core.Data;
using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Catalog;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Editing;
using ArcGIS.Desktop.Extensions;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Framework.Dialogs;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.KnowledgeGraph;
using ArcGIS.Desktop.Layouts;
using ArcGIS.Desktop.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcGPT
{
    internal class ChatGPTViewModel : DockPane
    {
        private const string _dockPaneID = "ArcGPT_ChatGPT";
        private const string StartUri = "https://chat.openai.com";
        private Uri _sourceUri = new(StartUri);
        private string _navInput = StartUri;

        protected ChatGPTViewModel() { }

        /// <summary>
        /// Show the DockPane.
        /// </summary>
        internal static void Show()
        {
            DockPane pane = FrameworkApplication.DockPaneManager.Find(_dockPaneID);
            if (pane == null)
                return;

            pane.Activate();
        }

        /// <summary>
        /// Text shown near the top of the DockPane.
        /// </summary>
        private string _heading = "ChatGPT";
        public string Heading
        {
            get => _heading;
            set => SetProperty(ref _heading, value);
        }

        public Uri SourceUri
        {
            get => _sourceUri;
            set
            {
                SetProperty(ref _sourceUri, value, () => SourceUri);
                if (_sourceUri.AbsoluteUri != _navInput)
                {
                    _navInput = _sourceUri.AbsoluteUri;
                    NotifyPropertyChanged(() => NavInput);
                }
            }
        }

        public string NavInput
        {
            get => _navInput;
            set => SetProperty(ref _navInput, value, () => NavInput);
        }
    }

    /// <summary>
    /// Button implementation to show the DockPane.
    /// </summary>
    internal class ChatGPT_ShowButton : Button
    {
        protected override void OnClick()
        {
            ChatGPTViewModel.Show();
        }
    }
}
