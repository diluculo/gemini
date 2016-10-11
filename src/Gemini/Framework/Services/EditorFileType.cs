namespace Gemini.Framework.Services
{
    public class EditorFileType
    {
        public string Label { get; set; }
        public string Name { get; set; }
        public string FileExtension { get; set; }

        public EditorFileType(string name, string fileExtension)
        {
            Label = name;
            Name = name;
            FileExtension = fileExtension;
        }

        public EditorFileType(string label, string name, string fileExtension)
        {
            Label = label;
            Name = name;
            FileExtension = fileExtension;
        }

        public EditorFileType()
        {
            
        }
    }
}