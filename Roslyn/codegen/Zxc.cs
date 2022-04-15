namespace Client
{
    using System.Collections.Generic;

    public class Zxc
    {
        public int PlayerID { get; set; }

        public string PlayerName { get; set; }

        public int HoursInDota2 { get; set; }

        public List<string> Achievements { get; set; }

        public Zxc(int playerId, string playerName, int hoursInDota2, List<string> achievements)
        {
            PlayerID = playerId;
            PlayerName = playerName;
            HoursInDota2 = hoursInDota2;
            Achievements = new List<string>(achievements);
        }
    }
}