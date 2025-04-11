using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Cordova.Common.Http
{
    /// <summary>
    /// HTTP 요청을 처리하기 위한 사용자 정의 클라이언트
    /// </summary>
    public class AppDownloadClient
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;

        /// <summary>
        /// AppDownloadClient 생성자
        /// </summary>
        /// <param name="baseUrl">기본 URL</param>
        public AppDownloadClient(string baseUrl = "https://github.com")
        {
            _baseUrl = baseUrl;
            _client = CreateHttpClient();
        }

        /// <summary>
        /// HttpClient 인스턴스 생성 및 설정
        /// </summary>
        /// <returns>설정된 HttpClient 인스턴스</returns>
        private HttpClient CreateHttpClient()
        {
            var client = new HttpClient();

            // 기본 헤더 설정
            client.DefaultRequestHeaders.UserAgent.TryParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36");
            client.DefaultRequestHeaders.Accept.TryParseAdd("text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3");
            client.DefaultRequestHeaders.AcceptEncoding.TryParseAdd("gzip, deflate, br");
            client.DefaultRequestHeaders.AcceptLanguage.TryParseAdd("ko-KR,ko;q=0.9,en;q=0.8,ja;q=0.7");
            client.DefaultRequestHeaders.Host = new Uri(_baseUrl).Host;
            client.DefaultRequestHeaders.Referrer = new Uri($"{_baseUrl}/biud436/MV-App-Builder/releases");
            client.DefaultRequestHeaders.TransferEncodingChunked = true;
            client.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
            client.Timeout = TimeSpan.FromMinutes(5);

            // SSL/TLS 설정
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                | SecurityProtocolType.Tls11
                | SecurityProtocolType.Tls12
                | SecurityProtocolType.Ssl3;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            return client;
        }

        /// <summary>
        /// 지정된 버전의 앱을 다운로드합니다.
        /// </summary>
        /// <param name="targetVersion">다운로드할 앱 버전</param>
        /// <returns>다운로드된 파일의 바이트 배열</returns>
        public async Task<byte[]> DownloadAppAsync(Version targetVersion)
        {
            var url = $"{_baseUrl}/biud436/MV-App-Builder/releases/download/v{targetVersion}/MVAppBuilder.exe";
            return await _client.GetByteArrayAsync(url);
        }

        /// <summary>
        /// 지정된 URL에서 파일을 다운로드합니다.
        /// </summary>
        /// <param name="url">다운로드할 파일의 URL</param>
        /// <returns>다운로드된 파일의 바이트 배열</returns>
        public async Task<byte[]> DownloadFileAsync(string url)
        {
            return await _client.GetByteArrayAsync(url);
        }
    }
}
