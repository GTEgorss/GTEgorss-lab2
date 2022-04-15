namespace Client
{
    using System;
    using System.Net.Http;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.Json;

    public class Client
    {
        public async void getZxc(int id)
        {
            Console.WriteLine(await new HttpClient().GetStringAsync($"http://localhost:8080/zxc?id={id}"));
        }

        public async void newZxc(int Id, string Name, int HoursInDota2, List<string> achievements)
        {
            var jsonZxc = JsonSerializer.Serialize<Zxc>(new Zxc(Id, Name, HoursInDota2, achievements));
            var data = new StringContent(jsonZxc, Encoding.UTF8, "application/json");
            var response = await new HttpClient().PostAsync("http://localhost:8080/zxc", data);
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }
    }
}