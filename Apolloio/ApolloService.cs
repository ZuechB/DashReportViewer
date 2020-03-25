using Apolloio.Models;
using Authsome;
using System;
using System.Threading.Tasks;

namespace Apolloio
{
    public interface IApolloService
    {
        Task<AuthenticationResult> Authenticate();
    }

    public class ApolloService : IApolloService
    {
        private const string apiKey = "";

        readonly IAuthsomeService authsomeService;
        public ApolloService(IAuthsomeService authsomeService)
        {
            this.authsomeService = authsomeService;
        }

        public async Task<AuthenticationResult> Authenticate()
        {
            var response = await authsomeService.GetAsync<AuthenticationResult>("https://api.apollo.io/v1/auth/health?api_key=" + apiKey);
            if (response.httpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Content;
            }
            else
            {
                return null;
            }
        }

        public async Task PeopleSearch()
        {
            var response = await authsomeService.PostAsync<PeopleResult>("https://api.apollo.io/v1/mixed_people/search", new
            {
                api_key = apiKey,
                q_organization_domains = "",
                page = 1,
                //person_titles = ["sales manager", "engineer manager"]
            });

            var test = response;
        }




    }
}
