using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using DashReportViewer.GoogleSheets.Models;
using System.Reflection;
using System;

namespace DashReportViewer.GoogleSheets
{
    public interface IGSheetsService
    {
        Task<IList<T>> ReadSheet<T>(object authenticationJson, string applicationName, string spreadsheetId, string range);
    }

    public class GSheetsService : IGSheetsService
    {
        readonly GoogleJson appSettings;
        public GSheetsService(IOptions<GoogleJson> appSettings)
        {
            this.appSettings = appSettings.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spreadsheetId">Example: "1h6MB5i2232_GAaKRiPHuH0Tf_mSNfb5Pe_sLs_9-RA"</param>
        /// <param name="range">Example: "A2:E"</param>
        /// <returns></returns>
        public async Task<IList<T>> ReadSheet<T>(object authenticationJson, string applicationName, string spreadsheetId, string range)
        {
            var credential = Google.Apis.Auth.OAuth2.GoogleCredential.FromJson(JsonConvert.SerializeObject(authenticationJson))
                .CreateScoped(new[] { SheetsService.Scope.SpreadsheetsReadonly });

            // Create Google Sheets API service.
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName
            });

            // Define request parameters.
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(spreadsheetId, range);

            ValueRange response = await request.ExecuteAsync();

            var instance = Activator.CreateInstance(typeof(T));
            var propertyInfos = instance.GetType().GetProperties();

            var list = new List<T>();

            int row = 0;
            foreach (var item in response.Values)
            {
                var newObj = Activator.CreateInstance(typeof(T));
                int column = 0;
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    try
                    {
                        var val = response.Values[row][column];
                        propertyInfo.SetValue(newObj, val);
                    }
                    catch(Exception) { }
                    
                    column++;
                }
                list.Add((T)newObj);

                row++;
            }

            return list;
        }
    }
}
