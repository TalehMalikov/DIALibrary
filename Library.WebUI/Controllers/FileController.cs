using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.Entities.Dtos;
using Library.WebUI.Models;
using Library.WebUI.Services.Abstract;
using Library.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using File = Library.Entities.Concrete.File;

namespace Library.WebUI.Controllers
{
    public class FileController : Controller
    {
        private readonly IFileService _fileService;
        private readonly ICategoryService _categoryService;
        private readonly IFileTypeService _fileTypeService;

        public FileController(IFileService fileService, ICategoryService categoryService, IFileTypeService fileTypeService)
        {
            _fileService = fileService;
            _categoryService = categoryService;
            _fileTypeService = fileTypeService;
        }

        public async Task<IActionResult> ShowAllFiles()
        {
            CategoryFileViewModel model = new CategoryFileViewModel
            {
                FileModel = new FileViewModel
                {
                    FileAuthors = await _fileService.GetAllFilesWithAuthors()
                }
            };


            return View(model);
        }

        public async Task<IActionResult> GetAllFilesByCategoryId(int id)
        {
            CategoryFileViewModel model = new CategoryFileViewModel
            {
                FileModel = new FileViewModel
                {
                    Files = await _fileService.GetAllFilesByCategoryId(id)
                },
                CategoryModel = new CategoryViewModel
                {
                    CategoryList = await _categoryService.GetAll(),
                    

                }
            };
            model.CategoryModel.Category =  new SuccessDataResult<Category>(model.CategoryModel.CategoryList.Data.FirstOrDefault(p => p.Id == id));

            return View(model);
        }

        public async Task<IActionResult> ShowFileInfo(int id)
        {
            FileViewModel model = new FileViewModel
            {
                FileAuthor = await _fileService.GetFileWithAuthors(id)
            };

            return View(model);
        }


        public async Task<IActionResult> SearchByName(CategoryFileViewModel data)
        {
            var result = await _fileService.GetAllFiles();
            var filteredData = result.Data.Where(p => p.Name.ToLower().Contains(data.FileModel.Name.ToLower())).ToList();
          
            CategoryFileViewModel model = new CategoryFileViewModel
            {
                FileModel = new FileViewModel
                {
                    Files = new SuccessDataResult<List<File>>(filteredData)
                }
            };
            return View(model);
        }

        public async Task<IActionResult> ShowFilteredFiles(int id)
        {
            CategoryFileViewModel model = new CategoryFileViewModel
            {
                FileModel = new FileViewModel
                {
                    Files = await _fileService.GetFilesByFileTypeId(id)
                }
            };

            var fileTypes = await _fileTypeService.GetAllFileTypes();

            ViewBag.AllFileTypes = fileTypes.Data;

            return View(model);
        }

        public async Task<IActionResult> ShowFileInfoFileType(int id)
        {
            FileViewModel model = new FileViewModel
            {
                FileAuthor = await _fileService.GetFileWithAuthors(id)
            };

            return View(model);
        }

        public async Task<IActionResult> FilterByCataloq()
        {
            var model = new FileViewModel();
            return View(model);
        }

        public async Task<IActionResult> ECatalogIndex()
        {
            ECatalogViewModel viewModel = new ECatalogViewModel();

            var fileTypeList = await _fileTypeService.GetAllFileTypes();
            fileTypeList.Data.Add(new FileType()
            {
                Name = "Seç",
                Id = 0
            });
            viewModel.FileTypeList = new SelectList(fileTypeList.Data, "Id", "Name", "Seç");

            var categoryList = await _categoryService.GetAll();
            categoryList.Data.Add(new Category()
            {
                Name = "Seç",
                Id = 0
            });
            viewModel.CategoryList = new SelectList(categoryList.Data, "Id", "Name", "Seç");

            return View(viewModel);
        }

        public async Task<IActionResult> ECatalogFilter(ECatalogViewModel viewModel)
        {
            bool isFiltered = false;
            var filteredFiles = new List<File>();
            if (viewModel.SelectedFileTypeId != 0 && !isFiltered)
            {
                var result = await _fileService.GetFilesByFileTypeId(viewModel.SelectedFileTypeId);
                filteredFiles = result.Data.Where(f=> f.ExistingStatus == true).ToList();
                isFiltered = true;
            }
            if (viewModel.SelectedCategoryId != 0)
            {
                if (!isFiltered)
                {
                    var result = await _fileService.GetAllFiles();
                    filteredFiles = result.Data.Where(f => f.Category.Id == viewModel.SelectedCategoryId && f.ExistingStatus==true).ToList();
                }
                else if(filteredFiles!=null)
                {
                    filteredFiles = filteredFiles.Where(f => f.Category.Id == viewModel.SelectedCategoryId).ToList();
                }
            }
            if (viewModel.AuthorFilter != null)
            {
                var result = await _fileService.GetAllFilesWithAuthors();
                filteredFiles = new List<File>();
                foreach (var fileAuthor in result.Data)
                {
                    foreach (var author in fileAuthor.Authors)
                    {
                        if(author.FirstName.Contains(viewModel.AuthorFilter) || author.LastName.Contains(viewModel.AuthorFilter))
                        {
                            filteredFiles.Add(fileAuthor.File);
                            break;
                        }
                    }
                }
            }
            if(viewModel.BookFilter != null)
            {
                if(!isFiltered)
                {
                    var result = await _fileService.GetAllFiles();
                    filteredFiles = result.Data.Where(f => f.Name.Contains(viewModel.BookFilter) && f.ExistingStatus==true).ToList();
                }
                else if (filteredFiles != null)
                {
                    filteredFiles = filteredFiles.Where(f => f.Name.Contains(viewModel.BookFilter)).ToList();
                }
            }
            if(viewModel.PublicationYearMin!=0)
            {
                if(!isFiltered)
                {
                    var result = await _fileService.GetAllFiles();
                    filteredFiles = result.Data.Where(f=> f.PublicationDate.Year>=viewModel.PublicationYearMin).ToList();
                }
                else if (filteredFiles != null)
                {
                    filteredFiles = filteredFiles.Where(f => f.PublicationDate.Year >= viewModel.PublicationYearMin).ToList();
                }
            }
            if (viewModel.PublicationYearMax != 0)
            {
                if (!isFiltered)
                {
                    var result = await _fileService.GetAllFiles();
                    filteredFiles = result.Data.Where(f => f.PublicationDate.Year <= viewModel.PublicationYearMax).ToList();
                }
                else if (filteredFiles != null)
                {
                    filteredFiles = filteredFiles.Where(f => f.PublicationDate.Year <= viewModel.PublicationYearMax).ToList();
                }
            }
            viewModel.FilteredFileList = filteredFiles;

            var fileTypeList = await _fileTypeService.GetAllFileTypes();
            viewModel.FileTypeList = new SelectList(fileTypeList.Data, "Id", "Name", "Seç");

            var categoryList = await _categoryService.GetAll();
            viewModel.CategoryList = new SelectList(categoryList.Data, "Id", "Name", "Seç");

            return View(viewModel);
        }


    }
}
