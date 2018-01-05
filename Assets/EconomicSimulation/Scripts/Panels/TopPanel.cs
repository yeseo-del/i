﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using Nashet.UnityUIUtils;
namespace Nashet.EconomicSimulation
{
    public class TopPanel : MonoBehaviour
    {
        [SerializeField]
        private Button btnPlay, btnStep, btnTrade;

        [SerializeField]
        private Text generalText;

        // Use this for initialization
        void Awake()
        {
            btnPlay.onClick.AddListener(() => onbtnPlayClick(btnPlay));
            btnStep.onClick.AddListener(() => onbtnStepClick(btnPlay));
            btnPlay.image.color = Color.grey;
            MainCamera.topPanel = this;
            hide();
        }
        public void hide()
        {
            gameObject.SetActive(false);
        }
        public void show()
        {
            gameObject.SetActive(true);
            //panelRectTransform.SetAsLastSibling();
            refresh();
        }
        public void refresh()
        {
            var sb = new StringBuilder();

            sb.Append("Date: ").Append(Game.date).Append("; Country: ").Append(Game.Player.getName())
                .Append("\nMoney: ").Append(Game.Player.cash.get().ToString("N0"))
                .Append("; Science points: ").Append(Game.Player.sciencePoints.get().ToString("F0"))
                .Append("; Men: ").Append(Game.Player.getMenPopulation().ToString("N0"))
                .Append("; avg. loyalty: ").Append(Game.Player.getAverageLoyalty());
            generalText.text = sb.ToString();
        }
        public void onTradeClick()
        {
            if (MainCamera.tradeWindow.isActiveAndEnabled)
                MainCamera.tradeWindow.hide();
            else
                MainCamera.tradeWindow.show(true);
        }
        public void onExitClick()
        {
            Application.Quit();
        }
        public void onMilitaryClick()
        {
            if (MainCamera.militaryPanel.isActiveAndEnabled)
                MainCamera.militaryPanel.hide();
            else
                MainCamera.militaryPanel.show(null);

        }
        public void onInventionsClick()
        {

            if (MainCamera.inventionsPanel.isActiveAndEnabled)
                MainCamera.inventionsPanel.hide();
            else
                MainCamera.inventionsPanel.show(true);
        }
        public void onEnterprisesClick()
        {
            if (MainCamera.productionWindow.isActiveAndEnabled)
                if (MainCamera.productionWindow.getShowingProvince() == null)
                    MainCamera.productionWindow.hide();
                else
                {
                    MainCamera.productionWindow.show(null, true);
                    MainCamera.productionWindow.onShowAllClick();
                }
            else
            {
                MainCamera.productionWindow.show(null, true);
                MainCamera.productionWindow.onShowAllClick();
            }
        }
        public void onPopulationClick()
        {

            if (MainCamera.populationPanel.isActiveAndEnabled)
                if (MainCamera.populationPanel.ShowingProvince == null)
                    MainCamera.populationPanel.hide();
                else
                    MainCamera.populationPanel.onShowAllClick();
            else
                MainCamera.populationPanel.onShowAllClick();
        }
        public void onPoliticsClick()
        {
            if (MainCamera.politicsPanel.isActiveAndEnabled)
                MainCamera.politicsPanel.hide();
            else
                MainCamera.politicsPanel.show(true);
        }
        public void onFinanceClick()
        {
            if (MainCamera.financePanel.isActiveAndEnabled)
                MainCamera.financePanel.hide();
            else
                MainCamera.financePanel.show();
        }
        void onbtnStepClick(Button button)
        {
            if (Game.isRunningSimulation())
            {
                Game.pauseSimulation();
                button.image.color = Color.grey;
                Text text = button.GetComponentInChildren<Text>();
                text.text = "Pause";
            }
            else
                Game.makeOneStepSimulation();
        }
        void onbtnPlayClick(Button button)
        {
            switchHaveToRunSimulation();
        }
        public void switchHaveToRunSimulation()
        {
            if (Game.isRunningSimulation())
                Game.pauseSimulation();
            else
                Game.continueSimulation();

            if (Game.isRunningSimulation())
            {
                btnPlay.image.color = Color.white;
                Text text = btnPlay.GetComponentInChildren<Text>();
                text.text = "Playing";
            }
            else
            {
                btnPlay.image.color = Color.grey;
                Text text = btnPlay.GetComponentInChildren<Text>();
                text.text = "Pause";
            }
        }
    }
}