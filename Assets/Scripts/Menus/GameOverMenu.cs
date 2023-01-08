using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private Text title;
    [SerializeField] private Text subtitle;

    private int score;

    private void Start()
    {
        Time.timeScale = 0;
        title.text = ScoreManager.VillainHealth <= 0 ? "you won" : "you died";
        subtitle.text = ScoreManager.VillainHealth <= 0
            ? "You defeated the raging bull! Honor and respect towards you! \n\n" +
              "You died " + ScoreManager.PlayerDeathCount + " times\n" +
              "Time taken " + ScoreManager.GetTime()
            : "But hey, that's not a bad thing! Here, you can have INFINITE lives!\n" +
              "There's a tradeoff though. You get your life back and our raging bull will get some of its life back too.\n\n" +
              "You died " + ScoreManager.PlayerDeathCount + " times\n" +
              "Time taken " + ScoreManager.GetTime() + "\n" +
              "The bull has " + (int)(ScoreManager.VillainHealth / ScoreManager.VillainMaxHealth * 100.0f) +
              "% life left\n" +
              "Wanna continue?";
    }

    public void Replay()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
        if (ScoreManager.VillainHealth > 0)
            ScoreManager.ReplenishPlayer();
        else
        {
            ScoreManager.PlayerHealth = ScoreManager.PlayerMaxHealth;
            ScoreManager.VillainHealth = ScoreManager.VillainMaxHealth;
            ScoreManager.timer = 0;
            MenuManager.GoTo("gameplay");
        }
    }

    public void Exit()
    {
        Time.timeScale = 1;
        ScoreManager.PlayerHealth = ScoreManager.PlayerMaxHealth;
        ScoreManager.VillainHealth = ScoreManager.VillainMaxHealth;
        ScoreManager.timer = 0;
        Destroy(gameObject);
        MenuManager.GoTo("main");
    }
}