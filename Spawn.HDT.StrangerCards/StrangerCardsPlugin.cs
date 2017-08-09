using System;
using System.Reflection;
using System.Windows.Controls;
using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Plugins;

namespace Spawn.HDT.StrangerCards
{
    public class StrangerCardsPlugin : IPlugin
    {
        #region Member Variables
        private StrangerCardsOverlay m_overlay; 
        #endregion

        #region Properties
        #region Name
        public string Name => "Stranger Cards";
        #endregion

        #region Description
        public string Description => "Visualizes the player hand in order to keep track of which cards started in your deck and which didn't.";
        #endregion

        #region ButtonText
        public string ButtonText => null;
        #endregion

        #region Author
        public string Author => "CLJunge";
        #endregion

        #region Version
        public Version Version => new Version(Assembly.GetExecutingAssembly().GetName().Version.ToString(2)); 
        #endregion

        #region MenuItem
        public MenuItem MenuItem => null;
        #endregion
        #endregion

        #region OnButtonPress
        public void OnButtonPress()
        {
        }
        #endregion

        #region OnLoad
        public void OnLoad()
        {
            GameEvents.OnInMenu.Add(OnInMenu);
            GameEvents.OnGameStart.Add(OnGameStart);

            m_overlay = new StrangerCardsOverlay();

            if (!Core.Game.IsInMenu)
            {
                m_overlay.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                m_overlay.Visibility = System.Windows.Visibility.Collapsed;
            }

            Core.OverlayCanvas.Children.Add(m_overlay);
        }
        #endregion

        #region OnUnload
        public void OnUnload() 
        {
            Core.OverlayCanvas.Children.Remove(m_overlay);
        }
        #endregion

        #region OnUpdate
        public void OnUpdate()
        {
            m_overlay.UpdateMarker();
            m_overlay.UpdatePosition();
        }
        #endregion

        #region Events
        #region OnGameStart
        private void OnGameStart()
        {
            m_overlay.Visibility = System.Windows.Visibility.Visible;
        }
        #endregion

        #region OnInMenu
        private void OnInMenu()
        {
            m_overlay.Visibility = System.Windows.Visibility.Collapsed;
        }
        #endregion 
        #endregion
    }
}