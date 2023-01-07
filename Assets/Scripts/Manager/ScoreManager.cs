using System;
using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private Text timerText;
        [SerializeField] private Text deathText;
        private static float timer = 0;

        private static int playerMaxHealth = 100;
        private static int villainMaxHealth = 500;
        private static int playerHealth = 100;
        private static int villainHealth = 500;
        private static int playerDeathCount = 0;

        private static int pillarDamage = 20;
        private static int mirrorDamage = 20;
        private static int bulletDamage = 10;
        private static int bombDamage = 50;
        private static int chainsawDamage = 30;


        private static HealthBar playerHealthBar;
        private static HealthBar villainHealthBar;

        public static int VillainHealth => villainHealth;
        public static int VillainMaxHealth => villainMaxHealth;

        public static int PlayerDeathCount => playerDeathCount;

        void Awake()
        {
            playerHealthBar = GameObject.FindGameObjectWithTag("player-healthbar").GetComponent<HealthBar>();
            villainHealthBar = GameObject.FindGameObjectWithTag("villain-healthbar").GetComponent<HealthBar>();

            playerHealthBar.SetMaxHealth(playerMaxHealth);
            playerHealthBar.SetHealth(playerMaxHealth);
            villainHealthBar.SetMaxHealth(villainMaxHealth);
            villainHealthBar.SetHealth(villainMaxHealth);
        }

        public static void DecreaseLifePlayer(string name)
        {
            print("decreased life by " + name);
            if (name == "pillar")
                playerHealth -= pillarDamage;
            else if (name == "bullet")
                playerHealth -= bulletDamage;
            else if (name == "bomb")
                playerHealth -= bombDamage;
            else if (name == "chainsaw")
                playerHealth -= chainsawDamage;
            playerHealthBar.SetHealth(playerHealth);

            if (playerHealth <= 0)
                MenuManager.GoTo("gameover");
        }

        public static void DecreaseLifeVillain(string name)
        {
            if (name == "pillar")
                villainHealth -= pillarDamage;
            else if (name == "bullet")
                villainHealth -= bulletDamage;
            else if (name == "bomb")
                villainHealth -= bombDamage;
            else if (name == "chainsaw")
                villainHealth -= chainsawDamage;
            villainHealthBar.SetHealth(villainHealth);

            if (villainHealth < 0)
                MenuManager.GoTo("gameover");
        }

        public static void ReplenishPlayer()
        {
            // deathText.text = "" + playerDeathCount++;
            playerHealth = playerMaxHealth;
            playerHealthBar.SetHealth(playerHealth);
        }

        public static string GetTime()
        {
            return (int)timer / 60 + " : " + (int)timer % 60;
        }

        private void Update()
        {
            timer += Time.deltaTime;
            timerText.text = (int)timer / 60 + " : " + (int)timer % 60;
            deathText.text = playerDeathCount + "";
        }
    }
}