#region GPLv3 LICENSE

/*
    Haruna is a simple moderation bot for Discord.
    Copyright (c) 2018 Sarmad Wahab (bin).

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.
    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.
    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/

#endregion GPLv3 LICENSE 

/* Author: https://github.com/binsenpai */

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Haruna.Services
{
    public class JoinService : IJoinService
    {
        private byte[] _template;
        private readonly ILogger _logger;
        private readonly HttpClient _client;

        public JoinService(IHttpClientFactory httpClientFactory, ILogger<JoinService> logger)
        {
            _logger = logger;
            _client = httpClientFactory.CreateClient();
        }

        public async Task<Stream> GenerateWelcomeImageAsync(string userName, string tag, Stream avatarStream)
        {
            await EnsureTemplateExistsAsync();

            Bitmap avatar = new Bitmap(new Bitmap(avatarStream), new Size(170, 170));
            Bitmap localTemplate = new Bitmap(new MemoryStream(_template));
            Graphics canvas = Graphics.FromImage(localTemplate);

            string userText = userName + "#" + tag;
            canvas.DrawImage(avatar, 66, 79);
            canvas.DrawString(userText, new Font("Segoe UI Light", 64.0f), Brushes.DarkGray, new PointF(200, 90));

            Stream finalStreamData = SaveBitmap(localTemplate);
            localTemplate.Dispose();
            avatar.Dispose();
            canvas.Dispose();

            return finalStreamData;
        }

        private async Task EnsureTemplateExistsAsync()
        {
            if (_template == null)
            {
                _template = await File.ReadAllBytesAsync("salmon_template.png");
            }
        }

        public async Task<Stream> GetStreamFromAvatarUrlAsync(string avatarUrl)
        {
            HttpResponseMessage imageStreamResponse = await _client.GetAsync(avatarUrl);
            return await imageStreamResponse.Content.ReadAsStreamAsync();
        }

        private Stream SaveBitmap(Bitmap localTemplate)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                localTemplate.Save(stream, ImageFormat.Png);
                return new MemoryStream(stream.ToArray());
            }
        }
    }
}
