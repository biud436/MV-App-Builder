using System.IO;

namespace Cordova.Common.IO
{
    /// <summary>
    /// 파일 시스템 관련 작업을 처리하는 클래스
    /// </summary>
    public class FileManager
    {
        /// <summary>
        /// 바이트 배열을 지정된 경로에 저장합니다.
        /// </summary>
        /// <param name="folderPath">저장할 폴더 경로</param>
        /// <param name="fileName">파일 이름</param>
        /// <param name="fileData">저장할 파일 데이터</param>
        /// <returns>저장된 파일의 전체 경로</returns>
        public string SaveFile(string folderPath, string fileName, byte[] fileData)
        {
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string targetPath = Path.Combine(folderPath, fileName);

            if (File.Exists(targetPath))
                File.Delete(targetPath);

            File.WriteAllBytes(targetPath, fileData);
            return targetPath;
        }

        /// <summary>
        /// 지정된 경로의 애플리케이션을 실행합니다.
        /// </summary>
        /// <param name="filePath">실행할 파일 경로</param>
        public void RunApplication(string filePath)
        {
            if (File.Exists(filePath))
                System.Diagnostics.Process.Start(filePath);
            else
                throw new FileNotFoundException("실행할 파일이 존재하지 않습니다.", filePath);
        }
    }
}
