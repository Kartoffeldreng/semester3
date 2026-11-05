namespace PersonApp.Repository {
    public interface IFileRepository {

        void Save(string fileName, Stream content);
        Stream? Open(string fileName);
        string GetUrl(string fileName);
    }
}
