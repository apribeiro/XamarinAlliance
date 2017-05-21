using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using XamarinAllianceApp.Helpers;
using XamarinAllianceApp.Models;

namespace XamarinAllianceApp.Services
{
    public class CharacterService
    {
        static CharacterService DefaultInstance = new CharacterService();
        private MobileServiceClient Client;
        private IMobileServiceTable<Character> CharacterTable;

        private CharacterService()
        {
            Client = new MobileServiceClient(Constants.MobileServiceClientUrl);
            CharacterTable = Client.GetTable<Character>();
        }

        public static CharacterService DefaultManager
        {
            get
            {
                return DefaultInstance;
            }
            private set
            {
                DefaultInstance = value;
            }
        }

        public MobileServiceClient CurrentClient
        {
            get { return Client; }
        }

        /// <summary>
        /// Get the list of characters
        /// </summary>
        /// <returns>ObservableCollection of Character objects</returns>
        public async Task<ObservableCollection<Character>> GetCharactersAsync()
        {
            // Load from JSON file
            //var characters = await ReadCharactersFromFile();
            //return new ObservableCollection<Character>(characters);

            // Load from Azure
            try
            {
                var query = CharacterTable.OrderBy(c => c.Name);
                var characters = await query.ToListAsync();
                return new ObservableCollection<Character>(characters);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get the list of characters from an embedded JSON file, including their child entities.
        /// </summary>
        /// <returns>Array of Character objects</returns>
        private async Task<Character[]> ReadCharactersFromFile()
        {
            var assembly = typeof(CharacterService).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream(Constants.CharactersFilename);
            string text;

            using (var reader = new StreamReader(stream))
            {
                text = await reader.ReadToEndAsync();
            }

            var characters = JsonConvert.DeserializeObject<Character[]>(text);
            return characters;
        }
    }
}
