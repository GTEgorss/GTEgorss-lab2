package com.example.demo;

import java.util.ArrayList;
import java.util.Arrays;

public class Zxc {
    public int PlayerID;
    public String PlayerName;
    public int HoursInDota2;
    public ArrayList<String> Achievements;

    public Zxc(){
        PlayerID = 239;
        PlayerName = "qbic";
        HoursInDota2 = 2923;
        Achievements = new ArrayList<>(Arrays.asList("The best", "The best of the best"));
    }

    public Zxc(int id) {
        PlayerID = id;
        PlayerName = "qbic";
        HoursInDota2 = 2923;
        Achievements = new ArrayList<>(Arrays.asList("The best", "The best of the best"));
    }

    public Zxc(int PlayerID, String PlayerName, int HoursInDota2, ArrayList<String> Achievements) {
        this.PlayerID = PlayerID;
        this.PlayerName = PlayerName;
        this.HoursInDota2 = HoursInDota2;
        this.Achievements = new ArrayList<>(Achievements);
    }

    @Override
    public String toString() {
        return "Zxc{" +
                "PlayerID=" + PlayerID +
                ", PlayerName='" + PlayerName + '\'' +
                ", HoursInDota2=" + HoursInDota2 +
                ", Achievements=" + Achievements +
                '}';
    }
}