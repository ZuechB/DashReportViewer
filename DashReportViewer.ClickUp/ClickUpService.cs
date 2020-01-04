using Authsome;
using DashReportViewer.ClickUp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DashReportViewer.ClickUp
{
    public interface IClickUpService
    {
        Task GetTasks(string taskId);
        Task<Dictionary<string, string>> GetLists();
    }

    public class ClickUpService : IClickUpService
    {
        private const string apiToken = "";
        private const string spaceId = "";

        readonly IAuthsomeService authsomeService;

        public ClickUpService(IAuthsomeService authsomeService)
        {
            this.authsomeService = authsomeService;
        }

        public async Task GetTasks(string taskId)
        {
            var response = await authsomeService.GetAsync<Tasks>("https://api.clickup.com/api/v2/list/" + taskId + "/task?archived=false", (headerBuilder) =>
            {
                headerBuilder.IncludeHeader("Authorization", apiToken);
            });

            foreach (var task in response.Content.tasks)
            {
                Console.WriteLine(task.name);
            }
        }

        public async Task<Dictionary<string, string>> GetLists()
        {
            var dictionary = new Dictionary<string, string>();

            var response = await authsomeService.GetAsync<Folders>("https://api.clickup.com/api/v2/space/" + spaceId + "/folder?archived=false", (headerBuilder) =>
            {
                headerBuilder.IncludeHeader("Authorization", apiToken);
            });

            foreach (var folder in response.Content.folders)
            {
                Console.WriteLine(folder.name);

                foreach (var list in folder.lists)
                {
                    if (list.name.ToLower().Contains("sprint"))
                    {
                        dictionary.Add(list.id, list.name);
                    }
                    Console.WriteLine(list.id + " - " + list.name);
                }
            }

            return dictionary;
        }
    }
}