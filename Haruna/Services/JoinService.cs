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
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SkiaSharp;

namespace Haruna.Services
{
    public class JoinService : IJoinService
    {
        private SKBitmap _template;
        private readonly SKPaint _paint;
        private readonly ILogger _logger;
        private readonly HttpClient _client;
        private readonly SKPaint _discrimPaint;

        public JoinService(IHttpClientFactory httpClientFactory, ILogger<JoinService> logger)
        {
            _logger = logger;
            _paint = CreateNewPaint(34.0f);  //CreateNewPaint(72.0f);
            _discrimPaint = CreateNewPaint(34.0f);
            _client = httpClientFactory.CreateClient();
        }

        public async Task<Stream> GenerateWelcomeImageAsync(string userName, string tag, Stream avatarStream)
        {
            await EnsureTemplateExistsAsync();

            SKBitmap avatar = SKBitmap.Decode(avatarStream);
            SKBitmap localTemplate = _template.Copy();
            SKCanvas canvas = new SKCanvas(localTemplate);

            int currentDelimiter = 0;
            string userText = string.Empty;
            string userTextParsable = userName + "#" + tag;
            for (int textLoc = 0; textLoc < userTextParsable.Length; textLoc++)
            {
                if(currentDelimiter == 13)
                {
                    currentDelimiter = 0;
                    userText += Environment.NewLine;
                }

                userText += userTextParsable[textLoc];
                currentDelimiter++;
            }

            // Hard-coded for the time being
            canvas.DrawBitmap(avatar, 66, 79, _paint);
            canvas.DrawText(userText, 217, 110, _paint);
            //canvas.DrawText(tag, 436, 86, _discrimPaint);
            canvas.Flush();

            SKImage image = SKImage.FromBitmap(localTemplate);
            SKData finalImageData = image.Encode(SKEncodedImageFormat.Png, 90);

            image.Dispose();
            avatarStream.Dispose();
            canvas.Dispose();
            localTemplate.Dispose();
            avatar.Dispose();

            return finalImageData.AsStream();
        }

        private async Task EnsureTemplateExistsAsync()
        {
            if (_template == null)
            {
                byte[] templateData = await File.ReadAllBytesAsync("salmon_template.png");
                _template = SKBitmap.Decode(templateData);
            }
        }

        public async Task<Stream> GetStreamFromAvatarUrlAsync(string avatarUrl)
        {
            HttpResponseMessage imageStreamResponse = await _client.GetAsync(avatarUrl);
            return await imageStreamResponse.Content.ReadAsStreamAsync();
        }

        private SKPaint CreateNewPaint(float fontSize = 32.0f)
        {
            return new SKPaint()
            {
                IsAntialias = true,
                TextSize = fontSize,
                Style = SKPaintStyle.Fill,
                TextAlign = SKTextAlign.Left,
                Color = SKColors.DarkSlateGray,
                Typeface = SKTypeface.FromFamilyName("Segoe UI")
            };
        }
    }
}
