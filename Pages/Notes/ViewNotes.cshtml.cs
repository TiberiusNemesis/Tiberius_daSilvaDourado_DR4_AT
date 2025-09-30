using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Pages.Notes
{
    public class ViewNotesModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "File name is required")]
        [StringLength(50, ErrorMessage = "File name must be at most 50 characters")]
        public string FileName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }

        public List<string> Files { get; set; }
        public string SelectedFileContent { get; set; }
        public string SelectedFileName { get; set; }
        public string Message { get; set; }

        public void OnGet()
        {
            LoadFiles();
        }

        public IActionResult OnPostCreate()
        {
            if (!ModelState.IsValid)
            {
                LoadFiles();
                return Page();
            }

            try
            {
                var fileName = $"{FileName}.txt";
                var filePath = Path.Combine("wwwroot", "files", fileName);

                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                System.IO.File.WriteAllText(filePath, Content);

                Message = $"File '{fileName}' created successfully!";

                FileName = string.Empty;
                Content = string.Empty;

                LoadFiles();
                return Page();
            }
            catch (Exception ex)
            {
                Message = $"Error creating file: {ex.Message}";
                LoadFiles();
                return Page();
            }
        }

        public IActionResult OnPostView(string selectedFile)
        {
            LoadFiles();

            if (!string.IsNullOrEmpty(selectedFile))
            {
                try
                {
                    var filePath = Path.Combine("wwwroot", "files", selectedFile);
                    if (System.IO.File.Exists(filePath))
                    {
                        SelectedFileContent = System.IO.File.ReadAllText(filePath);
                        SelectedFileName = selectedFile;
                    }
                    else
                    {
                        Message = "File not found.";
                    }
                }
                catch (Exception ex)
                {
                    Message = $"Error reading file: {ex.Message}";
                }
            }

            return Page();
        }

        private void LoadFiles()
        {
            var filesPath = Path.Combine("wwwroot", "files");

            if (Directory.Exists(filesPath))
            {
                Files = Directory.GetFiles(filesPath, "*.txt")
                    .Select(Path.GetFileName)
                    .ToList();
            }
            else
            {
                Files = new List<string>();
            }
        }
    }
}