using Authsome;
using DashReportViewer.ClickUp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace DashReportViewer.ClickUp
{
    public interface IClickUpService
    {
        Task<List<Tasks_Task>> GetTasks(string listId, bool includeClosed = false);
        Task<Dictionary<string, string>> GetLists(string spaceId);
    }

    public class ClickUpService : IClickUpService
    {
        private const string apiToken = "";

        readonly IAuthsomeService authsomeService;

        public ClickUpService(IAuthsomeService authsomeService)
        {
            this.authsomeService = authsomeService;
        }

        public async Task<List<Tasks_Task>> GetTasks(string listId, bool includeClosed = false)
        {
            var response = await authsomeService.GetAsync<Tasks>("https://api.clickup.com/api/v2/list/" + listId + "/task?archived=false&include_closed=" + includeClosed.ToString(), (headerBuilder) =>
            {
                headerBuilder.IncludeHeader("Authorization", apiToken);
            });

            return response.Content.tasks.ToList();
        }

        public async Task<Dictionary<string, string>> GetLists(string spaceId)
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