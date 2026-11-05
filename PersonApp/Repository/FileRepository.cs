namespace PersonApp.Repository {
    public class FileRepository : IFileRepository {

        private readonly string _uploadPath;

        public FileRepository(string uploadPath) {
            _uploadPath = uploadPath;
        }

        public void Save(string fileName, Stream content) {
            Directory.CreateDirectory(_uploadPath);

            using (FileStream stream = File.Create(GetPath(fileName)))
                content.CopyTo(stream);
        }

        public Stream? Open(string fileName) {
            string filePath = GetPath(fileName);

            return File.Exists(filePath) ? File.OpenRead(filePath) : null;
        }

        private string GetPath(string fileName) {
            return Path.Combine(_uploadPath, Path.GetFileName(fileName));
        }

        public string GetUrl(string fileName) {
            string folder = Path.GetFileName(_uploadPath);

            return $"/{folder}/{Path.GetFileName(fileName)}";
        }
    }
}