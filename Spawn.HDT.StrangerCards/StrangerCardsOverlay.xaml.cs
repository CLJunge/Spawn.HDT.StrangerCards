using System;
using System.Collections.Generic;
using System.Windows.Controls;
using HearthDb.Enums;
using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Controls;
using Hearthstone_Deck_Tracker.Enums;
using Hearthstone_Deck_Tracker.Hearthstone.Entities;

namespace Spawn.HDT.StrangerCards
{
    public partial class StrangerCardsOverlay : UserControl
    {
        #region Member Variables
        private CardMarker[] m_vMarkers;
        private bool m_blnSpellsOnly = false;
        #endregion

        #region Ctor
        public StrangerCardsOverlay()
        {
            InitializeComponent();

            m_vMarkers = new CardMarker[] { Mark0, Mark1, Mark2, Mark3, Mark4, Mark5, Mark6, Mark7, Mark8, Mark9 };

            for (int i = 0; i < m_vMarkers.Length; i++)
            {
                m_vMarkers[i].UpdateCardAge(null);
                m_vMarkers[i].UpdateCostReduction(0);
                m_vMarkers[i].UpdateIcon(CardMark.None);
            }
        }
        #endregion

        #region UpdateMarker
        public void UpdateMarker()
        {
            List<Entity> lstHand = new List<Entity>(Core.Game.Player.Hand);

            int nIndex = 0;

            for (; nIndex < lstHand.Count; nIndex++)
            {
                Entity cardEntity = lstHand[nIndex];

                int nZonePos = cardEntity.GetTag(GameTag.ZONE_POSITION);

                CardMarker marker = m_vMarkers[nZonePos - 1];

                //marker.Text = nZonePos.ToString();
                marker.UpdateCardAge(nZonePos);

                if (cardEntity.Info.Created && (!m_blnSpellsOnly || (m_blnSpellsOnly && cardEntity.Card.Type.Equals("spell", StringComparison.InvariantCultureIgnoreCase))))
                {
                    //marker.Mark = CardMark.Created;
                    marker.UpdateIcon(CardMark.Created);
                }
                else
                {
                    //marker.Mark = CardMark.None;
                    marker.UpdateIcon(CardMark.None);
                }
            }

            for (; nIndex < 10; nIndex++)
            {
                m_vMarkers[nIndex].UpdateCardAge(null);
                m_vMarkers[nIndex].UpdateIcon(CardMark.None);
            }

            UpdateLayout();
        }
        #endregion

        #region UpdatePosition
        public void UpdatePosition()
        {
            Canvas.SetBottom(this, 35);
            Canvas.SetLeft(this, Core.OverlayCanvas.Width * .1);
        }
        #endregion
    }
}